using System.Collections.Generic;

namespace Game.Menu
{
    /// <summary>
    /// Экран
    /// </summary>
    public class Screen
    {

        /// <summary>
        /// Заголовок
        /// </summary>
        private TextComponent _title;

        /// <summary>
        /// Список текстовых компонентов
        /// </summary>
        private List<TextComponent> _textComponents;

        /// <summary>
        /// Свойство заголовка
        /// </summary>
        public TextComponent Title { get => _title; set => _title = value; }

        /// <summary>
        /// Свойство списка текстовых компонентов
        /// </summary>
        public List<TextComponent> TextComponents { get => _textComponents; set => _textComponents = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parTitle">Заголовок</param>
        /// <param name="parRecordsTextComponents"></param>
        public Screen(TextComponent parTitle, List<TextComponent> parRecordsTextComponents)
        {
            _title = parTitle;
            _textComponents = parRecordsTextComponents;
        }

     
    }
}
