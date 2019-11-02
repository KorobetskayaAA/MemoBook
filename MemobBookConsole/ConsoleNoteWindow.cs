using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoBookLibrary;
using ConsoleInterface;
using ConsoleInterface.Controls;

namespace MemoBookConsole
{

    //Тема:
    //#################################################
    //Выполнить до:
    //<01>.<01>.<2000> <00>:<00>
    //Описание:
    //#################################################
    //#################################################
    //#################################################
    //#################################################
    //Важность:      Этап:          Цвет:
    //<Нужно   >     <В работе   >  <##DarkGreen >
    //          OK                  Отмена

    
    /// <summary>
    /// Окно для редактирования заметки
    /// </summary>
    class ConsoleNoteWindow : ConsoleDialog
    {
        /// <summary>
        /// Заметка, с которой работаем.
        /// </summary>
        public Memo Memo { get; }

        /// <summary>
        /// Конструктор для редактирования существующей заметки
        /// </summary>
        /// <param name="memo">Заметка, которую будем редактировать</param>
        public ConsoleNoteWindow(Memo memo)
            : base("Заметка №" + memo.Id, new Button[] { Button.OK, Button.Cancel })
        {
            Memo = memo;
            InitializeControls();
        }

        /// <summary>
        /// Конструктор для создания новой заметки и ввода ее свойств.
        /// </summary>
        public ConsoleNoteWindow()
            : base("Новая заметка", new Button[] { Button.OK, Button.Cancel })
        {
            Memo = new Memo();
            InitializeControls();
        }

        // Элементы управления для ввода и редактирования
        InputControl inputHeader;
        InputDateTime inputDeadline;
        InputTextArea inputText;
        const int textHeight = 5;
        InputList inputPriority;
        InputList inputPhase;
        InputColor inputColor;

        /// <summary>
        /// Инициализация компонентов и их размещение на форме.
        /// </summary>
        protected override void InitializeControls()
        {
            // Этот метод может вызываться несколько раз, но сработает только после 
            // связывания с Memo
            if (Memo == null)
            {
                return;
            }
            // Фиксированный размер окна
            this.ContentWidth = 50;
            this.ContentHeight = 5 + textHeight + 3;

            inputHeader = new InputControl("Тема:",
                ContentLeft, ContentTop, ContentWidth)
            {
                Value = Memo.Header
            };
            Controls.Add(inputHeader);

            inputDeadline = new InputDateTime("Выполнить до:",
                ContentLeft, inputHeader.Bottom + 1)
            {
                Value = Memo.Deadline
            };
            Controls.Add(inputDeadline);
            Controls.Add(inputDeadline.Day);
            Controls.Add(inputDeadline.Month);
            Controls.Add(inputDeadline.Year);
            Controls.Add(inputDeadline.Hour);
            Controls.Add(inputDeadline.Minute);

            inputText = new InputTextArea("Описание:",
                ContentLeft, inputDeadline.Bottom + 1, ContentWidth, textHeight)
            {
                Value = Memo.Text
            };
            Controls.Add(inputText);

            inputPriority = new InputList("Важность:",
                EnumExtenders.GetDescriptions<Priority>(),
                ContentLeft, inputText.Bottom + 1, 
                2 + EnumExtenders.GetDescriptions<Priority>().Select(s => s.Length).Max()
            )
            {
                SelectedItem = (int)Memo.Priority
            };
            Controls.Add(inputPriority);

            inputPhase = new InputList("Этап:",
                EnumExtenders.GetDescriptions<Phase>(),
                ContentLeft + 15, inputPriority.Top,
                2 + EnumExtenders.GetDescriptions<Phase>().Select(s => s.Length).Max()
            )
            {
                SelectedItem = (int)Memo.Phase
            };
            Controls.Add(inputPhase);

            inputColor = new InputColor("Цвет:",
                ContentLeft + 30, inputPriority.Top,
                4 + EnumExtenders.GetDescriptions<ConsoleColor>().Select(s => s.Length).Max()
            )
            {
                Color = ConsoleHelper.RgbToCosoleColor(Memo.Color)
            };
            Controls.Add(inputColor);

            base.InitializeControls();
        }

        /// <summary>
        /// При закртыии окна, если подтвержден ввод,
        /// переносим данные из элементов управления в Memo.
        /// </summary>
        protected override void OnClose()
        {
            if (ModalResult == Button.OK)
            {
                Memo.Header = inputHeader.Value;
                Memo.Deadline = inputDeadline.Value;
                Memo.Text = inputText.Value;
                Memo.Priority = (Priority)inputPriority.SelectedItem;
                Memo.Phase = (Phase)inputPhase.SelectedItem;
                Memo.Color = ConsoleHelper.CosoleColorToRgb(inputColor.Color);
            }
        }
    }
}
