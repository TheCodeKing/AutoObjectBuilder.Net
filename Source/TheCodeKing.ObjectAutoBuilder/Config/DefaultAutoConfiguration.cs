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
using AutoObjectBuilder.Base;

namespace AutoObjectBuilder.Config
{
    public sealed class DefaultAutoConfiguration : AutoConfiguration<DefaultAutoConfiguration, DefaultAutoConfiguration>
    {
        protected override void RegisterFactory(Type type, Func<Type, object> factory)
        {
            // special case for detault configuration, allow a sting factory to
            // override default setter behaviour
            if (type == typeof (string))
            {
                RegisterMemberResolver(type, m => factory(typeof (string)));
            }
            base.RegisterFactory(type, factory);
        }
    }
}