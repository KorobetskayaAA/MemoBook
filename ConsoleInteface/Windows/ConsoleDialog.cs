using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleInterface.Controls;

namespace ConsoleInterface
{
    /// <summary>
    /// Консольное диалоговое псевдоокно, содержащее набор кнопок и способное
    /// возвращать нажатую пользователем кнопку.
    /// </summary>
    public class ConsoleDialog : ConsoleWindow
    {
        readonly Buttons buttons;
        public Button ModalResult { get; private set; } = Button.None;

        protected void FitButtons()
        {
            buttons.Top = Bottom - 2;
            buttons.Left = ContentLeft;
            buttons.Width = ContentWidth;
        }

        public ConsoleDialog(string header, Button[] buttons, int contentWidth = -1, int contentHeight = -1) 
            : base (header)
        {
            this.ContentWidth = contentWidth > 0 ? contentWidth : Console.WindowWidth - 6;
            this.ContentHeight = contentHeight > 0 ? contentHeight : 5;
            this.buttons = new Buttons(Bottom - 2, ContentLeft, ContentWidth, buttons);
            Controls.Add(this.buttons);
            FitButtons();
        }

        protected override void ProcessKey(ConsoleKeyInfo keyInfo)
        {
            base.ProcessKey(keyInfo);
            if (buttons.PressedButton != Button.None)
            {
                ModalResult = buttons.PressedButton;
                Closed = true;
            }
        }

        public Button ShowModal()
        {
            ActiveControl = Controls.IndexOf(buttons);
            Show();
            return ModalResult;
        }
    }
}
