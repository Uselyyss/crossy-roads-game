using System;

namespace Game.Game 
{
    /// <summary>
    /// Координаты
    /// </summary>
    public class Coordinates
    {
        /// <summary>
        /// Координата по оси x
        /// </summary>
        private double _x;

        /// <summary>
        /// Координата по оси y
        /// </summary>
        private double _y;

        /// <summary>
        /// Свойство координаты по оси x
        /// </summary>
        public double X { get => _x; set => _x = value; }

        /// <summary>
        /// Свойство координаты по оси y
        /// </summary>
        public double Y { get => _y; set => _y = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parX">Координата x</param>
        /// <param name="parY">Координата y</param>
        public Coordinates(double parX, double parY)
        {
            _x = parX;
            _y = parY;
        }
        /// <summary>
        /// Переопределение метода Equals для сравнения объектов Coordinates
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>True, если координаты равны, в противном случае - false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Coordinates other = (Coordinates)obj;
            return X == other.X && Y == other.Y;
        }
    }
}
