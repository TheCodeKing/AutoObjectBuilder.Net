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
using System.IO;
using System.Linq;
using System.Threading;
using AutoObjectBuilder.Config;
using AutoObjectBuilder.Extensions;
using AutoObjectBuilder.Interfaces;

// ReSharper disable CheckNamespace
namespace AutoObjectBuilder
// ReSharper restore CheckNamespace
{
    public static class FluentExtension
    {
        public static DefaultAutoConfiguration UseDefaultConfiguration(this DefaultAutoConfiguration autoConfiguration)
        {
            ((IAutoConfigurationResolver)autoConfiguration).Clear();
            autoConfiguration.With("string")
                .With(new Uri("http://uri/"))
                .With(new FileInfo(@"C:\filename"))
                .With(new DirectoryInfo(@"C:\directoryname"))
                .With(IntPtr.Zero)
                .With(Guid.NewGuid)
                .With(Thread.CurrentThread.CurrentCulture)
                .Setter(m => new FileInfo(@"C:\" + m.Name.ToLowerInvariant()))
                .Setter(m => new DirectoryInfo(@"C:\" + m.Name.ToLowerInvariant()))
                .Setter(m => new Uri("http://" + m.Name.ToLowerInvariant()))
                .Setter(m => m.Name)
                .Max()
                .EnumerableSize(10);

            return autoConfiguration;
        }

        public static TReturn Max<TReturn>(this TReturn autoExpression)
            where TReturn : IAutoConfiguration
        {
            autoExpression.With(int.MaxValue)
                .With(true)
                .With(uint.MaxValue)
                .With(long.MaxValue)
                .With(ulong.MaxValue)
                .With(short.MaxValue)
                .With(ushort.MaxValue)
                .With(double.MaxValue)
                .With(sbyte.MaxValue)
                .With(byte.MaxValue)
                .With(char.MaxValue)
                .With(DateTime.MaxValue)
                .With(t => Enum.GetValues(t).Cast<Enum>().Last());

            return autoExpression;
        }

        public static TReturn Min<TReturn>(this TReturn autoExpression)
            where TReturn : IAutoConfiguration
        {
            autoExpression.With(int.MinValue)
                .With(false)
                .With(uint.MinValue)
                .With(long.MinValue)
                .With(ulong.MinValue)
                .With(short.MinValue)
                .With(ushort.MinValue)
                .With(double.MinValue)
                .With(byte.MinValue)
                .With(sbyte.MinValue)
                .With(char.MinValue)
                .With(DateTime.MinValue)
                .With(t => Enum.GetValues(t).Cast<Enum>().First());

            return autoExpression;
        }

        public static TReturn Default<TReturn>(this TReturn autoExpression)
            where TReturn : IAutoConfiguration
        {
            autoExpression.With(default(int))
                .With(default(bool))
                .With(default(uint))
                .With(default(long))
                .With(default(ulong))
                .With(default(short))
                .With(default(ushort))
                .With(default(double))
                .With(default(byte))
                .With(default(sbyte))
                .With(default(char))
                .With(default(DateTime))
                .With(t => Enum.GetValues(t).Cast<Enum>().First());

            return autoExpression;
        }

        public static TReturn EnumerableSize<TReturn>(this TReturn autoExpression, int count)
            where TReturn : IAutoConfiguration
        {
            ((IAutoConfigurationResolver)autoExpression).SetEnumerableSize(count);
            return autoExpression;
        }
    }
}
