using System.Collections.Generic;
using System.Windows.Controls;
using Game.Menu;
using Game.View;

namespace GameWPF.Menu.View
{
    /// <summary>
    /// Представление экрана
    /// </summary>
    public class ScreenView : IView
    {

        /// <summary>
        /// Экран
        /// </summary>
        private readonly Screen _screen;

        /// <summary>
        /// Список текстовых компонентов
        /// </summary>
        protected readonly List<TextComponentView> _textComponentsViews;


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parScreen">Экран</param>
        public ScreenView(Screen parScreen)
        {
            _screen = parScreen;

            _textComponentsViews = new List<TextComponentView>();

            _textComponentsViews.Add(new TextComponentView(_screen.Title, 0, 0));
            for (int i = 0; i < _screen.TextComponents.Count; i++)
            {
                _textComponentsViews.Add(new TextComponentView(_screen.TextComponents[i], i + 1, 0));
            }
        }

        /// <summary>
        /// Отрисовать
        /// </summary>
        public virtual void Draw()
        {
            ((Canvas)Program.Window.Content).Children.Clear();
            _textComponentsViews.ForEach(tcv => tcv.Draw());
        }

    }
}
