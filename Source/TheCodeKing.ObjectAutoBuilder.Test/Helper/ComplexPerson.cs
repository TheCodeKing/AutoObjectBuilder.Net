namespace ObjectAutoBuilder.Test.Helper
{
    public class ComplexPerson : Person
    {
        public ComplexPerson(Person mother, Person father, int id)
        {
            IntId = id;
            Mother = mother;
            Father = father;
        }

        public ConflictPerson ConflictPerson { get; set; }
    }
}