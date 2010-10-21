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

        private int? enumerableSize;

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

        int IAutoConfigurationResolver.EnumerableSize
        {
            get
            {
                int? value = enumerableSize;
                if (value == null && configuration != null)
                {
                    value = configuration.EnumerableSize;
                }
                return (value ?? 0);
            }
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

        public IAutoConfiguration With<TTarget>(TTarget value)
        {
            RegisterFactory(typeof(TTarget), t => value);
            return this;
        }

        public IAutoConfiguration With<TTarget>(Func<TTarget> factory)
        {
            RegisterFactory(typeof(TTarget), t => factory());
            return this;
        }

        public IAutoConfiguration With<TTarget>(Func<Type, TTarget> factory)
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
            With(int.MaxValue)
                .With(true)
                .With(uint.MaxValue)
                .With(long.MaxValue)
                .With(ulong.MaxValue)
                .With(short.MaxValue)
                .With(ushort.MaxValue)
                .With(double.MaxValue)
                .With(sbyte.MaxValue)
                .With(byte.MaxValue)
                .With(char.MaxValue)
                .With(DateTime.MaxValue)
                .With(t => Enum.GetValues(t).Cast<Enum>().Last())
                ;
            return this;
        }

        public IAutoConfiguration Min()
        {
            With(int.MinValue)
                .With(false)
                .With(uint.MinValue)
                .With(long.MinValue)
                .With(ulong.MinValue)
                .With(short.MinValue)
                .With(ushort.MinValue)
                .With(double.MinValue)
                .With(byte.MinValue)
                .With(sbyte.MinValue)
                .With(char.MinValue)
                .With(DateTime.MinValue)
                .With(t => Enum.GetValues(t).Cast<Enum>().First())
                ;
            return this;
        }

        public IAutoConfiguration Default()
        {
            With(default(int))
                .With(default(bool))
                .With(default(uint))
                .With(default(long))
                .With(default(ulong))
                .With(default(short))
                .With(default(ushort))
                .With(default(double))
                .With(default(byte))
                .With(default(sbyte))
                .With(default(char))
                .With(default(DateTime))
                .With(t => Enum.GetValues(t).Cast<Enum>().First())
                ;
            return this;
        }

        public IAutoConfiguration Empty()
        {
            With(string.Empty)
                .With<Uri>((Uri)null)
                .Setter<Uri>(m => null);
            return this;
        }

        public IAutoConfiguration EnumerableSize(int count)
        {
            enumerableSize = count;
            return this;
        }
    }
}
