using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test
{
    [TestFixture]
    public class PersonFixture : TestFixtureBase
    {
        protected Person Compare
        {
            get { return Person.PersonTest1; }
        }

        public void T5()
        {
            var p = Person.PersonTest1;

            Auto.Configure.Factory(p);

            Person person = Auto.Make<Person>();

            Assert.That(person, Is.Not.Null);
            Assert.That(person, Is.EqualTo(p));
        }

        [Test]
        public void T1()
        {
            Person p = Auto.Make<Person>();
            AssertDefault(p);
            AssertDefault(p.Mother);
            AssertDefault(p.Father);
        }

        [Test]
        public void T10()
        {
            int i = 0;
            Auto.Configure.Factory(() => new Person { FirstName = "Test" + (i++) });

            Person person1 = Auto.Make<Person>();

           Auto.Configure.UseDefaultConfiguration();

            Person person2 = Auto.Make<Person>()
                .Set<Person>(o => o.FirstName, "hello");

            Assert.That(person1.FirstName, Is.EqualTo("Test0"));
            Assert.That(person2.FirstName, Is.EqualTo("hello"));
        }

        [Test]
        public void T11()
        {
            Person person = Auto.Make<Person>()
                .Factory("TEST")
                .Factory(10)
                .Set<Person>(o => o.FirstName, "hello");

            Assert.That(person.FirstName, Is.EqualTo("hello"));
            Assert.That(person.LastName, Is.EqualTo("TEST"));
            Assert.That(person.IntId, Is.EqualTo(10));
            Assert.That(person.Father, Is.Not.Null);
            Assert.That(person.Father.IntId, Is.EqualTo(10));
            Assert.That(person.Father.FirstName, Is.EqualTo("hello"));
            Assert.That(person.Father.LastName, Is.EqualTo("TEST"));
        }

        [Test]
        public void T12()
        {
            Person person1 = Auto.Make<Person>();

            Person person = Auto.Make<Person>()
                .Set<Person>(o => o.FirstName, "hello");

            person1.Mother.FirstName = "other";

            Assert.That(person1.FirstName, Is.EqualTo("other"));
            Assert.That(person.FirstName, Is.EqualTo("hello"));
        }

        [Test]
        public void T13()
        {
            Person person = Auto.Make<Person>()
                .Factory(Compare);

            Assert.That(person == Compare, Is.True);

            Person person1 = Auto.Make<Person>()
                .Set<Person>(o => o.FirstName, "hello");

            Assert.That(person == Compare, Is.True);
            Assert.That(person1 == Compare, Is.Not.True);
            Assert.That(Compare.FirstName, Is.EqualTo("FirstName"));
        }

        [Test]
        public void T14()
        {
            Person person = Auto.Make<Person>().Factory(() => new Person { FirstName = "TEST1" });

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo("TEST1"));
        }

        [Test]
        public void T15()
        {
            const string motherName = "Mother";
            const string fatherName = "Father";

            Auto.Configure
                .Set<Person>(o => o.FirstName, Compare.FirstName)
                .Set<Person>(o => o.LastName, Compare.LastName)
                .Set<Person>(o => o.Mother, new Person {FirstName = motherName})
                .Set<Person>(o => o.Father, new Person {FirstName = fatherName});

            Person person = Auto.Make<Person>();

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo(Compare.FirstName));
            Assert.That(person.LastName, Is.EqualTo(Compare.LastName));

            Assert.That(person.Father, Is.Not.Null);
            Assert.That(person.Father.FirstName, Is.EqualTo(fatherName));
            Assert.That(person.Mother, Is.Not.Null);
            Assert.That(person.Mother.FirstName, Is.EqualTo(motherName));
        }

        [Test]
        public void T16()
        {
            Auto.Configure.Factory("T16");

            Person person = Auto.Make<Person>()
                .Set<Person>(o => o.FirstName, "hello");

            Assert.That(person.LastName, Is.EqualTo("T16"));
        }

        [Test]
        public void T2()
        {
            Auto.Configure.Min();

            Person p = Auto.Make<Person>();
            AssertDefault(p);
            AssertMinValues(p.Mother);
            AssertMinValues(p.Father);
        }

        [Test]
        public void T3()
        {
            Auto.Configure.Max();

            Person p = Auto.Make<Person>();
            AssertDefault(p);
            AssertMaxValues(p.Mother);
            AssertMaxValues(p.Father);
        }

        [Test]
        public void T4()
        {
            Auto.Configure.Min();

            Person p = Auto.Make<Person>();
            AssertDefault(p);
            AssertMinValues(p.Mother);
            AssertMinValues(p.Father);

            Auto.Configure.Max();

            p = Auto.Make<Person>();
            AssertDefault(p);
            AssertMaxValues(p.Mother);
            AssertMaxValues(p.Father);
        }

        [Test]
        public void T6()
        {
            var p = Person.PersonTest1;

            Auto.Configure.Factory(p);

            Person person = Auto.Make<Person>();

            Assert.That(person, Is.Not.Null);
            Assert.That(person, Is.EqualTo(p));
        }

        [Test]
        public void T7()
        {
            var p = Person.PersonTest1;

            Person person = Auto.Make<Person>().Factory(p);

            Assert.That(person, Is.Not.Null);
            Assert.That(person, Is.EqualTo(p));
        }

        [Test]
        public void T8()
        {
            var p = Person.PersonTest1;

            Person person = Auto.Make<Person>().Factory(100);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.IntId, Is.EqualTo(100));
        }

        [Test]
        public void T9()
        {
            int i = 0;
            Auto.Configure.Factory(() => new Person { FirstName = "Test" + (i++) });

            Person person1 = Auto.Make<Person>();
            Person person2 = Auto.Make<Person>();
            Person person3 = Auto.Make<Person>()
                .Factory(() => new Person { FirstName = "Test" + (i++) });

            Person person4 = Auto.Make<Person>()
                .Factory(Compare);

            Auto.Configure.UseDefaultConfiguration();

            Person person5 = Auto.Make<Person>()
                .Set<Person>(o => o.FirstName, "hello");

            Person person6 = Auto.Make<Person>()
                .Factory("TEST")
                .Factory(10)
                .Set<Person>(o => o.FirstName, "hello");

            Auto.Configure.Factory("T9");

            Person person7 = Auto.Make<Person>()
                .Set<Person>(o => o.FirstName, "hello");

            Person person8 = Auto.Make<Person>()
                .Factory("TEST")
                .Set<Person>(o => o.FirstName, "hello");

            Assert.That(person1.FirstName, Is.EqualTo("Test0"));
            Assert.That(person2.FirstName, Is.EqualTo("Test1"));
            Assert.That(person3.FirstName, Is.EqualTo("Test2"));
            Assert.That(person4.FirstName, Is.EqualTo(Compare.FirstName));
            Assert.That(person5.FirstName, Is.EqualTo("hello"));
            Assert.That(person6.LastName, Is.EqualTo("TEST"));
            Assert.That(person6.IntId, Is.EqualTo(10));
            Assert.That(person6.Father.IntId, Is.EqualTo(10));
            Assert.That(person7.LastName, Is.EqualTo("T9"));
            Assert.That(person8.LastName, Is.EqualTo("TEST"));
        }
    }
}