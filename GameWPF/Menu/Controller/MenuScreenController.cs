using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Game.Controller;
using Game.Menu;
using GameWPF.Game.Controller;
using GameWPF.Menu.View;

namespace GameWPF.Menu.Controller
{
    /// <summary>
    /// Контроллер экарана меню
    /// </summary>
    public class MenuScreenController: ScreenController
    {

        /// <summary>
        /// Экземпляр класса (singletone)
        /// </summary>
        private static readonly MenuScreenController _instance = new MenuScreenController();

        /// <summary>
        /// Экземпляр класса (singletone)
        /// </summary>
        public static MenuScreenController Instance => _instance;

        /// <summary>
        /// Контсруктор
        /// </summary>
        private MenuScreenController() { }

        /// <summary>
        /// Инициализация
        /// </summary>
        public void Init()
        {
            List<MenuPoint> menu = MenuInitializer.Instance.CreateMenu(Instance,
                GameController.Instance, ScreenController.GuideControllerInstance, ScreenController.RecordControllerInstance);

            MenuScreen menuScreen = new MenuScreen(new TextComponent("Crossy Roads"), menu, new());
            Screen = menuScreen;
            ScreenView = new MenuScreenView(menuScreen);
        }


        /// <summary>
        /// Запуск контроллера
        /// </summary>
        public override void Start()
        {
            Canvas canvas = new Canvas();
            Program.Window.Content = canvas;
            Instance.Init();
            ((Canvas)Program.Window.Content).Children.Clear();
            Program.Window.KeyDown += KeyDown;
            ScreenView.Draw();
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
                case Key.W:
                case Key.Up:
                    ((MenuScreen)Screen).upMenu();
                    ((MenuScreenView)ScreenView).Redraw();
                    break;
                case Key.S:
                case Key.Down:
                    ((MenuScreen)Screen).downMenu();
                    ((MenuScreenView)ScreenView).Redraw();
                    break;
                case Key.Escape:
                    Stop();
                    break;
                case Key.Enter:
                    Program.Window.KeyDown -= KeyDown;
                    ((MenuScreen)Screen).Points[((MenuScreen)Screen).CurrentMenuItem].Handler.Invoke();
                    break;
            }

        }

        /// <summary>
        /// Остановка контроллера
        /// </summary>
        public override void Stop()
        {
            Program.Window.KeyDown -= KeyDown;
            Program.Application.Shutdown();
        }
    }
}
