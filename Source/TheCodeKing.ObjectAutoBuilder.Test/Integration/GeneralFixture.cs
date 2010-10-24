using System;
using System.Net;
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


    }
}
