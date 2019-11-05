using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleInterface.Controls;

namespace ConsoleInterface
{
    /// <summary>
    /// Псевдоокно в консольном интерфейсе.
    /// Отрисовывается в рамке, содержит заголовок и набор элементов управления.
    /// Отлавливает нажатие кнопок клавиатуры, пока не будет закрыто.
    /// </summary>
    public class ConsoleWindow
    {
        public ConsoleWindow(string header)
        {
            Header = header;
            this.Centered = true;
            this.ContentWidth = Console.WindowWidth - 6;
            this.ContentHeight = 5;
            Controls = new List<Control>();
        }

        public bool Centered { get; set; }
        int top = 0;
        public int Top
        {
            get
            {
                if (Centered)
                {
                    return (Console.WindowHeight - Height) / 2;
                }
                else
                {
                    return top;
                }
            }
            set
            {
                top = value;
            }
        }

        int left = 0;
        public int Left
        {
            get
            {
                if (Centered)
                {
                    return (Console.WindowWidth - Width) / 2;
                }
                else
                {
                    return left;
                }
            }
            set
            {
                left = value;
            }
        }

        public int Bottom
        {
            get { return Top + Height; }
        }

        public int Right
        {
            get { return Left + Width; }
        }

        public int Width { get { return ContentWidth + 3; } }
        public int Height { get { return ContentHeight + HeaderHeight + 3; } }

        public virtual int ContentTop { get { return Top + HeaderHeight + 2; } }
        public virtual int ContentLeft { get { return Left + 1; } }
        public virtual int ContentWidth { get; set; }
        public virtual int ContentHeight { get; set; }

        public int HeaderHeight
        {
            get
            {
                return Header.Length / ContentWidth + (Header.Length % ContentWidth == 0 ? 0 : 1);
            }
        }

        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        string[] GetFrame()
        {
            //╔═╗╚╝║╟─╢
            string dl = new string('═', ContentWidth);
            string sl = new string('─', ContentWidth);
            string el = "║" + new string(' ', ContentWidth) + "║";
            string[] frame = new string[3 + HeaderHeight + ContentHeight];
            frame[0] = "╔" + dl + "╗";
            for (int i = 1; i <= HeaderHeight; i++)
            {
                frame[i] = el;
            }
            frame[HeaderHeight + 1] += "╟" + sl + "╢";
            for (int i = 1; i <= ContentHeight; i++)
            {
                frame[HeaderHeight + 1 + i] += el;
            }
            frame[frame.Length - 1] += "╚" + dl + "╝";
            return frame;
        }

        void DrawFrame()
        {
            string[] frame = GetFrame();
            Console.CursorTop = Top;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < frame.Length; i++)
            {
                Console.CursorLeft = Left;
                Console.Write(frame[i]);
                Console.CursorTop++;
            }
            Console.ResetColor();
        }

        public string Header { get; set; }

        void PrintHeader()
        {
            Console.CursorLeft = Left + 1;
            Console.CursorTop = Top + 1;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            ConsoleHelper.WriteCentered(Header, Console.CursorLeft, ContentWidth);
            Console.ResetColor();
        }

        protected List<Control> Controls { get; }

        int activeControl;
        protected int ActiveControl
        {
            get { return activeControl; }
            set
            {
                Controls[activeControl].Selected = false;
                activeControl = value;
                Controls[activeControl].Selected = true;
            }
        }

        protected virtual void PrintContent()
        {
            Console.CursorTop = ContentTop;
            Console.CursorLeft = ContentLeft;
            foreach (var control in Controls)
            {
                control.Print();
            }
        }

        public bool Closed { get; set; }

        protected void SelectControl(int shift)
        {
            do
            {
                ActiveControl = (Controls.Count + ActiveControl + shift) % Controls.Count;
            } while (!Controls[ActiveControl].Selectable);
        }


        protected virtual void ProcessKey(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.Escape:
                    Closed = true;
                    break;
                case ConsoleKey.Tab:
                    if (keyInfo.Modifiers == ConsoleModifiers.Shift)
                    {
                        SelectControl(-1);
                    }
                    else
                    {
                        SelectControl(+1);
                    }
                    break;
                default:
                    Controls[ActiveControl].ProcessKey(keyInfo);
                    break;
            }
        }

        public void Show()
        {
            Closed = false;
            do
            {
                Console.CursorVisible = false;
                DrawFrame();
                PrintHeader();
                PrintContent();
                Controls[ActiveControl].SetCursorPosition();
                ProcessKey(Console.ReadKey());
            } while (!Closed);
            OnClose();
        }

        protected virtual void OnClose()
        {
            // do nothing
        }
    }
}
