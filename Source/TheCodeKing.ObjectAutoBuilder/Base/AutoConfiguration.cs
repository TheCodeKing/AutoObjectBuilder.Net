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
using System.Linq.Expressions;
using System.Reflection;
using AutoObjectBuilder.Extensions;
using AutoObjectBuilder.Interfaces;

namespace AutoObjectBuilder.Base
{
    public class AutoConfiguration<T> : IAutoConfiguration<T>, IAutoConfiguration, IAutoConfigurationResolver where T : class, IAutoConfiguration
    {
        private readonly IAutoConfigurationResolver configuration;

        private readonly IDictionary<string, Func<Type, object>> factoryDictionary =
            new Dictionary<string, Func<Type, object>>();

        private readonly IDictionary<string, Func<MemberInfo, object>> resolverDictionary =
            new Dictionary<string, Func<MemberInfo, object>>();

        private readonly IDictionary<string, string> setting =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        private readonly IDictionary<string, List<MulticastDelegate>> postProcessors =
            new Dictionary<string, List<MulticastDelegate>>(StringComparer.InvariantCultureIgnoreCase);

        internal AutoConfiguration()
        {
        }

        internal AutoConfiguration(IAutoConfigurationResolver configuration)
        {
            this.configuration = configuration;
        }

        public void Clear()
        {
            factoryDictionary.Clear();
            resolverDictionary.Clear();
            postProcessors.Clear();
            setting.Clear();
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

        protected virtual void RegisterPostProcessor(Type type, MulticastDelegate action)
        {
            var key = type.CreateKey();
            List<MulticastDelegate> value = null;
            if (!postProcessors.TryGetValue(key, out value))
            {
                value = new List<MulticastDelegate>();
                postProcessors.Add(key, value);   
            }
            value.Add(action);
        }

        string IAutoConfigurationResolver.this[string key]
        {
            get
            {
                string value;
                if (setting.TryGetValue(key, out value))
                {
                    return value;
                }
                return configuration[key];
            }
            set { setting[key] = value; }
        }
        
        Func<Type, object> IAutoConfigurationResolver.GetFactory(Type type, bool cascade)
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

        Func<MemberInfo, object> IAutoConfigurationResolver.ResolveMemberByName(MemberInfo prop, Type type, bool cascade)
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

        Func<MemberInfo, object> IAutoConfigurationResolver.ResolveMemberByType(MemberInfo prop, Type type, bool cascade)
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

        Action<object> IAutoConfigurationResolver.ResolvePostProcessors(Type type)
        {
            Action<object> global = null;
            if (configuration != null)
            {
                global = configuration.ResolvePostProcessors(type);
            }
            string key = type.CreateKey();
            List<MulticastDelegate> actions;
            if (postProcessors.TryGetValue(key, out actions))
            {
                return o =>
                           {
                               if (global!=null)
                               {
                                   global(o);
                               }
                               actions.ForEach(i => i.Method.Invoke(i.Target, new[] {o}));
                           };
            }
            else if (global !=null)
            {
                return o => global(o);
            }
            return null;
        }

        public T With<TTarget>(TTarget value)
        {
            RegisterFactory(typeof(TTarget), t => value);
            return this as T;
        }

        public T With<TTarget>(Func<TTarget> factory)
        {
            RegisterFactory(typeof(TTarget), t => factory());
            return this as T;
        }

        public T With<TTarget>(Func<Type, TTarget> factory)
        {
            RegisterFactory(typeof(TTarget), t => factory(t));
            return this as T;
        }

        public T Do<TTarget>(Action<TTarget> expression)
        {
            RegisterPostProcessor(typeof (TTarget), expression);
            return this as T;
        }

        public T Set<TTarget>(Expression<Func<TTarget, object>> expression, object value)
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
            return this as T;
        }

        public T Setter<TTarget>(Func<MemberInfo, TTarget> setter)
        {
            RegisterMemberResolver(typeof(TTarget), m => setter(m));
            return this as T;
        }

        IAutoConfiguration IAutoConfiguration.With<TTarget>(TTarget value)
        {
            return With(value);
        }

        IAutoConfiguration IAutoConfiguration.With<TTarget>(Func<TTarget> factory)
        {
            return With(factory);
        }

        IAutoConfiguration IAutoConfiguration.With<TTarget>(Func<Type, TTarget> factory)
        {
            return With(factory);
        }

        IAutoConfiguration IAutoConfiguration.Set<TTarget>(Expression<Func<TTarget, object>> expression, object value)
        {
            return Set(expression, value);
        }

        IAutoConfiguration IAutoConfiguration.Do<TTarget>(Action<TTarget> expression)
        {
            return Do(expression);
        }

        IAutoConfiguration IAutoConfiguration.Setter<TTarget>(Func<MemberInfo, TTarget> setter)
        {
            return Setter(setter);
        }
    }
}
