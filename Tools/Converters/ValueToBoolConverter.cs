using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tools.Converters
{
    public class ValueToBoolConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //用于BoolToVisibility，true时显示
            if (parameter == null)
            {
                if (value is bool boolValue)
                    return boolValue;
                return false;
            }
            else
            {
                var stringParam = parameter.ToString();
                //不为空
                if (stringParam == "NotNull")
                    return value != null;
                //为空
                else if (stringParam == "Null")
                    return value == null;
                //为空或空字符串
                else if (stringParam == "IsNullOrEmpty")
                    return string.IsNullOrEmpty(value?.ToString());
                //不为空且不为空字符串
                else if (stringParam == "NotNullOrEmpty")
                    return !string.IsNullOrEmpty(value?.ToString());
                //数量大于
                else if (stringParam.Contains("Count>"))
                {
                    if (value is Array arrayValue)
                        return arrayValue.Length > System.Convert.ToDouble(stringParam.Split('>')[1]);
                    if (value is IList listValue)
                        return listValue.Count > System.Convert.ToDouble(stringParam.Split('>')[1]);
                }
                //数量小于
                else if (stringParam.Contains("Count<"))
                {
                    if (value is Array arrayValue)
                        return arrayValue.Length < System.Convert.ToDouble(stringParam.Split('<')[1]);
                    if (value is IList listValue)
                        return listValue.Count < System.Convert.ToDouble(stringParam.Split('<')[1]);
                }
                //大于某个值
                else if (stringParam.Contains(">"))
                    return System.Convert.ToDouble(value) > System.Convert.ToDouble(stringParam.Split('>')[1]);
                //小于某个值
                else if (stringParam.Contains("<"))
                    return System.Convert.ToDouble(value) < System.Convert.ToDouble(stringParam.Split('<')[1]);
                //数字区间
                else if (stringParam.Contains("[") && stringParam.Contains("]"))
                {
                    stringParam = stringParam.Replace("[", "").Replace("]", "");
                    var doubleValue = System.Convert.ToDouble(value);
                    return doubleValue >= System.Convert.ToDouble(stringParam.Split('-')[0]) && doubleValue <= System.Convert.ToDouble(stringParam.Split('-')[1]);
                }
                //不等于某个值
                else if (stringParam.Contains("!="))
                    return System.Convert.ToDouble(value) != System.Convert.ToDouble(stringParam.Replace("!=", ""));
                //反转
                else if (stringParam == "Inverse")
                    return !System.Convert.ToBoolean(value);
                //等于某个值
                else if (double.TryParse(stringParam, out var doubleValue))
                    return System.Convert.ToDouble(value) == doubleValue;
                //等于某个字符串或枚举
                return stringParam == value?.ToString();
            }
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}