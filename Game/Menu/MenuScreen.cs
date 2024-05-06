using System.Collections.Generic;

namespace Game.Menu
{
    /// <summary>
    /// Экран меню
    /// </summary>
    public class MenuScreen : Screen
    {

        /// <summary>
        /// Список пунктов меню
        /// </summary>
        private List<MenuPoint> _points;

        /// <summary>
        /// Выбранный пункт меню
        /// </summary>
        private int _currentMenuItem = 0;

        /// <summary>
        /// Делегат для изменения выбранного пункта меню
        /// </summary>
        public delegate void dDrawer();

        /// <summary>
        /// Событие изменения пункта меню
        /// </summary>
        public dDrawer Drawer;

        /// <summary>
        /// Свойство списка пунктов меню
        /// </summary>
        public List<MenuPoint> Points { get => _points; set => _points = value; }

        /// <summary>
        /// Свойство выбранного пункта меню
        /// </summary>
        public int CurrentMenuItem { get => _currentMenuItem; set => _currentMenuItem = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="points">Пункты меню</param>
        /// <param name="title">Заголовки меню</param>
        public MenuScreen(List<MenuPoint> parPoints, TextComponent parTitle) : base(parTitle, new List<TextComponent> { parTitle })
        {
            _points = parPoints;
        }

        /// <summary>
        /// Конструктор
        ///</summary>
        /// <param name="parTitle"></param>
        /// <param name="parMenuItems"></param>
        /// <param name="parTextComponents"></paramawqqq
        
        public MenuScreen(TextComponent parTitle, List<MenuPoint> parMenuItems, List<TextComponent> parTextComponents) :
           base(parTitle, parTextComponents)
        {
            _points = parMenuItems;
            _points[0].IsSelected = true;
        }

        /// <summary>
        /// Переход вверх по меню
        /// </summary>
        /// <returns>Номер выбранного пункта меню</returns>
        public int upMenu()
        {
            _points[_currentMenuItem].IsSelected = false;
            if (CurrentMenuItem - 1 >= 0)
            {
                CurrentMenuItem--;
            }
            else
            {
                CurrentMenuItem = Points.Count - 1;
            }
            _points[_currentMenuItem].IsSelected = true;
            return CurrentMenuItem;
        }

        /// <summary>
        /// Перейти вниз по меню
        /// </summary>
        /// <returns>Номер выбранного пункта меню</returns>
        public int downMenu()
        {
            _points[CurrentMenuItem].IsSelected = false;
            if (CurrentMenuItem + 1 < _points.Count)
            {
                CurrentMenuItem++;
            }
            else
            {
                CurrentMenuItem = 0;
            }
            _points[CurrentMenuItem].IsSelected = true;
            return CurrentMenuItem;
        }
    }
}
