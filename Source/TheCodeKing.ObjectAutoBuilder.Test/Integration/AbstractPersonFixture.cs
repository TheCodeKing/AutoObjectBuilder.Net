using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class AbstractPersonFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            AbstractPerson person = Auto.Make<AbstractPerson>().Object;
            var i = person["string", 1];

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void T2()
        {
            AbstractPerson person = Auto.Make<AbstractPerson>().Object;
            var i = person.GenericMethod<string, string>("arg1", "arg2");

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.EqualTo("string"));
        }
        
        [Test]
        public void T3()
        {
            AbstractPerson person = Auto.Make<AbstractPerson>().Object;
            var i = person.GenericMethod<string, AbstractPerson>("arg1", "arg2");

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.Not.Null);
            Assert.That(i.FirstName, Is.EqualTo("FirstName"));
        }
        
        [Test]
        public void T4()
        {
            AbstractPerson person = Auto.Make<AbstractPerson>().Object;
            var i = person.BasicMethod(1);

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.Not.Null);
        }

        [Test]
        public void T5()
        {
            AbstractPerson person = Auto.Make<AbstractPerson>();
            var i = person.Readonly;

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void T6()
        {
            AbstractPerson person = Auto.Make<AbstractPerson>();
            var i = person.NonAbstractBasicMethod(1);

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.Null);
        }

        [Test]
        public void T7()
        {
            AbstractPerson person = Auto.Make<AbstractPerson>();
            var i = person.NonAbstractIntId;

            Assert.That(i, Is.EqualTo(0));
        }

        [Test]
        public void T8()
        {
            InernalAbstractPerson person = Auto.Make<InernalAbstractPerson>();
            Assert.That(person, Is.Null);
        }

        [Test]
        public void T9()
        {
            Auto.Configure.With(5);

            AbstractPerson person = Auto.Make<AbstractPerson>().Min();
            var i = person.Readonly;

            Assert.That(person, Is.Not.Null);
            Assert.That(person.IntId, Is.EqualTo(int.MinValue));

            // readonly proxy methods don't respect instance config, but do respect
            // global config
            Assert.That(i, Is.EqualTo(5));
        }
    }
}
