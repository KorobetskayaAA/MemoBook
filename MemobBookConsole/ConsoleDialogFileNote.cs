using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleInterface;
using ConsoleInterface.Controls;

namespace MemoBookConsole
{
    /// <summary>
    /// Окно для выбора файла для загрузки/сохранения, и, при необходимости, -
    /// диапазона сроков записей.
    /// </summary>
    class ConsoleDialogFileNote : ConsoleDialogFileName
    {
        public ConsoleDialogFileNote(string header) :
            base(header)
        {
            ContentHeight += 3;
            // Добавляем к обычному диалогу выбора файла
            // контролы для выбора даты и времени.
            Controls[0].Top = ContentTop;
            InputHasDateRange = new InputList("Ограничить диапазон дат:",
                new List<string>() { "Нет", "Да" },
                ContentLeft, Controls[0].Bottom + 1, 3);
            InputHasDateRange.SelectedItem = 1;
            Controls.Add(InputHasDateRange);

            InputFrom = new InputDateTime("Начальная дата:",
                ContentLeft, InputHasDateRange.Bottom + 1);
            InputFrom.Hour.Value = 0; InputFrom.Minute.Value = 0;
            Controls.Add(InputFrom);
            Controls.Add(InputFrom.Day);
            Controls.Add(InputFrom.Month);
            Controls.Add(InputFrom.Year);
            Controls.Add(InputFrom.Hour);
            Controls.Add(InputFrom.Minute);

            InputTo = new InputDateTime("Конечная дата:",
                InputFrom.Right + 4, InputFrom.Top);
            InputTo.Hour.Value = 23; InputTo.Minute.Value = 59;
            Controls.Add(InputTo);
            Controls.Add(InputTo.Day);
            Controls.Add(InputTo.Month);
            Controls.Add(InputTo.Year);
            Controls.Add(InputTo.Hour);
            Controls.Add(InputTo.Minute);

            // Перетаскиваем кнопки в низ списка контролов.
            Controls[1].Top = Bottom - 2;
            Controls.Add(Controls[1]);
            Controls.RemoveAt(1);
        }

        // Контролы для выбора даты и времени
        InputList InputHasDateRange { get; }
        InputDateTime InputFrom { get; }
        InputDateTime InputTo { get; }

        /// <summary>
        /// Нужно фильтровать заметки по диапазону срока.
        /// </summary>
        public bool HasDateRange { get => InputHasDateRange.SelectedItem > 0; }
        /// <summary>
        /// Начало диапазона дат
        /// </summary>
        public DateTime From { get => InputFrom.Value; }
        /// <summary>
        /// Конец диапазона дат
        /// </summary>
        public DateTime To { get => InputTo.Value; }

        // Отключаем выбор дат, если пользователь отметил, что они не нужны.
        protected override void ProcessKey(ConsoleKeyInfo keyInfo)
        {
            base.ProcessKey(keyInfo);
            InputFrom.Disabled = InputHasDateRange.SelectedItem == 0;
            InputTo.Disabled = InputFrom.Disabled;
        }

    }
}
