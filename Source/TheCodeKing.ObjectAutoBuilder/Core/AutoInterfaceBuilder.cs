using System;
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
            var assemblyBuilder = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
            var typeBuilder = moduleBuilder.DefineType("Impl" + type.Name, TypeAttributes.Public | TypeAttributes.Class);
            
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

            return typeBuilder.CreateType();
        }

        private void BuildMethodStub(MethodInfo methodInfo, TypeBuilder type)
        {
            const MethodAttributes methodAttributes =
                MethodAttributes.Public
                | MethodAttributes.Virtual
                | MethodAttributes.Final
                | MethodAttributes.HideBySig
                | MethodAttributes.NewSlot;

            MethodBuilder method = type.DefineMethod(methodInfo.Name, methodAttributes);
            method.SetReturnType(methodInfo.ReturnType);
            var paramArr = methodInfo.GetParameters();
            method.SetParameters(methodInfo.GetParameters().OrderBy(o => o.Position).Select(o => o.ParameterType).ToArray());
            foreach (var p in paramArr)
            {
                method.DefineParameter(p.Position, p.Attributes, p.Name);
            }
            var generics = method.GetGenericArguments();
            if (generics.Length>0)
            {
                string[] names = generics.Select(o => o.Name).ToArray();
                var gparams = method.DefineGenericParameters(names);
                foreach (var p in gparams)
                {
                    var a = new GenericParameterAttributes();
                    var attr = generics.Where(o => o.Name == p.Name).Select(
                            o => o.GetGenericTypeDefinition().GenericParameterAttributes);
                    foreach (var i in attr)
                    {
                        a |= i;
                    }
                    p.SetGenericParameterAttributes(a);
                }
            }
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

        private void BuildPropertyStub(PropertyInfo property, TypeBuilder tBuilder)
        {
            const MethodAttributes ma = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.Virtual;
            
            FieldBuilder fieldBuilder = tBuilder.DefineField(string.Format("<{0}>k__BackingField", property.Name), property.PropertyType, FieldAttributes.Private);
            PropertyBuilder propertyBuilder = tBuilder.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.PropertyType, null);

            if (property.CanWrite)
            {

                MethodBuilder methbSet = tBuilder.DefineMethod(string.Format("set_{0}", property.Name), ma, null,
                                                               new[] {property.PropertyType});
                ILGenerator ilg = methbSet.GetILGenerator();
                ilg.Emit(OpCodes.Ldarg_0);
                ilg.Emit(OpCodes.Ldarg_1);
                ilg.Emit(OpCodes.Stfld, fieldBuilder);
                ilg.Emit(OpCodes.Ret);
                propertyBuilder.SetSetMethod(methbSet);

                MethodInfo interfaceMethod = property.ReflectedType.GetMethod("set_" + property.Name);
                tBuilder.DefineMethodOverride(methbSet, interfaceMethod);
            }

            if (property.CanRead)
            {
                MethodBuilder methbGet = tBuilder.DefineMethod(string.Format("get_{0}", property.Name), ma,
                                                               property.PropertyType, Type.EmptyTypes);
                ILGenerator ilg = methbGet.GetILGenerator();
                ilg.Emit(OpCodes.Ldarg_0);
                ilg.Emit(OpCodes.Ldfld, fieldBuilder);
                ilg.Emit(OpCodes.Ret);
                propertyBuilder.SetGetMethod(methbGet);

                MethodInfo interfaceMethod = property.ReflectedType.GetMethod("get_" + property.Name);
                tBuilder.DefineMethodOverride(methbGet, interfaceMethod);
            }
        }
    }
}
