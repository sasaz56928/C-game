using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Checked
    {
        /// <summary>
        /// Проверка на возможность падения фигуры
        /// </summary>
        public static bool CheckFallPossibility(Figure figure, int[,] map)
        {
            for (var i = figure.Y + figure.SizeOfMatrix - 1; i >= figure.Y; i--)
                for (var j = figure.X; j < figure.X + figure.SizeOfMatrix; j++)
                    if (figure.Matrix[i - figure.Y, j - figure.X] != 0)//нахождение материальных частей фигуры начиная снизу
                    {
                        if (i + 1 == 16)//проверка на низ сетки
                            return false;
                        if (map[i + 1, j] != 0)//проверка на пустое место в сетке
                            return false;
                    }
            return true;
        }
        /// <summary>
        /// Проверка на возможность движения в стороны фигуры
        /// </summary>
        public static bool CheckMovePossibility(int directions, Figure figure, int[,] map)
        {
            for (var i = figure.Y; i < figure.Y + figure.SizeOfMatrix; i++)
                for (var j = figure.X; j < figure.X + figure.SizeOfMatrix; j++)
                    if (figure.Matrix[i - figure.Y, j - figure.X] != 0)//нахождение материальных частей фигуры
                    {
                        if (j + 1 * directions > 8 || j + 1 * directions < 0)//проверка, выходим ли за сетку по команде
                            return false;
                        if (map[i, j + 1 * directions] != 0)//проверка что рядом стоит фигура и позже на их столкновения
                        {
                            if (j - figure.X + 1 * directions >= figure.SizeOfMatrix || j - figure.X + 1 * directions < 0)
                                return false;
                            if (figure.Matrix[i - figure.Y, j - figure.X + 1 * directions] == 0)
                                return false;
                        }
                    }
            return true;

        }
        /// <summary>
        /// Проверка на возможность поворота фигуры
        /// </summary>
        public static bool CheckPossibilityRotare(Figure figure, int[,] map)
        {
            for (var i = figure.Y; i < figure.Y + figure.SizeOfMatrix; i++)
                for (var j = figure.X; j < figure.X + figure.SizeOfMatrix; j++)
                    if (j >= 0 && j < 8)
                        if (figure.Matrix[i - figure.Y, j - figure.X] == 0 & map[i, j] != 0)
                            return false;
            return true;
        }
    }
}
