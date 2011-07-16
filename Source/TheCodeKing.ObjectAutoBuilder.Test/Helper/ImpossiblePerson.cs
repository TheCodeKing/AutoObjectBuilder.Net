namespace ObjectAutoBuilder.Test.Helper
{
    public class ImpossiblePerson : Person
    {
        private readonly bool isConstructed;
        private readonly ImpossiblePerson person;

        public ImpossiblePerson(ImpossiblePerson person)
        {
            this.person = person;
            isConstructed = true;
        }

        public bool IsContructed
        {
            get { return isConstructed; }
        }
    }
}