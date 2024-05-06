using System;
using System.Collections.Generic;
using Game.Game;
using Game.Menu;

namespace GameConsole.Menu.View
{

    /// <summary>
    /// Представление меню экрана
    /// </summary>
    public class MenuScreenView : ScreenView
    {
        /// <summary>
        /// Представления пунктов меню
        /// </summary>
        private readonly List<MenuPointView> _menuPointViews;

        /// <summary>
        /// Свойство представлений пунктов меню
        /// </summary>
        public List<MenuPointView> MenuPointViews => _menuPointViews;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parMenuScreen">Экран меню</param>
        public MenuScreenView(MenuScreen parMenuScreen) : base(parMenuScreen)
        {
            _menuPointViews = new List<MenuPointView>();
            int verticalSpacing = 1; 
            int topMargin = 1; // 

            for (int i = 0; i < parMenuScreen.Points.Count; i++)
            {
                _menuPointViews.Add(new MenuPointView(parMenuScreen.Points[i],
                    new Coordinates(1, topMargin + i * verticalSpacing)));
            }

            parMenuScreen.Drawer += Draw;
        }


        /// <summary>
        /// Отрисовка экрана
        /// </summary>
        public override void Draw()
        {
            MenuPointViews.ForEach(mpv => mpv.Draw());
        }
    }
}
