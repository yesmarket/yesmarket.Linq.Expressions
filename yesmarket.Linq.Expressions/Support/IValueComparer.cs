namespace yesmarket.Linq.Expressions.Support
{
    internal interface IValueComparer<in T>
    {
        bool Compare(T x, T y);
    }
}