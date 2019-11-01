using System.IO;

namespace ConsoleInterface.Controls
{
    public class InputFileName: InputControl
    {
        public InputFileName(string label, int left, int top, int width)
            : base(label, left, top, width)
        {
            this.Value = Directory.GetCurrentDirectory() + "\\";
            this.CursorPosition = this.Value.Length;
        }
    }
}
