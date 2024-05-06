using System.Drawing.Printing;
using System.Linq;
using Game.Game;

namespace TestsGame
{
    /// <summary>
    /// Класс тестов для проверки функциональности игрового поля.
    /// </summary>
    public class MainGameTests
    {
        private MainGame _mainGame;

        /// <summary>
        /// Устанавливает начальные условия перед выполнением каждого теста.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _mainGame = new MainGame();
        }
        
        /// <summary>
        /// Тест проверяет, что внешние стены создаются на правильных местах для заданного числа строк и столбцев
        /// </summary>
        [Test]
        public void CreateOuterWalls_CreatesWallsAroundField()
        {
            int columns = 10; // Предполагаем 10 столбцов
            int rows = 8; // Предполагаем 8 строк

            MainGame.Columns = columns;
            MainGame.Rows = rows;
            _mainGame.CreateOuterWalls();
            // Проверяем, что внешние стены созданы корректно

            // Проверяем верхнюю и нижнюю горизонтальные стены
            for (int i = 0; i < columns; i++)
            {
                Assert.That(_mainGame.GetFieldCellAtCoordinates(new Coordinates(i, 0)).Type, Is.EqualTo(CellType.Wall));
                Assert.That(_mainGame.GetFieldCellAtCoordinates(new Coordinates(i, rows - 1)).Type, Is.EqualTo(CellType.Wall));
            }

            // Проверяем левую и правую вертикальные стены
            for (int i = 0; i < rows - 1; i++)
            {
                Assert.That(_mainGame.GetFieldCellAtCoordinates(new Coordinates(0, i)).Type, Is.EqualTo(CellType.Wall));
                Assert.That(_mainGame.GetFieldCellAtCoordinates(new Coordinates(columns - 1, i)).Type, Is.EqualTo(CellType.Wall));
            }
        }
        
