using TheCodeKing.AutoBuilder.Test.Base;
using TheCodeKing.AutoBuilder.Test.Helper;
using NUnit.Framework;

namespace TheCodeKing.AutoBuilder.Test
{
    [TestFixture]
    public class ObjectBuilderUseByValFixture : TestFixtureBase
    {
        private int Compare
        {
            get { return 1354; }
        }

        [Test]
        public void T1()
        {
            Auto.Configure.Factory(Compare);

            int val = Auto.Make<int>();

            Assert.That(val, Is.EqualTo(Compare));
        }

        [Test]
        public void T2()
        {
            int val = Auto.Make<int>().Factory(Compare);

            Assert.That(val, Is.EqualTo(Compare));
        }

        [Test]
        public void T3()
        {
            Auto.Configure.Factory(-1);

            int val = Auto.Make<int>().Factory(Compare);

            Auto.Configure.Factory(-1);

            Assert.That(val, Is.EqualTo(Compare));
        }

        [Test]
        public void T4()
        {
            int val = Auto.Make<int>().Factory(-1).Factory(Compare);

            Assert.That(val, Is.EqualTo(Compare));
        }

        [Test]
        public void T5()
        {
            int val = Auto.Make<int>().Factory(Compare).Factory(-1);

            Assert.That(val, Is.EqualTo(-1));
        }

        [Test]
        public void T6()
        {
            int i = 100;

            Auto.Configure.Factory(() => i++);
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

            Auto.Configure.Factory(() => i++);
            int val1 = Auto.Make<int>();
            int val2 = Auto.Make<int>();

            Assert.That(val1, Is.EqualTo(100));
            Assert.That(val2, Is.EqualTo(101));
        }

        [Test]
        public void T8()
        {
            Person person = Auto.Make<Person>()
                .Factory(new Person()).Factory(Compare);

            Assert.That(person.IntId, Is.EqualTo(0));
        }

        [Test]
        public void T9()
        {
            Person person = Auto.Make<Person>()
                .Factory(Compare);

            Assert.That(person.IntId, Is.EqualTo(Compare));
        }
    }
}