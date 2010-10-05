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

namespace TheCodeKing.AutoBuilder.Extensions
{
    internal static class MemberInfoExtension
    {
        public static bool CanPropertyOrFieldWrite(this MemberInfo memberInfo)
        {
            var prop = memberInfo as PropertyInfo;
            if (prop != null)
            {
                return prop.CanWrite;
            }
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return true;
            }
            throw new InvalidOperationException("MemberInfo must be of type PropertyInfo or FieldInfo");
        }

        public static Type PropertyOrFieldType(this MemberInfo memberInfo)
        {
            var prop = memberInfo as PropertyInfo;
            if (prop != null)
            {
                return prop.PropertyType;
            }
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.FieldType;
            }
            throw new InvalidOperationException("MemberInfo must be of type PropertyInfo or FieldInfo");
        }

        public static void SetPropertyOrFieldValue(this MemberInfo memberInfo, object target, object value,
                                                   object[] args)
        {
            var prop = memberInfo as PropertyInfo;
            if (prop != null)
            {
                prop.SetValue(target, value, args);
                return;
            }
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(target, value);
                return;
            }
            throw new InvalidOperationException("MemberInfo must be of type PropertyInfo or FieldInfo");
        }
    }
}