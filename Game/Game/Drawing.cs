using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public class Drawing
    {
        /// <summary>
        /// Рисует подсказку: какая фигура будет дальше
        /// </summary>
        public static void DrawNextFigure(Graphics e, Figure figure, int cellSize)
        {
            for (var i = 0; i < figure.SizeOfNextMatrix; i++)
                for (var j = 0; j < figure.SizeOfNextMatrix; j++)
                    if (figure.NextMatrix[i, j] != 0)
                        e.FillRectangle(Brushes.Red, new Rectangle(500 + cellSize + j * cellSize + 1, 200 + cellSize + i * cellSize + 1, cellSize - 2, cellSize - 2));

        }
        /// <summary>
        /// Рисует фигуру на сетке
        /// </summary>
        public static void DrawMap(Graphics e, int[,] map, int cellSize)
        {
            for (var i = 0; i < 16; i++)
                for (var j = 0; j < 9; j++)
                {
                    if (map[i, j] != 0)
                        switch (map[i, j])
                        {
                            case (1):
                                e.FillRectangle(Brushes.Blue, new Rectangle(cellSize + j * cellSize + 1, cellSize + i * cellSize + 1, cellSize - 2, cellSize - 2));
                                break;
                            case (2):
                                e.FillRectangle(Brushes.Red, new Rectangle(cellSize + j * cellSize + 1, cellSize + i * cellSize + 1, cellSize - 2, cellSize - 2));
                                break;
                            case (3):
                                e.FillRectangle(Brushes.Green, new Rectangle(cellSize + j * cellSize + 1, cellSize + i * cellSize + 1, cellSize - 2, cellSize - 2));
                                break;
                            case (4):
                                e.FillRectangle(Brushes.Gray, new Rectangle(cellSize + j * cellSize + 1, cellSize + i * cellSize + 1, cellSize - 2, cellSize - 2));
                                break;
                            case (5):
                                e.FillRectangle(Brushes.Yellow, new Rectangle(cellSize + j * cellSize + 1, cellSize + i * cellSize + 1, cellSize - 2, cellSize - 2));
                                break;
                            case (6):
                                e.FillRectangle(Brushes.LightBlue, new Rectangle(cellSize + j * cellSize + 1, cellSize + i * cellSize + 1, cellSize - 2, cellSize - 2));
                                break;
                        }
                }
        }
        /// <summary>
        /// Рисует сетку
        /// </summary>
        public static void PaintGrid(Graphics e, int cellSize)
        {
            for (var i = 0; i <= 9; i++)
            {
                e.DrawLine(new Pen(Color.Black, 2), (i + 1) * cellSize, cellSize, (i + 1) * cellSize, 17 * cellSize);
            }

            for (var i = 0; i <= 16; i++)
            {
                e.DrawLine(new Pen(Color.Black, 2), cellSize, (i + 1) * cellSize, 10 * cellSize, (i + 1) * cellSize);
            }
        }

    }
}
