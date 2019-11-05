using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

namespace ConsoleInterface
{
    /// <summary>
    /// Общеупотребительные статические методы для вывода информации в консоль.
    /// </summary>
    public class ConsoleHelper
    {
        /// <summary>
        /// Для вывода многострочного текста заданной ширины. 
        /// Разбивает строку на несколько по символу пробела и \n.
        /// </summary>
        /// <param name="text">Исходная строка.</param>
        /// <param name="width">Максимально допустимая ширина текста.</param>
        /// <returns></returns>
        public static string[] WrapString(string text, int width)
        {
            List<string> lines = text.Split('\n').ToList();
            string pattern = @"^.{1," + width + @"}\s";
            for (int i = 0; i < lines.Count; i++)
            {
                while (lines[i].Length > width)
                {
                    string paragraph = lines[i];
                    Match match = Regex.Match(paragraph, pattern);
                    if (match.Success)
                    {
                        lines[i] = match.Value.Trim();
                        lines.Insert(i + 1, paragraph.Substring(match.Length));
                    }
                    else
                    {
                        lines[i] = paragraph.Substring(0, width - 1);
                        lines.Insert(i + 1, paragraph.Substring(width));
                    }
                    i++;
                }
            }
            return lines.ToArray();
        }

        /// <summary>
        /// Для вывода многострочного текста заданной ширины. 
        /// Разбивает строку на несколько по символу пробела и \n.
        /// Старается сделать строки сбалансированными по длине.
        /// Подходит для нередактируемого текста, выравниваемого по центру.
        /// </summary>
        /// <param name="text">Исходная строка.</param>
        /// <param name="width">Максимально допустимая ширина текста.</param>
        /// <returns></returns>
        public static string[] WrapStringBalanced(string text, int width)
        {
            List<string> lines = text.Split('\n').ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Length > width)
                {
                    string[] words = lines[i].Split(' ');
                    lines[i] = string.Join(" ",
                        words.Take(words.Length / 2 + words.Length % 2));
                    lines.Insert(i + 1, string.Join(" ",
                        words.Skip(words.Length / 2 + words.Length % 2)));
                    i--;
                }
            }
            return lines.ToArray();
        }

        /// <summary>
        /// Выводит предварительно разбитый на строки текст по центру заданного региона.
        /// Контроль длины строк не производится.
        /// </summary>
        /// <param name="lines">Строки текста, каждая строка должна быть не длиннее width</param>
        /// <param name="left">Отступ от края окна слева</param>
        /// <param name="width">Ширина региона для центрирования</param>
        public static void WriteCentered(string[] lines, int left = 0, int width = -1)
        {
            if (width < 0)
            {
                width = Console.WindowWidth;
            }
            int centerLine = left + width / 2;
            for (int i = 0; i< lines.Length; i++)
            {
                Console.CursorLeft = centerLine - lines[i].Length / 2;
                Console.Write(lines[i]);
                Console.CursorTop++;
            }
        }

        /// <summary>
        /// Выводит заданный текст по центру по центру заданного региона.
        /// Текст будет разюит на строки по символу \n и пробелм так, чтобы умещаться
        /// в заданную ширину.
        /// </summary>
        /// <param name="text">Исходный текст, может быть многострочным</param>
        /// <param name="left">Отступ от края окна слева</param>
        /// <param name="width">Ширина региона для центрирования</param>
        public static void WriteCentered(string text, int left = 0, int width = -1)
        {
            if (width < 0)
            {
                width = Console.WindowWidth;
            }
            WriteCentered(WrapStringBalanced(text, width), left, width);
        }

        /// <summary>
        /// Вывести в консоль текст, обратив цвета фона и текста.
        /// После вывода цвета возвращаются в первоначальное состояние.
        /// </summary>
        /// <param name="text">Строка текста, которую нужно вывести</param>
        public static void WriteColorInverted(string text)
        {
            var bg = Console.BackgroundColor;
            var fg = Console.ForegroundColor;
            Console.BackgroundColor = fg;
            Console.ForegroundColor = bg;
            Console.Write(text);
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
        }

        /// <summary>
        /// Преобразовать RGB-цвет в ближайший ConsoleColor.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ConsoleColor RgbToCosoleColor(Color color)
        {
            int index = ((color.R | color.G | color.B) > 128) ? 1 << 3 : 0; // Bright bit
            index |= (color.R > 64) ? 1 << 2 : 0; // Red bit
            index |= (color.G > 64) ? 1 << 1 : 0; // Green bit
            index |= (color.B > 64) ? 1 : 0; // Blue bit
            return (ConsoleColor)index;
        }

        /// <summary>
        /// Преобразовать консольный цвет в RGB
        /// </summary>
        /// <param name="consoleColor"></param>
        /// <returns></returns>
        public static Color CosoleColorToRgb(ConsoleColor consoleColor)
        {
            int index = (int)consoleColor;
            int r = (index & (1 << 2)) > 0 ? 128 : 0;
            int g = (index & (1 << 1)) > 0 ? 128 : 0;
            int b = (index & (1 << 0)) > 0 ? 128 : 0;
            if ((index & (1 << 3)) > 0)
            {
                if (r > 0) r += 127;
                if (g > 0) g += 127;
                if (b > 0) b += 127;
            }
            return Color.FromArgb(r,g,b);
        }
    }
}
