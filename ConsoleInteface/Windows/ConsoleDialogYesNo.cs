using System;
using ConsoleInterface.Controls;

namespace ConsoleInterface
{
    public class ConsoleDialogYesNo : ConsoleDialog
    {
        public ConsoleDialogYesNo(string header, string message)
            : base(header, new[] { Button.Yes, Button.No })
        {
            this.Message = new TextBlock(ContentLeft, ContentTop, ContentWidth,
                message);
            Controls.Insert(0, Message);
            Controls[1].Width = Message.Width;
            foreach (var control in Controls)
            {
                control.Left = Left + 1;
            }
            ActiveControl = 1;
        }

        TextBlock Message;

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
