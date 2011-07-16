using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class InternalPersonFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            InternalPerson p = Auto.Make<InternalPerson>();
            Assert.That(p, Is.Not.Null);
        }
    }
}