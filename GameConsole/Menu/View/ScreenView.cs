using System;
using System.Collections.Generic;
using System.Linq;
using Game.Game;
using Game.Menu;
using Game.View;

namespace GameConsole.Menu.View
{
    /// <summary>
    /// Представление экрана
    /// </summary>
    public class ScreenView : IView
    {
        private Screen _screen;
        private List<TextComponentView> _textComponentViews;

        public Screen Screen
        {
            get => _screen;
            set => _screen = value;
        }

        public List<TextComponentView> TextComponentViews
        {
            get => _textComponentViews;
            set => _textComponentViews = value;
        }

        public ScreenView(Screen parScreen)
        {
            _screen = parScreen;
            _textComponentViews = new List<TextComponentView>();
            int centerY = Console.WindowHeight / 4 - _screen.TextComponents.Count / 2;
            foreach (TextComponent textComponent in _screen.TextComponents)
            {
                int centerX = 1;
                _textComponentViews.Add(new TextComponentView(textComponent, new Coordinates(centerX, centerY)));
                centerY++; // Переходим к следующей строке
            }
        }

        public virtual void Draw()
        {
            TextComponentViews.ForEach(tc => tc.Draw());
        }
    }
}