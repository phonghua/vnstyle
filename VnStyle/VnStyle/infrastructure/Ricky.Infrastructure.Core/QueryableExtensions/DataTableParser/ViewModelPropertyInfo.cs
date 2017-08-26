using System.Reflection;

namespace Ricky.Infrastructure.Core.QueryableExtensions.DataTableParser
{
    internal class ViewModelPropertyInfo
    {
        public string MapUnderlyingProperty { get; set; }
        public ValueConverterAttribute ValueConverter { get; set; }
        public ConditionProviderAttribute Condition { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }
}