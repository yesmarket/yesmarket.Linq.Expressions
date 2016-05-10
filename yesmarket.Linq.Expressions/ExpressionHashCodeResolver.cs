using System.Linq.Expressions;

namespace yesmarket.Linq.Expressions
{
    internal sealed class ExpressionHashCodeResolver : ExpressionVisitor, IHashCodeResolver<Expression>
    {
        private int _runningTotal;

        public int GetHashCodeFor(Expression obj)
        {
            Visit(obj);
            return _runningTotal;
        }

        public override Expression Visit(Expression node)
        {
            if (null == node) return null;
            _runningTotal += node.GetHashCodeFor(node.NodeType, node.Type);
            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Method, node.IsLifted, node.IsLiftedToNull);
            return base.VisitBinary(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Value);
            return base.VisitConstant(node);
        }

        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.IsClear, node.EndColumn, node.EndLine, node.StartLine, node.StartColumn);
            return base.VisitDebugInfo(node);
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.DelegateType, node.Binder);
            return base.VisitDynamic(node);
        }

        protected override Expression VisitGoto(GotoExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Kind, node.Target);
            return base.VisitGoto(node);
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Indexer);
            return base.VisitIndex(node);
        }

        protected override Expression VisitLabel(LabelExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Target);
            return base.VisitLabel(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            _runningTotal += node.GetHashCodeFor(node.Name, node.TailCall);
            return base.VisitLambda(node);
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Initializers);
            return base.VisitListInit(node);
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.BreakLabel, node.ContinueLabel);
            return base.VisitLoop(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Member);
            return base.VisitMember(node);
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Bindings);
            return base.VisitMemberInit(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Method);
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitNew(NewExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Constructor, node.Members);
            return base.VisitNew(node);
        }

        protected override Expression VisitSwitch(SwitchExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Comparison);
            return base.VisitSwitch(node);
        }

        protected override Expression VisitTry(TryExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Handlers);
            return base.VisitTry(node);
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.TypeOperand);
            return base.VisitTypeBinary(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            _runningTotal += node.GetHashCodeFor(node.Method, node.IsLifted, node.IsLiftedToNull);
            return base.VisitUnary(node);
        }
    }
}