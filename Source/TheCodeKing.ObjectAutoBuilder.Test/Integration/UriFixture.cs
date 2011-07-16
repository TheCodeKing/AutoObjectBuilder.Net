using System;
using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class UriFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            Uri p = Auto.Make<Uri>();
            Assert.That(p, Is.Not.Null);
            Assert.That(p.ToString(), Is.EqualTo("http://uri/"));
        }

        [Test]
        public void T2()
        {
            UriPerson p = Auto.Make<UriPerson>();

            Assert.That(p, Is.Not.Null);
            Assert.That(p.PersonUri, Is.Not.Null);
            Assert.That(p.PersonUri.ToString(), Is.EqualTo("http://personuri/"));
        }
    }
}