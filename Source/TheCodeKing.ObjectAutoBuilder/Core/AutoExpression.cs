/*=============================================================================
*
*	(C) Copyright 2011, Michael Carlisle (mike.carlisle@thecodeking.co.uk)
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
using AutoObjectBuilder.Base;
using AutoObjectBuilder.Interfaces;

namespace AutoObjectBuilder.Core
{
    public class AutoExpression<T> : AutoConfiguration<T, AutoExpression<T>>, IAutoExpression<T>, IAutoConfiguration
    {
        private readonly IAutoBuilder builder;
        private bool intiailized;
        private T obj;

        protected AutoExpression()
        {
        }

        internal AutoExpression(
            Func
                <IAutoConfigurationResolver, Func<IAutoConfigurationResolver, IAutoBuilder, IObjectParser, IAutoFiller>,
                IAutoBuilder> builderFactory,
            Func<IAutoConfigurationResolver, IAutoBuilder, IObjectParser, IAutoFiller> filler,
            IAutoConfigurationResolver configuration)
            : base(configuration)
        {
            builder = builderFactory(this, filler);
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

        #region IAutoConfiguration Members

        TTarget IAutoConfiguration.Make<TTarget>()
        {
            if (typeof (TTarget) == typeof (T))
            {
                throw new InvalidOperationException("Preventing operation that would cause infinite recursion.");
            }
            return (TTarget) builder.CreateObject(typeof (TTarget));
        }

        #endregion

        public static implicit operator T(AutoExpression<T> exp)
        {
            return exp.Object;
        }
    }
}