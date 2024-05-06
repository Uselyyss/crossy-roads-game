using System;
using System.Windows.Controls;
using System.Windows.Input;
using Game.Game;
using Game.Menu;
using GameWPF.Menu.Controller;
using GameWPF.Menu.View;

namespace GameWPF.Game.Controller
{
    /// <summary>
    /// Контроллер сохранения рекордов
    /// </summary>
    public class InputRecordsController: ScreenController
    {

        /// <summary>
        /// Игрок
        /// </summary>
        private Player _player;

        /// <summary>
        /// Текстовый блок представления имени
        /// </summary>
        private TextBlock _textBlockName;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parScore">Количество очков</param>
        public InputRecordsController(int parScore)
        {
            Console.WriteLine(parScore);
            _player = new Player();
            _player.Score = parScore;
            Screen = ScreenFactory.Instance.CreateRecordSavingScreen(parScore);
            ScreenView = new ScreenView(Screen);
        }

        /// <summary>
        /// Запуск контроллера
        /// </summary>
        public override void Start()
        {
            ScreenView.Draw();
            _textBlockName = new TextBlock();
            _textBlockName.Width = 120;
            ((Canvas)Program.Window.Content).Children.Add(_textBlockName);

            Program.Window.KeyDown += KeyDown;
        }

        /// <summary>
        /// Обработка нажатия кнопок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Stop();
                    break;
                case Key.Enter:
                    Save();
                    Stop();
                    break;
                case Key.Back:
                    if (_textBlockName.Text.Length > 0)
                    {
                        _textBlockName.Text = _textBlockName.Text.Substring(0, _textBlockName.Text.Length - 1);
                    }
                    break;
                default:
                    string key = new KeyConverter().ConvertToString(e.Key);
                    char symbol = key.ToCharArray()[0];
                    if (_textBlockName.Text.Length < 16 && key.Length == 1 && char.IsLetter(symbol))
                    {
                        _textBlockName.Text += symbol;
                    }
                    break;
            }
        }

        /// <summary>
        /// Остановка контроллера
        /// </summary>
        public override void Stop()
        {
            Program.Window.KeyDown -= KeyDown;
            MenuScreenController.Instance.Start();
        }

        /// <summary>
        /// Сохранения рекорда
        /// </summary>
        private void Save()
        {
            _player.Nickname = _textBlockName.Text;
            RecordsFileUtils.Instance.WriteRecordToFile(_player);
        }

    }
}
