using System.Collections.Generic;
using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class APIFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            // Create instance explicitly by Type
            string s = Auto.Make<string>()
                .With("hello");

            Assert.That(s, Is.EqualTo("hello"));
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
            // Set member explicitly via expression
            Person person = Auto.Make<Person>()
                .Set<Person>(o => o.FirstName, "John");

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
            IPerson[] person = Auto.Make<IPerson[]>().EnumerableSize(10);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.Length, Is.EqualTo(10));
        }

        [Test]
        public void T10()
        {
            // create an List type, populated with a defined number of items
            IList<IPerson> person = Auto.Make<IList<IPerson>>().EnumerableSize(10).Object;

            Assert.That(person, Is.Not.Null);
            Assert.That(person.Count, Is.EqualTo(10));
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
    }
}