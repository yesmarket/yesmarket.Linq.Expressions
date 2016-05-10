namespace yesmarket.Linq.Expressions
{
    internal interface IValueComparer<in T>
    {
        bool Compare(T x, T y);
    }
}