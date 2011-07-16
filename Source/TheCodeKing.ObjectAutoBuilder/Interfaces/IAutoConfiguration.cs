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
using System.Linq.Expressions;
using System.Reflection;

namespace AutoObjectBuilder.Interfaces
{
    public interface IAutoConfiguration
    {
        IAutoConfiguration With<TTarget>(TTarget value);
        IAutoConfiguration With<TTarget>(Func<TTarget> factory);
        IAutoConfiguration With<TTarget>(Func<Type, TTarget> factory);

        [Obsolete("Use Do<T>(Action<T> action) syntax to set property value.")]
        IAutoConfiguration Set<TTarget>(Expression<Func<TTarget, object>> expression, object value);

        IAutoConfiguration Do<TTarget>(Action<TTarget> expression);
        IAutoConfiguration Setter<TTarget>(Func<MemberInfo, TTarget> setter);

        T Make<T>();
    }

    public interface IAutoConfiguration<TReturn, out T>
        where T : class, IAutoConfiguration
    {
        T With<TTarget>(TTarget value);
        T With<TTarget>(Func<TTarget> factory);
        T With<TTarget>(Func<Type, TTarget> factory);

        [Obsolete("Use Do<T>(Action<T> action) syntax to set property value.")]
        T Set<TTarget>(Expression<Func<TTarget, object>> expression, object value);

        T Do<TTarget>(Action<TTarget> expression);
        T Do(Action<TReturn> expression);
        T Setter<TTarget>(Func<MemberInfo, TTarget> setter);
        T Setter(Func<MemberInfo, TReturn> setter);
    }
}