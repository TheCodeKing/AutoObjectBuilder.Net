namespace ObjectAutoBuilder.Test.Helper
{
    public class PrivatePerson : Person
    {
        private PrivatePerson(Person mother, Person father, int id)
        {
            IntId = id;
            Mother = mother;
            Father = father;
        }
    }
}