using System;

namespace ObjectAutoBuilder.Test.Helper
{
    public interface IPerson
    {
        int Readonly { get; }
        int IntId { get; set; }
        uint UIntId { get; set; }
        long LongId { get; set; }
        ulong ULongId { get; set; }
        short ShortId { get; set; }
        ushort UShortId { get; set; }
        double DoulbeId { get; set; }
        byte ByteId { get; set; }
        char CharId { get; set; }
        sbyte SByteId { get; set; }
        Char CharRef { get; set; }
        Byte ByteRef { get; set; }
        SByte SByteRef { get; set; }
        Int16 Int16Id { get; set; }
        UInt16 UInt16Id { get; set; }
        Int32 Int32Id { get; set; }
        UInt32 UInt32Id { get; set; }
        Int64 Int64Id { get; set; }
        UInt64 UInt64Id { get; set; }
        DateTime DateTime { get; set; }
        Title TitleEnum { get; set; }
        ATitle ATitleEnum { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        Person Father { get; set; }

        TR GenericMethod<T, TR>(string arg1, T arg2);

        string BasicMethod(int arg1);

        int this[string arg1, int arg2] { get; set; }
    }
}