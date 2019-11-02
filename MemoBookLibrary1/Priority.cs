using System;
using System.ComponentModel;

namespace MemoBookLibrary
{
    /// <summary>
    /// Уровень важности заметки.
    /// </summary>
    public enum Priority {
        [Description("Важно")]
        Must,
        [Description("Нужно")]
        Should,
        [Description("Можно")]
        Could,
        [Description("Хочу")]
        Want
    }
}