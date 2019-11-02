using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterface.Controls
{
    /// <summary>
    /// Таблица данных, в которой каждой строке задан цвет.
    /// </summary>
    public class ColoredDataGrid : DataGrid
    {
        public ColoredDataGrid(int left, int top, int width, int height) :
            base(left, top, width, height)
        {
        }

        public Func<Object, ConsoleColor> GetRowColor;

        protected override void WriteRow(int index)
        {
            if (GetRowColor != null)
            {
                Console.ForegroundColor = GetRowColor(Data.ElementAt(index));
            }
            base.WriteRow(index);
            Console.ResetColor();
        }

    }
}
