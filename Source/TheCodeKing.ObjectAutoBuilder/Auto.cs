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
using TheCodeKing.AutoBuilder.Config;
using TheCodeKing.AutoBuilder.Core;
using TheCodeKing.AutoBuilder.Interfaces;

namespace TheCodeKing.AutoBuilder
{
    public sealed class Auto
    {
        private static readonly IAutoConfigurationResolver Resolver;

        static Auto()
        {
            Resolver = new DefaultAutoConfiguration();
        }

        public static IDefaultAutoConfiguration Configure
        {
            get
            {
                return Resolver;
            }
        }

        public static AutoExpression<T> Make<T>()
        {
            IObjectParser objParser = new ObjectParser();
            Func<IAutoConfigurationResolver, IAutoObjectBuilder, IObjectParser, IAutoFiller> fillerFactory = (config, builder, parser) => new AutoFiller(config, builder, parser);
            Func<IAutoConfigurationResolver, Func<IAutoConfigurationResolver, IAutoObjectBuilder, IObjectParser, IAutoFiller>, IAutoObjectBuilder> builderFactory = (config, filler) => new AutoObjectBuilder(config, filler, objParser);
            return new AutoExpression<T>(builderFactory, fillerFactory, new CurrentAutoConfiguration(Resolver));
        }
    }
}