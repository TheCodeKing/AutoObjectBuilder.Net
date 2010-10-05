using System;
using TheCodeKing.AutoBuilder.Test.Helper;
using NUnit.Framework;

namespace TheCodeKing.AutoBuilder.Test.Base
{
    public abstract class TestFixtureBase
    {
        [SetUp]
        public void SetUp()
        {
            Auto.Configure.UseDefaultConfiguration();
        }

        protected void AssertDefault(Person p)
        {
            Assert.That(p, Is.Not.Null);
            Assert.That(p.FirstName, Is.EqualTo("FirstName"));
            Assert.That(p.LastName, Is.EqualTo("LastName"));
            Assert.That(p.Readonly, Is.EqualTo(-1));
        }

        protected void AssertDefaultValues(Person p)
        {
            Assert.That(p.TitleEnum, Is.EqualTo(Title.Mr));
            Assert.That(p.IntId, Is.EqualTo(default(int)));
            Assert.That(p.UIntId, Is.EqualTo(default(uint)));
            Assert.That(p.LongId, Is.EqualTo(default(long)));
            Assert.That(p.ULongId, Is.EqualTo(default(ulong)));
            Assert.That(p.ShortId, Is.EqualTo(default(short)));
            Assert.That(p.UShortId, Is.EqualTo(default(ushort)));
            Assert.That(p.DoulbeId, Is.EqualTo(default(double)));
            Assert.That(p.ByteId, Is.EqualTo(default(byte)));
            Assert.That(p.CharId, Is.EqualTo(default(char)));
            Assert.That(p.SByteId, Is.EqualTo(default(sbyte)));
            Assert.That(p.CharRef, Is.EqualTo(default(Char)));
            Assert.That(p.ByteRef, Is.EqualTo(default(Byte)));
            Assert.That(p.SByteRef, Is.EqualTo(default(SByte)));
            Assert.That(p.Int16Id, Is.EqualTo(default(Int16)));
            Assert.That(p.UInt16Id, Is.EqualTo(default(UInt16)));
            Assert.That(p.Int32Id, Is.EqualTo(default(Int32)));
            Assert.That(p.UInt32Id, Is.EqualTo(default(UInt32)));
            Assert.That(p.Int64Id, Is.EqualTo(default(Int64)));
            Assert.That(p.UInt64Id, Is.EqualTo(default(UInt64)));
            Assert.That(p.DateTime, Is.EqualTo(default(DateTime)));
        }

        protected void AssertMinValues(Person p)
        {
            Assert.That(p.TitleEnum, Is.EqualTo(Title.Mr));
            Assert.That(p.IntId, Is.EqualTo(int.MinValue));
            Assert.That(p.UIntId, Is.EqualTo(uint.MinValue));
            Assert.That(p.LongId, Is.EqualTo(long.MinValue));
            Assert.That(p.ULongId, Is.EqualTo(ulong.MinValue));
            Assert.That(p.ShortId, Is.EqualTo(short.MinValue));
            Assert.That(p.UShortId, Is.EqualTo(ushort.MinValue));
            Assert.That(p.DoulbeId, Is.EqualTo(double.MinValue));
            Assert.That(p.ByteId, Is.EqualTo(byte.MinValue));
            Assert.That(p.CharId, Is.EqualTo(char.MinValue));
            Assert.That(p.SByteId, Is.EqualTo(sbyte.MinValue));
            Assert.That(p.CharRef, Is.EqualTo(Char.MinValue));
            Assert.That(p.ByteRef, Is.EqualTo(Byte.MinValue));
            Assert.That(p.SByteRef, Is.EqualTo(SByte.MinValue));
            Assert.That(p.Int16Id, Is.EqualTo(Int16.MinValue));
            Assert.That(p.UInt16Id, Is.EqualTo(UInt16.MinValue));
            Assert.That(p.Int32Id, Is.EqualTo(Int32.MinValue));
            Assert.That(p.UInt32Id, Is.EqualTo(UInt32.MinValue));
            Assert.That(p.Int64Id, Is.EqualTo(Int64.MinValue));
            Assert.That(p.UInt64Id, Is.EqualTo(UInt64.MinValue));
            Assert.That(p.DateTime, Is.EqualTo(DateTime.MinValue));
        }

        protected void AssertMaxValues(Person p)
        {
            Assert.That(p.TitleEnum, Is.EqualTo(Title.Sir));
            Assert.That(p.IntId, Is.EqualTo(int.MaxValue));
            Assert.That(p.UIntId, Is.EqualTo(uint.MaxValue));
            Assert.That(p.LongId, Is.EqualTo(long.MaxValue));
            Assert.That(p.ULongId, Is.EqualTo(ulong.MaxValue));
            Assert.That(p.ShortId, Is.EqualTo(short.MaxValue));
            Assert.That(p.UShortId, Is.EqualTo(ushort.MaxValue));
            Assert.That(p.DoulbeId, Is.EqualTo(double.MaxValue));
            Assert.That(p.ByteId, Is.EqualTo(byte.MaxValue));
            Assert.That(p.CharId, Is.EqualTo(char.MaxValue));
            Assert.That(p.SByteId, Is.EqualTo(sbyte.MaxValue));
            Assert.That(p.CharRef, Is.EqualTo(Char.MaxValue));
            Assert.That(p.ByteRef, Is.EqualTo(Byte.MaxValue));
            Assert.That(p.SByteRef, Is.EqualTo(SByte.MaxValue));
            Assert.That(p.Int16Id, Is.EqualTo(Int16.MaxValue));
            Assert.That(p.UInt16Id, Is.EqualTo(UInt16.MaxValue));
            Assert.That(p.Int32Id, Is.EqualTo(Int32.MaxValue));
            Assert.That(p.UInt32Id, Is.EqualTo(UInt32.MaxValue));
            Assert.That(p.Int64Id, Is.EqualTo(Int64.MaxValue));
            Assert.That(p.UInt64Id, Is.EqualTo(UInt64.MaxValue));
            Assert.That(p.DateTime, Is.EqualTo(DateTime.MaxValue));
        }
    }
}