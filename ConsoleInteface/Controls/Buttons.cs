using System;

namespace ConsoleInterface.Controls
{
    public class Buttons : Control
    {

        public Buttons(int left, int top, int width, Button[] buttons)
            : base(left, top, width)
        {
            this.buttons = buttons;
            SelectedIndex = 0;
            PressedIndex = -1;
        }

        Button[] buttons { get; }
        int SelectedIndex { get; set; }
        int PressedIndex { get; set; }

        public Button this [int i]
        {
            get { return buttons[i];  }
        }

        public int Count { get { return buttons.Length; } }

        public Button SelectedButton
        {
            get
            {
                if (SelectedIndex < 0 || SelectedIndex >= Count)
                    return Button.None;
                return buttons[SelectedIndex];
            }
        }

        public Button PressedButton
        {
            get
            {
                if (PressedIndex < 0 || PressedIndex >= Count)
                    return Button.None;
                return buttons[PressedIndex];
            }
        }

        const int ButtonWidth = 12;

        void WriteButton(int i)
        {
            string caption = buttons[i].GetDescription();
            int leftPadding = (ButtonWidth - caption.Length) / 2 + (ButtonWidth - caption.Length) % 2;
            int rightPadding = ButtonWidth - leftPadding - caption.Length;
            caption = new string(' ', leftPadding) + caption +
                    new string(' ', rightPadding);
            if (base.Selected && i == SelectedIndex)
            {
                WriteFocused(caption);
            }
            else
            {
                WriteUnfocused(caption);
            }
        }

        protected override void WriteContent()
        {
            int margin = (Width - ButtonWidth * buttons.Length) / buttons.Length / 2;

            for (int i = 0; i < buttons.Length; i++)
            {
                Console.CursorLeft += margin;
                WriteButton(i);
                Console.CursorLeft += margin;
            }
        }

        public override bool ProcessKey(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    SelectedIndex = (Count + SelectedIndex - 1) % Count;
                    return false;
                case ConsoleKey.RightArrow:
                    SelectedIndex = (SelectedIndex + 1) % Count;
                    return false;
                case ConsoleKey.Enter:
                    PressedIndex = SelectedIndex;
                    return true;
            }
            return false;
        }
    }
}
