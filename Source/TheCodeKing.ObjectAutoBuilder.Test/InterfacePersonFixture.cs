using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test
{
    [TestFixture]
    public class InterfacePersonFixture
    {
        [Test]
        public void T1()
        {
            IPerson person = Auto.Make<IPerson>().Object;

            Assert.That(person, Is.Not.Null);

        }
    }
}
