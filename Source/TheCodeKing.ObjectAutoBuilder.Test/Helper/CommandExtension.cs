using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoObjectBuilder.Config;
using AutoObjectBuilder.Interfaces;

namespace ObjectAutoBuilder.Test.Helper
{
    public static class CommandExtension
    {
         public static DefaultAutoConfiguration SetTestPerson(this DefaultAutoConfiguration autoExpression)
         {
             int id = 0;
             autoExpression.With<Person>(() => new Person {IntId = id++, FirstName="FirstName"+id, LastName="LastName"+id});

            return autoExpression;
        }
    }
}
