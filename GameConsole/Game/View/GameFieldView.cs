using System;
using Game.Game;
using Game.View;

namespace GameConsole.Game.View
{
    /// <summary>
    /// Класс представления игрового поля.
    /// </summary>
    public class GameFieldView : IView
    {
        private const char WALL_CHAR = '█';
        private const char ENEMY_CHAR = '#';
        private const char PLAYER_CHAR = 'O';

        private MainGame _mainGame;

        /// <summary>
        /// Создает новый экземпляр представления игрового поля.
        /// </summary>
        /// <param name="parMainGame">Игровое поле.</param>
        public GameFieldView(MainGame parMainGame)
        {
            _mainGame = parMainGame;
            _mainGame.OnGameOver += DrawMainGameOver;
            _mainGame.OnRedraw += Draw;
        }

        /// <summary>
        /// Отрисовывает текущее состояние игрового поля.
        /// </summary>
        public void Draw()
        {
            FastOutput fs = FastOutput.GetInstance();
            fs.ClearScreen();
            
                for (int i = 0; i < _mainGame.FieldCells.Length; i++)
                {
                    char charToDraw = ' ';

                    if (_mainGame.FieldCells[i].Type == CellType.Wall)
                    {
                        charToDraw = WALL_CHAR;
                    }
                    else if (_mainGame.FieldCells[i].Type == CellType.Enemy)
                    {
                        charToDraw = ENEMY_CHAR;
                    }
                    else if (_mainGame.FieldCells[i].Type == CellType.Player)
                    {
                        charToDraw = PLAYER_CHAR;
                    }

                    switch (charToDraw)
                    {
                        case PLAYER_CHAR:
                            fs.OutputCharacter(charToDraw, 3, 66, (int)_mainGame.FieldCells[i].Coordinates.X,
                                (int)_mainGame.FieldCells[i].Coordinates.Y, 1, 1);
                            break;
                        case ENEMY_CHAR:
                            fs.OutputCharacter(charToDraw, 5, 36, (int)_mainGame.FieldCells[i].Coordinates.X,
                                (int)_mainGame.FieldCells[i].Coordinates.Y, 1, 1);
                            break;
                        default:
                            fs.OutputCharacter(charToDraw, 0, 0, (int)_mainGame.FieldCells[i].Coordinates.X,
                                (int)_mainGame.FieldCells[i].Coordinates.Y, 1, 1);
                            break;
                    }
                }

                // отображения других элементов, таких как счет и новые блоки
                fs.OutputString("HP: " + _mainGame.Player.Health, 0, 127, 31, 11);
                fs.OutputString("Score: " + _mainGame.Player.Score, 0, 127, 31, 12);
                
        }

        /// <summary>
        /// Отрисовывает экран окончания игры.
        /// </summary>
        public void DrawMainGameOver()
        {
            FastOutput fs = FastOutput.GetInstance();
            fs.OutputString("GAME OVER!", 0, 0x44, 10, 5);
        }
    }
}
