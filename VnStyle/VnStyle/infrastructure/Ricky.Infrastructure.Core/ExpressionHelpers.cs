using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Ricky.Infrastructure.Core
{
    public enum eExpressionTextOperator
    {
        Equals,
        Contains,
        NotNullOrEmpty,
        StartsWith,
    }

    public enum eExpressionNumberOperator
    {
        Equals,
        NotEquals,
        NotNull
    }

    public class ExpressionHelpers
    {
        public static Expression<Func<T, bool>> CreateExpressionTextNoneCompile<T>(string propertyName, string propertyValue, eExpressionTextOperator exOperator = eExpressionTextOperator.Equals)
        {
            if (String.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("propertyName");
            if (String.IsNullOrEmpty(propertyValue)) throw new ArgumentNullException("propertyValue");

            switch (exOperator)
            {
                case eExpressionTextOperator.Equals:
                    {
                        var arg = Expression.Parameter(typeof(T), "p");
                        var property = typeof(T).GetProperty(propertyName);
                        var comparison = Expression.Equal(
                            Expression.MakeMemberAccess(arg, property),
                            Expression.Constant(propertyValue));

                        var lambda = Expression.Lambda<System.Func<T, bool>>(comparison, arg);
                        return lambda;
                    }
                case eExpressionTextOperator.NotNullOrEmpty:
                    {
                        var arg = Expression.Parameter(typeof(T), "p");
                        var property = typeof(T).GetProperty(propertyName);
                        var comparison = Expression.NotEqual(
                            Expression.MakeMemberAccess(arg, property),
                            Expression.Constant(null));

                        var lambda = Expression.Lambda<System.Func<T, bool>>(comparison, arg);
                        return lambda;
                    }
                case eExpressionTextOperator.Contains:
                    {
                        var parameterExp = Expression.Parameter(typeof(T), "p");
                        var propertyExp = Expression.Property(parameterExp, propertyName);
                        MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var someValue = Expression.Constant(propertyValue, typeof(string));
                        var methodExp = Expression.Call(propertyExp, method, someValue);

                        return Expression.Lambda<Func<T, bool>>(methodExp, parameterExp);
                    }
                case eExpressionTextOperator.StartsWith:
                    {
                        var parameterExp = Expression.Parameter(typeof(T), "p");
                        var propertyExp = Expression.Property(parameterExp, propertyName);
                        MethodInfo method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                        var someValue = Expression.Constant(propertyValue, typeof(string));
                        var methodExp = Expression.Call(propertyExp, method, someValue);

                        return Expression.Lambda<Func<T, bool>>(methodExp, parameterExp);
                    }
                default:
                    return null;
            }

        }

        public static Func<T, bool> ExpressionHasValue<T>(string propertyName)
        {
            var lambda = ExpressionHasValueNonCompile<T>(propertyName).Compile();
            return lambda;
        }

        public static Expression<Func<T, bool>> ExpressionHasValueNonCompile<T>(string propertyName)
        {
            var arg = Expression.Parameter(typeof(T), "p");
            var property = typeof(T).GetProperty(propertyName);
            var comparison = Expression.Equal(
                Expression.Property(Expression.MakeMemberAccess(arg, property), "HasValue"),
                Expression.Constant(true));

            var lambda = Expression.Lambda<System.Func<T, bool>>(comparison, arg);
            return lambda;
        }

        public static Expression<Func<T, bool>> CreateExpressionNumberNoneCompile<T, TM>(string propertyName, TM propertyValue, eExpressionNumberOperator exOperator = eExpressionNumberOperator.Equals)
        {
            if (String.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("propertyName");
            switch (exOperator)
            {
                case eExpressionNumberOperator.Equals:
                    {
                        var arg = Expression.Parameter(typeof(T), "p");
                        var property = typeof(T).GetProperty(propertyName);
                        var comparison = Expression.Equal(
                            Expression.MakeMemberAccess(arg, property),
                            Expression.Constant(propertyValue));

                        var lambda = Expression.Lambda<System.Func<T, bool>>(comparison, arg);
                        return lambda;
                    }
                case eExpressionNumberOperator.NotNull:
                    {
                        var arg = Expression.Parameter(typeof(T), "p");
                        var property = typeof(T).GetProperty(propertyName);
                        var comparison = Expression.NotEqual(
                            Expression.MakeMemberAccess(arg, property),
                            Expression.Constant(null));

                        var lambda = Expression.Lambda<System.Func<T, bool>>(comparison, arg);
                        return lambda;
                    }
                case eExpressionNumberOperator.NotEquals:
                    {
                        var arg = Expression.Parameter(typeof(T), "p");
                        var property = typeof(T).GetProperty(propertyName);
                        var comparison = Expression.NotEqual(
                            Expression.MakeMemberAccess(arg, property),
                            Expression.Constant(propertyValue));

                        var lambda = Expression.Lambda<System.Func<T, bool>>(comparison, arg);
                        return lambda;
                    }
                default:
                    return null;
            }
        }

        public static Func<T, bool> MakeQueryFilter<T>(string propertyName, object value, string dataOperator) where T : class
        {

            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            var property = typeof(T).GetProperty(propertyName);
            MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, property);

            Type propertyType = property.PropertyType;
            //var typeDefinition = propertyType.GetGenericTypeDefinition();
            //&& (typeDefinition == typeof(Nullable))
            if ((propertyType.IsGenericType) && propertyType.GetGenericArguments().Count() > 0)
                propertyType = propertyType.GetGenericArguments()[0];

            object queryValue = null;
            // convert the value appropriately 
            if (propertyType == typeof(System.Int32))
                queryValue = Convert.ToInt32(value);
            if (property.PropertyType == typeof(DateTime))
                queryValue = Convert.ToDateTime(value);
            if (property.PropertyType == typeof(Double))
                queryValue = Convert.ToDouble(value);
            if (property.PropertyType == typeof(String))
                queryValue = Convert.ToString(value);
            if (property.PropertyType == typeof(Guid))
                queryValue = new Guid(value.ToString());

            var constantValue = Expression.Constant(queryValue);

            Type[] types = new Type[2];
            types.SetValue(typeof(Expression), 0);
            types.SetValue(typeof(Expression), 1);

            var methodInfo = typeof(Expression).GetMethod(dataOperator, types);
            var equality2 = (BinaryExpression)methodInfo.Invoke(null, new object[] { propertyAccess, constantValue });

            //return equality2;
            var lambda = Expression.Lambda<System.Func<T, bool>>(equality2).Compile();
            return lambda;

        }

        
    }
}
