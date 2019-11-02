using System.IO;

namespace ConsoleInterface.Controls
{
    /// <summary>
    /// Элемент управления для ввода имени файла.
    /// По умолчанию указывается директория, в которой находится приложение.
    /// </summary>
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
