using System;
using System.Windows;
using GameWPF.Menu.Controller;

namespace GameWPF
{
    /// <summary>
    /// WPF-приложение
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Приложение
        /// </summary>
        private static readonly Application _application = new Application();

        /// <summary>
        /// Окно
        /// </summary>
        private static readonly Window _window = new Window();

        /// <summary>
        /// Окно
        /// </summary>
        public static Window Window => _window;

        /// <summary>
        /// Приложение
        /// </summary>
        public static Application Application => _application;


        /// <summary>
        /// Входной метод программы
        /// </summary>
        [STAThread]
        static void Main()
        {
            Program.Window.Width = 1280;
            Program.Window.Height = 600;
            _window.ResizeMode = ResizeMode.NoResize;
            MenuScreenController.Instance.Start();
            Application.Run(Window);
        }

    }
}
