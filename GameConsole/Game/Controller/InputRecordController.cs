using System;
using ConsoleColumns.Menu.Controller;
using Game.Game;
using Game.Menu;
using GameConsole.Menu.View;

namespace GameConsole.Game.Controller
{
    /// <summary>
    /// Контроллер ввода рекордов
    /// </summary>
    public class InputRecordController : ScreenController
    {
        /// <summary>
        /// Игрок
        /// </summary>
        private Player _player;
        /// <summary>
        /// Компонент для отображения имени
        /// </summary>
        private TextComponent _name;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parScore">Количество очков</param>
        public InputRecordController(int parScore)
        {
            _player = new Player();
            _player.Score = parScore;
            Screen = ScreenFactory.Instance.CreateRecordSavingScreen(parScore);
            _name = new TextComponent("".PadLeft(17));
            Screen.TextComponents.Add(_name);
            ScreenView = new ScreenView(Screen);
        }

        /// <summary>
        /// Запуск контроллера
        /// </summary>
        public override void Start()
        {
            IsExit = false;
            FastOutput.GetInstance().ClearScreen();
            ScreenView.Draw();
            _name.Text = "";
            while (!IsExit)
            {
                ScreenView.Draw();
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        _player.Nickname = _name.Text;
                        Save();
                        Stop();
                        break;
                    case ConsoleKey.Escape:
                        Stop();
                        break;
                    case ConsoleKey.Backspace:
                        if (_name.Text.Length > 0)
                        {
                            _name.Text = _name.Text.Substring(0, _name.Text.Length - 1);
                        }
                        break;
                    default:
                        if (char.IsLetter(consoleKeyInfo.KeyChar) && _name.Text.Length <= 16)
                        {
                            _name.Text += consoleKeyInfo.KeyChar;
                        }
                        break;
                }
            }
            FastOutput.GetInstance().ClearScreen();
        }

        /// <summary>
        /// Сохранение рекорда
        /// </summary>
        private void Save()
        {
            RecordsFileUtils.Instance.WriteRecordToFile(_player);
        }
    }
}
