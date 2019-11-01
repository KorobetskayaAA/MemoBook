using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using System.Reflection;

/// В отдельной записи должно быть не менее пяти полей

namespace MemoBookLibrary
{
    /// <summary>
    /// Заметка в ежедневнике о какой-либо задаче. 
    /// Задается привязкой ко времени, описанием, цетом, важностью и этапом выполнения.
    /// </summary>
    public class Memo
    {
        /// <summary>
        /// Конструктор со значениями по умолчанию.
        /// </summary>
        public Memo()
        {
            this.Id = GetNextId();
            this.CreationDate = DateTime.Now;
            this.Deadline = DateTime.Now.AddDays(1);
            this.Header = "";
            this.Text = "";
            this.Priority = 0;
            this.Phase = 0;
            this.Color = DEFAULT_COLOR;
        }

        /// <summary>
        /// Конструктор за заданным сроком выполнения и темой заметки.
        /// </summary>
        /// <param name="deadline">Срок выполнения</param>
        /// <param name="header">Тема (краткое описание)</param>
        public Memo(DateTime deadline, string header) : this()
        {
            this.Deadline = deadline;
            this.Header = header;
        }

        /// <summary>
        /// Используется для уникальных идентификаторов заметок в пределах сессии.
        /// </summary>
        static private int nextId = 1;

        /// <summary>
        /// Выдает следующий идентификатор заметки.
        /// </summary>
        /// <returns>Id для новой заметки</returns>
        static private int GetNextId()
        {
            return nextId++;
        }

        /// <summary>
        /// Уникальный идентификатор заметки.
        /// </summary>
        [DisplayName("Id")]
        public int Id { get; set; }
        /// <summary>
        /// Дата и время создания заметки (справочно, нередактируемое).
        /// </summary>
        [DisplayName("Создано")]
        public DateTime CreationDate { get; }

        /// <summary>
        /// Тема - краткое описание заметки.
        /// </summary>
        [DisplayName("Тема")]
        public string Header { get; set; }
        /// <summary>
        /// Срок выполнения.
        /// </summary>
        [DisplayName("Срок")]
        public DateTime Deadline { get; set; }
        /// <summary>
        /// Развернутое описание.
        /// </summary>
        [DisplayName("Описание")]
        public string Text { get; set; }
        /// <summary>
        /// Уровень важности.
        /// </summary>
        [DisplayName("Важность")]
        public Priority Priority { get; set; }
        /// <summary>
        /// Этап выполнения.
        /// </summary>
        [DisplayName("Этап")]
        public Phase Phase { get; set; }

        /// <summary>
        /// Цвет, которым заметка отображается в ежедневнике.
        /// </summary>
        [DisplayName("Цвет"),XmlIgnore]
        public Color Color { get; set; }
        /// <summary>
        /// Цвет заметки по умолчанию - серый.
        /// </summary>
        public static readonly Color DEFAULT_COLOR = Color.Gray;
        /// <summary>
        /// RGB-представление цвета. Необходимо для сохранения в файл.
        /// </summary>
        [DisplayName("Цвет")]
        public string ColorName
        {
            get
            {
                return Color.ToArgb().ToString();
            }
            set
            {
                Color = Color.FromArgb(int.Parse(value));
            }
        }

        /// <summary>
        /// Доступ к полям заметки по текстовому имени поля.
        /// </summary>
        /// <param name="propertyName">Имя поля (регистрозависимое)</param>
        /// <returns>Возвращает значение поля (null, если не найдено)</returns>
        public Object this[string propertyName]
        {
            get
            {
                Type myType = typeof(Memo);
                PropertyInfo fieldInfo = myType.GetRuntimeProperty(propertyName);
                return fieldInfo.GetValue(this);
            }
        }

        /// <summary>
        /// Проверяет, попадает ли заметка в указанный временной диапазон,
        /// включая начало и конец.
        /// </summary>
        /// <param name="from">Начало диапазона</param>
        /// <param name="to">Конец диапазона</param>
        /// <returns></returns>
        public bool IsInDateRange(DateTime from, DateTime to)
        {
            return Deadline >= from && Deadline <= to;
        }
    }
}

