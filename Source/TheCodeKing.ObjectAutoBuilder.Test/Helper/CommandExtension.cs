using AutoObjectBuilder.Config;

namespace ObjectAutoBuilder.Test.Helper
{
    public static class CommandExtension
    {
        public static DefaultAutoConfiguration SetTestPerson(this DefaultAutoConfiguration autoExpression)
        {
            int id = 0;
            autoExpression.With(
                () => new Person {IntId = id++, FirstName = "FirstName" + id, LastName = "LastName" + id});

            return autoExpression;
        }
    }
}