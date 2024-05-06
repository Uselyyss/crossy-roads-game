using System;
using Game.Game;
using GameConsole.Menu.Controller;

namespace GameConsole
{
    /// <summary>
    /// Консольное приложение
    /// </summary>
    class Program
    {
        /// <summary>
        /// Старт программы
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            MenuScreenController.Instance.Start();
        
        }
    }
}