using System;
using System.Linq;

namespace ConsoleInterface.Controls
{
    /// <summary>
    /// Выбор цвета из списка консольных цветов.
    /// </summary>
    public class InputColor : InputList
    {
        public ConsoleColor Color
        {
            get { return (ConsoleColor)SelectedItem; }
            set { SelectedItem = (int)value; }
        }

        protected override void WriteItem()
        {
            Console.BackgroundColor = Color;
            Console.Write("  ");
            Console.ResetColor();
            Console.Write(FitString(Value, Width - 2, 0));
        }

        public InputColor(string label, int left, int top, int width)
            : base(label, Enum.GetNames(typeof(ConsoleColor)).ToList(), left, top, width)
        {
            
        }
    }
}
