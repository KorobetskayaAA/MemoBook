using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoBookLibrary;
using ConsoleInterface;
using ConsoleInterface.Controls;

namespace MemoBookConsole
{
    /// <summary>
    /// Текстовый пользовательский интерфейс для работы с записной книжкой.
    /// </summary>
    class MemoBookTUI
    {
        MemoBook memoBook;
        ColoredDataGrid memoGrid;

        public MemoBookTUI()
        {
            memoBook = new MemoBook();
            memoGrid = new ColoredDataGrid(0, 1, 
                Console.WindowWidth, Console.WindowHeight - 2);
            AssignDataGrid();
            AssignDefaultKeyOptions();
        }

        public MemoBookTUI(string fileName) : this()
        {
            memoBook.ImportFromFile(fileName);
        }

        struct KeyOption
        {
            //public ConsoleKey Key;
            public string KeyName;
            public string Description;
            public Action Action;

            public KeyOption(string keyName, string description, Action action)
            {
                KeyName = keyName;
                Description = description;
                Action = action;
            }
        }

        Dictionary<ConsoleKey, KeyOption> keyOptions = new Dictionary<ConsoleKey, KeyOption>();
        void AssignDefaultKeyOptions()
        {
            keyOptions.Add(ConsoleKey.F1, new KeyOption("F1", "Помоги", KeyActionHelp));
            keyOptions.Add(ConsoleKey.F2, new KeyOption("F2", "Измени", KeyActionEditNote));
            keyOptions.Add(ConsoleKey.F3, new KeyOption("F3", "Создай", KeyActionAddNote));
            keyOptions.Add(ConsoleKey.F4, new KeyOption("F4", "Удали", KeyActionDeleteNote));
            keyOptions.Add(ConsoleKey.F6, new KeyOption("F6", "Сортируй", KeyActionSort));
            keyOptions.Add(ConsoleKey.F7, new KeyOption("F7", "Сохрани", KeyActionSave));
            keyOptions.Add(ConsoleKey.F8, new KeyOption("F8", "Загрузи", KeyActionLoad));
            keyOptions.Add(ConsoleKey.Escape, new KeyOption("Esc", "Выйди", KeyActionExit));
        }

        void ShowMenuItem(KeyOption keyOption)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(keyOption.KeyName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(keyOption.Description);
        }

