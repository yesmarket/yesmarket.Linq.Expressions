# yesmarket.Linq.Expressions

A custom equality comparer for comparing LINQ expressions.

### Overview

Comparing expressions using <code lang="cs" linenumbers="off">Equals</code> only checks reference equality. Therefore the following test would fail:

    Expression<Func<Order, object>> x = order => order.Customer.Address;
    Expression<Func<Order, object>> y = order => order.Customer.Address;
    Assert.True(x.Equals(y)); // will fail

To solve this problem, this repository introduces an <code lang="cs" linenumbers="off">IEqualityComparer<Expression></code> implementation, <code lang="cs" linenumbers="off">ExpressionEqualityComparer</code>, which can be used to compare seperate expression trees to see whether they are essentially the same.

The following example demostrates the use of this class:

    Expression<Func<Order, bool>> x = order => order.Customer.Name == "Joe Bloggs";
    Expression<Func<Order, bool>> y = order => order.Customer.Name == "Joe Bloggs";
    var comparer = new ExpressionEqualityComparer();
    Assert.True(comparer.Equals(x, y)); // will pass

The implementaion makes heavy use of the [ExpressionVisitor](https://msdn.microsoft.com/en-us/library/system.linq.expressions.expressionvisitor(v=vs.110).aspx) class to determine whether two expression trees are equal. As the nodes in the expression tree are traversed, individual nodes are compared for equality.

### Nuget

This repository has been published to the public nuget.org feed:
[https://www.nuget.org/packages/yesmarket.Linq.Expressions/](https://www.nuget.org/packages/yesmarket.Linq.Expressions/)