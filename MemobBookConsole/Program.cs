using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoBookLibrary;

namespace MemoBookConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // инициализация
            MemoBookTUI mmbci;
            if (args.Length > 0)
            {
                mmbci = new MemoBookTUI(args[0]);
            }
            else
            {
                mmbci = new MemoBookTUI();
            }

            // обработка
            mmbci.Run();
        }
    }
}
