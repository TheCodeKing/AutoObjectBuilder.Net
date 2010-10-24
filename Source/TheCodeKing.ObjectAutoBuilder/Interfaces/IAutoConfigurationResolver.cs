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
using System.Reflection;

namespace AutoObjectBuilder.Interfaces
{
    internal interface IAutoConfigurationResolver
    {
        void Clear();
        string this[string key] { get; set; }
        Func<Type, object> GetFactory(Type type, bool cascade = true);
        Func<MemberInfo, object> ResolveMemberByType(MemberInfo prop, Type type, bool cascade = true);
        Func<MemberInfo, object> ResolveMemberByName(MemberInfo prop, Type type, bool cascade = true);
    }
}