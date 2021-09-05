using GenericApp.Infra.Common.Enums;
using LinqToDB.Expressions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenericApp.Infra.Data.Utils
{
    public static class ExpressionsHelper
    {
        public static Func<ParameterExpression, KeyValuePair<string, object>, Expression> GetFieldFunc(FieldSource fieldSource)
        {
            return fieldSource switch
            {
                FieldSource.Propety => GetPropertyExpression,
                FieldSource.Column => GetColumnExpression,
                _ => throw new ArgumentOutOfRangeException(nameof(fieldSource), fieldSource, null),
            };
        }

        public static Expression<Func<T, P>> GetGetter<T, P>(ParameterExpression parameter, MemberExpression property)
        {
            return Expression.Lambda<Func<T, P>>(property, parameter);
        }

        public static Expression GetPropertyExpression(ParameterExpression instance, KeyValuePair<string, object> pair)
        {
            var propInfo = instance.Type.GetProperty(pair.Key);
            if (propInfo == null)
                return null;

            var propExpression = Expression.MakeMemberAccess(instance, propInfo);

            return propExpression;
        }

        public static Expression GetColumnExpression(ParameterExpression instance, KeyValuePair<string, object> pair)
        {
            var valueType = pair.Value != null ? pair.Value.GetType() : typeof(string);

            var method = MethodInfoHelper.SqlPropertyMethodInfo.MakeGenericMethod(valueType);
            var sqlPropertyCall = Expression.Call(null, method, instance, Expression.Constant(pair.Key, typeof(string)));
            var memberInfo = MemberHelper.GetMemberInfo(sqlPropertyCall);
            var memberAccess = Expression.MakeMemberAccess(instance, memberInfo);

            return memberAccess;
        }
    }
}
