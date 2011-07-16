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
using AutoObjectBuilder.Interfaces;

namespace AutoObjectBuilder.Extensions
{
    internal static class AutoConfigurationResolverExtension
    {
        private const string ENUMERATION_SIZE_KEY = "EnumerationSize";

        internal static int GetEnumerableSize(this IAutoConfigurationResolver configuration)
        {
            int valueInt;
            var value = configuration[ENUMERATION_SIZE_KEY];
            if (int.TryParse(value, out valueInt))
            {
                return valueInt;
            }
            return 0;
        }

        internal static void SetEnumerableSize(this IAutoConfigurationResolver configuration, int size)
        {
            configuration[ENUMERATION_SIZE_KEY] = Convert.ToString(size);
        }
    }
}