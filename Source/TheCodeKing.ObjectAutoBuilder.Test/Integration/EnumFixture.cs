using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class EnumFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            Title p = Auto.Make<Title>()
                .Min();
            Assert.That(p, Is.EqualTo(Title.Mr));
        }
        [Test]

        public void T2()
        {
            Title p = Auto.Make<Title>()
                .Default();
            Assert.That(p, Is.EqualTo(Title.Mr));
        }

        [Test]
        public void T3()
        {
            Title p = Auto.Make<Title>()
                .Max();
            Assert.That(p, Is.EqualTo(Title.Sir));
        }

        [Test]
        public void T4()
        {
            Title p = Auto.Make<Title>()
                .With(Title.Mrs);
            Assert.That(p, Is.EqualTo(Title.Mrs));
        }

        [Test]
        public void T5()
        {
            Person p = Auto.Make<Person>()
                .With(Title.Mrs);

            Assert.That(p.TitleEnum, Is.EqualTo(Title.Mrs));
        }

        [Test]
        public void T6()
        {
            Person p = Auto.Make<Person>()
                .Set<Person>(o => o.TitleEnum, Title.Dr);

            Assert.That(p.TitleEnum, Is.EqualTo(Title.Dr));
        }
    }
}
