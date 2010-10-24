using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class GeneralFixture : TestFixtureBase
    {
        public void T1()
        {
            Auto.Configure.Default();

            string s1 = Auto.Make<string>();

            Assert.That(s1, Is.EqualTo("string"));

            Auto.Configure.UseDefaultConfiguration();

            string s2 = Auto.Make<string>();

            Assert.That(s2, Is.EqualTo("string"));
        }

        public void T2()
        {
            Auto.Configure.With("Configure");

            string s1 = Auto.Make<string>();

            Assert.That(s1, Is.EqualTo("Configure"));

            Auto.Configure.UseDefaultConfiguration();

            string s2 = Auto.Make<string>();

            Assert.That(s2, Is.EqualTo("string"));
        }

        public void T3()
        {
            string s1 = Auto.Make<string>()
                .With("Instance");

            Assert.That(s1, Is.EqualTo("Instance"));

            Auto.Configure.UseDefaultConfiguration();

            string s2 = Auto.Make<string>();

            Assert.That(s2, Is.EqualTo("string"));
        }

        public void T4()
        {
            Auto.Configure.With("Instance");

            string s1 = Auto.Make<string>();

            Assert.That(s1, Is.EqualTo("Instance"));

            Auto.Configure.UseDefaultConfiguration();

            string s2 = Auto.Make<string>().Min();

            Assert.That(s2, Is.EqualTo("string"));
        }

        public void T5()
        {
            Auto.Configure.With("Instance");

            string s1 = Auto.Make<string>();

            Assert.That(s1, Is.EqualTo("Instance"));

            Auto.Configure.UseDefaultConfiguration();

            string s2 = Auto.Make<string>().Min();

            Assert.That(s2, Is.EqualTo("string"));
        }

        [Test]
        public void T6()
        {
            // try a random .Net class
            HttpWebRequest request = Auto.Make<HttpWebRequest>();

            Assert.That(request, Is.Not.Null);
            Assert.That(request.Expect, Is.EqualTo("Expect"));
            Assert.That(request.Headers, Is.Not.Null);
            Assert.That(request.IfModifiedSince, Is.EqualTo(DateTime.MaxValue));
            Assert.That(request.MaximumAutomaticRedirections, Is.EqualTo(int.MaxValue));
            Assert.That(request.MaximumResponseHeadersLength, Is.EqualTo(int.MaxValue));
            Assert.That(request.Pipelined, Is.EqualTo(true));
            Assert.That(request.MediaType, Is.EqualTo("MediaType"));
        }

        [Test]
        public void T7()
        {
            Guid guid = Auto.Make<Guid>();

            Assert.That(guid, Is.Not.Null);
        }

        [Test]
        public void T8()
        {
            object value = Auto.Make<object>();

            Assert.That(value, Is.Not.Null);
        }

        [Test]
        public void T9()
        {
            Dictionary<string, int> value = Auto.Make<Dictionary<string,int>>();

            Assert.That(value, Is.Not.Null);
            Assert.That(value.Count, Is.EqualTo(1));
            Assert.That(value["string"], Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void T10()
        {
            Regex value = Auto.Make<Regex>();

            Assert.That(value, Is.Not.Null);
        }

        [Test]
        public void T11()
        {
            Exception value = Auto.Make<Exception>();

            Assert.That(value, Is.Not.Null);
        }

        [Test]
        public void T12()
        {
            DataTable value = Auto.Make<DataTable>();

            Assert.That(value, Is.Not.Null);
        }

        [Test]
        public void T13()
        {
            CultureInfo value = Auto.Make<CultureInfo>();

            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.EqualTo(Thread.CurrentThread.CurrentCulture));
        }
    }
}
