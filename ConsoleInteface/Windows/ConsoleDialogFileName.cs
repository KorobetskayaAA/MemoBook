using System;
using ConsoleInterface.Controls;


namespace ConsoleInterface
{
    /// <summary>
    /// Консольное диалогоговое псвдоокно для выбора файла.
    /// </summary>
    public class ConsoleDialogFileName : ConsoleDialog
    {
        public ConsoleDialogFileName(string header)
            : base(header, new[] { Button.OK, Button.Cancel })
        {
            this.inputFileName = new InputFileName("Полный путь к файлу:",
                ContentLeft, ContentTop, ContentWidth - 1);
            Controls.Insert(0, inputFileName);
            FitButtons();
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
