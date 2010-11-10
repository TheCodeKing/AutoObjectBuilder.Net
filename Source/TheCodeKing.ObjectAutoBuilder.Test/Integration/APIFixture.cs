using System;
using System.Collections.Generic;
using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class ApiFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            // Create instance explicitly by Type
            string s = Auto.Make<string>()
                .With("hello");

            Assert.That(s, Is.EqualTo("hello"));
        }

        public void T15()
        {
            int id = 0;
            Auto.Configure.With(() => id++);

            // Create instance explicitly by Type
            int value = Auto.Make<int>();
            int value2 = Auto.Make<int>();

            Assert.That(value, Is.EqualTo(0));
            Assert.That(value2, Is.EqualTo(1));    

        }

        public void T16()
        {
            Auto.Configure.SetTestPerson();

            // Create instance explicitly by Type
            Person value = Auto.Make<Person>();
            Person value2 = Auto.Make<Person>();

            Assert.That(value.IntId, Is.EqualTo(0));
            Assert.That(value2.IntId, Is.EqualTo(1));

        }

        [Test]
        public void T2()
        {
            // Create instance by Type
            Person person = Auto.Make<Person>()
                .With(t => t.FullName);

            Assert.That(person.FirstName, Is.EqualTo("System.String"));
        }

        [Test]
        public void T3()
        {
            // Set member using MemberInfo instance
            Person person = Auto.Make<Person>()
                .Setter(m => m.Name+": A STRING");

            Assert.That(person.FirstName, Is.EqualTo("FirstName: A STRING"));
        }

        [Test]
        public void T4()
        {
            // Set member explicitly via Action
            Person person = Auto.Make<Person>()
                .Do<Person>(o => o.FirstName = "John");

            Assert.That(person.FirstName, Is.EqualTo("John"));
        }

        [Test]
        public void T5()
        {
            // Create with default values (value types)
            Person person = Auto.Make<Person>()
                .Default();

            Assert.That(person.IntId, Is.EqualTo(0));
        }

        [Test]
        public void T6()
        {
            // Create with Max values (value types)
            Person person = Auto.Make<Person>()
                .Max();

            Assert.That(person.IntId, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void T7()
        {
            // Create with Min values  (value types)
            Person person = Auto.Make<Person>()
                .Min();

            Assert.That(person.IntId, Is.EqualTo(int.MinValue));
        }

        [Test]
        public void T8()
        {
            // create an interface instance, use .Object as implicit cast does not work
            // with interface types
            IPerson person = Auto.Make<IPerson>().Object;

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo("FirstName"));
        }

        [Test]
        public void T9()
        {
            // create an Array type, populated with a defined number of items
            IPerson[] person = Auto.Make<IPerson[]>().EnumerableSize(5);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.Length, Is.EqualTo(5));
        }

        [Test]
        public void T10()
        {
            // create an List type, populated with a defined number of items
            IList<IPerson> person = Auto.Make<IList<IPerson>>().EnumerableSize(5).Object;

            Assert.That(person, Is.Not.Null);
            Assert.That(person.Count, Is.EqualTo(5));
        }

        [Test]
        public void T11()
        {
            // create an Collection type, populated with a defined number of items
            ICollection<IPerson> person = Auto.Make<ICollection<IPerson>>().EnumerableSize(10).Object;

            Assert.That(person, Is.Not.Null);
            Assert.That(person.Count, Is.EqualTo(10));
        }

        [Test]
        public void T12()
        {
            // create an Collection type, populated with a defined number of items
            IEnumerable<IPerson> person = Auto.Make<IEnumerable<IPerson>>().EnumerableSize(10).Object;

            Assert.That(person, Is.Not.Null);

            int count =0;
            foreach(var p in person)
            {
                count++;
                Assert.That(p, Is.Not.Null);   
            }
            Assert.That(count, Is.EqualTo(10));
        }

        [Test]
        public void T13()
        {
            // create an Collection type, populated with a defined number of items
            IDictionary<string, IPerson> peopleLookups = Auto.Make<IDictionary<string, IPerson>>().EnumerableSize(10).Object;

            Assert.That(peopleLookups, Is.Not.Null);
            // only one item due to dictionary key
            Assert.That(peopleLookups.Count, Is.EqualTo(1));
        }

        [Test]
        public void T14()
        {
            // create an Collection type, populated with a defined number of items
            IDictionary<int, IPerson> peopleLookups = Auto.Make<IDictionary<int, IPerson>>().EnumerableSize(0).Object;

            Assert.That(peopleLookups, Is.Not.Null);
            Assert.That(peopleLookups.Count, Is.EqualTo(0));
        }

        [Test]
        public void T17()
        {
            // Set member explicitly via Action
            Person person = Auto.Make<Person>()
                .Do(o => o.FirstName = "John")
                .Do(o => o.LastName="Smith");

            Assert.That(person.FirstName, Is.EqualTo("John"));
            Assert.That(person.LastName, Is.EqualTo("Smith"));
        }
    }
}