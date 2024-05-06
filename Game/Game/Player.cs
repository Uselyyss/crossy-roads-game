namespace Game.Game
{
    /// <summary>
    /// Игрок
    /// </summary>
    public class Player
    {

        /// <summary>
        /// Никнейм игрока
        /// </summary>
        private string _nickname;

        /// <summary>
        /// Количество очков
        /// </summary>
        private int _score;

        /// <summary>
        /// Свойства для никнейма
        /// </summary>
        public string Nickname { get => _nickname; set => _nickname = value; }
        
        /// <summary>
        /// Свойство для количества очков
        /// </summary>
        public int Score { get => _score; set => _score = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parNickname">Никнейм игрока</param>
        /// <param name="parScore">Количество очков игрока</param>
        public Player(string parNickname, int parScore)
        {
            _nickname = parNickname;
            _score = parScore;
        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Player() { }
    }
}
