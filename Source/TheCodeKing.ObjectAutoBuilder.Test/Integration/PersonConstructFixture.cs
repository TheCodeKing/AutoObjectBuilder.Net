using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test
{
    [TestFixture]
    public class PersonConstructFixture : TestFixtureBase
    {

        [Test]
        public void T1()
        {
            Person p = Auto.Make<Person>();
            AssertDefault(p);
            AssertDefault(p.Mother);
            AssertDefault(p.Father);
        }

        [Test]
        public void T2()
        {
            ComplexPerson p = Auto.Make<ComplexPerson>();
            AssertDefault(p);
            AssertDefault(p.Mother);
            AssertDefault(p.Father);
        }

        [Test]
        public void T3()
        {
            PrivatePerson p = Auto.Make<PrivatePerson>();
            AssertDefault(p);
            AssertDefault(p.Mother);
            AssertDefault(p.Father);
        }

        [Test]
        public void T4()
        {
            ImpossiblePerson p = Auto.Make<ImpossiblePerson>();
            AssertDefault(p);
            AssertDefault(p.Mother);
            AssertDefault(p.Father);
            Assert.That(p.IsContructed, Is.False);
        }
    }
}