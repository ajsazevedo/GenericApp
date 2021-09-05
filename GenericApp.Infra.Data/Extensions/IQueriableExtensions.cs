using GenericApp.Infra.CC.Localization.Resources;
using GenericApp.Infra.Common.Enums;
using GenericApp.Infra.Common.Extensions;
using GenericApp.Infra.Data.Utils;
using LinqToDB;
using LinqToDB.Expressions;
using LinqToDB.Linq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GenericApp.Infra.Data.Extensions
{
    public static class IQueriableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, bool descending)
        {
            if (ordering.IsNullOrEmpty())
                return source;
            var type = typeof(T);
            var property = type.GetProperty(ordering.ToTitleCase());
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var methodName = descending ? "OrderByDescending" : "OrderBy";
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> ContainValues<T>(this IQueryable<T> source,
            IEnumerable<KeyValuePair<string, object>> values)
        {
            if (!(values != null && values.Any()))
                return source;

            var newValues = values
                        .Where(x => x.Value != null)
                        .Select(x => new { Key = x.Key.ToTitleCase(), x.Value })
                        .ToDictionary(x => x.Key, x => x.Value);

            foreach (var pair in newValues)
            {
                var parameter = Expression.Parameter(typeof(T));
                var property = Expression.Property(parameter, pair.Key);
                if (property.Type == typeof(string))
                    source = source.Search(ExpressionsHelper.GetGetter<T, string>(parameter, property), (string)pair.Value, "Contains");
                else if (property.Type == typeof(int))
                    source = source.Search(ExpressionsHelper.GetGetter<T, int>(parameter, property), (int)pair.Value, "Equals");
                else if (property.Type == typeof(int?))
                    source = source.Search(ExpressionsHelper.GetGetter<T, int?>(parameter, property), pair.Value.ToInt32());
                else if (property.Type == typeof(long))
                    source = source.Search(ExpressionsHelper.GetGetter<T, long>(parameter, property), (long)pair.Value, "Equals");
                else if (property.Type == typeof(long?))
                    source = source.Search(ExpressionsHelper.GetGetter<T, long?>(parameter, property), pair.Value.ToInt64());
                else if (property.Type == typeof(Role))
                    source = source.Search(ExpressionsHelper.GetGetter<T, Role>(parameter, property), (Role)pair.Value.ToInt32());
                else if (property.Type == typeof(DateTime))
                {
                    if (pair.Value is JArray array)
                        source = source.SearchDate(array.ToObject<DateTime[]>(), pair.Key.ToString());
                    else
                        source = source.SearchDate(pair.Value.ToDateTime(), pair.Key.ToString());
                }
                else if (property.Type == typeof(DateTime?))
                {
                    if (pair.Value is JArray array)
                        source = source.SearchDateNull(array.ToObject<DateTime[]>(), pair.Key.ToString());
                    else
                        source = source.SearchDateNull(pair.Value.ToDateTime(), pair.Key.ToString());
                }
                else if (property.Type == typeof(Guid))
                    source = source.Search(ExpressionsHelper.GetGetter<T, Guid>(parameter, property), new Guid(pair.Value.ToString()), "Equals");
                else if (property.Type == typeof(Guid?))
                    source = source.Search(ExpressionsHelper.GetGetter<T, Guid?>(parameter, property), new Guid(pair.Value.ToString()));
                else if (property.Type == typeof(bool))
                    source = source.Search(ExpressionsHelper.GetGetter<T, bool>(parameter, property), pair.Value.ToBoolean(), "Equals");
                else if (property.Type == typeof(bool?))
                    source = source.Search(ExpressionsHelper.GetGetter<T, bool?>(parameter, property), pair.Value as bool?);
            }

            return source;
        }

        public static IQueryable<T> SearchDate<T>(this IQueryable<T> source, DateTime searchTerm, string property)
        {
            var nextDay = searchTerm.Date.AddDays(1);
            Expression<Func<T, DateTime, bool>> compareExpression = (ex, value) => value >= searchTerm.Date && value <= nextDay;
            var memberExpression = Expression.Property(compareExpression.Parameters[0], property);
            var bodyExpression = compareExpression.Body.Replace(compareExpression.Parameters[1], memberExpression);
            var lambda = Expression.Lambda<Func<T, bool>>(bodyExpression, compareExpression.Parameters[0]);
            return source.GetWhereExpression(lambda);
        }

        public static IQueryable<T> SearchDate<T>(this IQueryable<T> source, DateTime[] searchTerm, string property)
        {
            if (searchTerm.Length < 2)
                return source.SearchDate(searchTerm[0], property);
            if (searchTerm.Length > 2)
                throw new ArgumentException(SharedResource.InvalidDateArgument, nameof(searchTerm));
            var nextDay = searchTerm[1].AddDays(1);
            Expression<Func<T, DateTime, bool>> compareExpression = (ex, value) => value >= searchTerm[0] && value <= nextDay;
            var memberExpression = Expression.Property(compareExpression.Parameters[0], property);
            var bodyExpression = compareExpression.Body.Replace(compareExpression.Parameters[1], memberExpression);
            var lambda = Expression.Lambda<Func<T, bool>>(bodyExpression, compareExpression.Parameters[0]);
            return source.GetWhereExpression(lambda);
        }

        public static IQueryable<T> SearchDateNull<T>(this IQueryable<T> source, DateTime searchTerm, string property)
        {
            var nextDay = searchTerm.Date.AddDays(1);
            Expression<Func<T, DateTime?, bool>> compareExpression = (ex, value) => value >= searchTerm.Date && value <= nextDay;
            var memberExpression = Expression.Property(compareExpression.Parameters[0], property);
            var bodyExpression = compareExpression.Body.Replace(compareExpression.Parameters[1], memberExpression);
            var lambda = Expression.Lambda<Func<T, bool>>(bodyExpression, compareExpression.Parameters[0]);
            return source.GetWhereExpression(lambda);
        }

        public static IQueryable<T> SearchDateNull<T>(this IQueryable<T> source, DateTime[] searchTerm, string property)
        {
            if (searchTerm.Length < 2)
                return source.SearchDate(searchTerm[0], property);
            if (searchTerm.Length > 2)
                throw new ArgumentException(SharedResource.InvalidDateArgument, nameof(searchTerm));
            var nextDay = searchTerm[1].AddDays(1);
            Expression<Func<T, DateTime?, bool>> compareExpression = (ex, value) => value >= searchTerm[0] && value <= nextDay;
            var memberExpression = Expression.Property(compareExpression.Parameters[0], property);
            var bodyExpression = compareExpression.Body.Replace(compareExpression.Parameters[1], memberExpression);
            var lambda = Expression.Lambda<Func<T, bool>>(bodyExpression, compareExpression.Parameters[0]);
            return source.GetWhereExpression(lambda);
        }

        public static IQueryable<T> Search<T, P>(this IQueryable<T> source, Expression<Func<T, P>> stringProperty, P searchTerm)
        {
            //Create expression to represent x.[property].Contains(searchTerm)
            var searchTermExpression = Expression.Constant(searchTerm, typeof(P));
            return source.GetWhereExpression(stringProperty, Expression.Equal(stringProperty.Body, searchTermExpression));
        }

        public static IQueryable<T> Search<T, P>(this IQueryable<T> source, Expression<Func<T, P>> stringProperty, P searchTerm, string methodName)
        {
            if (typeof(P) == typeof(string) && string.IsNullOrEmpty(searchTerm.ToString()))
            {
                return source;
            }

            //Create expression to represent x.[property].Contains(searchTerm)
            var searchTermExpression = Expression.Constant(searchTerm);
            var checkContainsExpression = Expression.Call(stringProperty.Body, MethodInfoHelper.GetMethod<P>(methodName), searchTermExpression);

            Expression binaryExpression;

            //Create expression to represent x.[property] != null
            if (typeof(P) == typeof(string))
            {
                var isNotNullExpression = Expression.NotEqual(stringProperty.Body,
                                                 Expression.Constant(null));

                //Join not null and contains expressions
                binaryExpression = Expression.AndAlso(isNotNullExpression, checkContainsExpression);
            }
            else
            {
                binaryExpression = checkContainsExpression;
            }

            return source.GetWhereExpression(stringProperty, binaryExpression);
        }

        private static IQueryable<T> GetWhereExpression<T>(this IQueryable<T> source, Expression<Func<T, bool>> expression)
        {
            var methodCallExpression = Expression.Call(typeof(Queryable),
                                                       "Where",
                                                       new Type[] { source.ElementType },
                                                       source.Expression,
                                                       expression);

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

        private static IQueryable<T> GetWhereExpression<T, P>(this IQueryable<T> source, Expression<Func<T, P>> stringProperty, Expression binaryExpression)
        {
            return source.GetWhereExpression(Expression.Lambda<Func<T, bool>>(binaryExpression, stringProperty.Parameters));
        }

        public static IQueryable<T> FilterByValues<T>(this IQueryable<T> source,
            IEnumerable<KeyValuePair<string, object>> values,
            Func<ParameterExpression, KeyValuePair<string, object>, Expression> fieldFunc)
        {
            if (!(values != null && values.Any()))
                return source;

            var newValues = values
                        .Where(x => x.Value != null)
                        .Select(x => new { Key = x.Key.ToTitleCase(), x.Value })
                        .ToDictionary(x => x.Key, x => x.Value);

            var param = Expression.Parameter(typeof(T));

            foreach (var pair in newValues)
            {
                var fieldExpression = fieldFunc(param, pair);
                if (fieldExpression != null)
                {
                    var equality = Expression.Equal(fieldExpression, Expression.Constant(pair.Value, fieldExpression.Type));
                    var lambda = Expression.Lambda<Func<T, bool>>(equality, param);

                    source = source.Where(lambda);
                }
            }

            return source;
        }

        public static IQueryable<T> FilterByValues<T>(this IQueryable<T> source,
            IEnumerable<KeyValuePair<string, object>> values,
            FieldSource fieldSource = FieldSource.Propety)
        {
            return FilterByValues(source, values, ExpressionsHelper.GetFieldFunc(fieldSource));
        }

        public static IUpdatable<T> SetValues<T>(this IQueryable<T> source,
           IEnumerable<KeyValuePair<string, object>> values,
           FieldSource fieldSource = FieldSource.Propety)
        {
            return source.AsUpdatable().SetValues(values, fieldSource);
        }

        public static int UpdateDynamic<T>(this IQueryable<T> source,
           IEnumerable<KeyValuePair<string, object>> filterValues,
           IEnumerable<KeyValuePair<string, object>> setValues,
           FieldSource fieldSource = FieldSource.Propety)
        {
            return source
               .FilterByValues(filterValues, fieldSource)
               .SetValues(setValues, fieldSource)
               .Update();
        }
    }
}
