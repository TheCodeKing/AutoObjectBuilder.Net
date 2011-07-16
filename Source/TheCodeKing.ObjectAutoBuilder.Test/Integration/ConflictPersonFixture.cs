using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class ConflictPersonFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            ComplexPerson person = Auto.Make<ComplexPerson>()
                .Set<ConflictPerson>(o => o.FirstName, 1);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo("FirstName"));
            Assert.That(person.ConflictPerson.FirstName, Is.EqualTo(1));
        }

        [Test]
        public void T1_1()
        {
            ComplexPerson person = Auto.Make<ComplexPerson>()
                .Do<ConflictPerson>(o => o.FirstName = 1);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo("FirstName"));
            Assert.That(person.ConflictPerson.FirstName, Is.EqualTo(1));
        }

        [Test]
        public void T2()
        {
            ComplexPerson person = Auto.Make<ComplexPerson>()
                .Set<ConflictPerson>(o => o.FirstName, 1);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo("FirstName"));
            Assert.That(person.ConflictPerson.FirstName, Is.EqualTo(1));
        }

        [Test]
        public void T2_1()
        {
            ComplexPerson person = Auto.Make<ComplexPerson>()
                .Do<ConflictPerson>(o => o.FirstName = 1);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo("FirstName"));
            Assert.That(person.ConflictPerson.FirstName, Is.EqualTo(1));
        }
    }
}