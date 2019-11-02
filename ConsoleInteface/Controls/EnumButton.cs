using System;
using System.ComponentModel;

namespace ConsoleInterface.Controls
{
    /// <summary>
    /// Варианты кнопок для диалоговых окон.
    /// </summary>
    public enum Button
    {
        None,
        [Description("OK")]
        OK,
        [Description("Отмена")]
        Cancel,
        [Description("Да")]
        Yes,
        [Description("Нет")]
        No
    }
}