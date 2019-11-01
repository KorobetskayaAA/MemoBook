using System;
using System.Linq;

namespace ConsoleInterface.Controls
{
    public class InputDateTime : InputControl
    {
        public InputDateTime(string label, int left, int top)
            : base(label, left, top, 10)
        {
            Day = new InputNumber("", 1, 31, left, top + 1, 2) {
                ShowLabel = false
            };

            Month = new InputList("", months.ToList(), 
                Day.Right + 2, top + 1, months.Select(m => m.Length).Max()) {
                ShowLabel = false
            };

            Year = new InputNumber("", 2000, 9999, Month.Right + 2, top + 1, 4) {
                ShowLabel = false
            };

            Hour = new InputNumber("", 0, 23, Year.Right + 2, top + 1, 2) {
                ShowLabel = false
            };

            Minute = new InputNumber("", 0, 59, Hour.Right + 2, top + 1, 2) {
                ShowLabel = false
            };

            Width = Minute.Right - left + 1;
            Value = DateTime.Now;
        }

        static readonly string[] months = { "январь", "февраль", "март", "апрель", "май",  
                "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };

        public override bool Selectable { get { return false; } }

        public override bool Disabled
        {
            get => base.Disabled;
            set
            {
                base.Disabled = value;
                foreach (var subcontrol in new Control[]
                                            { Day, Month, Year, Hour, Minute })
                {
                    subcontrol.Disabled = value;
                }
            }
        }

        public int GetMonthLength()
        {
            if (Month.SelectedItem == 1)
            {
                if (Year.Value % 4 == 0 && Year.Value % 100 != 0 ||
                    Year.Value % 400 == 0)
                {
                    return 29;
                }
                else
                {
                    return 28;
                }
            }
            if (new [] { 4, 6, 8, 9, 11 }.Contains(Month.SelectedItem + 1))
            {
                return 30;
            }
            else
            {
                return 31;
            }
        }

        public InputNumber Day { get; }
        public InputList Month { get; }
        public InputNumber Year { get; }
        public InputNumber Hour { get; }
        public InputNumber Minute { get; }

        public override int Top
        {
            get => base.Top;
            set
            {
                base.Top = value;
                if (Day != null)
                {
                    foreach (var subcontrol in new Control[] 
                                                { Day, Month, Year, Hour, Minute })
                    {
                        subcontrol.Top = value + 1;
                    }
                }
            }
        }

        public override int Left
        {
            get => base.Left;
            set
            {
                base.Left = value;
                if (Day != null)
                {
                    Day.Left = value;
                    Month.Left = Day.Right + 2;
                    Year.Left = Month.Right + 2;
                    Hour.Left = Year.Right + 2;
                    Minute.Left = Hour.Right + 2;
                }
            }
        }

        public new DateTime Value
        {
            get
            {
                return new DateTime(Year.Value, Month.SelectedItem + 1, Day.Value,
                    Hour.Value, Minute.Value, 0);
            }
            set
            {
                Day.Value = value.Day;
                Month.SelectedItem = value.Month - 1;
                Year.Value = value.Year;
                Hour.Value = value.Hour;
                Minute.Value = value.Minute;
            }
        }


        protected override void WriteValue()
        {
            Day.MaxValue = GetMonthLength();
            Day.Print();
            Console.Write(".");
            Month.Print();
            Console.Write(".");
            Year.Print();
            Console.Write(" ");
            Hour.Print();
            Console.Write(":");
            Minute.Print();
        }
    }
}
