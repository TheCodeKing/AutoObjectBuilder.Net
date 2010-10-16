using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using AutoObjectBuilder.Interfaces;

namespace AutoObjectBuilder.Core
{
    public class AutoInterfaceBuilder : IAutoBuilder
    {
        private readonly ConstructorInfo notImplCtor = typeof(NotImplementedException).GetConstructor(
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
            if (!type.IsInterface)
            {
                throw new NotSupportedException("Only interfaces are supported.");
            }
            type = CreateProxyType(type);
            if (type == null)
            {
                return null;
            }
            return Activator.CreateInstance(type);
        }

        private Type CreateProxyType(Type type)
        {
            var assemblyName = new AssemblyName("AutoObjectBuilder.Proxy");
            var appDomain = Thread.GetDomain();
            var assemblyBuilder = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
            var typeBuilder = moduleBuilder.DefineType("Impl" + type.Name, TypeAttributes.Public | TypeAttributes.Class);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            typeBuilder.AddInterfaceImplementation(type);

            foreach (var method in type.GetMethods())
            {
                if (!method.IsSpecialName)
                {
                    BuildMethodStub(method, typeBuilder);
                }
            }

            foreach (var property in type.GetProperties())
            {
                BuildPropertyStub(property, typeBuilder);
            }
            var dType = typeBuilder.CreateType();
            return dType;
        }

        private void BuildMethodStub(MethodInfo methodInfo, TypeBuilder type)
        {
            MethodBuilder method = GetMethodDefinition(type, methodInfo);

            var ilg = method.GetILGenerator();
            if (methodInfo.ReturnType.Equals(typeof(void)))
            {
                ilg.Emit(OpCodes.Nop);
                ilg.Emit(OpCodes.Newobj, notImplCtor);
                ilg.Emit(OpCodes.Throw);
            }
            else
            {
                // Preparing locals
                ilg.DeclareLocal(methodInfo.ReturnType);
                // Preparing labels
                ilg.DefineLabel();
                // Writing body
                ilg.Emit(OpCodes.Nop);
                ilg.Emit(OpCodes.Newobj, notImplCtor);
                ilg.Emit(OpCodes.Throw);
            }
        }

        private MethodBuilder GetMethodDefinition(TypeBuilder type, MethodInfo methodInfo)
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

        private void BuildPropertyStub(PropertyInfo property, TypeBuilder typeBuilder)
        {
            const MethodAttributes ma = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.Virtual;
            
            FieldBuilder fieldBuilder = typeBuilder.DefineField(string.Format("<{0}>k__BackingField", property.Name), property.PropertyType, FieldAttributes.Private);
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.PropertyType, property.GetRequiredCustomModifiers());

            if (property.CanWrite)
            {
                MethodInfo interfaceMethod = property.ReflectedType.GetMethod(string.Format("set_{0}", property.Name));
                MethodBuilder methbSet = GetMethodDefinition(typeBuilder, interfaceMethod);

                ILGenerator ilg = methbSet.GetILGenerator();

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

                propertyBuilder.SetSetMethod(methbSet);
                typeBuilder.DefineMethodOverride(methbSet, interfaceMethod);
            }

            if (property.CanRead)
            {
                MethodInfo interfaceMethod = property.ReflectedType.GetMethod(string.Format("get_{0}", property.Name));
                MethodBuilder methbGet = GetMethodDefinition(typeBuilder, interfaceMethod);

                ILGenerator ilg = methbGet.GetILGenerator();
                ilg.Emit(OpCodes.Ldarg_0);
                ilg.Emit(OpCodes.Ldfld, fieldBuilder);
                ilg.Emit(OpCodes.Ret);

                propertyBuilder.SetGetMethod(methbGet);
                typeBuilder.DefineMethodOverride(methbGet, interfaceMethod);
            }
        }
    }
}
