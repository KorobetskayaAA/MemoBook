using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterface.Controls
{
    /// <summary>
    /// Элемент управления для вывода текстовой информации без возможности ввода.
    /// </summary>
    class TextBlock : Control
    {
        public TextBlock(int left, int top, int width, string text) :
            base (left, top, width)
        {
            this.Text = ConsoleHelper.WrapString(text, Width - 2);
            Width = Text.Select(line => line.Length).Max() + 2;
        }

        public Alignment Align { get; set; } = Alignment.Center;

        public override bool Selectable { get { return false; } }
        public override int Height { get { return Text.Length; } }

        string[] Text { get; }

        protected override void WriteContent()
        {
            switch (Align)
            {
                case Alignment.Left:
                    foreach (var line in Text)
                    {
                        Console.CursorLeft = Left;
                        Console.Write("{0,-" + Width + "}", line);
                        Console.CursorTop++;
                    }
                    break;
                case Alignment.Center:
                    ConsoleHelper.WriteCentered(Text, Left, Width);
                    break;
                case Alignment.Right:
                    foreach (var line in Text)
                    {
                        Console.CursorLeft = Left;
                        Console.Write("{0," + Width + "}", line);
                        Console.CursorTop++;
                    }
                    break;
            }
        }

    }
}
