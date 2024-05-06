namespace Game.Menu
{
    /// <summary>
    /// Пункт меню
    /// </summary>
    public class MenuPoint
    {

        /// <summary>
        /// Флаг выбранного пункта
        /// </summary>
        private bool _isSelected;

        /// <summary>
        /// Текст
        /// </summary>
        private string _text;

        /// <summary>
        /// Делегат обработки нажатия
        /// </summary>
        public delegate void dHandler();

        /// <summary>
        /// Обработчик
        /// </summary>
        private dHandler _handler;

        /// <summary>
        /// Свойство текста
        /// </summary>
        public string Text { get => _text; set => _text = value; }

        /// <summary>
        /// Свойство выбранности пункта меню
        /// </summary>
        public bool IsSelected { get => _isSelected; set => _isSelected = value; }

        /// <summary>
        /// Свойство делегата обработки
        /// </summary>
        public dHandler Handler { get => _handler; set => _handler = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parText">Текст</param>
        public MenuPoint(string parText)
        {   
            _text = parText;
            _isSelected = false;
        }
    }
}
