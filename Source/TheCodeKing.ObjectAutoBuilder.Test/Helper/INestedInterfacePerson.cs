namespace ObjectAutoBuilder.Test.Helper
{
    public interface INestedInterfacePerson : IPerson
    {
        string ParentProp { get; }

        TR ParentGenericMethod<T, TR>(string arg1, T arg2);

        new string BasicMethod(int arg1);
    }
}