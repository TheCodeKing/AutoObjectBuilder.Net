namespace TheCodeKing.AutoBuilder.Test.Helper
{
   public class ImpossiblePerson : Person
   {
       public readonly bool IsContructed; 

       public ImpossiblePerson(ImpossiblePerson person)
       {
           IsContructed = true;
       }
    }
}