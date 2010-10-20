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
            IPerson[] people = Auto.Make<IPerson[]>().EnumerableSize(0);

            Assert.That(people, Is.Not.Null);
            Assert.That(people.Length, Is.EqualTo(0));
        }

        [Test]
        public void T5()
        {
            Person[] people = Auto.Make<Person[]>().EnumerableSize(1);

            Assert.That(people, Is.Not.Null);
            Assert.That(people.Length, Is.EqualTo(1));
            Assert.That(people[0].FirstName, Is.EqualTo("FirstName"));
        }

        [Test]
        public void T6()
        {
            IEnumerable<int> value = Auto.Make<IEnumerable<int>>().EnumerableSize(1).Object;

            Assert.That(value, Is.Not.Null);
        }

        [Test]
        public void T7()
        {
            IList<IPerson> people = Auto.Make<IList<IPerson>>().EnumerableSize(1).Object;

            Assert.That(people, Is.Not.Null);
            Assert.That(people.Count, Is.EqualTo(1));
        }

        [Test]
        public void T8()
        {
            ICollection<IPerson> people = Auto.Make<ICollection<IPerson>>().EnumerableSize(1).Object;

            Assert.That(people, Is.Not.Null);
            Assert.That(people.Count, Is.EqualTo(1));
        }
    }
}