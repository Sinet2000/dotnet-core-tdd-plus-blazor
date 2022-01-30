using System.ComponentModel;
using System.Linq.Expressions;
using RangeAttribute = BlazorExperience.Core.Attributes.RangeAttribute;

namespace BlazorExperience.Core
{
    public static class Extensions
    {
        #region IQueryable extensions to support ordering by property name

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName)
        {
            return CallOrderedQueryable(query, "OrderBy", propertyName);
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName)
        {
            return CallOrderedQueryable(query, "OrderByDescending", propertyName);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, string propertyName)
        {
            return CallOrderedQueryable(query, "ThenBy", propertyName);
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> query, string propertyName)
        {
            return CallOrderedQueryable(query, "ThenByDescending", propertyName);
        }

        /// <summary>
        /// Builds the Queryable functions using a TSource property name.
        /// </summary>
        public static IOrderedQueryable<T> CallOrderedQueryable<T>(this IQueryable<T> query, string methodName,
            string propertyName)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T), body.Type },
                    query.Expression,
                    Expression.Lambda(body, param)
                )
            );
        }

        #endregion

        #region Enum Extensions

        public static string GetDescription(this Enum enumeration)
        {
            if (enumeration == null)
                return string.Empty;

            var value = enumeration.ToString();
            var type = enumeration.GetType();
            var descAttribute =
                (DescriptionAttribute[])type.GetField(value).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descAttribute.Length > 0 ? descAttribute[0].Description : value;
        }

        public static KeyValuePair<int, int> GetRange(this Enum enumeration)
        {
            var value = enumeration.ToString();
            var type = enumeration.GetType();
            // There is used custom attribute that is inherited from Attribute class!
            var attribute = (RangeAttribute[])type.GetField(value).GetCustomAttributes(typeof(RangeAttribute), false);

            return attribute.Length > 0 ? attribute[0].Range : new KeyValuePair<int, int>();
        }
        #endregion
    }
}
