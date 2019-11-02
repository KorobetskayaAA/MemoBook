using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterface.Controls
{
    /// <summary>
    /// Таблица с данными. Подгрузка данных осуществляется через делегаты для каждого столбца.
    /// </summary>
    public class DataGrid : Control
    {
        public DataGrid(int left, int top, int width, int height) :
            base(left, top, width)
        {
            Columns = new List<Column>();
            Height = height;
        }

        public int SelectedRow { get; set; }
        public int SelectedColumn { get; set; }
        public int SortedColumn { get; set; } = -1;
        public bool SortDecreasing { get; set; }

        public IEnumerable<Object> Data { get; set; }

        public int RowsCount { get => Data == null ? 0 : Data.Count(); }
        public int VisibleRowsCount
        {
            get => RowsCount < Height - 1 ? RowsCount : Height - 1;
        }
        public int FirstVisibleRow
        {
            get => SelectedRow < VisibleRowsCount ? 0 : SelectedRow - VisibleRowsCount + 1;
        }
        public int LastVisibleRow
        {
            get => FirstVisibleRow + VisibleRowsCount;
        }

        /// <summary>
        /// Настройки столбца таблицы.
        /// </summary>
        public struct Column
        {
            public string Header;
            public string Name;
            public Func<Object, string> GetData;
            public int Width;

            public string Format(int i)
            {
                int w = Width;
                if (w < 0)
                {
                    w = AutoWidth;
                }
                return "{" + i + ",-" + w + "}";
            }

            public string GetFormattedData(Object obj)
            {
                return string.Format(Format(0), GetData(obj));
            }

            static public int AutoWidth = 1;
        }

        public List<Column> Columns { get; }

        int[] ColumnWidths { get => Columns.Select(col => col.Width).ToArray(); }

        void SetAutoWidths()
        {
            int[] AutoColumns = ColumnWidths.Where(wid => wid < 0).ToArray();
            int autoWidthCount = AutoColumns.Count();
            if (autoWidthCount > 0)
            {
                int autoWidth = Console.WindowWidth - 1;
                autoWidth -= ColumnWidths.Where(wid => wid > 0).Sum();
                autoWidth /= autoWidthCount;
                Column.AutoWidth = autoWidth;
            }
        }

        void WriteColumnHeaders()
        {
            for (int j = 0; j < Columns.Count; j++)
            {
                string format = Columns[j].Format(0);
                string header = Columns[j].Header;
                if (j == SortedColumn)
                {
                    header = (SortDecreasing ? "↓" : "↑") + header;
                }
                if (j == SelectedColumn)
                {
                    WriteFocused(string.Format(format, header));
                }
                else
                {
                    WriteUnfocused(string.Format(format, header));
                }
            }
        }

        public string EmptyMessage { get; set; } = "Нет данных";

        protected virtual void WriteRow(int index)
        {
            string row = string.Join("",
                Columns.Select(col => col.GetFormattedData(Data.ElementAt(index)))
            );

            if (index == SelectedRow)
            {
                ConsoleHelper.WriteColorInverted(row);
            }
            else
            {
                Console.Write(row);
            }
        }

        protected override void WriteContent()
        {
            SetAutoWidths();
            WriteColumnHeaders();

            if (Data == null || RowsCount == 0)
            {
                Console.CursorLeft = Left;
                Console.CursorTop++;
                Console.Write(EmptyMessage);
                return;
            }

            for (int i = FirstVisibleRow; i < LastVisibleRow; i++)
            {
                Console.CursorLeft = Left;
                Console.CursorTop++;
                WriteRow(i);
            }

            if (VisibleRowsCount < RowsCount)
            {
                PrintVerticalScrollBar((double)SelectedRow / (RowsCount - 1),
                    Height - 1, Right, Top + 1);
            }
        }

        public override bool ProcessKey(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow: MoveByColumns(-1); break;
                case ConsoleKey.RightArrow: MoveByColumns(1); break;
                case ConsoleKey.UpArrow: MoveByRows(-1); break;
                case ConsoleKey.DownArrow: MoveByRows(1); break;
                case ConsoleKey.PageUp: MoveByRows(-VisibleRowsCount); break;
                case ConsoleKey.PageDown: MoveByRows(VisibleRowsCount); break;
                case ConsoleKey.Home: SelectFirst(); break;
                case ConsoleKey.End: SelectLast(); break;
                default: return false;
            }
            return true;
        }

        void MoveByRows(int i)
        {
            i = i % RowsCount;
            SelectedRow = (RowsCount + SelectedRow + i) % RowsCount;
        }

        void MoveByColumns(int j)
        {
            j = j % Columns.Count;
            SelectedColumn = (Columns.Count + SelectedColumn + j) % Columns.Count;
        }

        public void SelectFirst()
        {
            SelectedRow = 0;
        }

        public void SelectLast()
        {
            SelectedRow = RowsCount - 1;
        }

        public void OnSort()
        {
            if (SortedColumn == SelectedColumn)
            {
                SortDecreasing = !SortDecreasing;
            }
            else
            {
                SortDecreasing = false;
            }
            SortedColumn = SelectedColumn;
        }
    }
}
