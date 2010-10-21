using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class ObjectBuilderUseByValFixture : TestFixtureBase
    {
        private static int Compare
        {
            get { return 1354; }
        }

        [Test]
        public void T1()
        {
            Auto.Configure.With(Compare);

            int val = Auto.Make<int>();

            Assert.That(val, Is.EqualTo(Compare));
        }

        [Test]
        public void T2()
        {
            int val = Auto.Make<int>().With(Compare);

            Assert.That(val, Is.EqualTo(Compare));
        }

        [Test]
        public void T3()
        {
            Auto.Configure.With(-1);

            int val = Auto.Make<int>().With(Compare);

            Auto.Configure.With(-1);

            Assert.That(val, Is.EqualTo(Compare));
        }

        [Test]
        public void T4()
        {
            int val = Auto.Make<int>().With(-1).With(Compare);

            Assert.That(val, Is.EqualTo(Compare));
        }

        [Test]
        public void T5()
        {
            int val = Auto.Make<int>().With(Compare).With(-1);

            Assert.That(val, Is.EqualTo(-1));
        }

        [Test]
        public void T6()
        {
            int i = 100;

            Auto.Configure.With(() => i++);
            int val1 = Auto.Make<int>(); 
            int val2 = Auto.Make<int>();
            int val3 = Auto.Make<int>();

            Assert.That(val1, Is.EqualTo(100));
            Assert.That(val2, Is.EqualTo(101));
            Assert.That(val3, Is.EqualTo(102));
        }

        [Test]
        public void T7()
        {
            int i = 100;

            Auto.Configure.With(() => i++);
            int val1 = Auto.Make<int>();
            int val2 = Auto.Make<int>();

            Assert.That(val1, Is.EqualTo(100));
            Assert.That(val2, Is.EqualTo(101));
        }

        [Test]
        public void T8()
        {
            Person person = Auto.Make<Person>()
                .With(new Person()).With(Compare);

            Assert.That(person.IntId, Is.EqualTo(0));
        }

        [Test]
        public void T9()
        {
            Person person = Auto.Make<Person>()
                .With(Compare);

            Assert.That(person.IntId, Is.EqualTo(Compare));
        }
    }
}