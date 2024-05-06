using System.Collections.Generic;
using Game.Menu;

namespace GameWPF.Menu.View
{
    /// <summary>
    /// Представление экрана меню
    /// </summary>
    public class MenuScreenView : ScreenView
    {

        /// <summary>
        /// Представления пунктов меню
        /// </summary>
        private List<MenuPointView> _menuPointviews = new List<MenuPointView>();


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parMenuScreen">Экран меню</param>
        public MenuScreenView(MenuScreen parMenuScreen) : base(parMenuScreen)
        {
            for (int i = 0; i < parMenuScreen.Points.Count; i++)
            {
                _menuPointviews.Add(new MenuPointView(parMenuScreen.Points[i], i + 1));
            }
        }

        /// <summary>
        /// Отрисовать
        /// </summary>
        public override void Draw()
        {
            _textComponentsViews.ForEach(v => v.Draw());
            _menuPointviews.ForEach(v => v.Draw());
        }

        /// <summary>
        /// Перерисовать
        /// </summary>
        public void Redraw()
        {
            _menuPointviews.ForEach(v => v.Redraw());
        }
    }
}
