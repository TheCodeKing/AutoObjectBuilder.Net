using System.Reflection;
using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test
{
    [TestFixture]
    public class InterfacePersonFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            IPerson person = Auto.Make<IPerson>().Object;
            var i = person["string", 1];

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void T2()
        {
            IPerson person = Auto.Make<IPerson>().Object;
            var i = person.GenericMethod<string, string>("arg1", "arg2");

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.EqualTo("string"));
        }

        [Test]
        public void T3()
        {
            IPerson person = Auto.Make<IPerson>().Object;
            var i = person.GenericMethod<string, Person>("arg1", "arg2");

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.Not.Null);
            Assert.That(i.FirstName, Is.EqualTo("FirstName"));
        }

        [Test]
        public void T4()
        {
            IPerson person = Auto.Make<IPerson>().Object;
            var i = person.BasicMethod(1);

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.Not.Null);
        }
    }
}
