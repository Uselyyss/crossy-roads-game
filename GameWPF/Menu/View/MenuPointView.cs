using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Game.Menu;
using Game.View;

namespace GameWPF.Menu.View
{
    /// <summary>
    /// Представление пункта меню
    /// </summary>
    public class MenuPointView: IView
    {

        /// <summary>
        /// Ширина компонента
        /// </summary>
        private const int TEXT_BLOCK_WIDTH = 300;

        /// <summary>
        /// Высота компонента
        /// </summary>
        private const int TEXT_BLOCK_HEIGHT = 50;

        /// <summary>
        /// Отступы компонента
        /// </summary>
        private static readonly Thickness THICKNESS = new Thickness(5, 10, 5, 10);

        /// <summary>
        /// Пункт меню
        /// </summary>
        private MenuPoint _menuPoint;

        /// <summary>
        /// Компонент-контейнер окна
        /// </summary>
        private Canvas _canvas = (Canvas)Program.Window.Content;

        /// <summary>
        /// Текстовый компонент (представление пункта меню)
        /// </summary>
        private TextBlock _textBlock;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parMenuPoint">Пункт меню</param>
        /// <param name="parPosition">Позиция пункта</param>
        public MenuPointView(MenuPoint parMenuPoint, int parPosition)
        {
            _menuPoint = parMenuPoint;

            _textBlock = new TextBlock();
            _textBlock.Background = _menuPoint.IsSelected ? Brushes.Plum : Brushes.Bisque;
            _textBlock.FontFamily = new FontFamily("Alias");
            _textBlock.Foreground = new SolidColorBrush(Colors.Black);

            _textBlock.Height = TEXT_BLOCK_HEIGHT;
            _textBlock.Width = TEXT_BLOCK_WIDTH;
            _textBlock.Text = _menuPoint.Text;
            _textBlock.TextAlignment = TextAlignment.Center;
            _textBlock.Padding = THICKNESS;
            _textBlock.Background = _menuPoint.IsSelected ? Brushes.Plum : Brushes.Bisque;
            Canvas.SetLeft(_textBlock, 100);
            Canvas.SetTop(_textBlock, parPosition * 80);
        }

        /// <summary>
        /// Отрисовать
        /// </summary>
        public void Draw()
        {
            _canvas.Children.Add(_textBlock);
        }

        /// <summary>
        /// Перерисовать
        /// </summary>
        public void Redraw()
        {
            _textBlock.Background = _menuPoint.IsSelected ? Brushes.Plum : Brushes.Bisque;
        }

    }
}
