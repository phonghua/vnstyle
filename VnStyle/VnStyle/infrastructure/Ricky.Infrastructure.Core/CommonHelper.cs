using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ricky.Infrastructure.Core.ComponentModel;
using Ricky.Infrastructure.Core.ExtendMethods;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ricky.Infrastructure.Core
{
    public static class CommonHelper
    {
        public static String GenerateStringKey()
        {
            return Guid.NewGuid().ToString().ToLower().Replace("-", "");
        }
        public static string IsSelected(this HtmlHelper html, string controllers = "", string actions = "", string cssClass = "current-menu-item")
        {
            ViewContext viewContext = html.ViewContext;
            bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
                viewContext = html.ViewContext.ParentActionViewContext;

            RouteValueDictionary routeValues = viewContext.RouteData.Values;
            string currentAction = routeValues["action"].ToString();
            string currentController = routeValues["controller"].ToString();

            if (String.IsNullOrEmpty(actions))
                actions = currentAction;

            if (String.IsNullOrEmpty(controllers))
                controllers = currentController;

            string[] acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
            string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }

        /// <summary>
        /// Ensures the subscriber email or throw.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static string EnsureSubscriberEmailOrThrow(string email)
        {
            string output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);

            if (!IsValidEmail(output))
            {
                throw new Exception("Email is not valid.");
            }

            return output;
        }

        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        public static bool IsNumeric(String text)
        {
            if (String.IsNullOrEmpty(text)) return false;
            int n;
            bool isNumeric = int.TryParse(text.Trim(), out n);
            return isNumeric;
        }

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// Returns an random interger number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// Ensure that a string doesn't exceed maximum allowed length
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="postfix">A string to add to the end if the original string was shorten</param>
        /// <returns>Input string if its lengh is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var result = str.Substring(0, maxLength);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();
        }

        /// <summary>
        /// Ensure that a string is not null
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        /// <summary>
        /// Indicates whether the specified strings are null or empty strings
        /// </summary>
        /// <param name="stringsToValidate">Array of strings to validate</param>
        /// <returns>Boolean</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            bool result = false;
            Array.ForEach(stringsToValidate, str =>
            {
                if (string.IsNullOrEmpty(str)) result = true;
            });
            return result;
        }

        public static bool IsUrl(string uriName)
        {
            Uri uriResult = null;
            bool result = Uri.TryCreate(uriName, UriKind.Absolute, out uriResult);
            return result;
        }


        /// <summary>
        /// convert datetimeString with format dd/MM/yyyy to DateTime
        /// </summary>
        /// <param name="dateTimeText"></param>
        /// <returns></returns>
        public static DateTime? ConvertToDateTime(string dateTimeText)
        {
            return ConvertToDateTime(dateTimeText, "dd/MM/yyyy");
        }

        public static DateTime? ConvertToDateTime(string dateTimeText, string format)
        {
            try
            {
                var dateTime = DateTime.ParseExact(dateTimeText, format, null);
                return dateTime;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int? ConvertToInt32(string text)
        {
            if (String.IsNullOrEmpty(text)) return null;
            try
            {
                return Convert.ToInt32(text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static long? ConvertToInt64(string text)
        {
            if (String.IsNullOrEmpty(text)) return null;
            try
            {
                return Convert.ToInt64(text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int[] ConvertToArray(string text, char separator)
        {
            if (String.IsNullOrEmpty(text)) return null;
            try
            {
                return text.Split(separator).Select(p => Convert.ToInt32(EnsureNumericOnly(p))).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void ForEach<T>(IEnumerable<T> data, bool runAsync, Action<T> action)
        {
            if (runAsync)
            {
                Parallel.ForEach(data, action);
            }
            else
            {
                data.ForEach((p, index) => action(p));
            }
        }

        public static decimal? GetMedian(decimal[] arr)
        {
            if (arr == null || !arr.Any()) return null;
            var count = arr.Count();
            var sortedArr = arr.OrderBy(p => p).ToArray();
            if (count % 2 == 0) return (sortedArr[count / 2] + sortedArr[(count / 2) - 1]) / 2; ;
            return sortedArr[(count / 2)];

        }

        public static string ToPascalCaseNaming(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;
            return char.ToUpper(name[0]) + name.Substring(1, name.Length - 1);
        }

        //public static string PascalCase(string text)
        //{
        //    return char.ToLower(text[0]) + text.Substring(1);
        //}

        public static string ToCamelCaseNaming(string text)
        {
            return char.ToLower(text[0]) + text.Substring(1);
        }


        /// <summary>
        /// Sets a property on an object to a valuae.
        /// </summary>
        /// <param name="instance">The object whose property to set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        public static void SetProperty(object instance, string propertyName, object value)
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

        /// <summary>
        /// Convert enum for front-end
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Converted string</returns>
        public static string ConvertEnum(string str)
        {
            string result = string.Empty;
            char[] letters = str.ToCharArray();
            foreach (char c in letters)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();
            return result;
        }


        public static string StripTagsRegex(string source)
        {
            if (string.IsNullOrEmpty(source)) return source;
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        public static string SubString(string source, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;
            if (source.Length <= length) return source;
            return source.Substring(startIndex, length);
        }


        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static readonly Regex HtmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return HtmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static string TinyString(string text, int length, string subfix = "...")
        {
            if (String.IsNullOrEmpty(text) || text.Length <= length) return text;
            return text.Substring(0, length) + subfix;

        }

        private static IDictionary<Type, string> _typeFullName = new Dictionary<Type, string>();
        public static string FullName<T>()
        {
            var type = typeof(T);
            if (_typeFullName.ContainsKey(type)) return _typeFullName[type];
            _typeFullName[type] = type.FullName;
            return _typeFullName[type];
        }

        
        public static string FriendlyUrl(string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }

        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }

        public class PersonName
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
        }

        public static void SplitPersonName(string fullName, out string lastName, out string firstName, out string middleName)
        {
            lastName = string.Empty;
            firstName = string.Empty;
            middleName = string.Empty;
            if (string.IsNullOrEmpty(fullName)) return;
            var nameSplit = fullName.Split(' ').Select(p => p.Trim()).Where(p => !string.IsNullOrEmpty(p)).ToList();
            if (nameSplit.Count == 0) return;
            firstName = nameSplit[nameSplit.Count - 1];
            if (nameSplit.Count > 1)
            {
                lastName = nameSplit[0];
                nameSplit.RemoveAt(nameSplit.Count - 1);
                nameSplit.RemoveAt(0);
                middleName = string.Join(" ", nameSplit);
            }
        }

        public static PersonName SplitPersonName(string fullName)
        {
            string lastName;
            string middleName;
            string firstName;
            SplitPersonName(fullName, out lastName, out firstName, out middleName);
            return new PersonName { FirstName = firstName, MiddleName = middleName, LastName = lastName };
        }

        public static string ConvertNumberToString(long number, int length)
        {
            if (length < 1) return number.ToString();
            if (number > (Math.Pow(10, (length + 1)) - 1)) return number.ToString();
            var numOfZeroChar = (length - number.ToString().Length);
            var result = "";
            for (int i = 0; i < numOfZeroChar; i++)
            {
                result += "0";
            }
            return result + number;
        }


        public static string EscapeName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = NormalizeString(name);

                // Replaces all non-alphanumeric character by a space
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < name.Length; i++)
                {
                    builder.Append(char.IsLetterOrDigit(name[i]) ? name[i] : ' ');
                }

                name = builder.ToString();

                // Replace multiple spaces into a single dash
                name = Regex.Replace(name, @"[ ]{1,}", @"-", RegexOptions.None);
            }

            return name;
        }

        /// <summary>
        /// Strips the value from any non english character by replacing thoses with their english equivalent.
        /// </summary>
        /// <param name="value">The string to normalize.</param>
        /// <returns>A string where all characters are part of the basic english ANSI encoding.</returns>
        /// <seealso cref="http://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net"/>
        private static string NormalizeString(string value)
        {
            string normalizedFormD = value.Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < normalizedFormD.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedFormD[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(normalizedFormD[i]);
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static bool IsNullOrEmpty(string source)
        {
            if (string.IsNullOrEmpty(source)) return true;
            source = source.Trim();
            return string.IsNullOrEmpty(source);
        }

        public static string GetHostNameFromUrl(string url)
        {
            Uri uri = new Uri(url);
            return uri.Host.ToLower();
        }

        public static string GetHashId(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static void UpdateListUtilities(List<long> newList, List<long> existList, out List<long> needToInsert, out List<long> needToDelete)
        {
            needToInsert = existList.Any()
                ? newList.Where(p => !existList.Contains(p)).ToList()
                : newList;

            needToDelete = newList.Any() ?
                existList.Where(p => !newList.Contains(p)).ToList()
                : existList;
        }
    }
}
