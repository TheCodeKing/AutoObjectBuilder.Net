using TheCodeKing.AutoBuilder.Test.Base;
using TheCodeKing.AutoBuilder.Test.Helper;
using NUnit.Framework;

namespace TheCodeKing.AutoBuilder.Test
{
    [TestFixture]
    public class APIFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            // Create explicitly by Type
            string s = Auto.Make<string>()
                .Factory("hello");

            Assert.That(s, Is.EqualTo("hello"));
        }

        [Test]
        public void T2()
        {
            // Create by member Type
            Person person = Auto.Make<Person>()
                .Factory(t => t.FullName);

            Assert.That(person.FirstName, Is.EqualTo("System.String"));
        }

        [Test]
        public void T3()
        {
            // Set member using MemberInfo
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
    }
}