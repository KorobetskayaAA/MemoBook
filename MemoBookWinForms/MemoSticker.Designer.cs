namespace MemoBookWinForms
{
    partial class MemoSticker
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.textBoxHeader = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.dateTimePickerDeadLine = new System.Windows.Forms.DateTimePicker();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxHeader
            // 
            this.textBoxHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxHeader.Location = new System.Drawing.Point(3, 3);
            this.textBoxHeader.Multiline = true;
            this.textBoxHeader.Name = "textBoxHeader";
            this.textBoxHeader.Size = new System.Drawing.Size(216, 21);
            this.textBoxHeader.TabIndex = 6;
            this.textBoxHeader.Text = "Заголовок";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.AcceptsReturn = true;
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxDescription.Location = new System.Drawing.Point(3, 24);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(216, 31);
            this.textBoxDescription.TabIndex = 7;
            this.textBoxDescription.Text = "Текст\r\nзаметки";
            // 
            // dateTimePickerDeadLine
            // 
            this.dateTimePickerDeadLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.dateTimePickerDeadLine.Location = new System.Drawing.Point(3, 55);
            this.dateTimePickerDeadLine.Name = "dateTimePickerDeadLine";
            this.dateTimePickerDeadLine.Size = new System.Drawing.Size(216, 20);
            this.dateTimePickerDeadLine.TabIndex = 8;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(3, 75);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(216, 30);
            this.checkedListBox1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(193, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MemoSticker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.dateTimePickerDeadLine);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxHeader);
            this.Name = "MemoSticker";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(222, 108);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox textBoxHeader;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.DateTimePicker dateTimePickerDeadLine;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button1;
    }
}
