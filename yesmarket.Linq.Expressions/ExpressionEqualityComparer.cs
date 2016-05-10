using System.Collections.Generic;
using System.Linq.Expressions;
using yesmarket.Linq.Expressions.Support;

namespace yesmarket.Linq.Expressions
{
    public class ExpressionEqualityComparer : IEqualityComparer<Expression>
    {
        public bool Equals(Expression x, Expression y)
        {
            return new ExpressionValueComparer().Compare(x, y);
        }

        public int GetHashCode(Expression obj)
        {
            return new ExpressionHashCodeResolver().GetHashCodeFor(obj);
        }
    }
}