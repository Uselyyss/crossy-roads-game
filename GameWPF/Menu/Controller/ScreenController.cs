using System.Windows.Controls;
using System.Windows.Input;
using Game.Controller;
using Game.Menu;
using GameWPF.Menu.View;

namespace GameWPF.Menu.Controller
{
    /// <summary>
    /// Контроллер экрана
    /// </summary>
    public class ScreenController: IController
    {

        /// <summary>
        /// Экземпляр контроллера инструкции
        /// </summary>
        private static readonly ScreenController _guideControllerInstance = 
            new ScreenController(ScreenFactory.Instance.CreateGuideScreen());

        /// <summary>
        /// Экземпляр контроллера рекордов
        /// </summary>
        private static readonly ScreenController _recordControllerInstance = 
            new ScreenController(ScreenFactory.Instance.CreateRecordScreen());

        /// <summary>
        /// Экран
        /// </summary>
        private Screen _screen;

        /// <summary>
        /// Представление экрана
        /// </summary>
        private ScreenView _screenView;

        /// <summary>
        /// Экран
        /// </summary>
        public Screen Screen { get => _screen; set => _screen = value; }

        /// <summary>
        /// Представление экрана
        /// </summary>
        public ScreenView ScreenView { get => _screenView; set => _screenView = value; }

        /// <summary>
        /// Экземпляр контроллера инструкции
        /// </summary>
        public static IController GuideControllerInstance => _guideControllerInstance;

        /// <summary>
        /// Экземпляр контроллера рекордов
        /// </summary>
        public static ScreenController RecordControllerInstance => _recordControllerInstance;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parScreen">Экран</param>
        public ScreenController(Screen parScreen)
        {
            _screen = parScreen;
            _screenView = new ScreenView(parScreen);
        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        protected ScreenController() { }

        /// <summary>
        /// Обновить контроллер рекордов
        /// </summary>
        public static void UpdateRecordController()
        {
            RecordControllerInstance.Screen = ScreenFactory.Instance.CreateRecordScreen();
            RecordControllerInstance.ScreenView = new ScreenView(RecordControllerInstance.Screen);
        }

        /// <summary>
        /// Запуск контроллера экрана
        /// </summary>
        public virtual void Start()
        {
            ScreenController.UpdateRecordController();  
            ((Canvas)Program.Window.Content).Children.Clear();
            Program.Window.KeyDown += KeyDown;
            _screenView.Draw();
        }


        /// <summary>
        /// Остановка контроллера экрана
        /// </summary>

        public virtual void Stop()
        {
            Program.Window.KeyDown -= KeyDown;
            MenuScreenController.Instance.Start();
        }

        /// <summary>
        /// Обработка нажатия клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                case Key.Enter:
                    Stop();
                    break;
            }

        }
    }
}
