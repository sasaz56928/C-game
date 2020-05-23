using System;

namespace Tetris
{
    public class Figure
    {
        public int X;
        public int Y;
        public int[,] Matrix;
        public int[,] NextMatrix;
        public int SizeOfMatrix;
        public int SizeOfNextMatrix;


        public Figure(int x, int y, int fortests = 1)
        {
            X = x;
            Y = y;
            Matrix = CreateMatrix();
            SizeOfMatrix = Matrix.GetLength(0);
            var random = new Random();
            var randomCollor = random.Next(1, 7);
            for (var i = 0; i < SizeOfMatrix; i++)
                for (var j = 0; j < SizeOfMatrix; j++)
                    if (Matrix[i, j] == -1)
                        Matrix[i, j] = randomCollor;
            NextMatrix = CreateMatrix(fortests);
            SizeOfNextMatrix = NextMatrix.GetLength(0);
            CreateCollor();
        }

        public void GetNextFigure(int x, int y)
        {
            X = x;
            Y = y;
            Matrix = NextMatrix;
            SizeOfMatrix = Matrix.GetLength(0);
            NextMatrix = CreateMatrix();
            SizeOfNextMatrix = NextMatrix.GetLength(0);
            CreateCollor();
        }

        /// <summary>
        /// Создаёт матрицу
        /// </summary>
        private int[,] CreateMatrix(int forTests = 1)
        {
                var next = new int[1, 1];
                var blanks = new BlanksFigures();
                var random = new Random();
                switch (random.Next(forTests, 9))
                {
                    case (1):
                        next = blanks.figure1;
                        break;
                    case (2):
                        next = blanks.figure2;
                        break;
                    case (3):
                        next = blanks.figure3;
                        break;
                    case (4):
                        next = blanks.figure4;
                        break;
                    case (5):
                        next = blanks.figure5;
                        break;
                    case (6):
                        next = blanks.figure6;
                        break;
                    case (7):
                        next = blanks.figure7;
                        break;
                    case (8):
                        next = blanks.figure8;
                        break;
                }
                return next;
        }

        /// <summary>
        /// Поворачивет фигуру
        /// </summary>
        public void Rotate()
        {
            if (SizeOfMatrix < 3)
                return;
            int[,] rotatedMatrix = new int[SizeOfMatrix, SizeOfMatrix];
            for (int i = 0; i < SizeOfMatrix; i++)
                for (int j = 0; j < SizeOfMatrix; j++)
                    rotatedMatrix[i, j] = Matrix[j, (SizeOfMatrix - 1) - i];//найденая формула для разворота

            Matrix = rotatedMatrix;
            int borderChecker = (8 - (X + SizeOfMatrix));
            if (borderChecker < 0)//2 if'a для того, чтобы фигура не выходила за границы карты
                for (int i = 0; i < Math.Abs(borderChecker); i++)
                    MoveLeft();

            if (X < 0)
                for (int i = 0; i < Math.Abs(X) + 1; i++)
                    MoveRight();
        }

        /// <summary>
        /// Выбирает цвет фигуры
        /// </summary>
        private void CreateCollor()
        {
            var random = new Random();
            var randomCollor = random.Next(1, 7);
            for (var i = 0; i < SizeOfNextMatrix; i++)
                for (var j = 0; j < SizeOfNextMatrix; j++)
                    if (NextMatrix[i, j] == -1)
                        NextMatrix[i, j] = randomCollor;
        }

        public void MoveDown()
        {
            Y++;
        }
        public void MoveRight()
        {
            X++;
        }
        public void MoveLeft()
        {
            X--;
        }
    }

    public class BlanksFigures
    {
        public int[,] figure1 = new int[4, 4]
        {
            {0,0,-1,0 },
            {0,0,-1,0 },
            {0,0,-1,0 },
            {0,0,-1,0 }
        };
        public int[,] figure2 = new int[3, 3]
        {
            {0,-1, 0, },
            {0,-1,-1, },
            {0, 0,-1, },
        };
        public int[,] figure3 = new int[3, 3]
        {
            { 0,-1,0, },
            {-1,-1,0, },
            {-1, 0,0, },
        };
        public int[,] figure4 = new int[3, 3]
        {
            {0,-1, 0, },
            {0,-1,-1, },
            {0,-1, 0, }

        };
        public int[,] figure5 = new int[3, 3]
        {
            {-1,-1,0, },
            { 0,-1,0, },
            { 0,-1,0, },

        };
        public int[,] figure6 = new int[3, 3]
        {
            {0,-1,-1, },
            {0,-1, 0, },
            {0,-1, 0, },

        };
        public int[,] figure7 = new int[2, 2]
        {
            {-1,-1, },
            {-1,-1, }

        };
        public int[,] figure8 = new int[1, 1]
        {
            {-1 }
        };
    }
}
