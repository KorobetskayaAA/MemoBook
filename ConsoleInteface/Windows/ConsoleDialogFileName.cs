using System;
using ConsoleInterface.Controls;


namespace ConsoleInterface
{
    /// <summary>
    /// Диалогоговое окно для выбора файла.
    /// </summary>
    public class ConsoleDialogFileName : ConsoleDialog
    {
        public ConsoleDialogFileName(string header)
            : base(header, new[] { Button.OK, Button.Cancel })
        {
            this.inputFileName = new InputFileName("Полный путь к файлу:",
                ContentLeft, ContentTop, ContentWidth - 1);
            Controls.Insert(0, inputFileName);
            foreach (var control in Controls)
            {
                control.Left = Left + 1;
            }
            ActiveControl = 0;
        }

        InputFileName inputFileName;

        public string FileName
        {
            get => inputFileName.Value;
        }

        public new bool ShowModal()
        {
            return base.ShowModal() == Button.OK;
        }
    }
}
