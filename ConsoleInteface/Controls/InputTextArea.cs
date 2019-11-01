using System;
using System.Linq;

namespace ConsoleInterface.Controls
{
    public class InputTextArea : InputControl
    {

        public InputTextArea(string label, int left, int top, int width, int height)
            : base(label, left, top, width)
        {
            this.Height = height;
        }

        int TextAreaHeight { get; set; }
        public override int Height
        {
            get => TextAreaHeight + (ShowLabel ? 1 : 0);
            set => TextAreaHeight = value - (ShowLabel ? 1 : 0);
        }

        public int UpperVisibleLine
        {
            get
            {
                int upperLine = InputLine + 1 - TextAreaHeight;
                if (upperLine < 0)
                {
                    return 0;
                }
                return upperLine;
            }
        }

        public string[] Lines
        {
            get
            {
                return ConsoleHelper.WrapString(Value, Width - 1);
            }
        }

        protected override void WriteValue()
        {
            // local copy to avoid remaking
            var lines = Lines;

            // cursor position
            if (CursorPosition == Value.Length)
            {
                InputLine = lines.Length - 1;
                InputColumn = lines[InputLine].Length;
                lines[InputLine] += " ";
            }
            else
            {
                InputLine = 0;
                int sum = 0;
                while (InputLine < lines.Length && sum + Width < CursorPosition)
                {
                    sum += lines[InputLine].Length + 1;
                    InputLine++;
                }
                InputColumn = CursorPosition - sum + 1;
            }


            int upperLine = UpperVisibleLine;

            int left = Console.CursorLeft;
            for (int r = 0; r < TextAreaHeight; r++)
            {
                string line = FitString((r + upperLine < lines.Length) ? 
                        lines[r + upperLine] : " ", 
                    Width);
                Console.CursorLeft = left;
                WriteInput(line);
                Console.CursorTop++;
            }

            if (lines.Length > TextAreaHeight)
            {
                PrintVerticalScrollBar((double)InputLine / TextAreaHeight,
                    TextAreaHeight, Right, Top + (ShowLabel ? 1 : 0));
            }
        }

        int InputLine { get; set; } = 0;
        int InputColumn { get; set; } = 0;

        public override void SetCursorPosition()
        {
            Console.CursorVisible = true;
            Console.SetCursorPosition(Left + InputColumn,
                Top + InputLine - UpperVisibleLine + (ShowLabel ? 1 : 0));
        }

        public override bool ProcessKey(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                if (InputLine > 0)
                {
                    InputLine--;
                    CursorPosition = InputColumn - 1
                        + Lines.Take(InputLine).Select(ln => ln.Length).Sum()
                        + InputLine;
                }
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (InputLine < Lines.Length)
                {
                    InputLine++;
                    CursorPosition = InputColumn - 1
                        + Lines.Take(InputLine).Select(ln => ln.Length).Sum()
                        + InputLine;
                }
            }
            else
            {
                return base.ProcessKey(keyInfo);
            }
            return true;
        }
    }
}
