namespace ObjectAutoBuilder.Test.Helper
{
    public abstract class AbstractPerson
    {
        public int NonAbstractReadonly
        {
            get { return 0; }
        }
        public int NonAbstractIntId { get; set; }

        public abstract int Readonly { get; }
        public abstract int IntId { get; set; }
        
        public abstract string FirstName { get; set; }

        public abstract TR GenericMethod<T, TR>(string arg1, T arg2);

        public abstract string BasicMethod(int arg1);

        public string NonAbstractBasicMethod(int arg1)
        {
            return null;
        }

        public abstract int this[string arg1, int arg2] { get; set; }
    }
}