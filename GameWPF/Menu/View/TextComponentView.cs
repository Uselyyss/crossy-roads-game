using System.Windows;
using System.Windows.Controls;
using Game.Menu;
using Game.View;

namespace GameWPF.Menu.View
{

    /// <summary>
    /// Представление текстового компонента
    /// </summary>
    public class TextComponentView: IView
    {

        /// <summary>
        /// Текстовый компонент
        /// </summary>
        private TextComponent _textComponent;

        /// <summary>
        /// Текстовый блок (представление для текстового компонента)
        /// </summary>
        private TextBlock _textBlock;

        /// <summary>
        /// Текстовый компонент
        /// </summary>
        public TextComponent TextComponent { get => _textComponent; set => _textComponent = value; }

        /// <summary>
        /// Текстовый блок (представление для текстового компонента)
        /// </summary>
        public TextBlock TextBlock { get => _textBlock; set => _textBlock = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parTextComponent">Текстовый компонент</param>
        /// <param name="parRow">Позиция строки</param>
        /// <param name="parColumn">Позиция столбца</param>
        public TextComponentView(TextComponent parTextComponent, int parRow, int parColumn)
        {
            _textComponent = parTextComponent;
            _textBlock = new TextBlock();
            _textBlock.Text = _textComponent.Text;
            _textBlock.TextAlignment = TextAlignment.Left;
            _textBlock.FontStyle = FontStyles.Italic;
            _textBlock.FontSize = 16;
            _textBlock.Height = 32;
            _textBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
            Canvas.SetTop(_textBlock, parRow * 50);
            Canvas.SetLeft(_textBlock, parColumn * 50 + 100);
        }

        /// <summary>
        /// Отрисовать
        /// </summary>
        public void Draw()
        {
            ((Canvas)Program.Window.Content).Children.Add(_textBlock);
        }
    }
}
