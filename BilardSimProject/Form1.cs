using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace BilardSimProject
{
    public partial class Form1 : Form
    {
        public Board _board;

        public int NumOfBalls;
        public int BallRadius;
        public int BallMargin;
        public int BallMass;

        public int BoardWidth;
        public int BoardHeight;


        public Form1()
        {
            InitializeComponent();
            NumOfBalls = 9;
            BallRadius = 20;
            BallMargin = 10;
            BoardWidth = 300;
            BoardHeight = 550;
            _board = new Board(new Vector2((pictureBox1.Width - BoardWidth) / 2, (pictureBox1.Height - BoardHeight) / 2), BoardWidth, BoardHeight, NumOfBalls, BallRadius, BallMargin, BallMass);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            _board.Draw(g);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _board.Update(10f);
            pictureBox1.Invalidate();
        }
    }
}
