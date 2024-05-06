using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace Game.Game
{
    /// <summary>
    /// Игровое поле
    /// </summary>
    public class MainGame
    {
        //private List<String> _battleLog = new List<string>();

        /// <summary>
        /// Количество колонок в массиве
        /// </summary>
        public static int Columns { get; set; } = 30;

        /// <summary>
        /// Количество строк в массиве
        /// </summary>
        public static int Rows { get; set; } = 20;

        /// <summary>
        /// Основной массив со всеми данными для вывода на форму
        /// </summary>
        private int[,] _fields = new int[Columns, Rows];

        /// <summary>
        /// 
        /// </summary>
        private int _clockDelay = 1000;

        /// <summary>
        /// 
        /// </summary>
        public int ClockDelay
        {
            get => _clockDelay;
            set => _clockDelay = value;
        }

        /// <summary>
        /// Основной массив со всеми данными для вывода на форму
        /// </summary> 
        public GameCell[] FieldCells { get; set; } = new GameCell[Rows * Columns];


        /// <summary>
        /// Направление нажатия
        /// </summary>
        private KeyDirection _keyDirection;

        /// <summary>
        /// Флаг завершения игры
        /// </summary>
        private bool _isGameEnd;

        /// <summary>
        /// Флаг в битве ли игрок
        /// </summary>
        private bool _isInBattle = false;

        /// <summary>
        /// Поток для обработки врагов
        /// </summary>
        private Thread _thread;

        /// <summary>
        /// Свойство для очков, набранных в процессе игры
        /// </summary>
        public int Score { get; set; } = 0; // очки, набираемые в процессе игры


        /// <summary>
        /// Завершение игры
        /// </summary>
        public bool IsGameEnd
        {
            get => _isGameEnd;
            set => _isGameEnd = value;
        }

        /// <summary>
        /// Основной массив со всеми данными для вывода на форму
        /// </summary>
        public int[,] Fields
        {
            get => _fields;
            set => _fields = value;
        }

        /// <summary>
        /// Игрок
        /// </summary>
        private static Unit _player;

        /// <summary>
        /// Игрок
        /// </summary>
        public Unit Player
        {
            get => _player;
            set => _player = value;
        }

        /// <summary>
        /// список юнитов на карте
        /// </summary>
        private List<Unit> _units;

        /// <summary>
        /// Лок для отрисовки
        /// </summary>
        private readonly object _drawLock = new object();

        /// <summary>
        /// геттеры и сеттеры для ююнитов
        /// </summary>
        public List<Unit> Units
        {
            get => _units;
            set => _units = value;
        }

        /// <summary>
        /// Делегат перерисовки
        /// </summary>
        public delegate void dRedraw();

        /// <summary>
        /// Делегат завершения игры
        /// </summary>
        public delegate void dGameOver();

        /// <summary>
        /// Событие перерисовки
        /// </summary>
        public event dRedraw OnRedraw;

        /// <summary>
        /// Событие завершения игры
        /// </summary>
        public event dGameOver OnGameOver;


        /// <summary>
        /// Конструктор
        /// </summary>
        public MainGame()
        {
            InitializeFieldCells();
            InitializeUnits();
        }

        /// <summary>
        /// Инициализация юнитов
        /// </summary>
        private void InitializeUnits()
        {
            InitPlayer();
            InitEnemies();
        }

        /// <summary>
        /// Инициализация противников
        /// </summary>
        public void InitEnemies()
        {
            _units = new List<Unit>();
            foreach (var cell in FieldCells)
            {
                if (cell.Type == CellType.Enemy)
                {
                    Units.Add(new Unit(999, new Coordinates(cell.Coordinates.X, cell.Coordinates.Y), 0));
                }
            }
        }

        /// <summary>
        /// Инициализация игрока
        /// </summary>
        private void InitPlayer()
        {
            Coordinates spawnPosition = new Coordinates(Columns / 2, Rows - 2);
            _player = new Unit(3, spawnPosition, 0);
            var cell = GetFieldCellAtCoordinates(spawnPosition);
            cell.Type = CellType.Player;
        }

        /// <summary>
        /// Инициализация ячеек поля
        /// </summary>
        private void InitializeFieldCells()
        {
            CreateOuterWalls();
            CreateEnemiesOrEmptyCells();
        }


        /// <summary>
        /// Добавление внешних стен
        /// </summary>
        public void CreateOuterWalls()
        {

            for (int i = 0; i < Columns; i++)
            {
                FieldCells[i] = new GameCell(new Coordinates(i, 0), CellType.Wall);

                FieldCells[i + (Rows - 1) * Columns] = new GameCell(new Coordinates(i, Rows - 1), CellType.Wall);
            }

            for (int i = 0; i < Rows - 1; i++)
            {
                FieldCells[i * Columns] = new GameCell(new Coordinates(0, i), CellType.Wall);

                FieldCells[i * Columns + Columns - 1] = new GameCell(new Coordinates(Columns - 1, i), CellType.Wall);
            }
        }


        /// <summary>
        /// Создание врагов или пустых клеток
        /// </summary>
        public void CreateEnemiesOrEmptyCells()
        {
            Random random = new Random();
            
            for (int i = 1; i < Rows - 1; i++)
            {
                for (int j = 1; j < Columns - 1; j++)
                {
                    if (FieldCells[j + i * Columns] == null && random.Next(100) < 3 && (i != Rows - 2))
                    {
                        FieldCells[j + i * Columns] = new GameCell(new Coordinates(j, i), CellType.Enemy);
                    }
                    else if (FieldCells[j + i * Columns] == null)
                    {
                        FieldCells[j + i * Columns] = new GameCell(new Coordinates(j, i), CellType.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Старт игры
        /// </summary>
        public void Start()
        {
            IsGameEnd = false;
            _thread = new Thread(MainGameCycle);
            _thread.Start();
        }

        /// <summary>
        /// Остановка игры
        /// </summary>
        public void Stop()
        {
            IsGameEnd = true;
        }

        /// <summary>
        /// Цикл игры
        /// </summary>
        public void MainGameCycle()
        {
            while (!CheckGameOver() && !IsGameEnd)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                ReDraw();
                
                lock (_drawLock)
                {
                        var unitsCopy = new List<Unit>(Units);
                        foreach (var unit in unitsCopy)
                        {
                            //Пытаемся переместить юнит с учетом нового направления, то есть меняем его на противополжное
                            if (!TryMoveUnit(unit.Direction, unit, CellType.Enemy))
                            {
                                unit.Direction = GetOppositeDirection(unit.Direction);
                            }

                            // Если столкнулись, то спавним игрока с очком здоровья - 1
                            if (IsPlayerInDirectionOfMove(unit)) 
                            {
                                RespawnPlayerAfterDeath();
                            }
                        }
                }

                while (watch.ElapsedMilliseconds < ClockDelay)
                {
                }

                watch.Stop();
            }

            OnGameOver.Invoke();
        }

        /// <summary>
        /// Переместить игрока на стартовую позицию с очками здоровья на 1 меньше 
        /// </summary>
        public void RespawnPlayerAfterDeath()
        {
            var cell = GetFieldCellAtCoordinates(Player.Position);
            cell.Type = CellType.Empty;
            Coordinates spawnPosition = new Coordinates(Columns / 2, Rows - 2);
            _player = new Unit(Player.Health - 1, spawnPosition, Player.Score);
            var cellNew = GetFieldCellAtCoordinates(spawnPosition);
            cellNew.Type = CellType.Player;
        }

        /// <summary>
        /// Проверка дошел ли игрок до верхнего края
        /// </summary>
        public void CheckNewDifficultyLevel()
        {
            if (Math.Abs(Player.Position.Y - 1) < 0.1) // Если игрок достиг верхней части игрового поля
            {
                Player.Score++;
                var cell = GetFieldCellAtCoordinates(Player.Position);
                cell.Type = CellType.Empty;
                Player.Position.Y = Rows - 2;
                cell = GetFieldCellAtCoordinates(Player.Position);
                cell.Type = CellType.Player;
                if (ClockDelay >= 200)
                {
                    ClockDelay -= 100;
                }
                
            }
        }

        /// <summary>
        /// Получить противополжное направление
        /// </summary>
        /// <param name="unitDirection"> Текущее направление юнита</param>
        /// <returns></returns>
        private KeyDirection GetOppositeDirection(KeyDirection unitDirection)
        {
            if (unitDirection == KeyDirection.Left)
            {
                return KeyDirection.Right;
            }

            if (unitDirection == KeyDirection.Right)
            {
                return KeyDirection.Left;
            }

            return unitDirection;
        }

        /// <summary>
        /// Находится ли игрок в напрвлении движения
        /// </summary>
        /// <param name="unit">Юнит, вокруг которого мы смотрим местность</param>
        /// <returns></returns>
        public bool IsPlayerInDirectionOfMove(Unit unit)
        {
            // Проверяем только направления влево и вправо
            foreach (var direction in new[] { KeyDirection.Left, KeyDirection.Right })
            {
                Coordinates newPosition = GetLeftOrRightNeighborCell(unit.Position, direction);
                var cell = GetFieldCellAtCoordinates(newPosition);
                if (cell.Type == CellType.Player)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка заврешения игры
        /// </summary>
        /// <returns>Игра завершена или нет</returns>
        public bool CheckGameOver()
        {
            if (Player.Health <= 0)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Перемещение
        /// </summary>
        /// <param name="parKeyDirection">Направление нажатия</param>
        public void MovePlayer(KeyDirection parKeyDirection)
        {
            TryMoveUnit(parKeyDirection, _player, CellType.Player);
            CheckNewDifficultyLevel();
        }

        /// <summary>
        /// попытка передвинуть юнит
        /// </summary>
        /// <param name="parKeyDirection">направление движения</param>
        /// <param name="unit">юнит для передвижения</param>
        /// <param name="cellType">тип поля</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool TryMoveUnit(KeyDirection parKeyDirection, Unit unit, CellType cellType)
        {
            Coordinates position = unit.Position;

            Coordinates newPosition = position;
            switch (parKeyDirection)
            {
                case KeyDirection.Left:
                    newPosition = new Coordinates(position.X - 1, position.Y);
                    break;
                case KeyDirection.Right:
                    newPosition = new Coordinates(position.X + 1, position.Y);
                    break;
                case KeyDirection.Up:
                    newPosition = new Coordinates(position.X, position.Y - 1);
                    break;
                case KeyDirection.Down:
                    newPosition = new Coordinates(position.X, position.Y + 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parKeyDirection), parKeyDirection, null);
            }
            
            // Проверка, является ли новая позиция пустой
            if (IsCellEmpty(newPosition))
            {
                // Перемещение юнита
                var oldPosition = unit.Position;
                unit.Position = newPosition;
                var fieldCellAtCoordinates = GetFieldCellAtCoordinates(newPosition);
                fieldCellAtCoordinates.Type = cellType;

                var oldCell = GetFieldCellAtCoordinates(oldPosition);
                oldCell.Type = CellType.Empty;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Проверка является ли ячека пустой
        /// </summary>
        /// <param name="position">Координата ячейки</param>
        /// <returns></returns>
        public bool IsCellEmpty(Coordinates position)
        {
            // Проверка, является ли клетка пустой
            if (IsValidCoordinate(position))
            {
                int index = (int)(position.X + position.Y * Columns);
                return FieldCells[index].Type == CellType.Empty;
            }

            // Если координата недопустима, считаем, что клетка не пуста
            return false;
        }

        /// <summary>
        /// Получить ячейку поля по координате
        /// </summary>
        /// <param name="parCoordinates"> координаты</param>
        /// <returns>ячейка</returns>
        public GameCell GetFieldCellAtCoordinates(Coordinates parCoordinates)
        {
            if (IsValidCoordinate(parCoordinates))
            {
                return FieldCells[(int)(parCoordinates.X + parCoordinates.Y * Columns)];
            }

            // Обработка недопустимой координаты, например, возврат null или другого значения по умолчанию
            return null;
        }

        /// <summary>
        /// Находится ли ячейка с заданными координатами в поле
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsValidCoordinate(Coordinates position)
        {
            // Проверка, находится ли координата в пределах игрового поля
            return position.X >= 0 && position.X < Columns && position.Y >= 0 && position.Y < Rows;
        }

        /// <summary>
        /// Перерироска поля
        /// </summary>
        public void ReDraw()
        {
            lock (_drawLock)
            {
                OnRedraw?.Invoke();
            }
        }
        

        /// <summary>
        /// Получить координату соседней ячейки в указанном направлении
        /// </summary>
        /// <param name="position">позиция</param>
        /// <param name="direction">направление</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private Coordinates GetLeftOrRightNeighborCell(Coordinates position, KeyDirection direction)
        {
            switch (direction)
            {
                case KeyDirection.Left:
                    return new Coordinates(position.X - 1, position.Y);
                case KeyDirection.Right:
                    return new Coordinates(position.X + 1, position.Y);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, "Ошибка в методе " +
                        "получения координаты соседа");
            }
        }

        /// <summary>
        /// получить Врага из списка
        /// </summary>
        /// <param name="storageCoord">координата</param>
        /// <returns>враг</returns>
        public Unit FindEnemyByCoordinates(Coordinates parEnemyCoordinates)
        {
            return Units.FirstOrDefault(unit => unit.Position.Equals(parEnemyCoordinates));
        }
        
        
    }
}