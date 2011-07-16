using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class NestedInterfacePersonFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            INestedInterfacePerson person = Auto.Make<INestedInterfacePerson>().Object;
            var i = person["string", 1];

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void T2()
        {
            INestedInterfacePerson person = Auto.Make<INestedInterfacePerson>().Object;
            var i = person.GenericMethod<string, string>("arg1", "arg2");

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.EqualTo("string"));
        }

        [Test]
        public void T3()
        {
            INestedInterfacePerson person = Auto.Make<INestedInterfacePerson>().Object;
            var i = person.GenericMethod<string, Person>("arg1", "arg2");

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo("FirstName"));
            Assert.That(i, Is.Not.Null);
            Assert.That(i.FirstName, Is.EqualTo("FirstName"));
        }

        [Test]
        public void T4()
        {
            INestedInterfacePerson person = Auto.Make<INestedInterfacePerson>().Object;
            var i = person.BasicMethod(1);

            Assert.That(person, Is.Not.Null);

            Assert.That(i, Is.Not.Null);
            Assert.That(i, Is.EqualTo("string"));
        }

        [Test]
        public void T5()
        {
            Auto.Configure.With("Global Config");

            INestedInterfacePerson person = Auto.Make<INestedInterfacePerson>().With("Instance Config").Object;
            var i = person.BasicMethod(1);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo("Instance Config"));

            // methods return values do not respect instance config, but do obey global config
            Assert.That(i, Is.EqualTo("Global Config"));
        }

        [Test]
        public void T6()
        {
            Auto.Configure.With("Global Config");

            INestedInterfacePerson person = Auto.Make<INestedInterfacePerson>().With("Instance Config").Object;
            var i = person.BasicMethod(1);

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo("Instance Config"));

            // methods return values do not respect instance config, but do obey global config
            Assert.That(i, Is.EqualTo("Global Config"));
        }

        [Test]
        public void T7()
        {
            INestedInterfacePerson person = Auto.Make<INestedInterfacePerson>().Object;
            var i = person.ParentProp;

            Assert.That(person, Is.Not.Null);

            Assert.That(i, Is.Not.Null);
            Assert.That(i, Is.EqualTo("string"));
        }

        [Test]
        public void T8()
        {
            INestedInterfacePerson person = Auto.Make<INestedInterfacePerson>().Object;
            var i = person.ParentGenericMethod<string, Person>("arg1", "arg2");

            Assert.That(person, Is.Not.Null);
            Assert.That(person.FirstName, Is.EqualTo("FirstName"));
            Assert.That(i, Is.Not.Null);
            Assert.That(i.FirstName, Is.EqualTo("FirstName"));
        }
    }
}