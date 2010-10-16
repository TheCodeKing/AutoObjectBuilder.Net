using System.Reflection;
using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test
{
    [TestFixture]
    public class InterfacePersonFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            IPerson person = Auto.Make<IPerson>().Object;
            var i = person["string", 1];

            Assert.That(person, Is.Not.Null);
            Assert.That(i, Is.EqualTo(int.MaxValue));
        }
    }
}
