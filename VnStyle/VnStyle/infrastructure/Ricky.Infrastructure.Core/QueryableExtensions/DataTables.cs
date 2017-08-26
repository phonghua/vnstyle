using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Ricky.Infrastructure.Core.DataTables;
using Ricky.Infrastructure.Core.Generic;
using Ricky.Infrastructure.Core.QueryableExtensions.DataTableParser;

namespace Ricky.Infrastructure.Core.QueryableExtensions
{
    public static class DataTables
    {
        //private static readonly Dictionary<string, ViewModelPropertyInfo> ViewModelProperties = new Dictionary<string, ViewModelPropertyInfo>();

        #region "Paged list"
        public static IPagedList<TDestination> ToPagedListResult<TSource, TDestination>(this IQueryable<TSource> source,
            IDataTablesRequest request, Expression<Func<TSource, TDestination>> selectExpression)
        {
            var query = BuildQuery(source, request, selectExpression);
            return new PagedList<TDestination>(query, request.Start / request.Length, request.Length);
        }

        public static IPagedList<T> ToPagedListResult<T>(this IQueryable<T> source, IDataTablesRequest request)
        {
            return ToPagedListResult<T, T>(source, request, p => p);
        }

        

        #endregion

        #region "DataTables Result"

        public static DataTableJsResult<TDestination> ToDataTableJsResult<TSource, TDestination>(this IQueryable<TSource> source,
            IDataTablesRequest request, Expression<Func<TSource, TDestination>> selectExpression)
        {
            var pagedListResult = ToPagedListResult(source, request, selectExpression);
            return new DataTableJsResult<TDestination>(request.Draw, pagedListResult, pagedListResult.TotalCount, pagedListResult.TotalCount);
        }

        public static DataTableJsResult<T> ToDataTableJsResult<T>(this IQueryable<T> source, IDataTablesRequest request)
        {
            return ToDataTableJsResult<T, T>(source, request, p => p);
        }

        #endregion

        private static IQueryable<TDestination> BuildQuery<TSource, TDestination>(IQueryable<TSource> source, IDataTablesRequest request,
            Expression<Func<TSource, TDestination>> selectExpression)
        {
            Dictionary<string, ViewModelPropertyInfo> viewModelProperties = new Dictionary<string, ViewModelPropertyInfo>();
            var vmProperties = typeof(TDestination).GetProperties();
            foreach (var property in vmProperties)
            {
                var underlyingProperty = property.GetCustomAttribute<MapUnderlyingPropertiesAttribute>();
                var valueConverter = property.GetCustomAttribute<ValueConverterAttribute>(true) ?? new DefaultValueConverterAttribute(property.PropertyType);
                var customCondition = property.GetCustomAttribute<ConditionProviderAttribute>(true) ?? new ConditionProviderAttribute();
                PropertyInfo prop = property;
                viewModelProperties.Add(property.Name, new ViewModelPropertyInfo()
                {
                    MapUnderlyingProperty = underlyingProperty == null ? property.Name : underlyingProperty.Expression,
                    ValueConverter = valueConverter,
                    Condition = customCondition,
                    PropertyInfo = prop
                });
            }

            #region "Search, Sort, Select"

            Expression<Func<TDestination, bool>> genericExpression = null;
            var query = source.Select(selectExpression);

            if (request.Columns != null && request.Columns.Any())
            {
                foreach (var c in request.Columns)
                {
                    ViewModelPropertyInfo vmProperty;
                    if (c.Searchable && viewModelProperties.TryGetValue(c.Data, out vmProperty))
                    {
                        var col = new FilterHelper.ColumnFilterInfo()
                        {
                            PropertyNameOrExpression = vmProperty.MapUnderlyingProperty
                        };
                        if (!string.IsNullOrEmpty(c.Search.Value))
                        {
                            vmProperty.ValueConverter.Parse(c.Search.Value, col);
                        }

                        var condition = vmProperty.Condition.GetCondition<TDestination>(col);
                        if (condition != null)
                        {
                            query = query.Where(condition);
                        }

                        //Generic Search
                        if (!string.IsNullOrEmpty(request.Search.Value))
                        {
                            var tmpCol = new FilterHelper.ColumnFilterInfo()
                            {
                                PropertyNameOrExpression = vmProperty.MapUnderlyingProperty
                            };
                            vmProperty.ValueConverter.Parse(request.Search.Value, tmpCol);
                            var singleExpression = vmProperty.Condition.GetCondition<TDestination>(tmpCol);
                            if (singleExpression != null)
                            {
                                genericExpression = genericExpression == null
                                    ? singleExpression
                                    : genericExpression.OrElse(singleExpression);
                            }
                        }
                    }
                }
            }
            


            if (genericExpression != null)
            {
                query = query.Where(genericExpression);
            }

            // Ordering

            IOrderedQueryable<TDestination> orderQuery = null;
            if (request.Columns != null && request.Columns.Any())
            {
                var orderColumns = request.Columns.Where(c => c.IsOrdered).OrderBy(c => c.OrderNumber);
                
                if (orderColumns.Any())
                {
                    foreach (var column in orderColumns)
                    {
                        ViewModelPropertyInfo vmProperty;
                        if (column.Searchable && viewModelProperties.TryGetValue(column.Data, out vmProperty))
                        {
                            orderQuery = column.SortDirection == OrderDirection.Ascendant
                                ? (orderQuery == null
                                    ? query.OrderBy(vmProperty.MapUnderlyingProperty)
                                    : orderQuery.ThenBy(vmProperty.MapUnderlyingProperty))
                                : (orderQuery == null
                                    ? query.OrderByDescending(vmProperty.MapUnderlyingProperty)
                                    : orderQuery.ThenByDescending(vmProperty.MapUnderlyingProperty));
                        }
                    }
                }

            }

            if (orderQuery == null)
            {
                var firstOrDefault = typeof(TDestination).GetProperties().FirstOrDefault();
                if (firstOrDefault != null)
                {
                    var propName = firstOrDefault.Name;
                    orderQuery = query.OrderBy("" + propName + "");
                }
            }

            #endregion

            return orderQuery;
        }
    }
}
