using System;
using System.ComponentModel;

namespace MemoBookLibrary
{
    /// <summary>
    /// Уровень важности замеки.
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