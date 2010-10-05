using TheCodeKing.AutoBuilder.Test.Base;
using TheCodeKing.AutoBuilder.Test.Helper;
using NUnit.Framework;

namespace TheCodeKing.AutoBuilder.Test
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
        public void T2()
        {
            ComplexPerson person = Auto.Make<ComplexPerson>()
                .Set<ConflictPerson>(o => o.FirstName, 1);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo("FirstName"));
            Assert.That(person.ConflictPerson.FirstName, Is.EqualTo(1));
        }
    }
}
