using System.Collections.Generic;
using Game.Controller;

namespace Game.Menu
{
    /// <summary>
    /// Инициализация меню
    /// </summary>
    public class MenuInitializer
    {       

        /// <summary>
        /// Название пункта меню для начал игры 
        /// </summary>
        private const string START_MENU_POINT = "START GAME";

        /// <summary>
        /// Название пункат меню для рекордов игры
        /// </summary>
        private const string RECORDS_MENU_POINT = "TOP SCORES";

        /// <summary>
        /// Название пункта меню для инструкции игры
        /// </summary>
        private const string GUIDE_TEXT = "GUIDES";

        /// <summary>
        /// Название пункта меню для выхода из игры
        /// </summary>
        private const string EXIT_MENU_POINT = "EXIT GAME";

        /// <summary>
        /// Экзмепляр объекта (singleton)
        /// </summary>
        private static readonly MenuInitializer _instance = new MenuInitializer();

        /// <summary>
        /// Экзмепляр объекта (singleton)
        /// </summary>
        public static MenuInitializer Instance => _instance;


        /// <summary>
        /// Пустой констроктор
        /// </summary>
        private MenuInitializer() { }

        /// <summary>
        /// Создание меню
        /// </summary>
        /// <param name="parMainMenuController">Контроллер главного меню</param>
        /// <param name="parGameController">Контроллер игры</param>
        /// <param name="parGuideController">Контроллер экрана с гайдами</param>
        /// <param name="parRecordController">Контроллер экрана с рекордами</param>
        /// <returns></returns>
        public List<MenuPoint> CreateMenu(IController parMainMenuController,
            IController parGameController, IController parGuideController, IController parRecordController)
        {
            List<MenuPoint> menuPoints = new List<MenuPoint>()
            {
                CreateStartGameMenuPoint(parGameController),
                CreateGuideGameMenuPoint(parGuideController),
                CreateRecordsGameMenuPoint(parRecordController),
                CreateExitGameMenuPoint(parMainMenuController)
            };
            return menuPoints;
        }

        /// <summary>
        /// Создать пункт в меню "Старт"
        /// </summary>
        /// <param name="parController">Контроллер меню</param>
        /// <returns>Пункт меню</returns>
        private MenuPoint CreateStartGameMenuPoint(IController parController)
        {
            MenuPoint startGame = new MenuPoint(START_MENU_POINT);
            startGame.IsSelected = true;
            startGame.Handler += parController.Start;
            return startGame;
        }

        /// <summary>
        /// Создать пункт меню "Инструкция"
        /// </summary>
        /// <param name="parController">Контроллер экрана с инструкцией</param>
        /// <returns>Пункт меню</returns>
        private MenuPoint CreateGuideGameMenuPoint(IController parController)
        {
            MenuPoint startGame = new MenuPoint(GUIDE_TEXT);
            startGame.Handler += parController.Start;
            return startGame;
        }

        /// <summary>
        /// Создать пункт меню "Рекорды"
        /// </summary>
        /// <param name="parController">Контроллер экрана с рекордами</param>
        /// <returns>Пункт меню</returns>
        private MenuPoint CreateRecordsGameMenuPoint(IController parController)
        {
            MenuPoint startGame = new MenuPoint(RECORDS_MENU_POINT);
            startGame.Handler += parController.Start;
            return startGame;
        }

        /// <summary>
        /// Создать пункт меню "Выход"    
        /// </summary>
        /// <param name="parController">Игровой контроллер</param>
        /// <returns>Пункт меню</returns>
        private MenuPoint CreateExitGameMenuPoint(IController parController)
        {
            MenuPoint exitGame = new MenuPoint(EXIT_MENU_POINT);
            exitGame.Handler += parController.Stop;
            return exitGame;
        }
    }
}
