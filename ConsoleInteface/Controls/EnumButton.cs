using System;
using System.ComponentModel;

namespace ConsoleInterface.Controls
{
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