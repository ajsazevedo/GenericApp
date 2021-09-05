using LinqToDB;
using LinqToDB.Expressions;
using LinqToDB.Linq;
using System;
using System.Linq;
using System.Reflection;

namespace GenericApp.Infra.Data.Utils
{
    public static class MethodInfoHelper
    {
        public static readonly MethodInfo SetMethodInfo = MemberHelper
          .MethodOf<object>(o => ((IUpdatable<object>)null).Set(null, 0))
          .GetGenericMethodDefinition();

        public static readonly MethodInfo SqlPropertyMethodInfo = typeof(Sql).GetMethod("Property")
           .GetGenericMethodDefinition();

        public static MethodInfo GetMethod<T>(string methodName)
        {
            if (typeof(T).IsPrimitive || typeof(T).Equals(typeof(string)) || typeof(T).Equals(typeof(DateTime)) || typeof(T).Equals(typeof(Guid)))
                return typeof(T).GetMethods().Single(
                    m =>
                    m.Name == methodName &&
                    m.GetParameters().Length == 1 &&
                    m.GetParameters()[0].ParameterType == typeof(T));
            return typeof(T).GetMethods().Single(
                m =>
                m.Name == methodName &&
                m.GetParameters().Length == 1);
        }
    }
}
