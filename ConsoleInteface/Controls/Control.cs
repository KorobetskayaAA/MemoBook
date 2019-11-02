using System;

namespace ConsoleInterface.Controls
{
    /// <summary>
    /// Абстрактный класс для элементов управления в консольных псевдоокнах.
    /// </summary>
    public abstract class Control
    {
        public Control(int left, int top, int width)
        {
            this.Left = left;
            this.Top = top;
            this.Width = width;
        }

        public virtual int Top { get; set; }
        public virtual int Left { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; } = 1;
        public int Right { get => Left + Width - 1; }
        public int Bottom { get => Top + Height - 1; }


        public bool Selected { get; set; }
        public virtual bool Selectable { get { return !Disabled; } }
        public virtual bool Disabled { get; set; } = false;

        public virtual void SetCursorPosition()
        {
            Console.SetCursorPosition(Left, Top);
        }

        protected virtual void WriteContent()
        {
        }

        public virtual void Print()
        {
            Console.SetCursorPosition(Left, Top);
            WriteContent();
        }

        public virtual bool ProcessKey(ConsoleKeyInfo keyInfo)
        {
            return true;
        }

        protected static void WriteFocused(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            Console.ResetColor();
        }

        protected static void WriteUnfocused(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write(text);
            Console.ResetColor();
        }

        protected static void WriteDisabled(string text)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(text);
            Console.ResetColor();
        }

        public static int FitStringHiddenLength(int textLength, int width, int position)
        {
            if (textLength > width)
            {
                int beginPosition = position - width + 1;
                if (beginPosition < 0)
                {
                    beginPosition = 0;
                }
                return beginPosition;
            }
            return 0;
        }

        /// <summary>
        /// Подгоняет строку под заданную ширину окна. Не умещающийся текст обрежется.
        /// Если текст меньше, он будет дополнен пробелами. Точка обрезки выбирается так,
        /// чтобы заданна позиция была в конце.
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <param name="width">Ширина окна для вывода текста</param>
        /// <param name="position">Эта позиция обязательно должна отображаться</param>
        /// <returns></returns>
        public static string FitString(string text, int width, int position = 0)
        {
            string croppedValue = text;
            // обрезаем, что не влезает
            if (croppedValue.Length > width)
            {
                int hiddenWidth = FitStringHiddenLength(croppedValue.Length, width, position);
                if (hiddenWidth + width > croppedValue.Length)
                    croppedValue = croppedValue.Substring(hiddenWidth);
                else
                    croppedValue = croppedValue.Substring(hiddenWidth, width);
            }
            // добавляем пробелы, если не дотягиваем по ширине
            if (croppedValue.Length < width)
            {
                croppedValue += new string(' ', width - croppedValue.Length);
            }
            return croppedValue;
        }

        public static void PrintVerticalScrollBar(double scrollPosition, int height,
            int left, int top)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int r = 0; r < height - 1; r++)
            {
                Console.SetCursorPosition(left, top + r);
                Console.Write("▒");
            }
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(left, top);
            Console.Write("↑");
            Console.SetCursorPosition(left,
                top + 1 + (int)(scrollPosition * (height - 3)));
            Console.Write("▓");
            Console.SetCursorPosition(left, top + height - 1);
            Console.Write("↓");

            Console.ResetColor();
        }
    }
}
