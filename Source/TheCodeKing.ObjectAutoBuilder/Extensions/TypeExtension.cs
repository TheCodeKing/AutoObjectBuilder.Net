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

namespace TheCodeKing.AutoBuilder.Extensions
{
    internal static class TypeExtension
    {
        public static bool IsOfRawGenericTypeDefinition(this Type toCheck, Type generic)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                if (toCheck.IsGenericType && toCheck.GetGenericTypeDefinition() == generic)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        public static string CreateKey(this Type type, string variation = "")
        {
            return string.Concat(type.FullName, "+", variation).Trim('+');
        }

        public static object GetDefault(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}