        void ShowMenu()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, 0);
            foreach (var keyOption in keyOptions.Values)
            {
                ShowMenuItem(keyOption);
                Console.CursorLeft++;
            }
            Console.ResetColor();
        }

        void AssignDataGrid()
        {
            memoGrid.Data = memoBook.Memos;
            memoGrid.Columns.AddRange(new List<DataGrid.Column>()
            {
                new DataGrid.Column {
                    Header = "№", Name = "Id",
                    GetData = mm => (mm as Memo).Id.ToString(),
                    Width = 3
                },
                new DataGrid.Column {
                    Header = "Этап", Name = "Phase",
                    GetData = mm => (mm as Memo).Phase.GetDescription(),
                    Width = 9
                },
                new DataGrid.Column {
                    Header = "Дедлайн", Name = "Deadline",
                    GetData = mm => string.Format("{0:dd.MM.yy HH:mm}", (mm as Memo).Deadline),
                    Width = 15
                },
                new DataGrid.Column {
                    Header = "Тема", Name = "Header",
                    GetData = mm => (mm as Memo).Header,
                    Width = -1
                },
                new DataGrid.Column {
                    Header = "Важность", Name = "Priority",
                    GetData = mm => (mm as Memo).Priority.GetDescription(),
                    Width = 9
                }
            });
            memoGrid.GetRowColor = mm => 
                ConsoleHelper.RgbToCosoleColor((mm as Memo).Color);

        }

        public void Run()
        {
            do
            {
                Console.CursorVisible = false;
                Console.Clear();
                ShowMenu();
                memoGrid.Data = memoBook.Memos;
                memoGrid.Print();
            } while (ProcessKey());
        }


        public bool ProcessKey()
        {
            ConsoleKeyInfo pressed = Console.ReadKey();
            ConsoleKey key = pressed.Key;

            if (pressed.Key == ConsoleKey.Escape)
            {
                return false;
            }
            else if (keyOptions.ContainsKey(pressed.Key))
            {
                keyOptions[key].Action();
            }
            else
            {
                memoGrid.ProcessKey(pressed);
            }
            return true;
        }

        void KeyActionExit()
        {
            //Do nothing
        }

        void KeyActionHelp()
        {
            var helpWindow = new ConsoleMessageDialog("Справка", 
                "В ежедневнике:\n" +
                "  Стрелки, Home, End, PageUp, PageDown - перемещение по таблице\n" +
                "  Другие клавиши указаны в меню\n" +
                "В окне:\n" +
                "  Tab, Shift+Tab - переключение элементов управления\n" +
                "  Вправо, Влево - выбор опции, в том числе активной кнопки\n" +
                "  Esc - закрыть окно\n" +
                "  Enter - нажать выбранную кнопку\n" +
                "",
                new[] { Button.OK });
            helpWindow.Align = Alignment.Left;
            helpWindow.Show();
        }

        void KeyActionAddNote()
        {
            memoBook.AddMemo(DateTime.Now.AddDays(1), "Заметка");
            memoGrid.SelectLast();
        }

        void KeyActionDeleteNote()
        {
            if (memoBook.Count == 0)
            {
                return;
            }
            if (memoBook.Count >= 0 &&
                DialogYesNo("Вы действительно хотите удалить эту заметку?\n" +
                            "Данное действие невозможно отменить."))
            {
                memoBook.DeleteMemo(memoBook[memoGrid.SelectedRow]);
                if (memoGrid.SelectedRow >= memoBook.Count)
                {
                    memoGrid.SelectedRow--;
                }
            }
        }

        void KeyActionEditNote()
        {
            if (memoBook.Count == 0)
            {
                return;
            }
            new ConsoleNoteWindow(memoBook[memoGrid.SelectedRow]).ShowModal();
        }

        void KeyActionSort()
        {
            memoGrid.OnSort();
            memoBook.SortBy(memoGrid.Columns[memoGrid.SelectedColumn].Name, 
                memoGrid.SortDecreasing);
        }

        void KeyActionSave()
        {
            try
            {
                var dialogSave = new ConsoleDialogFileNote("Сохранение заметок в файл");
                if (dialogSave.ShowModal())
                {
                    if (dialogSave.HasDateRange)
                    {
                        memoBook.ExportToFile(dialogSave.FileName, dialogSave.From, dialogSave.To);
                    }
                    else
                    {
                        memoBook.ExportToFile(dialogSave.FileName);
                    }
                }
            }
            catch
            {
                new ConsoleMessageDialog("Ошибка!", "Не удалось записать данные в файл", 
                    new ConsoleInterface.Controls.Button[] { ConsoleInterface.Controls.Button.OK }).Show();
            }
        }

        void KeyActionLoad()
        {
            try
            {
                if (memoBook.Count == 0 ||
                    DialogYesNo("Удалить существующие заметки перед загрузкой?\n" +
                                "Данное действие невозможно отменить."))
                {
                    var dialogLoad = new ConsoleDialogFileNote("Загрузка заметок из файла");
                    if (dialogLoad.ShowModal())
                    {
                        if (dialogLoad.HasDateRange)
                        {
                            memoBook.ImportFromFile(dialogLoad.FileName, dialogLoad.From, dialogLoad.To);
                        }
                        else
                        {
                            memoBook.ImportFromFile(dialogLoad.FileName);
                        }
                    }
                }
                else
                {
                    var dialogAppend = new ConsoleDialogFileNote("Добавление заметок из файла");
                    if (dialogAppend.ShowModal())
                    {
                        if (dialogAppend.HasDateRange)
                        {
                            memoBook.AppendFromFile(dialogAppend.FileName, dialogAppend.From, dialogAppend.To);
                        }
                        else
                        {
                            memoBook.AppendFromFile(dialogAppend.FileName);
                        }
                    }
                }
            }
            catch
            {
                new ConsoleMessageDialog("Ошибка!", "Не удалось открыть файл",
                    new ConsoleInterface.Controls.Button[] { ConsoleInterface.Controls.Button.OK }).Show();
            }
        }

        bool DialogYesNo(string message)
        {
            return new ConsoleDialogYesNo("Вопрос", message).ShowModal();
        }


    }
}
