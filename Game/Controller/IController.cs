namespace Game.Controller
{
    /// <summary>
    /// Контроллер
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Запустить контроллер
        /// </summary>
        public void Start();

        /// <summary>
        /// Остановка работы контроллера
        /// </summary>
        public void Stop();
    }
}