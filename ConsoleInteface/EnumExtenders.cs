using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace ConsoleInterface
{
    /// <summary>
    /// Расширение для типа Enum с возможностью узнать текстовое описание
    /// каждого значения или класса в целом.
    /// </summary>
    public static class EnumExtenders
    {
        /// <summary>
        /// Получить текстовое описание Description для значения Enum.
        /// Если описание не задано, будет преобразовано к строке по умолчанию.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Выводит список описаний для каждого значения в Enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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
