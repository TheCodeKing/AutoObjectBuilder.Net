using System.IO;
using AutoObjectBuilder;
using NUnit.Framework;
using ObjectAutoBuilder.Test.Base;
using ObjectAutoBuilder.Test.Helper;

namespace ObjectAutoBuilder.Test.Integration
{
    public class SystemInfoFixture : TestFixtureBase
    {
        [Test]
        public void T1()
        {
            FileInfo info = Auto.Make<FileInfo>();

            Assert.That(info, Is.Not.Null);
            Assert.That(info.FullName, Is.EqualTo(@"C:\filename"));
        }

        [Test]
        public void T2()
        {
            SystemInfoTest info = Auto.Make<SystemInfoTest>();

            Assert.That(info, Is.Not.Null);
            Assert.That(info.FilePath.FullName, Is.EqualTo(@"C:\filepath"));
        }

        [Test]
        public void T3()
        {
            DirectoryInfo info = Auto.Make<DirectoryInfo>();

            Assert.That(info, Is.Not.Null);
            Assert.That(info.FullName, Is.EqualTo(@"C:\directoryname"));
        }

        [Test]
        public void T4()
        {
            SystemInfoTest info = Auto.Make<SystemInfoTest>();

            Assert.That(info, Is.Not.Null);
            Assert.That(info.DirectoryPath.FullName, Is.EqualTo(@"C:\directorypath"));
        }
    }
}