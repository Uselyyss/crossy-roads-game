using System;
using System.Collections.Generic;
using ConsoleColumns.Menu.Controller;
using Game.Menu;
using GameConsole.Game.Controller;
using GameConsole.Menu.View;

namespace GameConsole.Menu.Controller
{
    /// <summary>
    /// Контроллер главного меню
    /// </summary>
    public class MenuScreenController : ScreenController
    {

        /// <summary>
        /// Экземпляр класса (singleton)
        /// </summary>
        private static readonly MenuScreenController _instance = new MenuScreenController();

        /// <summary>
        /// Свойство экземпляра класса (singleton)
        /// </summary>
        public static MenuScreenController Instance => _instance;

        /// <summary>
        /// Конструктор
        /// </summary>
        private MenuScreenController() : base()
        {

        }

        /// <summary>
        /// Инициализация
        /// </summary>
        public void Init()
        {
            List<MenuPoint> menuPoints = MenuInitializer.Instance.CreateMenu(
                Instance, GameController.Instance, ScreenController.GuideControllerInstance, 
                ScreenController.RecordControllerInstance);
            MenuScreen menuScreen =
                new(menuPoints, new TextComponent("Columns"));
            Screen = menuScreen;
            ScreenView = new MenuScreenView(menuScreen);
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Запуск контроллера
        /// </summary>
        public override void Start()
        {
            MenuScreenController.Instance.Init();
            ScreenView.Draw();
            while (!IsExit)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        ((MenuScreen)Screen).Points[((MenuScreen)Screen).CurrentMenuItem].Handler.Invoke();
                        ScreenView.Draw();
                        break;

                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        ((MenuScreen)Screen).upMenu();
                        break;

                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        ((MenuScreen)Screen).downMenu();
                        break;

                    case ConsoleKey.Escape:
                        Stop();
                        break;
                }
                if (consoleKeyInfo.Key == ConsoleKey.W || consoleKeyInfo.Key == ConsoleKey.S)
                {
                    ((MenuScreen)Screen).Drawer.Invoke();
                }
            }
            FastOutput.GetInstance().ClearScreen();
        }
    }
}