        /// <summary>
        /// Тест проверяет что враги созданы в правильных ячейках
        /// </summary>
        [Test]
        public void CreateEnemies_CreatesEnemiesInCorrectCells()
        {
            int columns = 10; // Предполагаем 10 столбцов
            int rows = 8; // Предполагаем 8 строк
            MainGame.Columns = columns;
            MainGame.Rows = rows;
            // Проверяем, что враги созданы в правильных ячейках
            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < columns - 1; j++)
                {
                    CellType cellType = _mainGame.GetFieldCellAtCoordinates(new Coordinates(j, i)).Type;
                    if (cellType == CellType.Enemy)
                    {
                        // Проверяем, что враг создан только в случайной ячейке
                        Assert.That(_mainGame.GetFieldCellAtCoordinates(new Coordinates(j, i)).Type, Is.EqualTo(CellType.Enemy));
                    }
                    else
                    {
                        // Проверяем, что в пустых ячейках нет врагов
                        Assert.That(_mainGame.GetFieldCellAtCoordinates(new Coordinates(j, i)).Type, Is.Not.EqualTo(CellType.Enemy));
                    }
                }
            }
        }
        
        /// <summary>
        /// Тест проверяет обновляются ли тип клетки и позиция игрока при достижении игроком верхней стены
        /// </summary>
        [Test]
        public void CheckNewDifficultyLevel_PlayerReachesTopOfField_PositionAndCellTypeUpdated()
        {
            _mainGame.Player.Position = new Coordinates(3, 1); // Предполагаем, что игрок достиг верхней части поля
            _mainGame.CheckNewDifficultyLevel();
            Assert.That(_mainGame.Player.Position, Is.EqualTo(new Coordinates(3, MainGame.Rows - 2)));
            Assert.That(_mainGame.GetFieldCellAtCoordinates(_mainGame.Player.Position).Type, Is.EqualTo(CellType.Player));
        }

        /// <summary>
        /// Тест проверяет увеличивается ли скорость врагов при достижении игроком верхней стены
        /// </summary>
        [Test]
        public void CheckNewDifficultyLevel_PlayerReachesTopOfField_ClockDelayDecremented()
        {
            _mainGame.Player.Position = new Coordinates(3, 1); // Предполагаем, что игрок достиг верхней части поля
            _mainGame.ClockDelay = 300; // Предполагаем, что начальная задержка ClockDelay равна 300
            _mainGame.CheckNewDifficultyLevel();
            Assert.That(_mainGame.ClockDelay, Is.EqualTo(200)); // Предполагаем, что ClockDelay уменьшается на 100
        }

        /// <summary>
        /// Тест проверяет увеличивается ли число очков игрока при достижении им верхней стены
        /// </summary>
        [Test]
        public void CheckNewDifficultyLevel_PlayerReachesTopOfField_PlayerScoreIncremented()
        {
            _mainGame.Player.Position = new Coordinates(3, 1); // Предполагаем, что игрок достиг верхней части поля
            _mainGame.CheckNewDifficultyLevel();
            Assert.That(_mainGame.Player.Score, Is.EqualTo(1)); // Предполагаем, что количество очков игрока увеличивается на 1
        }

        /// <summary>
        /// Тест проверяет что число очков не должно увеличится, если игрок не достиг верхней стены
        /// </summary>
        [Test]
        public void CheckNewDifficultyLevel_PlayerDoesNotReachTopOfField_NoChangeInState()
        {
            var player = new Player();
            _mainGame.Player.Position = new Coordinates(3, 3); // Предполагаем, что игрок не достиг верхней части поля
            _mainGame.ClockDelay = 300; // Предполагаем, что начальная задержка ClockDelay равна 300
            _mainGame.CheckNewDifficultyLevel();
            Assert.That(player.Score, Is.EqualTo(0)); // Очки игрока должны остаться неизменными
        }
        
        /// <summary>
        /// Тест проверяет появлется ли игрок на правильной позиции после возрождения и является ли клетка где он появился типа "игрок"
        /// </summary>
        [Test]
        public void RespawnPlayerAfterDeath_PlayerRespawnsAtCorrectPosition()
        {
            var initialPlayerPosition = new Coordinates(3, 3); // Предполагаем начальную позицию игрока
            var respawnPosition = new Coordinates(MainGame.Columns / 2, MainGame.Rows - 2); // Предполагаем позицию возрождения
            _mainGame.GetFieldCellAtCoordinates(initialPlayerPosition).Type = CellType.Empty;
            _mainGame.Player.Position = initialPlayerPosition; // Предполагаем, что игрок с здоровьем = 2
            _mainGame.GetFieldCellAtCoordinates(respawnPosition).Type = CellType.Player;
            _mainGame.RespawnPlayerAfterDeath();
            Assert.That(_mainGame.Player.Position, Is.EqualTo(respawnPosition));
            Assert.That(_mainGame.GetFieldCellAtCoordinates(respawnPosition).Type, Is.EqualTo(CellType.Player));
        }

        /// <summary>
        /// Тест проверяет корректность перемещения (в данном случае левого, но они все одинаковые)
        /// </summary>
        [Test]
        public void MoveUnit_Left_SuccessfullyMovesUnitLeft()
        {
            Unit _unit = new Unit(1, new Coordinates(2, 2),0);
            GameCell _gameCell = new GameCell(new Coordinates(1, 2), CellType.Empty);
            
            bool moveSuccessful = _mainGame.TryMoveUnit(KeyDirection.Left, _unit, CellType.Enemy);
            
            Assert.IsTrue(moveSuccessful);
            Assert.That(_unit.Position, Is.EqualTo(new Coordinates(1, 2)));
            Assert.That(_mainGame.GetFieldCellAtCoordinates(new Coordinates(1, 2)).Type, Is.EqualTo(CellType.Enemy));
            Assert.That(_mainGame.GetFieldCellAtCoordinates(new Coordinates(2, 2)).Type, Is.EqualTo(CellType.Empty));
        }
        
        /// <summary>
        /// Тест проверяет правильность определения находится ли игрок в направлении движения врага
        /// </summary>
        [Test]
        public void HasPlayerInVision_PlayerToLeft_ReturnsTrue()
        {
            _mainGame.Player.Position = new Coordinates(2, 3);
            var cell = _mainGame.GetFieldCellAtCoordinates(new Coordinates(2, 3));
            cell.Type = CellType.Player;
            var unit = new Unit(2,new Coordinates(3, 3),0); // Предполагаем, что юнит находится в (3, 3)
            var playerInVision = _mainGame.IsPlayerInDirectionOfMove(unit);
            Assert.IsTrue(playerInVision);
        }


        /// <summary>
        /// Тест проверяет завершение игры при нулевом здоровье игрока.
        /// </summary>
        [Test]
        public void TestCheckGameOver_PlayerHealthZero()
        {
            // Установим здоровье игрока в 0
            _mainGame.Player.Health = 0;
            // Проверим, что игра завершается
            Assert.IsTrue(_mainGame.CheckGameOver());
        }
        

        /// <summary>
        /// Тест проверяет, что клетка пуста по корректной позиции.
        /// </summary>
        [Test]
        public void TestIsCellEmpty_ValidEmptyPosition()
        {
            // Установим позицию, которая должна быть пустой
            Coordinates emptyPosition = new Coordinates(5, 5);
            int cols = MainGame.Columns;
            int index = (int)(emptyPosition.X + emptyPosition.Y * cols);
            _mainGame.FieldCells[index].Type = CellType.Empty;
            Assert.IsTrue(_mainGame.IsCellEmpty(emptyPosition));
        }

        /// <summary>
        /// Тест проверяет, что клетка не пуста по корректной позиции.
        /// </summary>
        [Test]
        public void TestIsCellEmpty_ValidNonEmptyPosition()
        {
            // Установим позицию, которая должна быть не пустой
            Coordinates nonEmptyPosition = new Coordinates(0, 0);
            // Проверим, что клетка не пуста
            Assert.IsFalse(_mainGame.IsCellEmpty(nonEmptyPosition));
        }

        /// <summary>
        /// Тест проверяет получение клетки поля по корректной позиции.
        /// </summary>
        [Test]
        public void TestGetFieldCellAtCoord_ValidPosition()
        {
            // Установим корректную позицию
            Coordinates validPosition = new Coordinates(0, 0);
            // Проверим, что полученная клетка не равна null
            Assert.IsNotNull(_mainGame.GetFieldCellAtCoordinates(validPosition));
        }

        /// <summary>
        /// Тест проверяет получение клетки поля по некорректной позиции.
        /// </summary>
        [Test]
        public void TestGetFieldCellAtCoord_InvalidPosition()
        {
            // Установим некорректную позицию
            Coordinates invalidPosition = new Coordinates(-1, -1);
            // Проверим, что полученная клетка равна null
            Assert.IsNull(_mainGame.GetFieldCellAtCoordinates(invalidPosition));
        }
        

        /// <summary>
        /// Тест проверяет поиск врага по корректной координате.
        /// </summary>
        [Test]
        public void TestFindEnemyByCoord_ValidCoord()
        {
            // Получим координаты первого врага на поле
            Coordinates enemyCoordinates = _mainGame.Units[0].Position;
            // Проверим, что найденный враг не равен null
            Assert.IsNotNull(_mainGame.FindEnemyByCoordinates(enemyCoordinates));
        }

        /// <summary>
        /// Тест проверяет поиск врага по некорректной координате.
        /// </summary>
        [Test]
        public void TestFindEnemyByCoord_InvalidCoord()
        {
            // Установим некорректные координаты
            Coordinates invalidCoordinates = new Coordinates(-1, -1);
            // Проверим, что найденный враг равен null
            Assert.IsNull(_mainGame.FindEnemyByCoordinates(invalidCoordinates));
        }
        

        /// <summary>
        /// Тест проверяет остановку игры.
        /// </summary>
        [Test]
        public void TestStop()
        {
            // Установим флаг завершения игры в false
            _mainGame.IsGameEnd = false;

            // Остановим игру
            _mainGame.Stop();

            // Проверим, что флаг завершения игры изменился на true
            Assert.IsTrue(_mainGame.IsGameEnd);
        }

        /// <summary>
        /// Тест проверяет корректность координаты.
        /// </summary>
        [Test]
        public void TestIsValidCoordinate()
        {
            // Проверим корректные координаты
            Assert.IsTrue(_mainGame.IsValidCoordinate(new Coordinates(0, 0)));
            Assert.IsTrue(_mainGame.IsValidCoordinate(new Coordinates(MainGame.Columns - 1, MainGame.Rows - 1)));

            // Проверим некорректные координаты
            Assert.IsFalse(_mainGame.IsValidCoordinate(new Coordinates(-1, 0)));
            Assert.IsFalse(_mainGame.IsValidCoordinate(new Coordinates(MainGame.Columns, 0)));
            Assert.IsFalse(_mainGame.IsValidCoordinate(new Coordinates(0, -1)));
            Assert.IsFalse(_mainGame.IsValidCoordinate(new Coordinates(0, MainGame.Rows)));
        }
    }
}
