using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

/// Разработать ежедневник.
/// В ежедневнике реализовать возможность 
/// - создания
/// - удаления
/// - реактирования 
/// записей
/// 
/// В отдельной записи должно быть не менее пяти полей
/// 
/// Реализовать возможность
/// - Загрузки даннах из файла
/// - Выгрузки даннах в файл
/// - Добавления данных в текущий ежедневник из выбранного файла
/// - Импорт записей по выбранному диапазону дат
/// - Упорядочивания записей ежедневника по выбранному полю


namespace MemoBookLibrary
{
    /// <summary>
    /// Ежедневник, состоящий из отдельных заметок.
    /// Поддерживает управление списком заметок, загрузку и сохранение в файл.
    /// </summary>
    public class MemoBook
    {
        List<Memo> memos = new List<Memo>();

        /// <summary>
        /// Список заметок.
        /// </summary>
        public List<Memo> Memos { get => memos; }

        public Memo this [int index]
        {
            get
            {
                return Memos[index];
            }
        }

        /// <summary>
        /// Количество заметок в ежедневнике.
        /// </summary>
        public int Count { get => memos.Count; }

        /// <summary>
        /// Добавить новую заметку в дневник.
        /// </summary>
        /// <param name="deadline">Срок выполнения</param>
        /// <param name="header">Тема (краткое описание)</param>
        public void AddMemo(DateTime deadline, string header)
        {
            memos.Add(new Memo(deadline, header));
        }

        /// <summary>
        /// Удалить указанную заметку из ежедневника.
        /// Удаление безвозвратное.
        /// </summary>
        /// <param name="memo">Заметка, которую необходимо удалить.</param>
        public void DeleteMemo(Memo memo)
        {
            memos.Remove(memo);
        }

        /// <summary>
        /// Удалить заметку по id.
        /// </summary>
        /// <param name="id">id заметки, которую требуется удалить.</param>
        public void DeleteMemo(uint id)
        {
            DeleteMemo(memos.Find(mm => mm.Id == id));
        }

        /// <summary>
        /// Полная очистка ежедневника.
        /// Все заметки будут безвозвратно удалены.
        /// </summary>
        public void Clear()
        {
            memos.Clear();
        }

        /// <summary>
        /// Удалить заметки, попадающие в заданный диапазон, включая границы.
        /// </summary>
        /// <param name="startDate">Начало диапазона</param>
        /// <param name="endDate">Конец диапазона</param>
        public void RemoveByDate(DateTime startDate, DateTime endDate)
        {
            memos.RemoveAll(mm => mm.IsInDateRange(startDate, endDate));
        }

        /// <summary>
        /// Сохранить все заметки в указанный файл.
        /// </summary>
        /// <param name="fileName">Путь и имя файла</param>
        public void ExportToFile(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Memo>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
               serializer.Serialize(fs, memos);
            }
        }

        /// <summary>
        /// Сохранить заметки в файл по диапазону дат.
        /// </summary>
        /// <param name="fileName">Путь и имя файла</param>
        /// <param name="startDate">Начало диапазона</param>
        /// <param name="endDate">Конец диапазона</param>
        public void ExportToFile(string fileName, DateTime startDate, DateTime endDate)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Memo>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(fs, 
                    memos.Where(mm => mm.IsInDateRange(startDate, endDate)));
            }
        }

        /// <summary>
        /// Десериализация списка заметок из файла.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        List<Memo> DeserializeFromFile(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Memo>));
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                return serializer.Deserialize(fs) as List<Memo>;
            }
        }

        /// <summary>
        /// Загрузка заметок из файла. Существующие заметки будут удалены.
        /// </summary>
        /// <param name="fileName">Путь и имя файла.</param>
        public void ImportFromFile(string fileName)
        {
            memos = DeserializeFromFile(fileName);
        }

        /// <summary>
        /// Загрузка из файла заметок за указанный диапазон времени. 
        /// Существующие заметки будут удалены.
        /// </summary>
        /// <param name="fileName">Путь и имя файла</param>
        /// <param name="from">Начало диапазона</param>
        /// <param name="to">Конец диапазона</param>
        public void ImportFromFile(string fileName, DateTime from, DateTime to)
        {
            memos = DeserializeFromFile(fileName).Where(mm =>
                                    mm.IsInDateRange(from, to)).ToList();
        }

        /// <summary>
        /// Загрузка заметок из файла. Загруженные заметки будут добавлены к существующим.
        /// </summary>
        /// <param name="fileName">Путь и имя файла.</param>
        public void AppendFromFile(string fileName)
        {
            memos.AddRange(DeserializeFromFile(fileName));
        }

        /// <summary>
        /// Загрузка заметок из файла за указанный диапазон времени. 
        /// Загруженные заметки будут добавлены к существующим.
        /// </summary>
        /// <param name="fileName">Путь и имя файла</param>
        /// <param name="from">Начало диапазона</param>
        /// <param name="to">Конец диапазона</param>
        public void AppendFromFile(string fileName, DateTime from, DateTime to)
        {
            var newMemos = DeserializeFromFile(fileName).Where(mm =>
                                    mm.IsInDateRange(from, to)).ToList();
            if (newMemos != null)
            {
                memos.AddRange(newMemos);
            }
        }

        /// <summary>
        /// Сортировка заметок по указанному полю.
        /// </summary>
        /// <param name="field">Имя поля, по которому нужно сортировать</param>
        /// <param name="descending">Сортировать ли по убыванию</param>
        public void SortBy(string field, bool descending = false)
        {
            if (descending)
            {
                memos = memos.OrderByDescending(mm => mm[field]).ToList();
            }
            else
            {
                memos = memos.OrderBy(mm => mm[field]).ToList();
            }
        }
    }
}
