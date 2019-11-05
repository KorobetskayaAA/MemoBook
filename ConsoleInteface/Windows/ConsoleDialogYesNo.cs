using System;
using ConsoleInterface.Controls;

namespace ConsoleInterface
{
    /// <summary>
    /// Диалоговое окно для подтверждения или отмены действия.
    /// Содержит вопрос и кнопки с вариантами ДА/НЕТ.
    /// </summary>
    public class ConsoleDialogYesNo : ConsoleDialog
    {
        public ConsoleDialogYesNo(string header, string message)
            : base(header, new[] { Button.Yes, Button.No })
        {
            this.Message = new TextBlock(ContentLeft, ContentTop, ContentWidth, message);
            ContentWidth = Message.Width;
            Message.Left = ContentLeft;
            Controls.Insert(0, Message);
            FitButtons();
            ActiveControl = 1;
        }

        readonly TextBlock Message;

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
