namespace Game.Menu
{
    /// <summary>
    /// Текстовый компонент
    /// </summary>
    public class TextComponent
    {

        /// <summary>
        /// Текст
        /// </summary>
        private string _text;

        /// <summary>
        /// Свойства для текста
        /// </summary>
        public string Text { get => _text; set => _text = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parText">Текст</param>
        public TextComponent(string parText)
        {
            _text = parText;
        }
    }
}
