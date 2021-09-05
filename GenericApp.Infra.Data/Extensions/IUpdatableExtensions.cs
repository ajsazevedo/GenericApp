using GenericApp.Infra.Common.Enums;
using GenericApp.Infra.Data.Utils;
using LinqToDB.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenericApp.Infra.Data.Extensions
{
    public static class IUpdatableExtensions
    {
        public static IUpdatable<T> SetValues<T>(this IUpdatable<T> source,
           IEnumerable<KeyValuePair<string, object>> values,
           Func<ParameterExpression, KeyValuePair<string, object>, Expression> fieldFunc)
        {
            var param = Expression.Parameter(typeof(T));
            object current = source;
            foreach (var pair in values)
            {
                var fieldExpression = fieldFunc(param, pair);
                if (fieldExpression != null)
                {
                    var lambda = Expression.Lambda(fieldExpression, param);

                    var method = MethodInfoHelper.SetMethodInfo.MakeGenericMethod(typeof(T), fieldExpression.Type);
                    current = method.Invoke(null, new[] { current, lambda, pair.Value });
                }
            }

            return (IUpdatable<T>)current;
        }

        public static IUpdatable<T> SetValues<T>(this IUpdatable<T> source,
           IEnumerable<KeyValuePair<string, object>> values,
           FieldSource fieldSource = FieldSource.Propety)
        {
            return SetValues(source, values, ExpressionsHelper.GetFieldFunc(fieldSource));
        }
    }
}
