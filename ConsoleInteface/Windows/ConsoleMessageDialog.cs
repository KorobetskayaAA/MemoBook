using System;
using ConsoleInterface.Controls;

namespace ConsoleInterface
{
    public class ConsoleMessageDialog : ConsoleDialog
    {
        public ConsoleMessageDialog(string header, string message, Button[] buttons)
            : base(header, buttons)
        {
            this.Message = new TextBlock(ContentLeft, ContentTop, ContentWidth,
                message);
            InitializeControls();
        }

        protected override void InitializeControls()
        {
            if (Message == null)
            {
                return;
            }
            Controls.Add(Message);
            ContentHeight = Message.Height + 1;
            ContentWidth = Message.Width;
            Message.Top = ContentTop;
            base.InitializeControls();
            foreach (var control in Controls)
            {
                control.Left = Left + 1;
            }
            ActiveControl = 0;
        }

        TextBlock Message;

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
