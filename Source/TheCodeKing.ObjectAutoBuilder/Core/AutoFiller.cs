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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using AutoObjectBuilder.Extensions;
using AutoObjectBuilder.Interfaces;

namespace AutoObjectBuilder.Core
{
    internal class AutoFiller : IAutoFiller
    {
        private readonly IAutoBuilder builder;
        private readonly IObjectParser parser;
        private readonly IAutoConfigurationResolver config;

        internal AutoFiller(IAutoConfigurationResolver config, IAutoBuilder builder, IObjectParser parser)
        {
            this.config = config;
            this.parser = parser;
            this.builder = builder;
        }

        public void FillObject(object o)
        {
            Type type = o.GetType();
            if (type.IsOfRawGenericTypeDefinition(typeof(Collection<>))
                     || type.IsOfRawGenericTypeDefinition(typeof(List<>)))
            {
                FillCollection(o);
            }
            else if (type.IsOfRawGenericTypeDefinition(typeof(Dictionary<,>)))
            {
                FillDictionary(o);
            }
            else
            {
                FillInstance(o);
            }
        }

        private void FillDictionary(object o)
        {
            if (config.GetEnumerableSize() > 0)
            {
                var type = o.GetType();
                MethodInfo addMethod = type.GetMethod("Add");
                var args = type.GetGenericArguments();

                var key = builder.CreateObject(args[0]);
                var value = builder.CreateObject(args[1]);
                addMethod.Invoke(o, new[] {key, value});
            }
        }

        private void FillCollection(object o)
        {
            var type = o.GetType();
            MethodInfo addMethod = type.GetMethod("Add");
            if (type.IsGenericType)
            {
                Type arg = type.GetGenericArguments().SingleOrDefault();
                var item = builder.CreateObject(arg);
                for (var i = 0; i < config.GetEnumerableSize(); i++)
                {
                    addMethod.Invoke(o, new[] { item });
                }
            }
            else
            {
                var pArr = addMethod.GetParameters();
                var args = pArr.Select(p => builder.CreateObject(p.ParameterType)).ToArray();
                for (var i = 0; i < config.GetEnumerableSize(); i++)
                {
                    addMethod.Invoke(o, args);
                }
            }
        }

        private void FillInstance(object o)
        {
            Type type = o.GetType();
            parser.ParseMembers(type, p =>
            {
                if (p.CanPropertyOrFieldWrite())
                {
                    var propType = p.PropertyOrFieldType();
                    object value;
                    var getter = config.ResolveMemberByName(p, type, false)
                        ?? config.ResolveMemberByType(p, type, false)
                        ?? config.ResolveMemberByName(p, type)
                        ?? config.ResolveMemberByType(p, type);
                    if (getter != null)
                    {
                        value = getter(p);
                    }
                    else
                    {
                        value = builder.CreateObject(propType);
                    }
                    if (value != null)
                    {
                        if (propType.IsAssignableFrom(value.GetType()))
                        {
                            object[] args = null; 
                            if (p is PropertyInfo)
                            {
                                var property = p as PropertyInfo;
                                args = property.GetIndexParameters().Select(pp => builder.CreateObject(pp.ParameterType)).ToArray();
                            }
                            try
                            {
                                p.SetPropertyOrFieldValue(o, value, args);
                            }
                            catch (TargetInvocationException)
                            {
                                
                            }
                        }
                        else if (!propType.IsAssignableFrom(value.GetType()))
                        {
                            throw new InvalidCastException(
                                string.Format("Expected {0}, but was {1}: {2}",
                                              propType.ToString(),
                                              value.GetType().ToString(), p.Name));
                        }
                    }
                }
            });
            var actions = config.ResolvePostProcessors(type);
            if (actions != null)
            {
               actions(o);
            }
        }
    }
}
