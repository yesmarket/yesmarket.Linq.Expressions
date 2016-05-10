namespace yesmarket.Linq.Expressions.Support
{
    internal interface IHashCodeResolver<in T>
    {
        int GetHashCodeFor(T obj);
    }
}