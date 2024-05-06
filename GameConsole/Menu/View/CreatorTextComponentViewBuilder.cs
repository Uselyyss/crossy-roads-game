using System;
using Game.Game;
using Game.Menu;

namespace GameConsole.Menu.View
{
    /// <summary>
    /// Создание отображения текстового компонента
    /// </summary>
    public class CreatorTextComponentViewBuilder : TextComponentViewBuilder
    {

        /// <summary>
        /// Экран
        /// </summary>
        private Screen _screen;


        /// <summary>
        /// Свойство для представления экрана
        /// </summary>
        public Screen Screen { get => _screen; }

        /// <summary>
        /// Установить текстовый компоненты
        /// </summary>
        /// <returns>Текстовый компонент</returns>
        public override TextComponent SetTextComponent()
        {
            return Screen.Title;
        }

        /// <summary>
        /// Установить координаты
        /// </summary>
        /// <returns>Координаты</returns>
        public override Coordinates SetCoord()
        {
            return new Coordinates(1, 3);
        }

        /// <summary>
        /// Установить цвет заднего фона 
        /// </summary>
        /// <returns>Цвет заднего фона</returns>
        public override int SetBackgroundColor()
        {
            return 50;
        }

        /// <summary>
        /// Установить цвет шрифта
        /// </summary>
        /// <returns>Цвет шрифта</returns>
        public override int SetFontColor()
        {
            return 0x00;
        }
    }
}
