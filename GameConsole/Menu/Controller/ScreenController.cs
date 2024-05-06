using System;
using Game.Controller;
using Game.Menu;
using GameConsole;
using GameConsole.Menu.View;

namespace ConsoleColumns.Menu.Controller
{
    /// <summary>
    /// Контроллер экрана
    /// </summary>
    public class ScreenController : IController
    {
        /// <summary>
        /// Экземпляр класса экрана рекордов
        /// </summary>
        private static readonly ScreenController _recordControllerInstance =
            new ScreenController(ScreenFactory.Instance.CreateRecordScreen());

        /// <summary>
        /// Экземпляр класса экрана гайдов игры
        /// </summary>
        private static readonly ScreenController _guideControllerInstance =
            new ScreenController(ScreenFactory.Instance.CreateGuideScreen());

        /// <summary>
        /// Флаг для завершения работы контроллера
        /// </summary>
        private bool _isExit = false;

        /// <summary>
        /// Экран
        /// </summary>
        private Screen _screen;

        /// <summary>
        /// Представления экрана
        /// </summary>
        private ScreenView _screenView;

        /// <summary>
        /// Свйоство для флага для завершения работы контроллера
        /// </summary>
        public bool IsExit { get => _isExit; set => _isExit = value; }

        /// <summary>
        /// Свойство для экрана
        /// </summary>
        public Screen Screen { get => _screen; set => _screen = value; }

        /// <summary>
        /// Свойство для представления экрана
        /// </summary>
        public ScreenView ScreenView { get => _screenView; set => _screenView = value; }

        /// <summary>
        /// Экземпляр класса экрана рекордов
        /// </summary>
        public static ScreenController RecordControllerInstance => _recordControllerInstance;

        /// <summary>
        /// Экземпляр класса экрана гайдов игры
        /// </summary>
        public static ScreenController GuideControllerInstance => _guideControllerInstance;


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parSceen">Экран</param>
        private ScreenController(Screen parSceen)
        {
            _screen = parSceen;
            _screenView = new ScreenView(parSceen);
        }


        /// <summary>
        /// Пустой конструктор
        /// </summary>
        protected ScreenController() { }

        /// <summary>
        /// Обновление контроллера с рекордами
        /// </summary>
        public static void UpdateRecordsController()
        {
            RecordControllerInstance.Screen = ScreenFactory.Instance.CreateRecordScreen();
            RecordControllerInstance.ScreenView = new ScreenView(RecordControllerInstance.Screen);
        }

        /// <summary>
        /// Запуск контроллера
        /// </summary>
        public virtual void Start()
        {
            ScreenController.UpdateRecordsController();
            FastOutput fastOutput = FastOutput.GetInstance();
            fastOutput.ClearScreen();
            _isExit = false;
            ScreenView.Draw();
            while (!_isExit)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Enter:
                    case ConsoleKey.Escape:
                        Stop();
                        break;
                }
            }
            FastOutput.GetInstance().ClearScreen();
        }

        /// <summary>
        /// Остановка контроллера
        /// </summary>
        public virtual void Stop()
        {
            _isExit = true;
        }
    }
}
