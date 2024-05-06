using Game.Game;
using Game.Menu;
using Game.View;

namespace GameConsole.Menu.View
{
    /// <summary>
    /// Пункты меню отображение
    /// </summary>
    public class MenuPointView : IView
    {

        /// <summary>
        /// Белый цвет
        /// </summary>
        private static readonly int WHITE_COLOR = 0xFF;

        /// <summary>
        /// Цвет Plum
        /// </summary>
        private static readonly int GREY_COLOR = 0x800080;

        /// <summary>
        /// Пункт меню
        /// </summary>
        private MenuPoint _menuPoint;

        /// <summary>
        /// Координаты
        /// </summary>
        private Coordinates _coordinates;

        /// <summary>
        /// Свойства для пункта меню
        /// </summary>
        public MenuPoint MenuPoint { get => _menuPoint; set => _menuPoint = value; }

        /// <summary>
        /// Свойства для координат
        /// </summary>
        public Coordinates Coordinates { get => _coordinates; set => _coordinates = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parMenuPoint">Пункт меню</param>
        /// <param name="parCoordinates">Координаты</param>
        public MenuPointView(MenuPoint parMenuPoint, Coordinates parCoordinates)
        {
            _menuPoint = parMenuPoint;
            _coordinates = parCoordinates;
        }

        /// <summary>
        /// Отрисовка пунктом меню
        /// </summary>
        public void Draw()
        {
            FastOutput fastOutput = FastOutput.GetInstance();
            string text = " " + MenuPoint.Text + " ";
            int _bc;
            int _fc;
            if (MenuPoint.IsSelected)
            {
                _bc = GREY_COLOR;
                _fc = WHITE_COLOR;
            }
            else
            {
                _bc = WHITE_COLOR;
                _fc = GREY_COLOR;
            }
            fastOutput.OutputString(text, _bc, _fc, (int)Coordinates.X, (int)Coordinates.Y + 1);
        }
    }
}
