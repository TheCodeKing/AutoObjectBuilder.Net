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

namespace AutoObjectBuilder.Extensions
{
    internal static class ExpressionExtension
    {
        internal static string ResolveMemberName(this LambdaExpression expression)
        {
            MemberExpression mem = null;
            if (ExpressionType.MemberAccess == expression.Body.NodeType)
            {
                mem = expression.Body as MemberExpression;
            }
            else if (ExpressionType.Convert == expression.Body.NodeType)
            {
                var urnary = expression.Body as UnaryExpression;
                if (urnary != null)
                {
                    mem = urnary.Operand as MemberExpression;
                }
            }
            if (mem != null)
            {
                return mem.Member.Name;
            }
            return null;
        }

        internal static Type ResolveMemberType(this LambdaExpression expression)
        {
            MemberExpression mem = null;
            if (ExpressionType.MemberAccess == expression.Body.NodeType)
            {
                mem = expression.Body as MemberExpression;
            }
            else if (ExpressionType.Convert == expression.Body.NodeType)
            {
                var urnary = expression.Body as UnaryExpression;
                if (urnary != null)
                {
                    mem = urnary.Operand as MemberExpression;
                }
            }
            if (mem != null)
            {
                return mem.Member.PropertyOrFieldType();
            }
            return null;
        }
    }
}