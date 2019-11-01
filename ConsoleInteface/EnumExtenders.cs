using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace ConsoleInterface
{
    public static class EnumExtenders
    {
        public static string GetDescription(this Enum enumerable)
        {
            var type = enumerable.GetType();
            var memInfo = type.GetMember(enumerable.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return (attrs[0] as DescriptionAttribute).Description;
                }
            }
            return enumerable.ToString();
        }

        public static List<string> GetDescriptions<T>() where T : struct
        {
            Type t = typeof(T);
            if (!t.IsEnum)
            {
                return null;
            }
            return Enum.GetValues(t).Cast<Enum>().Select(x => x.GetDescription()).ToList();
        }
    }
}
