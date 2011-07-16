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
using System.Linq;
using System.Reflection;
using AutoObjectBuilder.Interfaces;

namespace AutoObjectBuilder.Core
{
    internal class ObjectParser : IObjectParser
    {
        private const BindingFlags DEFAULT_FLAGS = BindingFlags.Instance | BindingFlags.Public;

        #region IObjectParser Members

        public void ParseMembers<T>(Action<MemberInfo> callback)
        {
            ParseMembers(typeof (T), callback);
        }

        public void ParseMembers(Type type, Action<MemberInfo> callback)
        {
            type.GetProperties(DEFAULT_FLAGS)
                .Cast<MemberInfo>()
                .Concat(type.GetFields(DEFAULT_FLAGS))
                .ToList()
                .ForEach(callback);
        }

        #endregion
    }
}