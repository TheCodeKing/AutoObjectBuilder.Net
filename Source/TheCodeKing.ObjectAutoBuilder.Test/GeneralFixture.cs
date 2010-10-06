using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;

namespace ObjectAutoBuilder.Test
{
    [TestFixture]
    public class GeneralFixture : TestFixtureBase
    {
        public void T1()
        {
            Auto.Configure.Empty().Default();

            string s1 = Auto.Make<string>();

            Assert.That(s1, Is.Empty);

            Auto.Configure.UseDefaultConfiguration();

            string s2 = Auto.Make<string>();

            Assert.That(s2, Is.EqualTo("string"));
        }

        public void T2()
        {
            Auto.Configure.Factory("Configure");

            string s1 = Auto.Make<string>();

            Assert.That(s1, Is.EqualTo("Configure"));

            Auto.Configure.UseDefaultConfiguration();

            string s2 = Auto.Make<string>();

            Assert.That(s2, Is.EqualTo("string"));
        }

        public void T3()
        {
            string s1 = Auto.Make<string>()
                .Factory("Instance");

            Assert.That(s1, Is.EqualTo("Instance"));

            Auto.Configure.UseDefaultConfiguration();

            string s2 = Auto.Make<string>();

            Assert.That(s2, Is.EqualTo("string"));
        }

        public void T4()
        {
            Auto.Configure.Factory("Instance");

            string s1 = Auto.Make<string>();

            Assert.That(s1, Is.EqualTo("Instance"));

            Auto.Configure.UseDefaultConfiguration();

            string s2 = Auto.Make<string>().Min();

            Assert.That(s2, Is.EqualTo("string"));
        }

        public void T5()
        {
            Auto.Configure.Factory("Instance");

            string s1 = Auto.Make<string>();

            Assert.That(s1, Is.EqualTo("Instance"));

            Auto.Configure.UseDefaultConfiguration();

            string s2 = Auto.Make<string>().Min();

            Assert.That(s2, Is.EqualTo("string"));
        }
    }
}
