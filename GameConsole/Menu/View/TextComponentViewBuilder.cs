using Game.Game;
using Game.Menu;

namespace GameConsole.Menu.View
{
    /// <summary>
    /// Абстрактный класс для построения отображения текстового компонента
    /// </summary>
    public abstract class TextComponentViewBuilder
    {
        /// <summary>
        /// Отображение текстового компонента
        /// </summary>
        public TextComponentView TextComponentView { get; private set; }

        /// <summary>
        /// Создание отображения текстового компонента
        /// </summary>
        public void CreateTextComponentView()
        {
            TextComponentView = new TextComponentView();
        }

        /// <summary>
        /// Установить текстовый компоненты
        /// </summary>
        /// <returns>Текстовый компонент</returns>
        public abstract TextComponent SetTextComponent();

        /// <summary>
        /// Установить координаты
        /// </summary>
        /// <returns>Координаты</returns>
        public abstract Coordinates SetCoord();

        /// <summary>
        /// Установить цвет заднего фона 
        /// </summary>
        /// <returns>Цвет заднего фона</returns>
        public abstract int SetBackgroundColor();

        /// <summary>
        /// Установить цвет шрифта
        /// </summary>
        /// <returns>Цвет шрифта</returns>
        public abstract int SetFontColor();
    }
}
