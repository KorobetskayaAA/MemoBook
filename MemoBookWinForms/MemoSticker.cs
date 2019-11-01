using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoBookLibrary;

namespace MemoBookWinForms
{
    public partial class MemoSticker : UserControl
    {
        Memo myMemo;

        void LoadMemo()
        {
            textBoxHeader.Text = myMemo.Header;
            textBoxDescription.Text = myMemo.Text;
            dateTimePickerDeadLine.Value = myMemo.Deadline;
            CheckedListBox
        }

        public MemoSticker(Memo memo)
        {
            InitializeComponent();
            myMemo = memo;
        }
    }
}
