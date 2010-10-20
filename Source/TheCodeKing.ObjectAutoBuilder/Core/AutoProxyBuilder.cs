/*=============================================================================
*
*	(C) Copyright 2010, Michael Carlisle (mike.carlisle@thecodeking.co.uk)
*
*   http://www.TheCodeKing.co.uk
*  
*	All rights reserved.
*	The code and information is provided "as-is" without waranty of any kind,
*	either expressed or implied.
*
*=============================================================================
*/
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using AutoObjectBuilder.Interfaces;

namespace AutoObjectBuilder.Core
{
    internal class AutoProxyBuilder : IAutoBuilder
    {
        private static readonly MethodInfo MakeMethod = typeof(Auto).GetMethod(
                   "Make",
                   BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                   null,
                   new Type[]{
                        },
                   null
                   );

        private static readonly ConstructorInfo NotImplCtor = typeof(NotImplementedException).GetConstructor(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            null,
            new Type[]{},
            null
            );

        public T CreateObject<T>()
        {
            return (T)CreateObject(typeof (T));
        }

        public object CreateObject(Type type)
        {
            if (!type.IsInterface && !type.IsAbstract)
            {
                throw new NotSupportedException("Only interfaces and abstract classes are supported.");
            }
            type = CreateProxyType(type);
            if (type == null)
            {
                return null;
            }
            return Activator.CreateInstance(type);
        }

        private static Type CreateProxyType(Type type)
        {
            var assemblyName = new AssemblyName("AutoObjectBuilder.Proxy");
            var appDomain = Thread.GetDomain();
            var assemblyBuilder = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
            var typeBuilder = moduleBuilder.DefineType("Impl" + type.Name, TypeAttributes.Public | TypeAttributes.Class);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            if (type.IsInterface)
            {
                typeBuilder.AddInterfaceImplementation(type);
            } 
            else if (type.IsAbstract)
            {
                typeBuilder.SetParent(type);
            }

            foreach (var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (!method.IsSpecialName && method.IsVirtual)
                {
                    BuildMethodStub(method, typeBuilder);
                }
            }

            foreach (var property in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                BuildPropertyStub(property, typeBuilder);
            }
            try
            {
                return typeBuilder.CreateType();
            }
            catch (TypeLoadException)
            {
                return null;
            }
        }

        private static void BuildMethodStub(MethodInfo methodInfo, TypeBuilder type)
        {
            MethodBuilder method = GetMethodDefinition(type, methodInfo);

            var ilg = method.GetILGenerator();
            if (methodInfo.ReturnType.Equals(typeof(void)))
            {
                ilg.Emit(OpCodes.Nop);
                ilg.Emit(OpCodes.Newobj, NotImplCtor);
                ilg.Emit(OpCodes.Throw);
            }
            else
            {
                // return an Auto.Make instance of the return value
                CreateAutoMakeImplementation(method);
            }

            if (methodInfo.IsAbstract)
            {
                type.DefineMethodOverride(method, methodInfo);
            }
        }

        private static MethodBuilder GetMethodDefinition(TypeBuilder type, MethodInfo methodInfo)
        {
            const MethodAttributes methodAttributes =
                MethodAttributes.Public
                | MethodAttributes.Virtual
                | MethodAttributes.Final
                | MethodAttributes.HideBySig
                | MethodAttributes.NewSlot;

            MethodBuilder method = type.DefineMethod(methodInfo.Name, methodAttributes);
            if (methodInfo.IsGenericMethod)
            {
                var generics = methodInfo.GetGenericArguments();

                string[] names = generics.Select(o => o.Name).ToArray();
                var gparams = method.DefineGenericParameters(names);
        
                foreach (var p in gparams)
                {
                    GenericTypeParameterBuilder p1 = p;
                    var attr = generics.Where(o => o.Name == p1.Name).SingleOrDefault();
                    p.SetGenericParameterAttributes(attr.GenericParameterAttributes);
                    p.SetInterfaceConstraints(attr.GetGenericParameterConstraints());
                }
            }

            var paramArr = methodInfo.GetParameters();

            method.SetParameters(paramArr.OrderBy(o => o.Position).Select(o => o.ParameterType).ToArray());

            method.SetReturnType(methodInfo.ReturnType);
            return method;
        }

        private static void BuildPropertyStub(PropertyInfo property, TypeBuilder typeBuilder)
        {
            FieldBuilder fieldBuilder = typeBuilder.DefineField(string.Format("<{0}>k__BackingField", property.Name), property.PropertyType, FieldAttributes.Private);
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.PropertyType, property.GetRequiredCustomModifiers());

            bool canCreateWrite = property.CanWrite &&
                                         (property.ReflectedType.IsInterface || property.GetSetMethod(true).IsVirtual);

            // if the property has an overridable setter, then wire up to a backing field
            if (canCreateWrite)
            {
                MethodInfo interfaceMethod = property.GetSetMethod(true);
                MethodBuilder methodSet = GetMethodDefinition(typeBuilder, interfaceMethod);

                ILGenerator ilg = methodSet.GetILGenerator();

                if (interfaceMethod.GetParameters().Length > 1)
                {
                    ilg.Emit(OpCodes.Nop);
                    ilg.Emit(OpCodes.Ldarg_0);
                    ilg.Emit(OpCodes.Ldarg_2);
                }
                else
                {
                    ilg.Emit(OpCodes.Ldarg_0);
                    ilg.Emit(OpCodes.Ldarg_1);
                }
                ilg.Emit(OpCodes.Stfld, fieldBuilder);
                ilg.Emit(OpCodes.Ret);

                propertyBuilder.SetSetMethod(methodSet);
                typeBuilder.DefineMethodOverride(methodSet, interfaceMethod);
            }

            bool canCreateRead = property.CanRead &&
                                    (property.ReflectedType.IsInterface || property.GetGetMethod(true).IsVirtual);
            
            // if the property has an overridable setter, and getter then wire up to backing field
            if (canCreateWrite && canCreateRead)
            {
                MethodInfo interfaceMethod = property.GetGetMethod(true);
                MethodBuilder methodGet = GetMethodDefinition(typeBuilder, interfaceMethod);

                ILGenerator ilg = methodGet.GetILGenerator();
                ilg.Emit(OpCodes.Ldarg_0);
                ilg.Emit(OpCodes.Ldfld, fieldBuilder);
                ilg.Emit(OpCodes.Ret);

                propertyBuilder.SetGetMethod(methodGet);
                typeBuilder.DefineMethodOverride(methodGet, interfaceMethod);
            }
            // if only the property getter can be overriden then hook this up to an Auto.Make instance
            else if (canCreateRead)
            {
                MethodInfo interfaceMethod = property.GetGetMethod();
                MethodBuilder methodGet = GetMethodDefinition(typeBuilder, interfaceMethod);

                CreateAutoMakeImplementation(methodGet);
                typeBuilder.DefineMethodOverride(methodGet, interfaceMethod);
            }
        }

        private static void CreateAutoMakeImplementation(MethodBuilder methodBuilder)
        {
            MethodInfo method1 = MakeMethod.MakeGenericMethod(methodBuilder.ReturnType);

            MethodInfo method2 = typeof(AutoExpression<>).MakeGenericType(methodBuilder.ReturnType).GetMethod(
                "get_Object",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[] { },
                null
                );
            // Adding parameters
            ILGenerator gen = methodBuilder.GetILGenerator();
            // Writing body
            gen.Emit(OpCodes.Call, method1);
            gen.Emit(OpCodes.Callvirt, method2);
            gen.Emit(OpCodes.Ret);
        }
    }
}
