﻿using System;
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
        Buttons buttons;
        public Button ModalResult { get; private set; } = Button.None;

        public ConsoleDialog(string header, Button[] buttons) 
            : base (header)
        {
            this.ContentWidth = Console.WindowWidth - 6;
            this.ContentHeight = 5;
            this.buttons =  new Buttons(Bottom - 2, ContentLeft, ContentWidth, buttons);
            Controls.Add(this.buttons);
            InitializeControls();
        }

        protected override void InitializeControls()
        {
            if (buttons == null)
            {
                return;
            }
            this.buttons.Top = Bottom - 2;
            this.buttons.Left = ContentLeft;
            this.buttons.Width = ContentWidth;
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
