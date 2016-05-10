using System.Collections.Generic;

namespace yesmarket.Linq.Expressions.Tests.Helpers
{
    public class Order
    {
        public int Number { get; set; }
        public Customer Customer { get; set; }

        public IEnumerable<OrderLineItem> LineItems { get; set; }
    }
}