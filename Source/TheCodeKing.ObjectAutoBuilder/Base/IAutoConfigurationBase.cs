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

namespace AutoObjectBuilder.Base
{
    public interface IAutoConfigurationBase<T>
    {
        T Factory<TTarget>(TTarget value);
        T Factory<TTarget>(Func<TTarget> factory);
        T Factory<TTarget>(Func<Type, TTarget> factory);
        T Set<TTarget>(Expression<Func<TTarget, object>> expression, object value);
        T Setter<TTarget>(Func<MemberInfo, TTarget> setter);
        T Max();
        T Min();
        T Default();
        T Empty();
    }
}