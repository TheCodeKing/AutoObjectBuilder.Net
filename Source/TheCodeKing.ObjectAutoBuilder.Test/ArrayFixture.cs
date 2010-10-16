using System;
using System.Collections.Generic;
using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test
{
    [TestFixture]
    public class ArrayFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            string[] p = Auto.Make<string[]>();

            Assert.That(p, Is.Not.Null);
            Assert.That(p.Length, Is.EqualTo(2));
            Assert.That(p[0], Is.EqualTo("string"));
            Assert.That(p[1], Is.EqualTo("string"));
        }

        [Test]
        public void T2()
        {
            string[] p = Auto.Make<string[]>().EnumerableSize(5);

            Assert.That(p, Is.Not.Null);
            Assert.That(p.Length, Is.EqualTo(5));
        }

        [Test]
        public void T3()
        {
            Auto.Configure.EnumerableSize(10);

            string[] p = Auto.Make<string[]>();

            Assert.That(p, Is.Not.Null);
            Assert.That(p.Length, Is.EqualTo(10));
        }

        [Test]
        public void T4()
        {
            Person[] person = Auto.Make<Person[]>().EnumerableSize(0);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.Length, Is.EqualTo(0));
        }

        [Test]
        public void T5()
        {
            Person[] person = Auto.Make<Person[]>().EnumerableSize(1);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.Length, Is.EqualTo(1));
            Assert.That(person[0].FirstName, Is.EqualTo("FirstName"));
        }

        [Test]
        public void T6()
        {
            IEnumerable<int> person = Auto.Make<IEnumerable<int>>().EnumerableSize(1).Object;

            Assert.That(person, Is.Not.Null);
        }
    }
}