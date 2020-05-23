using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TetrisTests
{
    [TestClass]
    public class TetrisTest
    {
        [TestMethod]
        public void MoveRightTest()
        {
            var a = new Tetris.MyForm();
            var key = new MouseEventArgs(MouseButtons.Left, 1, 575, 625, 0);
            
           // Assert.AreEqual();
        }
    }
}
