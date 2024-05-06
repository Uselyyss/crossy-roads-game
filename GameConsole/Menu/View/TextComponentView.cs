using Game.Game;
using Game.Menu;
using Game.View;

namespace GameConsole.Menu.View
{
    /// <summary>
    /// Вид текстового компонента
    /// </summary>
    public class TextComponentView : IView
    {

        /// <summary>
        /// Цвет заднего фона
        /// </summary>
        private int _backgroundColor = 0xEE;

        /// <summary>
        /// Цвет шрифта
        /// </summary>
        private int _fontColor = 0xDDA0DD;

        /// <summary>
        /// Текстовый компонент
        /// </summary>
        private TextComponent _textComponent;

        /// <summary>
        /// Координаты
        /// </summary>
        private Coordinates _coordinates;


        /// <summary>
        /// Свойство для текстового компонента
        /// </summary>
        public TextComponent TextComponent { get => _textComponent; set => _textComponent = value; }

        /// <summary>
        /// Свойство для координаты
        /// </summary>
        public Coordinates Coordinates { get => _coordinates; set => _coordinates = value; }

        /// <summary>
        /// Свойство для цвета заднего фона
        /// </summary>
        public int BackgroundColor { get => _backgroundColor; set => _backgroundColor = value; }

        /// <summary>
        /// Свойство для цвета шрифта
        /// </summary>
        public int FontColor { get => _fontColor; set => _fontColor = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parTextComponent">Текстовый компонент</param>
        /// <param name="parCoordinates">Координаты</param>
        /// <param name="parBackgroundColor">Цвет заднего фона</param>
        /// <param name="parFontColor">Цвет шрифта</param>
        public TextComponentView(TextComponent parTextComponent, Coordinates parCoordinates, int parBackgroundColor, int parFontColor)
        {
            _textComponent = parTextComponent;
            _coordinates = parCoordinates;
            _backgroundColor = parBackgroundColor;
            _fontColor = parFontColor;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parTextComponent">Текстовый компонент</param>
        /// <param name="parCoordinates">Координаты</param>
        public TextComponentView(TextComponent parTextComponent, Coordinates parCoordinates)
        {
            _textComponent = parTextComponent;
            _coordinates = parCoordinates;
        }

        /// <summary>
        /// Построение отображение текстового компонента
        /// </summary>
        /// <returns>Представление текстового компонента</returns>
        public static TextComponentView BuilderTextComponentView()
        {
            CreatorTextComponentViewBuilder creatorTextComponentViewBuilder = new();
            creatorTextComponentViewBuilder.CreateTextComponentView();
            creatorTextComponentViewBuilder.SetTextComponent();
            creatorTextComponentViewBuilder.SetCoord();
            creatorTextComponentViewBuilder.SetFontColor();
            creatorTextComponentViewBuilder.SetBackgroundColor();
            return creatorTextComponentViewBuilder.TextComponentView;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public TextComponentView()
        {
        }

        /// <summary>
        /// Отрисовка
        /// </summary>
        public void Draw()
        {
            FastOutput fs = FastOutput.GetInstance();
            string back = " " + "".PadRight(_textComponent.Text.Length, ' ') + " ";
            string text = " " + _textComponent.Text + " ";
            fs.OutputString(text, 0, _fontColor, (int)_coordinates.X, (int)_coordinates.Y + 1);
        }
    }
}
