using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Game.Game;
using Game.View;

namespace GameWPF.Game.View
{
    /// <summary>
    /// Представление игры
    /// </summary>
    public class GameFieldView : IView
    {
        /// <summary>
        /// Текст завершения игры
        /// </summary>
        private const string GAME_OVER = "GAME OVER!";

        /// <summary>
        /// Компонент-контейнер игрового поля
        /// </summary>
        private Canvas _canvas;

        /// <summary>
        /// Игра
        /// </summary>
        private readonly MainGame _mainGame;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parMainGame">Игра</param>
        /// <param name="parGrid">Контейнер окна</param>
        public GameFieldView(MainGame parMainGame, Canvas parCanvas)
        {
            _mainGame = parMainGame;
            _canvas = parCanvas;
            _mainGame.OnGameOver += DrawMainGameOver;
            _mainGame.OnRedraw += Draw;
        }

        /// <summary>
        /// Отрисовать
        /// </summary>
        public void Draw()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _canvas.Children.Clear();
                    DrawField();
                    DrawCells();
                    DrawRecords();
                    DrawPlayerInfo();
            });
        }

        /// <summary>
        /// Отрисовать информацию об игроке
        /// </summary>
        private void DrawPlayerInfo()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TextBlock positionTextBlock = new TextBlock();
                positionTextBlock.Text = "HP: " + _mainGame.Player.Health;
                positionTextBlock.FontSize = 14;
                positionTextBlock.FontFamily = new FontFamily("Arial");
                Canvas.SetLeft(positionTextBlock, 800);
                Canvas.SetTop(positionTextBlock, 430);
                _canvas.Children.Add(positionTextBlock);
                
            });
        }

        /// <summary>
        /// Отрисовать поле
        /// </summary>
        private void DrawField()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Rectangle r = new Rectangle();
                r.Width = MainGame.Columns * 25;
                r.Height = MainGame.Rows * 25;
                r.StrokeThickness = 1;
                r.Stroke = new SolidColorBrush(Colors.Gold);
                _canvas.Children.Add(r);
            });
        }

        /// <summary>
        /// Отрисовать ячейки
        /// </summary>
        private void DrawCells()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var fieldCell in _mainGame.FieldCells)
                {
                    Shape shape = new Rectangle();
                    Brush brush = GetColor(fieldCell);

                    shape.Fill = brush;
                    shape.StrokeThickness = 1;
                    shape.Stroke = Brushes.Black;

                    double left = fieldCell.Coordinates.X * 25;
                    double top = fieldCell.Coordinates.Y * 25;

                    if (shape is Rectangle)
                    {
                        Rectangle rectangle = shape as Rectangle;
                        rectangle.Width = 25;
                        rectangle.Height = 25;
                        Canvas.SetLeft(shape, left);
                        Canvas.SetTop(shape, top);
                    }
                   

                    _canvas.Children.Add(shape);
                }
            });
        }

        /// <summary>
        /// Отрисовать рекорды
        /// </summary>
        private void DrawRecords()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "Score: " + _mainGame.Player.Score;
                textBlock.FontSize = 18;
                textBlock.FontFamily = new FontFamily("Arial");
                Canvas.SetLeft(textBlock, 800);
                Canvas.SetTop(textBlock, 400);
                _canvas.Children.Add(textBlock);
            });
        }

        /// <summary>
        /// Вывод сообщения о том, что игра была завершена
        /// </summary>
        public void DrawMainGameOver()
        {
            ShowMessage(GAME_OVER);
        }

        /// <summary>
        /// Показать сообщение
        /// </summary>
        /// <param name="parMessage">Текст</param>
        private void ShowMessage(string parMessage)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = parMessage;
                textBlock.FontSize = 64;
                textBlock.Foreground = new SolidColorBrush(Colors.Red);
                textBlock.FontFamily = new FontFamily("Arial");
                Canvas.SetLeft(textBlock, 800);
                Canvas.SetTop(textBlock, 50);
                Canvas.SetZIndex(textBlock, 10);

                _canvas.Children.Add(textBlock);
            });
        }

        /// <summary>
        /// Получение цвета фигуры
        /// </summary>
        /// <param name="gameCell">Конкретная ячейка поля</param>
        /// <returns>Цвет фигуры</returns>
        private Brush GetColor(GameCell gameCell)
        {
            switch (gameCell.Type)
            {
                case CellType.Empty:
                    return new SolidColorBrush(Colors.LightBlue);
                case CellType.Player:
                    return new SolidColorBrush(Colors.Green);
                case CellType.Enemy:
                    return new SolidColorBrush(Colors.Red);
                case CellType.Wall:
                    return new SolidColorBrush(Colors.Black);
                default:
                    return new SolidColorBrush(Colors.Fuchsia);
            }
        }
    }
}