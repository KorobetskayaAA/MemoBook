using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterface.Controls
{
    /// <summary>
    /// Элемент управления для выбора значения из заданного диапазона.
    /// Работает по принципу списка.
    /// </summary>
    public class InputNumber : InputControl
    {
        public InputNumber(string label, int minValue, int maxValue, 
            int left, int top, int width)
          : base(label, left, top, width + 2)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public new int Value { get; set; }

        int minValue;
        public int MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                if (Value < minValue)
                    Value = minValue;
            }
        }

        int maxValue;
        public int MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                if (Value > maxValue)
                    Value = maxValue;
            }
        }

        protected override void WriteValue()
        {
            if (Disabled)
                WriteDisabled("<");
            else if (Selected)
                WriteFocused("<");
            else
                WriteUnfocused("<");

            WriteInput(string.Format("{0," + (Width - 2) + "}", Value));

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
                if (Value > MinValue)
                {
                    Value--;
                }
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                if (Value < MaxValue)
                {
                    Value++;
                }
            }
            return true;
        }
    }
}
