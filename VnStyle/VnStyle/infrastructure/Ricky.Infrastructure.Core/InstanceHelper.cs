using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Ricky.Infrastructure.Core.ComponentModel;
using Ricky.Infrastructure.Core.DataAnnotations;

namespace Ricky.Infrastructure.Core
{
    public class InstanceHelper
    {
        private static IDictionary<Type, List<PropertyInfo>> _dic = new Dictionary<Type, List<PropertyInfo>>();

        public static void EnsureCubeDateInfo<T>(T entity) where T : class, new()
        {
            //typeof(T).GetProperties().ToList()
            List<PropertyInfo> properties;
            Type type = typeof(T);
            if (_dic.ContainsKey(type)) properties = _dic[type];
            else
            {
                properties = type.GetProperties().ToList();
                _dic[type] = properties;
            }

            var typeCubeDateDimentionAttribute = typeof(CubeDateDimentionAttribute);
            properties.Where(p => Attribute.IsDefined(p, typeCubeDateDimentionAttribute) && (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))).ToList().ForEach(property =>
            {
                var cubeDateDimentionAttribute = (CubeDateDimentionAttribute)property.GetCustomAttributes(true).FirstOrDefault(p => p is CubeDateDimentionAttribute);
                if (cubeDateDimentionAttribute != null)
                {
                    if (string.IsNullOrEmpty(cubeDateDimentionAttribute.OriginalProperty)) cubeDateDimentionAttribute.OriginalProperty = property.Name.Replace("Key", "");
                    var originalProperty = properties.FirstOrDefault(p => p.Name == cubeDateDimentionAttribute.OriginalProperty);
                    if (originalProperty != null)
                    {
                        var originalPropertyValue = GetPropValue(entity, originalProperty.Name);
                        DateTime datetime = new DateTime(1970, 1, 1);
                        if (originalPropertyValue is DateTime)
                        {
                            datetime = (DateTime)originalPropertyValue;
                            datetime = DateTimeHelper.GetDateTimeCubeKey(datetime);
                            SetPropValue(entity, property.Name, datetime);
                        }
                        else
                        {
                            var tmpOriginalPropertyValue = ((DateTime?)originalPropertyValue);
                            if (tmpOriginalPropertyValue.HasValue) datetime = tmpOriginalPropertyValue.Value;

                            datetime = DateTimeHelper.GetDateTimeCubeKey(datetime);

                            DateTime? nullableDateTime = datetime;
                            //SetPropValue(entity, property.Name, nullableDateTime);


                            PropertyInfo propertyInfo = typeof(T).GetProperty(property.Name);
                            propertyInfo.SetValue(entity, nullableDateTime, null);
                        }

                    }
                }
            });
        }


        public static void SetPropValue<T>(T obj, string propertyName, object value)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName);
            propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }


        public static object GetPropValue<T>(T instance, string propertyName)
        {
            if (instance == null) return null;
            return instance.GetType().GetProperty(propertyName).GetValue(instance, null);
        }

        public static object GetPropValue(Type type, object instance, string propertyName)
        {
            return type.GetProperty(propertyName).GetValue(instance, null);
        }

        /// <summary>
        /// Sets a property on an object to a valuae.
        /// </summary>
        /// <param name="instance">The object whose property to set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        public static void SetPropValue(object instance, string propertyName, object value)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            if (propertyName == null) throw new ArgumentNullException("propertyName");

            Type instanceType = instance.GetType();
            PropertyInfo pi = instanceType.GetProperty(propertyName);
            if (pi == null)
                throw new Exception(string.Format("No property '{0}' found on the instance of type '{1}'.", propertyName, instanceType));
            if (!pi.CanWrite)
                throw new Exception(string.Format("The property '{0}' on the instance of type '{1}' does not have a setter.", propertyName, instanceType));
            if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
                value = To(value, pi.PropertyType);
            pi.SetValue(instance, value, new object[0]);
        }

        public static TypeConverter GetCustomTypeConverter(Type type)
        {
            //we can't use the following code in order to register our custom type descriptors
            //TypeDescriptor.AddAttributes(typeof(List<int>), new TypeConverterAttribute(typeof(GenericListTypeConverter<int>)));
            //so we do it manually here

            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();

            //if (type == typeof(ShippingOption))
            //    return new ShippingOptionTypeConverter();
            //if (type == typeof(List<ShippingOption>) || type == typeof(IList<ShippingOption>))
            //    return new ShippingOptionListTypeConverter();

            return TypeDescriptor.GetConverter(type);
        }


        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = GetCustomTypeConverter(destinationType);
                TypeConverter sourceConverter = GetCustomTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsAssignableFrom(value.GetType()))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The converted value.</returns>
        public static T To<T>(object value)
        {
            //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            return (T)To(value, typeof(T));
        }
    }
}