using TheCodeKing.AutoBuilder.Test.Base;
using TheCodeKing.AutoBuilder.Test.Helper;
using NUnit.Framework;

namespace TheCodeKing.AutoBuilder.Test
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
        public void T10()
        {
            Person person = Auto.Make<Person>()
                .Empty().Max();

            Assert.That(person.LastName, Is.Empty);
            Assert.That(person.FirstName, Is.Empty);
        }

        [Test]
        public void T11()
        {
            Person person = Auto.Make<Person>()
                .Empty().Min();

            Assert.That(person.LastName, Is.Empty);
            Assert.That(person.FirstName, Is.Empty);
        }

        [Test]
        public void T2()
        {
            string p = Auto.Make<string>()
                .Factory("Test")
                .Setter(m => m.Name);

            Assert.That(p, Is.EqualTo("Test"));
        }

        [Test]
        public void T3()
        {
            Auto.Configure
                .Factory("Test")
                .Setter(m => "Test"+m.Name);

            Person p = Auto.Make<Person>();

            Assert.That(p.FirstName, Is.EqualTo("TestFirstName"));
        }

        [Test]
        public void T4()
        {
            Auto.Configure
                .Factory("Config")
                .Setter(m => "Config"+m.Name);

            string p = Auto.Make<string>()
                .Factory("Instance")
                .Setter(m => "Instance" + m.Name);

            Assert.That(p, Is.EqualTo("Instance"));
        }

        [Test]
        public void T5()
        {
            Auto.Configure
                .Factory("Config")
                .Setter(m => "Config" + m.Name);

            Person p = Auto.Make<Person>()
                .Factory("Instance")
                .Setter(m => "Instance" + m.Name);

            string s = Auto.Make<string>()
                .Factory("Instance")
                .Setter(m => "Instance" + m.Name);

            Assert.That(s, Is.EqualTo("Instance"));
            Assert.That(p.FirstName, Is.EqualTo("InstanceFirstName"));
        }

        [Test]
        public void T6()
        {
            Auto.Configure.Empty();

            string s = Auto.Make<string>();

            Assert.That(s, Is.EqualTo(""));
        }


        [Test]
        public void T7()
        {
            string s = Auto.Make<string>()
                .Empty();

            Assert.That(s, Is.EqualTo(""));
        }

        [Test]
        public void T8()
        {
            Person person = Auto.Make<Person>()
                .Empty();

            Assert.That(person.LastName, Is.Empty);
            Assert.That(person.FirstName, Is.Empty);
        }

        [Test]
        public void T9()
        {
            Person person = Auto.Make<Person>()
                .Empty().Default();

            Assert.That(person.LastName, Is.Empty);
            Assert.That(person.FirstName, Is.Empty);
        }
    }
}
