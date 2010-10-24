using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class EnumerableFixture
    {
        [Test]
        public void T1()
        {
            PersonCollection collection = Auto.Make<PersonCollection>();

            Assert.That(collection, Is.Not.Null);
            Assert.That(collection.Count, Is.EqualTo(10));
        }
    }
}
