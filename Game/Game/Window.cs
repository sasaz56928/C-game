using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    class Window : Form
    {
        public Button CreateButton(int size, string text)
        {
            return new Button
            {
                Location = new Point(550, 600),
                Size = new Size(size, 50),
                Text = text
            };
        }

        public Label CreateLabel(string text, int locationY = 500)
        {
            return new Label
            {
                Location = new Point(480, locationY),
                Size = new Size(200, 50),
                Text = text
            };
        }

        public Label CreateLabelScore()
        {
            return new Label
            {
                Location = new Point(500, 100),
                Size = new Size(220, 50),
                Text = "Score = " + 0,
                BackColor = Color.Red
            };
        }


        public TextBox CreateBox()
        {
            return new TextBox
            {
                Location = new Point(480, 550),
                Size = new Size(200, 50)
            };
        }
    }
}
