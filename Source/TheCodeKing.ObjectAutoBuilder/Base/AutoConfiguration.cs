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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoObjectBuilder.Extensions;
using AutoObjectBuilder.Interfaces;

namespace AutoObjectBuilder.Base
{
    public class AutoConfiguration : IAutoConfigurationResolver
    {
        private readonly IAutoConfigurationResolver configuration;

        private readonly IDictionary<string, Func<Type, object>> factoryDictionary =
            new Dictionary<string, Func<Type, object>>();

        private readonly IDictionary<string, Func<MemberInfo, object>> resolverDictionary =
            new Dictionary<string, Func<MemberInfo, object>>();

        internal AutoConfiguration()
        {
        }

        internal AutoConfiguration(IAutoConfigurationResolver configuration)
        {
            this.configuration = configuration;
        }

        public virtual void UseDefaultConfiguration()
        {
            factoryDictionary.Clear();
            resolverDictionary.Clear();
        }

        protected virtual void RegisterFactory(Type type, Func<Type, object> factory)
        {
            var key = type.CreateKey();
            factoryDictionary[key] = factory;
        }

        protected virtual void RegisterMemberResolver(Type type, Func<MemberInfo, object> resolver, string memberName = null)
        {
            var key = type.CreateKey(memberName);
            resolverDictionary[key] = resolver;
        }

        Func<Type, object> IAutoConfigurationResolver.GetFactory(Type type, bool cascade = true)
        {
            var key = type.CreateKey();
            Func<Type, object> value;
            if (factoryDictionary.TryGetValue(key, out value))
            {
                return value;
            }
            if (cascade && configuration!=null)
            {
                return configuration.GetFactory(type);
            }
            return null;
        }

        Func<MemberInfo, object> IAutoConfigurationResolver.ResolveMemberByName(MemberInfo prop, Type type, bool cascade = true)
        {
            var key = type.CreateKey(prop.Name);
            Func<MemberInfo, object> value;
            if (resolverDictionary.TryGetValue(key, out value))
            {
                return value;
            }
            if (cascade && configuration != null)
            {
                return configuration.ResolveMemberByName(prop, type);
            }
            return null;
        }

        public Func<MemberInfo, object> ResolveMemberByType(MemberInfo prop, Type type, bool cascade = true)
        {
            var key = prop.PropertyOrFieldType().CreateKey();
            Func<MemberInfo, object> value;
            if (resolverDictionary.TryGetValue(key, out value))
            {
                return value;
            }
            Func<Type, object> factory;
            if (factoryDictionary.TryGetValue(key, out factory))
            {
                return m => factory(prop.PropertyOrFieldType());
            }
            if (cascade && configuration != null)
            {
                return configuration.ResolveMemberByType(prop, type);
            }
            return null;
        }

        public IAutoConfiguration Factory<TTarget>(TTarget value)
        {
            RegisterFactory(typeof(TTarget), t => value);
            return this;
        }

        public IAutoConfiguration Factory<TTarget>(Func<TTarget> factory)
        {
            RegisterFactory(typeof(TTarget), t => factory());
            return this;
        }

        public IAutoConfiguration Factory<TTarget>(Func<Type, TTarget> factory)
        {
            RegisterFactory(typeof(TTarget), t => factory(t));
            return this;
        }

        public IAutoConfiguration Set<TTarget>(System.Linq.Expressions.Expression<Func<TTarget, object>> expression, object value)
        {
            string name = expression.ResolveMemberName();
            if (name == null)
            {
                throw new ArgumentException("Expression invalid. Use property or field access.", "expression");
            }
            Type checkType = expression.ResolveMemberType();
            if (value != null && !checkType.IsAssignableFrom(value.GetType()))
            {
                throw new ArgumentException("Expression invalid. Value must be of type {0}.", value.GetType().Name);
            }
            RegisterMemberResolver(typeof(TTarget), m => value, name);
            return this;
        }

        public IAutoConfiguration Setter<TTarget>(Func<MemberInfo, TTarget> setter)
        {
            RegisterMemberResolver(typeof(TTarget), m => setter(m));
            return this;
        }

        public IAutoConfiguration Max()
        {
            Factory(int.MaxValue)
                .Factory(uint.MaxValue)
                .Factory(long.MaxValue)
                .Factory(ulong.MaxValue)
                .Factory(short.MaxValue)
                .Factory(ushort.MaxValue)
                .Factory(double.MaxValue)
                .Factory(byte.MaxValue)
                .Factory(char.MaxValue)
                .Factory(Char.MaxValue)
                .Factory(Byte.MaxValue)
                .Factory(SByte.MaxValue)
                .Factory(Int16.MaxValue)
                .Factory(Int32.MaxValue)
                .Factory(Int64.MaxValue)
                .Factory(UInt16.MaxValue)
                .Factory(UInt32.MaxValue)
                .Factory(UInt64.MaxValue)
                .Factory(DateTime.MaxValue)
                .Factory(t => Enum.GetValues(t).Cast<Enum>().Last())
                ;
            return this;
        }

        public IAutoConfiguration Min()
        {
            Factory(int.MinValue)
                .Factory(uint.MaxValue)
                .Factory(long.MinValue)
                .Factory(ulong.MinValue)
                .Factory(short.MinValue)
                .Factory(ushort.MinValue)
                .Factory(double.MinValue)
                .Factory(byte.MinValue)
                .Factory(sbyte.MinValue)
                .Factory(char.MinValue)
                .Factory(Char.MinValue)
                .Factory(Byte.MinValue)
                .Factory(SByte.MinValue)
                .Factory(Int16.MinValue)
                .Factory(Int32.MinValue)
                .Factory(Int64.MinValue)
                .Factory(UInt16.MinValue)
                .Factory(UInt32.MinValue)
                .Factory(UInt64.MinValue)
                .Factory(DateTime.MinValue)
                .Factory(t => Enum.GetValues(t).Cast<Enum>().First())
                ;
            return this;
        }

        public IAutoConfiguration Default()
        {
            Factory(default(int))
                .Factory(default(uint))
                .Factory(default(long))
                .Factory(default(ulong))
                .Factory(default(short))
                .Factory(default(ushort))
                .Factory(default(double))
                .Factory(default(byte))
                .Factory(default(sbyte))
                .Factory(default(char))
                .Factory(default(Char))
                .Factory(default(Byte))
                .Factory(default(SByte))
                .Factory(default(Int16))
                .Factory(default(Int32))
                .Factory(default(Int64))
                .Factory(default(UInt16))
                .Factory(default(UInt32))
                .Factory(default(UInt64))
                .Factory(default(DateTime))
                .Factory(t => Enum.GetValues(t).Cast<Enum>().First())
                ;
            return this;
        }

        public IAutoConfiguration Empty()
        {
            Factory(string.Empty)
                .Factory<Uri>((Uri)null)
                .Setter<Uri>(m => null);
            return this;
        }
    }
}
