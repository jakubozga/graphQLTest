using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace S360.GraphQL.Query
{
    public class SelectList<TSource>
    {
        private List<MemberInfo> members = new List<MemberInfo>();
        private Dictionary<MemberInfo, List<MemberInfo>> dict = new Dictionary<MemberInfo, List<MemberInfo>>();

        public void Add<TRelation, TValue>(Expression<Func<TSource, TRelation>> memberToSet, string prop)
        {
            var selector = GenerateMemberExpression<TRelation, TValue>(prop);
            var member = (selector.Body as MemberExpression ?? ((UnaryExpression)selector.Body).Operand as MemberExpression).Member;

            var relation = ((MemberExpression)memberToSet.Body).Member;
            if (!dict.ContainsKey(relation))
            {
                dict.Add(relation, new List<MemberInfo>());
            }

            dict[relation].Add(member);
        }

        public SelectList<TSource> Add<TValue>(string fieldName)
        {
            Expression<Func<TSource, TValue>> member = GenerateMemberExpression<TSource, TValue>(fieldName);
            members.Add(((MemberExpression)member.Body).Member);

            return this;
        }
        public IQueryable<TResult> Select<TResult>(IQueryable<TSource> source)
        {
            var sourceType = typeof(TSource);
            var resultType = typeof(TResult);
            var parameter = Expression.Parameter(sourceType, "e");

            var binds = GetRelationBindings<TResult>(parameter);

            var b = members.Select(member => Expression.Bind(resultType.GetProperty(member.Name), Expression.MakeMemberAccess(parameter, member)));
            var bindings = b.Union(binds);

            var body = Expression.MemberInit(Expression.New(resultType), bindings);
            var selector = Expression.Lambda<Func<TSource, TResult>>(body, parameter);
            return source.Select(selector);
        }

        public List<MemberBinding> GetRelationBindings<TResult>(ParameterExpression parameter)
        {
            var list = new List<MemberBinding>();
            foreach (var keyValuePair in dict)
            {
                var memberExpression = Expression.MakeMemberAccess(parameter, keyValuePair.Key);
                var relationProperty = typeof(TResult).GetProperty(keyValuePair.Key.Name);

                var assignments = new List<MemberAssignment>();

                foreach (var memberInfo in keyValuePair.Value)
                {
                    var entityProperty = Expression.MakeMemberAccess(memberExpression, memberInfo);
                    var bid = Expression.Bind(relationProperty.PropertyType.GetMember(memberInfo.Name).Single(), entityProperty);
                    assignments.Add(bid);
                }

                var init = Expression.MemberInit(Expression.New(relationProperty.PropertyType), assignments);
                
                var assign = Expression.Bind(relationProperty, init);
                list.Add(assign);
            }

            return list;
        }

        public static Expression<Func<TModel, T>> GenerateMemberExpression<TModel, T>(string propertyName)
        {
            var propertyInfo = typeof(TModel).GetProperties().Single(x => x.Name.ToUpper() == propertyName.ToUpper());

            var entityParam = Expression.Parameter(typeof(TModel), "e");
            Expression columnExpr = Expression.Property(entityParam, propertyInfo);

            /*if (propertyInfo.PropertyType != typeof(T))
                columnExpr = Expression.Convert(columnExpr, typeof(T));*/

            return Expression.Lambda<Func<TModel, T>>(columnExpr, entityParam);
        }
    }
}