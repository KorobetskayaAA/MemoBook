using System;

namespace ConsoleInterface.Controls
{
    /// <summary>
    /// Элемент управления для ввода данных.
    /// Содержит встроенню метку с описанием, метка размещается сверху.
    /// </summary>
    public class InputControl : Control
    {
        public InputControl(string label, int left, int top, int width)
            : base (left, top, width)
        {
            this.Label = label;
            this.Value = "";
            this.ShowLabel = true;
        }

        public string Label { get; }
        public bool ShowLabel { get; set; }
        public virtual string Value { get; set; }
        public override int Height
        {
            get => 1 + (ShowLabel ? 1 : 0);
            set => base.Height = value;
        }

        int cursorPosition;
        public int CursorPosition
        {
            get
            {
                return cursorPosition;
            }
            set
            {
                if (value > Value.Length)
                {
                    value = Value.Length;
                }
                if (value < 0)
                {
                    value = 0;
                }
                cursorPosition = value;
            }
        }

        public override void SetCursorPosition()
        {
            Console.CursorVisible = true;
            Console.SetCursorPosition(Left + CursorPosition 
                - FitStringHiddenLength(Value.Length + 1, Width, CursorPosition), 
                Top + (ShowLabel ? 1 : 0));
        }

        protected void WriteInput(string text)
        {
            if (Disabled)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.BackgroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            Console.Write(text);
            Console.ResetColor();
        }

        protected static void WriteLabel(string text)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(text);
            Console.ResetColor();
        }

        protected virtual void WriteValue()
        {
            WriteInput(FitString(Value + " ", Width, CursorPosition));
        }

        protected override void WriteContent()
        {
            if (ShowLabel)
            {
                WriteLabel(Label);
                Console.SetCursorPosition(Left, Top + 1);
            }
            WriteValue();
        }

        public virtual void Input()
        {
            CursorPosition = Value.Length - 1;
            while (!ProcessKey(Console.ReadKey()));
        }

        protected virtual bool IsCharAccepted(char chr)
        {
            return chr >= ' ';
        }

        protected virtual void ProcessChar(char chr)
        {
            if (IsCharAccepted(chr))
            {
                if (CursorPosition < Value.Length - 1)
                {
                    Value = Value.Insert(CursorPosition, chr.ToString());
                }
                else
                {
                    Value += chr;
                }
                CursorPosition++;
            }
        }

        public override bool ProcessKey(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.Delete)
            {
                if (CursorPosition < Value.Length)
                {
                    Value = Value.Remove(CursorPosition, 1);
                }
            }
            else if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (CursorPosition > 0)
                {
                    Value = Value.Remove(--CursorPosition, 1);
                }
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                CursorPosition--;
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                CursorPosition++;
            }
            else if (keyInfo.Key == ConsoleKey.Home)
            {
                CursorPosition = 0;
            }
            else if (keyInfo.Key == ConsoleKey.End)
            {
                CursorPosition = Value.Length - 1;
            }
            else
            {
                ProcessChar(keyInfo.KeyChar);
            }
            return true;
        }
    }
}
