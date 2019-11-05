using System;
using ConsoleInterface.Controls;

namespace ConsoleInterface
{
    /// <summary>
    /// Консольное диалоговое псевдоокно для вывода информационных сообщений.
    /// Содержит текст сообщения и кнопку ОК.
    /// </summary>
    public class ConsoleMessageDialog : ConsoleDialog
    {
        public ConsoleMessageDialog(string header, string message, Button[] buttons)
            : base(header, buttons)
        {
            Message = new TextBlock(ContentLeft, ContentTop, ContentWidth, message);
            Controls.Insert(0, Message);
            ContentHeight = Message.Height + 2;
            ContentWidth = Message.Width;
            Message.Top = ContentTop;
            Message.Left = ContentLeft;
            FitButtons();
            ActiveControl = 1;
        }

        readonly TextBlock Message;

        public Alignment Align { get => Message.Align; set => Message.Align = value; }

        public override int ContentWidth
        {
            get
            {
                if (Message != null && Message.Width > 0)
                    return Message.Width;
                return base.ContentWidth;
            }
        }

        public new bool ShowModal()
        {
            return base.ShowModal() == Button.Yes;
        }
    }
}
