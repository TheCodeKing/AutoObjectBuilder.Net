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
using System.Linq.Expressions;
using System.Reflection;
using AutoObjectBuilder.Interfaces;

namespace AutoObjectBuilder.Core
{
    public class AutoExpression<T> : IAutoExpression<T>
    {
        private readonly IAutoBuilder builder;
        private readonly IAutoConfigurationResolver configuration;
        private bool intiailized;
        private T obj;

        internal AutoExpression(Func<IAutoConfigurationResolver, Func<IAutoConfigurationResolver, IAutoBuilder, IObjectParser, IAutoFiller>, IAutoBuilder> builderFactory,
            Func<IAutoConfigurationResolver, IAutoBuilder, IObjectParser, IAutoFiller> filler,
            IAutoConfigurationResolver configuration)
        {
            builder = builderFactory(configuration, filler);
            this.configuration = configuration;
        }

        public T Object
        {
            get
            {
                if (!intiailized)
                {
                    intiailized = true;
                    obj = builder.CreateObject<T>();
                }
                return obj;
            }
        }

        #region IAutoExpression<T> Members

        public AutoExpression<T> With<TTarget>(TTarget value)
        {
            configuration.With(value);
            return this;
        }

        public AutoExpression<T> With<TTarget>(Func<TTarget> factory)
        {
            configuration.With(factory);
            return this;
        }

        public AutoExpression<T> With<TTarget>(Func<Type, TTarget> factory)
        {
            configuration.With(factory);
            return this;
        }

        public AutoExpression<T> Set<TTarget>(Expression<Func<TTarget, object>> expression, object value)
        {
            configuration.Set(expression, value);
            return this;
        }

        public AutoExpression<T> Setter<TTarget>(Func<MemberInfo, TTarget> setter)
        {
            configuration.Setter(setter);
            return this;
        }

        public AutoExpression<T> Max()
        {
            configuration.Max();
            return this;
        }

        public AutoExpression<T> Min()
        {
            configuration.Min();
            return this;
            }

        public AutoExpression<T> Default()
        {
            configuration.Default();
            return this;
        }

        public AutoExpression<T> Empty()
        {
            configuration.Empty();
            return this;
        }

        public AutoExpression<T> EnumerableSize(int count)
        {
            configuration.EnumerableSize(count);
            return this;
        }

        #endregion

        public static implicit operator T(AutoExpression<T> exp)
        {
            return exp.Object;
        }
    }
}
