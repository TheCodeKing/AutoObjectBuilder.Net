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
using TheCodeKing.AutoBuilder.Base;
using TheCodeKing.AutoBuilder.Interfaces;

namespace TheCodeKing.AutoBuilder.Config
{
    internal sealed class CurrentAutoConfiguration : AutoConfiguration
    {
        public CurrentAutoConfiguration(IAutoConfigurationResolver globalConfiguration)
            :base(globalConfiguration)
        {
        }
    }
}
