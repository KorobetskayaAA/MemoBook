using System;
using System.ComponentModel;

namespace MemoBookLibrary
{
    /// <summary>
    /// Этап выполнения
    /// </summary>
    public enum Phase
    {
        [Description("Сделать")]
        ToDo,
        [Description("В работе")]
        Doing,
        [Description("Готово")]
        Done,
    }
}
