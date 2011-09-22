using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoObjectBuilder;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class RecursiveConstructorFixture
    {
        [Test]
        public void Should_bypass_constructor_if_constructor_recursions_create_of_type()
        {
            RecursiveItemA item = Auto.Make<RecursiveItemA>();
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.RecursiveItemB);
            Assert.IsNotNull(item.RecursiveItemB.RecursiveItemA);
        }
    }
}
