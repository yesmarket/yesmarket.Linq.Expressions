using System.Collections.Generic;
using System.Linq.Expressions;

namespace yesmarket.Linq.Expressions
{
    internal interface IExpressionCollection : IEnumerable<Expression>
    {
        void Fill();
    }
}