using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class StringFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            string p = Auto.Make<string>()
                .Min();
            Assert.That(p, Is.EqualTo("string"));
        }

        [Test]
        public void T2()
        {
            string p = Auto.Make<string>()
                .With("Test")
                .Setter(m => m.Name);

            Assert.That(p, Is.EqualTo("Test"));
        }

        [Test]
        public void T3()
        {
            Auto.Configure
                .With("Test")
                .Setter(m => "Test"+m.Name);

            Person p = Auto.Make<Person>();

            Assert.That(p.FirstName, Is.EqualTo("TestFirstName"));
        }

        [Test]
        public void T4()
        {
            Auto.Configure
                .With("Config")
                .Setter(m => "Config"+m.Name);

            string p = Auto.Make<string>()
                .With("Instance")
                .Setter(m => "Instance" + m.Name);

            Assert.That(p, Is.EqualTo("Instance"));
        }

        [Test]
        public void T5()
        {
            Auto.Configure
                .With("Config")
                .Setter(m => "Config" + m.Name);

            Person p = Auto.Make<Person>()
                .With("Instance")
                .Setter(m => "Instance" + m.Name);

            string s = Auto.Make<string>()
                .With("Instance")
                .Setter(m => "Instance" + m.Name);

            Assert.That(s, Is.EqualTo("Instance"));
            Assert.That(p.FirstName, Is.EqualTo("InstanceFirstName"));
        }
    }
}
