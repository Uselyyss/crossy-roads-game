namespace Game.Game
{
    /// <summary>
    /// Класс, представляющий юнита в игре.
    /// </summary>
    public class Unit
    {
        private int _health;
        private Coordinates _position;
        private int _score = 0;
        private KeyDirection _direction = KeyDirection.Left;

        /// <summary>
        /// Конструктор класса Unit.
        /// </summary>
        /// <param name="health">Здоровье юнита.</param>
        /// <param name="position">Позиция юнита на игровом поле.</param>
        /// <param name="score">Число очков</param>
        public Unit(int health, Coordinates position, int score)
        {
            _health = health;
            _position = position;
            _score = score;
        }

        /// <summary>
        /// Уровень юнита.
        /// </summary>
        public int Score
        {
            get => _score;
            set => _score = value;
        }

        /// <summary>
        /// Позиция юнита на игровом поле.
        /// </summary>
        public Coordinates Position
        {
            get => _position;
            set => _position = value;
        }

        /// <summary>
        /// Здоровье юнита.
        /// </summary>
        public int Health
        {
            get => _health;
            set => _health = value;
        }

        public KeyDirection Direction { get; set; }
    }
}
