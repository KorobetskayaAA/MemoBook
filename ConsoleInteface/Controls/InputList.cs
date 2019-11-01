using System;
using System.Collections.Generic;

namespace ConsoleInterface.Controls
{
    public class InputList : InputControl
    {
        public InputList(string label, List<string> items, int left, int top, int width)
           : base(label, left, top, width + 2)
        {
            this.Items = items;
        }

        public List<string> Items { get; set; }

        int selectedItem = 0;
        public int SelectedItem
        {
            get { return selectedItem; }
            set
            {
                value = value % Items.Count;
                selectedItem = value < 0 ? value + Items.Count : value;
            }
        }

        public override string Value
        {
            get
            {
                return Items[SelectedItem];
            }
        }

        protected virtual void WriteItem()
        {
            WriteInput(FitString(Value, Width - 2, 0));
        }

        protected override void WriteValue()
        {
            if (Disabled)
                WriteDisabled("<");
            else if (Selected)
                WriteFocused("<");
            else
                WriteUnfocused("<");
            WriteItem();
            if (Disabled)
                WriteDisabled(">");
            else if (Selected)
                WriteFocused(">");
            else
                WriteUnfocused(">");
        }

        public override bool ProcessKey(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                SelectedItem = (Items.Count + SelectedItem - 1) % Items.Count;
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                SelectedItem = (SelectedItem + 1) % Items.Count;
            }
            return true;
        }
    }
}
