using System;
using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test
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

        [Test]
        public void T3()
        {
            Auto.Configure.Empty();

            Uri url = Auto.Make<Uri>();

            Assert.That(url, Is.Null);
        }

        [Test]
        public void T4()
        {
            Uri url = Auto.Make<Uri>()
                .Empty();

            Assert.That(url, Is.Null);
        }

        [Test]
        public void T5()
        {
            UriPerson person = Auto.Make<UriPerson>()
                .Empty();

            Assert.That(person.PersonUri, Is.Null);
        }

        [Test]
        public void T6()
        {
            Auto.Configure.Empty();

            UriPerson person = Auto.Make<UriPerson>();

            Assert.That(person.PersonUri, Is.Null);
        }

        [Test]
        public void T7()
        {
            Auto.Configure.Empty().Default();

            UriPerson person = Auto.Make<UriPerson>();

            Assert.That(person.PersonUri, Is.Null);
        }

        [Test]
        public void T8()
        {
            Auto.Configure.Empty().Max();

            UriPerson person = Auto.Make<UriPerson>();

            Assert.That(person.PersonUri, Is.Null);
        }

        [Test]
        public void T9()
        {
            Auto.Configure.Empty().Min();

            UriPerson person = Auto.Make<UriPerson>();

            Assert.That(person.PersonUri, Is.Null);
        }
    }
}