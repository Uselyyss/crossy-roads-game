using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Game.Controller;
using Game.Game;
using Game.Menu;
using GameWPF.Game.View;

namespace GameWPF.Game.Controller
{
    /// <summary>
    /// Игровой контроллер
    /// </summary>
    public class GameController : IController
    {

        /// <summary>
        /// Экземпляр объекта (singletone)
        /// </summary>
        private static readonly GameController _instance = new GameController();

        /// <summary>
        /// Игра
        /// </summary>
        private MainGame _mainGame;

        /// <summary>
        /// Представление игры
        /// </summary>
        private GameFieldView _gameScreenView;

        /// <summary>
        /// Окно приложения
        /// </summary>
        private Window _window;

        /// <summary>
        /// Экземпляр объекта (singletone)
        /// </summary>
        public static IController Instance => _instance;

        /// <summary>
        /// Конструктор
        /// </summary>
        private GameController()
        {
            _window = Program.Window;
        }

        /// <summary>
        /// Инициализация
        /// </summary>
        private void InitGame()
        {
            _mainGame = new MainGame();
            _gameScreenView = new GameFieldView(_mainGame, (Canvas) _window.Content);
        }


        /// <summary>
        /// Запуск контроллера
        /// </summary>
        public void Start()
        {
            InitGame();
            _mainGame.Start();
            _gameScreenView.Draw();
            _window.KeyDown += KeyDown;
        }

        /// <summary>
        /// Остановка контроллера
        /// </summary>
        public void Stop()
        {
            _mainGame.Stop();
            _window.KeyDown -= KeyDown;
            _window.KeyDown += ExitKeyDown;
        }

        /// <summary>
        /// Обработка нажатия кнопки для завершения игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitKeyDown(object sender, KeyEventArgs e)
        {
            _window.KeyDown -= ExitKeyDown;
            Screen screen = new Screen(new TextComponent("Save record"), new List<TextComponent>());
            new InputRecordsController(_mainGame.Player.Score).Start();
        }

        /// <summary>
        /// Обработка нажатия кнопок во время игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    _mainGame.MovePlayer(KeyDirection.Up);
                    break;
                case Key.S:
                case Key.Down:
                    _mainGame.MovePlayer(KeyDirection.Down);
                    break;
                case Key.D:
                case Key.Right:
                    _mainGame.MovePlayer(KeyDirection.Right);
                    break;
                case Key.A:
                case Key.Left:
                    _mainGame.MovePlayer(KeyDirection.Left);
                    break;
                case Key.Escape:
                    Stop();
                    break;
                case Key.Back:
                    Stop();
                    break;
            }
        }
    }
}
