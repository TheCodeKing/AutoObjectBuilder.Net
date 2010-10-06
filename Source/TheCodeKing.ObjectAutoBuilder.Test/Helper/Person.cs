using System;
using System.Collections.Generic;

namespace ObjectAutoBuilder.Test.Helper
{
    public enum Title
    {
        Mr=0,
        Mrs=1,
        Dr=2,
        Sir=3
    }

    public enum ATitle
    {
        AMr = 0,
        AMrs = 1,
        ADr = 2,
        ASir = 3
    }

    public class Person : IEquatable<Person>
    {
        public readonly static Person PersonTest1;
        public List<Person> Collection = new List<Person>();
        public Person Mother;
        public List<int> Numbers = new List<int>();

        static Person()
        {
            PersonTest1 = new Person
                              {
                                  TitleEnum = Title.Sir,
                                  FirstName = "FirstName",
                                  LastName = "LastName",
                                  Father = PersonTest1,
                                  Mother = PersonTest1
                              };
        }

        public int Readonly { get { return -1; }}
        public int IntId { get; set; }
        public uint UIntId { get; set; }
        public long LongId { get; set; }
        public ulong ULongId { get; set; }
        public short ShortId { get; set; }
        public ushort UShortId { get; set; }
        public double DoulbeId { get; set; }
        public byte ByteId { get; set; }
        public char CharId { get; set; }
        public sbyte SByteId { get; set; }
        public Char CharRef { get; set; }
        public Byte ByteRef { get; set; }
        public SByte SByteRef { get; set; } 
        public Int16 Int16Id { get; set; }
        public UInt16 UInt16Id { get; set; }
        public Int32 Int32Id { get; set; }
        public UInt32 UInt32Id { get; set; }
        public Int64 Int64Id { get; set; }
        public UInt64 UInt64Id { get; set; }
        public DateTime DateTime { get; set; }

        public Title TitleEnum { get; set; }
        public ATitle ATitleEnum { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private Person Hidden { get; set; }
        public Person Father { get; set; }

        #region IEquatable<Person> Members

        public bool Equals(Person other)
        {
            if (other==null)
            {
                return false;
            }
            return Readonly == other.Readonly
                   && IntId == other.IntId
                   && UIntId == other.UIntId
                   && LongId == other.LongId
                   && ULongId == other.ULongId
                   && ShortId == other.ShortId
                   && UShortId == other.UShortId
                   && DoulbeId == other.DoulbeId
                   && ByteId == other.ByteId
                   && CharId == other.CharId
                   && SByteId == other.SByteId
                   && CharRef == other.CharRef
                   && ByteRef == other.ByteRef
                   && SByteRef == other.SByteRef
                   && Int16Id == other.Int16Id
                   && UInt16Id == other.UInt16Id
                   && Int32Id == other.Int32Id
                   && UInt32Id == other.UInt32Id
                   && Int64Id == other.Int64Id
                   && UInt64Id == other.UInt64Id
                   && DateTime == other.DateTime
                   && TitleEnum == other.TitleEnum
                   && ATitleEnum == other.ATitleEnum
                   && FirstName == other.FirstName
                   && LastName == other.LastName
                   && Hidden == null ? other.Hidden == null : Hidden.Equals(other.Hidden)
                                                              && Mother == null ? other.Mother == null : Mother.Equals(other.Mother)
                                                                                                         && Father == null ? other.Father == null : Father.Equals(other.Father);

        }

        #endregion

        public override int GetHashCode()
        {
            var hash = base.GetHashCode();
            hash ^= Readonly.GetHashCode();
            hash ^= IntId.GetHashCode();
            hash ^= UIntId.GetHashCode();
            hash ^= LongId.GetHashCode();
            hash ^= ULongId.GetHashCode();
            hash ^= ShortId.GetHashCode();
            hash ^= UShortId.GetHashCode();
            hash ^= DoulbeId.GetHashCode();
            hash ^= ByteId.GetHashCode();
            hash ^= CharId.GetHashCode();
            hash ^= SByteId.GetHashCode();
            hash ^= CharRef.GetHashCode();
            hash ^= ByteRef.GetHashCode();
            hash ^= SByteRef.GetHashCode();
            hash ^= Int16Id.GetHashCode();
            hash ^= UInt16Id.GetHashCode();
            hash ^= Int32Id.GetHashCode();
            hash ^= UInt32Id.GetHashCode();
            hash ^= Int64Id.GetHashCode();
            hash ^= UInt64Id.GetHashCode();
            hash ^= DateTime.GetHashCode();
            hash ^= TitleEnum.GetHashCode();
            hash ^= ATitleEnum.GetHashCode();
            hash ^= FirstName.GetHashCode();
            hash ^= LastName.GetHashCode();

            if (Father!=null)
            {
                hash ^= Father.GetHashCode();
            }
            if (Mother != null)
            {
                hash ^= Mother.GetHashCode();
            }
            if (Hidden != null)
            {
                hash ^= Hidden.GetHashCode();
            }
            return hash;
        }
        
        public override bool Equals(object obj)
        {
            return Equals(obj as Person);
        }
    }
}