using System.Collections.Generic;
using System.Linq.Expressions;

namespace yesmarket.Linq.Expressions
{
    internal sealed class ExpressionValueComparer : ExpressionVisitor, IValueComparer<Expression>
    {
        private Queue<Expression> _tracked;
        private Expression _current;

        private bool _eq = true;

        public bool Compare(Expression x, Expression y)
        {
            IExpressionCollection expressionCollection = new ExpressionCollection(y);
            expressionCollection.Fill();

            _tracked = new Queue<Expression>(expressionCollection);

            Visit(x);

            return _eq;
        }

        public override Expression Visit(Expression node)
        {
            if (!_eq)
            {
                return node;
            }

            if (node == null || _tracked.Count == 0)
            {
                _eq = false;
                return node;
            }

            var peeked = _tracked.Peek();

            if (peeked == null || peeked.NodeType != node.NodeType || peeked.Type != node.Type)
            {
                _eq = false;
                return node;
            }

            _current = _tracked.Dequeue();

            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var other = (BinaryExpression)_current;
            var eq =
                node.IsEqualTo(other, _ => _.Method) &&
                node.IsEqualTo(other, _ => _.IsLifted) &&
                node.IsEqualTo(other, _ => _.IsLiftedToNull);
            return eq.IfTrue(() => base.VisitBinary(node));
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            var other = (ConstantExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Value);
            return eq.IfTrue(() => base.VisitConstant(node));
        }

        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            var other = (DebugInfoExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.IsClear, _ => _.EndColumn, _ => _.EndLine, _ => _.StartLine, _ => _.StartColumn);
            return eq.IfTrue(() => base.VisitDebugInfo(node));
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            var other = (DynamicExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.DelegateType, _ => _.Binder);
            return eq.IfTrue(() => base.VisitDynamic(node));
        }

        protected override Expression VisitGoto(GotoExpression node)
        {
            var other = (GotoExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Kind, _ => _.Target);
            return eq.IfTrue(() => base.VisitGoto(node));
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            var other = (IndexExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Indexer);
            return eq.IfTrue(() => base.VisitIndex(node));
        }

        protected override Expression VisitLabel(LabelExpression node)
        {
            var other = (LabelExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Target);
            return eq.IfTrue(() => base.VisitLabel(node));
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var other = (LambdaExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Name, _ => _.TailCall);
            return eq.IfTrue(() => base.VisitLambda(node));
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            var other = (ListInitExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Initializers);
            return eq.IfTrue(() => base.VisitListInit(node));
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            var other = (LoopExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.BreakLabel, _ => _.ContinueLabel);
            return eq.IfTrue(() => base.VisitLoop(node));
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var other = (MemberExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Member);
            return eq.IfTrue(() => base.VisitMember(node));
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            var other = (MemberInitExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Bindings);
            return eq.IfTrue(() => base.VisitMemberInit(node));
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var other = (MethodCallExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Method);
            return eq.IfTrue(() => base.VisitMethodCall(node));
        }

        protected override Expression VisitNew(NewExpression node)
        {
            var other = (NewExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Constructor, _ => _.Members);
            return eq.IfTrue(() => base.VisitNew(node));
        }

        protected override Expression VisitSwitch(SwitchExpression node)
        {
            var other = (SwitchExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Comparison);
            return eq.IfTrue(() => base.VisitSwitch(node));
        }

        protected override Expression VisitTry(TryExpression node)
        {
            var other = (TryExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Handlers);
            return eq.IfTrue(() => base.VisitTry(node));
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            var other = (TypeBinaryExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.TypeOperand);
            return eq.IfTrue(() => base.VisitTypeBinary(node));
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var other = (UnaryExpression)_current;
            var eq = node.IsEqualTo(other, _ => _.Method, _ => _.IsLifted, _ => _.IsLiftedToNull);
            return eq.IfTrue(() => base.VisitUnary(node));
        }
    }
}