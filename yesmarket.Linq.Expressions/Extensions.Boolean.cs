using System;
using System.Linq.Expressions;

namespace yesmarket.Linq.Expressions
{
    internal static class BooleanExtensions
    {
        public static Expression IfTrue(this bool value, Func<Expression> func)
        {
            if (value)
            {
                return func.Invoke();
            }
            return null;
            //return value ? func.Invoke() : null;
        }
    }
}