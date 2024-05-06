namespace Game.Game
{
    /// <summary>
    /// Ячейка поля
    /// </summary>
    public class GameCell
    {
        /// <summary>
        /// Координата объекта
        /// </summary>
        private Coordinates _coordinates;
        
        /// <summary>
        /// Координата объекта
        /// </summary>
        public Coordinates Coordinates
        {
            get => _coordinates;
            set => _coordinates = value;
        }

        /// <summary>
        /// Тип поля
        /// </summary>
        public CellType Type { get; set; }

        /// <summary>
        /// конструктор 
        /// </summary>
        public GameCell(Coordinates parCoordinates, CellType type)
        {
            _coordinates = parCoordinates;
            Type = type;
        }
    }
}