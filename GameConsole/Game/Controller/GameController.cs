using System;
using Game.Controller;
using Game.Game;
using GameConsole.Game.View;

namespace GameConsole.Game.Controller
{
    /// <summary>
    /// Котнроллер игры
    /// </summary>
    public class GameController : IController
    {
        /// <summary>
        /// Экземпляр объекта (singleton)
        /// </summary>
        private static readonly GameController _instance = new GameController();

        /// <summary>
        /// Флаг для завершения работы контроллера
        /// </summary>
        private bool _isExit = false;

        /// <summary>
        /// Представление игры
        /// </summary>
        private GameFieldView _gameView;

        /// <summary>
        /// Поле игры
        /// </summary>
        private MainGame _mainGame;

        /// <summary>
        /// Экземпляр объекта (singleton)
        /// </summary>
        public static GameController Instance => _instance;

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameController()
        {
        }

        /// <summary>
        /// Инициализация игры
        /// </summary>
        private void InitGame()
        {
            _mainGame = new MainGame();
            _gameView = new GameFieldView(_mainGame);
            _mainGame.OnGameOver += Stop;
        }

        /// <summary>
        /// Старт игры
        /// </summary>
        public void Start()
        {
            InitGame();
            _isExit = false;
            FastOutput.GetInstance().ClearScreen();
            _gameView.Draw();
            _mainGame.Start();
            while (!_isExit)
            {
                _mainGame.ReDraw();
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        _mainGame.MovePlayer(KeyDirection.Up);
                        _mainGame.ReDraw();
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        _mainGame.MovePlayer(KeyDirection.Down);
                        _mainGame.ReDraw();
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        _mainGame.MovePlayer(KeyDirection.Left);
                        _mainGame.ReDraw();
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        _mainGame.MovePlayer(KeyDirection.Right);
                        _mainGame.ReDraw();
                        break;
                    case ConsoleKey.Backspace:
                        Stop();
                        break;
                    
                }
            }

            new InputRecordController(_mainGame.Player.Score).Start();
            FastOutput.GetInstance().ClearScreen();
        }

        /// <summary>
        /// Остановка игры
        /// </summary>
        public void Stop()
        {
            _mainGame.Stop();
            _isExit = true;
        }

        /// <summary>
        /// Завершение игры
        /// </summary>
        private void GameOver()
        {
            Stop();
            _gameView.DrawMainGameOver();
        }
    }
}