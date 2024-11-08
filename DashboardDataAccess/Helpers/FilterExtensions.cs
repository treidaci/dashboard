using System.Linq.Expressions;

namespace DashboardDataAccess.Helpers;

internal static class FilterExtensions
{
    /// <summary>
    /// converts sourceExpression of TSource into sourceExpression of TDestination
    /// limitation is that member names need to match exactly
    /// </summary>
    /// <param name="sourceExpression">filter expression for the source object</param>
    /// <typeparam name="TDestination">usually a Models class</typeparam>
    /// <typeparam name="TSource">source object usually coming from Core entities</typeparam>
    /// <returns>expression filter for Models class that allows EF Core to understand the filter</returns>
    internal static Expression<Func<TDestination, bool>> ExpressionConvert<TDestination, TSource>(Expression<Func<TSource, bool>> sourceExpression)
    {
        var parameter = Expression.Parameter(typeof(TDestination), sourceExpression.Parameters[0].Name);
        var body = new Visitor<TSource, TDestination>(parameter).Visit(sourceExpression.Body);
        return Expression.Lambda<Func<TDestination, bool>>(body, parameter);
    }

    private class Visitor<TSource, TDestination>(ParameterExpression parameter) : ExpressionVisitor
    {
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType != typeof(TSource)) return base.VisitMember(node);
            var memberInfo = typeof(TDestination).GetMember(node.Member.Name).FirstOrDefault();
            return Expression.MakeMemberAccess(parameter, memberInfo);

        }
    }
}