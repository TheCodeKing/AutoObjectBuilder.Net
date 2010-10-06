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
using AutoObjectBuilder.Base;
using AutoObjectBuilder.Interfaces;

namespace AutoObjectBuilder.Config
{
    internal sealed class CurrentAutoConfiguration : AutoConfiguration
    {
        public CurrentAutoConfiguration(IAutoConfigurationResolver globalConfiguration)
            :base(globalConfiguration)
        {
            UseDefaultConfiguration();
        }
    }
}
