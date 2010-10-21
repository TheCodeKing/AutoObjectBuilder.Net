namespace ObjectAutoBuilder.Test.Helper
{
   public class ImpossiblePerson : Person
   {
       private readonly ImpossiblePerson person;
       private readonly bool isConstructed;

       public bool IsContructed
       {
            get { return isConstructed; }
       }

       public ImpossiblePerson(ImpossiblePerson person)
       {
           this.person = person;
           isConstructed = true;
       }
   }
}