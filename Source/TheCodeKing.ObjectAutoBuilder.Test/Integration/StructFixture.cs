using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Drawing;
using AutoObjectBuilder;

namespace ObjectAutoBuilder.Test.Integration
{
    [TestFixture]
    public class StructFixture
    {
        [Test]
        public void Should_make_struct_type()
        {
            // fix for issue with stackoverflow, recursive constructor lookup
            // prefer value types in constructors
            Rectangle rect = Auto.Make<Rectangle>();
            Assert.AreEqual(int.MaxValue, rect.Height); 
        } 
    }
}
