using System;
using System.Linq;
using System.Linq.Expressions;
using Shouldly;
using Xunit;
using yesmarket.Linq.Expressions.Tests.Helpers;

namespace yesmarket.Linq.Expressions.Tests
{
    /// <summary>
    /// Some very basic integration tests
    /// TODO; write some more thorough unit tests...
    /// </summary>
    public class ExpressionEqualityComparerTests
    {
        private readonly ExpressionEqualityComparer _sut;

        public ExpressionEqualityComparerTests()
        {
            _sut = new ExpressionEqualityComparer();
        }

        [Fact]
        public void Equals_Same1_AreEqual()
        {
            Expression<Func<Order, object>> x = order => order.Customer.Address;
            Expression<Func<Order, object>> y = order => order.Customer.Address;
            var e = _sut.Equals(x, y);
            e.ShouldBeTrue();
        }

        [Fact]
        public void Equals_Same2_AreEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Number == 5;
            Expression<Func<Order, bool>> y = order => order.Number == 5;
            var e = _sut.Equals(x, y);
            e.ShouldBeTrue();
        }

        [Fact]
        public void Equals_Same3_AreEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Customer == new Customer { Name = "john" };
            Expression<Func<Order, bool>> y = order => order.Customer == new Customer { Name = "john" };
            var e = _sut.Equals(x, y);
            e.ShouldBeFalse();
        }

        [Fact]
        public void Equals_Different1_AreNotEqual()
        {
            Expression<Func<Order, object>> x = order => order.Number;
            Expression<Func<Order, object>> y = order => order.LineItems;
            var e = _sut.Equals(x, y);
            e.ShouldBeFalse();
        }

        [Fact]
        public void Equals_Different2_AreNotEqual()
        {
            Expression<Func<Order, object>> x = order => order.Customer.Address;
            Expression<Func<Order, object>> y = order => order.LineItems.Select(item => item.Product);
            var e = _sut.Equals(x, y);
            e.ShouldBeFalse();
        }

        [Fact]
        public void Equals_Different3_AreNotEqual()
        {
            Expression<Func<Order, object>> x = order => order.Customer.Address;
            Expression<Func<Customer, object>> y = customer => customer.Address;
            var e = _sut.Equals(x, y);
            e.ShouldBeFalse();
        }

        [Fact]
        public void Equals_Different4_AreNotEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Number != 5;
            Expression<Func<Order, bool>> y = order => order.Number == 5;
            var e = _sut.Equals(x, y);
            e.ShouldBeFalse();
        }

        [Fact]
        public void Equals_Different5_AreNotEqual()
        {
            Expression<Func<Customer, bool>> x = customer => customer.Name == "john";
            Expression<Func<Customer, bool>> y = customer => customer.Name == "paul";
            var e = _sut.Equals(x, y);
            e.ShouldBeFalse();
        }

        [Fact]
        public void Equals_Different6_AreNotEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Customer == new Customer {Name = "john"};
            Expression<Func<Order, bool>> y = order => order.Customer == new Customer {Name = "paul"};
            var e = _sut.Equals(x, y);
            e.ShouldBeFalse();
        }

        [Fact]
        public void Equals_Different7_AreNotEqual()
        {
            var a = new Customer {Name = "john"};
            var b = new Customer {Name = "paul"};
            Expression<Func<Order, bool>> x = order => order.Customer == a;
            Expression<Func<Order, bool>> y = order => order.Customer == b;
            var e = _sut.Equals(x, y);
            e.ShouldBeFalse();
        }

        [Fact]
        public void Equals_Different8_AreNotEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Number < 5;
            Expression<Func<Order, bool>> y = order => order.Number > 5;
            var e = _sut.Equals(x, y);
            e.ShouldBeFalse();
        }

        [Fact]
        public void Equals_Different9_AreNotEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Customer.Address.Postcode == 5;
            Expression<Func<Order, bool>> y = order => order.Number > 5;
            var e = _sut.Equals(x, y);
            e.ShouldBeFalse();
        }

        [Fact]
        public void GetHashCode_Same1_AreEqual()
        {
            Expression<Func<Order, object>> x = order => order.Customer.Address;
            Expression<Func<Order, object>> y = order => order.Customer.Address;
            Assert.Equal(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }

        [Fact]
        public void GetHashCode_Same2_AreEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Number == 5;
            Expression<Func<Order, bool>> y = order => order.Number == 5;
            Assert.Equal(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }

        [Fact]
        public void GetHashCode_Same3_AreEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Customer == new Customer { Name = "john" };
            Expression<Func<Order, bool>> y = order => order.Customer == new Customer { Name = "john" };
            Assert.Equal(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }

        [Fact]
        public void GetHashCode_Different1_AreNotEqual()
        {
            Expression<Func<Order, object>> x = order => order.Number;
            Expression<Func<Order, object>> y = order => order.LineItems;
            Assert.NotEqual(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }

        [Fact]
        public void GetHashCode_Different2_AreNotEqual()
        {
            Expression<Func<Order, object>> x = order => order.Customer.Address;
            Expression<Func<Order, object>> y = order => order.LineItems.Select(item => item.Product);
            Assert.NotEqual(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }

        [Fact]
        public void GetHashCode_Different3_AreNotEqual()
        {
            Expression<Func<Order, object>> x = order => order.Customer.Address;
            Expression<Func<Customer, object>> y = customer => customer.Address;
            Assert.NotEqual(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }

        [Fact]
        public void GetHashCode_Different4_AreNotEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Number != 5;
            Expression<Func<Order, bool>> y = order => order.Number == 5;
            Assert.NotEqual(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }

        [Fact]
        public void GetHashCode_Different5_AreNotEqual()
        {
            Expression<Func<Customer, bool>> x = customer => customer.Name == "john";
            Expression<Func<Customer, bool>> y = customer => customer.Name == "paul";
            Assert.NotEqual(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }

        [Fact]
        public void GetHashCode_Different6_AreNotEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Customer == new Customer { Name = "john" };
            Expression<Func<Order, bool>> y = order => order.Customer == new Customer { Name = "paul" };
            Assert.NotEqual(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }

        [Fact]
        public void GetHashCode_Different7_AreNotEqual()
        {
            var a = new Customer { Name = "john" };
            var b = new Customer { Name = "paul" };
            Expression<Func<Order, bool>> x = order => order.Customer == a;
            Expression<Func<Order, bool>> y = order => order.Customer == b;
            Assert.NotEqual(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }

        [Fact]
        public void GetHashCode_Different8_AreNotEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Number < 5;
            Expression<Func<Order, bool>> y = order => order.Number > 5;
            Assert.NotEqual(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }

        [Fact]
        public void GetHashCode_Different9_AreNotEqual()
        {
            Expression<Func<Order, bool>> x = order => order.Customer.Address.Postcode == 5;
            Expression<Func<Order, bool>> y = order => order.Number > 5;
            Assert.NotEqual(_sut.GetHashCode(x), _sut.GetHashCode(y));
        }
    }
}
