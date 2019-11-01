using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterface.Controls
{
    public class InputCheck: InputControl
    {
        public bool Checked { get; set; }

        protected override void WriteValue()
        {
            Console.Write((Checked ? "+" : "-") + " ");
            Console.Write(FitString(Value, Width - 2, CursorPosition));
        }

        public InputCheck(string label, int left, int top, int width)
            : base(label, left, top, width)
        {
            Checked = false;
        }
    }
}
