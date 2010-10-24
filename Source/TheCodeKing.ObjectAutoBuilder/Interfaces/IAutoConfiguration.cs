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

namespace AutoObjectBuilder.Interfaces
{
    public interface IAutoConfiguration
    {
        IAutoConfiguration With<TTarget>(TTarget value);
        IAutoConfiguration With<TTarget>(Func<TTarget> factory);
        IAutoConfiguration With<TTarget>(Func<Type, TTarget> factory);
        IAutoConfiguration Set<TTarget>(Expression<Func<TTarget, object>> expression, object value);
        IAutoConfiguration Setter<TTarget>(Func<MemberInfo, TTarget> setter);
    }

    public interface IAutoConfiguration<out T>
         where T : class, IAutoConfiguration
    {
        T With<TTarget>(TTarget value);
        T With<TTarget>(Func<TTarget> factory);
        T With<TTarget>(Func<Type, TTarget> factory);
        T Set<TTarget>(Expression<Func<TTarget, object>> expression, object value);
        T Setter<TTarget>(Func<MemberInfo, TTarget> setter);
    }
}