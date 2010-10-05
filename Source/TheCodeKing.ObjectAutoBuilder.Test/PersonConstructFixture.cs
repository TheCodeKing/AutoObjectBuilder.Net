using TheCodeKing.AutoBuilder.Test.Base;
using TheCodeKing.AutoBuilder.Test.Helper;
using NUnit.Framework;

namespace TheCodeKing.AutoBuilder.Test
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