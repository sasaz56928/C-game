using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace Tetris
{

    public class MyForm : Form
    {
        private Figure figure;
        private readonly int[,] map = new int[16, 9];
        const int CellSize = 40;
        private int score;
        private int factorScore;
        private int startSpeed;
        public MyForm(int forTests = 1)
        {

            score = 0;
            factorScore = 10;
            startSpeed = 500;
            figure = new Figure(3, 0, forTests);

            BackgroundImage = new Bitmap(@"G:\C# game\Game\Game\Background1.jpg");
            DoubleBuffered = true;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            var window = new Window();
            var button = window.CreateButton(50, "OK");
            var buttonRestart = window.CreateButton(100, "Начать заного");
            var labelLost = window.CreateLabel("Вы проиграли");
            var labelName = window.CreateLabel("Введите своё имя");
            var labelInstructions = window.CreateLabel("Управление осуществляется стрелками:\nПеворот - стрелка вверх\nБыстрое падение - стрелка вниз", 430);
            var labelScore = window.CreateLabelScore();
            labelName.Font = new Font(labelName.Font.FontFamily, 15.0f, labelName.Font.Style);
            labelScore.Font = new Font(labelScore.Font.FontFamily, 30.0f, labelScore.Font.Style);
            var box = window.CreateBox();
            Controls.Add(labelInstructions);

            Played(button, buttonRestart, labelName, labelScore, labelLost, box);
        }

        public void Played(Button button, Button buttonRestart,
            Label labelName, Label labelScore, Label labelLost,
            TextBox box
            )
        {
            Controls.Add(button);
            Controls.Add(labelName);
            Controls.Add(labelScore);
            Controls.Add(box);

            var timer = new Timer();
            this.KeyDown += (sender, args) =>
            {
                switch (args.KeyCode)
                {
                    case Keys.Down:
                        timer.Interval = 50;
                        break;
                    case Keys.Up:
                        if (Checked.CheckPossibilityRotare(figure, map))
                        {
                            ResetCells();
                            figure.Rotate();
                            Combine();
                            Invalidate();
                        }
                        break;

                    case Keys.Right:
                        if (Checked.CheckMovePossibility(1, figure, map))
                        {
                            ResetCells();
                            figure.MoveRight();
                            Combine();
                            Invalidate();
                        }
                        break;
                    case Keys.Left:
                        if (Checked.CheckMovePossibility(-1, figure, map))
                        {
                            ResetCells();
                            figure.MoveLeft();
                            Combine();
                            Invalidate();
                        }
                        break;
                }
            };
            CreateSpeed(timer);





            button.Click += (sender, args) =>
            {
                var sw = new StreamWriter(@"G:\C# game\Game\Game\Рекорды.txt", true);
                sw.Write(box.Text);
                Controls.Remove(button);
                Controls.Remove(box);
                Controls.Remove(labelName);
                timer.Tick += (senderT, argsT) =>
                {
                    ResetCells();
                    if (Checked.CheckFallPossibility(figure, map))
                        figure.MoveDown();
                    else
                    {
                        Combine();
                        DeleteCompletedLine(labelScore);
                        CreateSpeed(timer);
                        figure.GetNextFigure(3, 0);
                        if (!Checked.CheckFallPossibility(figure, map))
                        {
                            timer.Stop();
                            sw.WriteLine(": " + labelScore.Text);
                            sw.Close();
                            Controls.Add(labelLost);
                            Controls.Add(buttonRestart);
                            buttonRestart.Click += (senderB, argB) =>
                            {
                                Application.Restart();

                            };
                        }
                    }
                    Combine();
                    Invalidate();
                };
            };
            timer.Start();
            Invalidate();
        }
        /// <summary>
        /// Cопоставить массив карты и массив фигуры (синхронизация)
        /// </summary>
        private void Combine()//сопоставляет массив карты и массив фигуры
        {
            for (var i = figure.Y; i < figure.Y + figure.SizeOfMatrix; i++)
                for (var j = figure.X; j < figure.X + figure.SizeOfMatrix; j++)
                    if (figure.Matrix[i - figure.Y, j - figure.X] != 0)
                        map[i, j] = figure.Matrix[i - figure.Y, j - figure.X];
        }

        /// <summary>
        /// Очищает сетку от предыдущего положения фигуры
        /// (От фигуры предыдущего тика)
        /// </summary>
        private void ResetCells()
        {
            for (var i = figure.Y; i < figure.Y + figure.SizeOfMatrix; i++)
                for (var j = figure.X; j < figure.X + figure.SizeOfMatrix; j++)
                    if (i >= 0 && j >= 0 && i < 16 && j < 9)
                        if (figure.Matrix[i - figure.Y, j - figure.X] != 0)

                            map[i, j] = 0;
        }

        /// <summary>
        /// Убирает завершённую линию и добавляет очки
        /// </summary>
        private void DeleteCompletedLine(Label labelScore)
        {
            var countCompletedCell = 0;
            var countCompletedLine = 0;
            for (var i = 0; i < 16; i++)
            {
                countCompletedCell = 0;
                for (var j = 0; j < 9; j++)
                    if (map[i, j] != 0)
                        countCompletedCell++;
                if (countCompletedCell == 9)
                {
                    countCompletedLine++;
                    for (var iShift = i; iShift > 0; iShift--)
                        for (var jShift = 0; jShift < 9; jShift++)
                            map[iShift, jShift] = map[iShift - 1, jShift];
                }
            }
            for (var i = 1; i <= countCompletedLine; i++)
                score += countCompletedLine * i;

            labelScore.Text = "Score = " + score;
        }
        /// <summary>
        /// Расчёт скорости относительно счёта
        /// </summary>
        private void CreateSpeed(Timer timer)
        {
            timer.Interval = startSpeed;
            if (score > factorScore && timer.Interval > 120)
            {
                factorScore += 10;
                timer.Interval -= factorScore * 2;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Drawing.PaintGrid(e.Graphics, CellSize);
            Drawing.DrawMap(e.Graphics, map, CellSize);
            Drawing.DrawNextFigure(e.Graphics, figure, CellSize);
        }

    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MyForm() { ClientSize = new Size(750, 700) });
        }
    }
}