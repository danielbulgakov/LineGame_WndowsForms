using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LineGame
{
    public partial class Form1 : Form
    {
        List<Line> LL;
        Timer timer;
        
        int CountStart = 5;

        int IntroTime = 200;
        int GameTime = 1000;

        public Form1()
        {
            
            InitializeComponent();
            LL = new List<Line>();

            timer = new Timer();

            timer.Interval = IntroTime;
            timer.Tick += MakeGame;
            timer.Start();

            LL.Add(new Line(this.Height, this.Width));
          
            
        }

        private void MakeGame(object sender, EventArgs e)
        {
            if (LL.Count >= CountStart)
                timer.Interval = GameTime;
            LL.Add(new Line(this.Height, this.Width));
            Invalidate();
        }

        private void PaintLines(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();
            g.Clear(Color.White);

            if (LL.Count == 0)
            {
                
                timer.Stop();
                textBox1.Text = "You Win!";
                textBox1.Visible = true;
                
                

            }

            else if (LL.Count == CountStart * 2)
            {
                timer.Stop();
                textBox1.Text = "Game Over!";
                textBox1.Visible = true;
                
                

            }

            foreach (Line line in LL)
            {
                line.Paint(g);
            }
        }

        private void LineClicked(object sender, MouseEventArgs e)
        {
            if (timer.Interval == IntroTime ) return;
            if (LL.Count == 2 * CountStart) return;
            int susline = -1;
            for (int i = 0; i < LL.Count; i++)
                if (LL[i].isInside(e.X, e.Y)) susline = i;

            if (susline == -1) return;

            for (int j = susline + 1; j < LL.Count; j++)
                if (LL[j].isOver(LL[susline])) return;

            LL.Remove(LL[susline]);
            Invalidate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}