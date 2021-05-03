using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LineGame
{
    class Line 
    {
        Rectangle _Line;
        Color LineColor;

        int X, Y; // координаты линии
        int W = 230, H = 35; // размеры линии
        int IsHorizontal; // Поворот фигуры

        int ClientWidth;
        int ClientHeight;

        private enum LineColors
        {
            Red,
            Yellow,
            Khaki,
            SteelBlue,
            Pink,
            CadetBlue,
            OrangeRed,
            Orchid,
            Olive,

        };

        public Line(int ClientHeight, int ClientWidth )
        {
            this.ClientHeight = ClientHeight;
            this.ClientWidth = ClientWidth;
            MakeRandom();
            _Line = new Rectangle(X, Y, W, H);
            
            
        }

        internal void Paint(Graphics g)
        {
            SolidBrush brash = new SolidBrush(LineColor);
            g.FillRectangle(brash, _Line);
            g.DrawRectangle(Pens.Black, _Line);
        }

        private void MakeRandom()
        {
            Random randomGen = new Random();
            Array values = Enum.GetValues(typeof(LineColors));
            KnownColor randomBar = (KnownColor)values.GetValue(randomGen.Next(values.Length));
            string name = Enum.GetName(typeof(LineColors), randomBar);
            var type = typeof(System.Drawing.Color);
            LineColor = (System.Drawing.Color)type.GetProperty(name).GetValue(null);

            Random randomGen1 = new Random((int)(DateTime.Now.Ticks));
            IsHorizontal = randomGen1.Next(0, 250);
            if (IsHorizontal % 2 == 0)
            {
                (W, H) = (H, W);
                X = randomGen1.Next(30, ClientWidth - W - H);
                Y = randomGen1.Next(30, ClientHeight - W - H);
            }
            else
            {
                X = randomGen1.Next(30, ClientWidth - W - H);
                Y = randomGen1.Next(30, ClientHeight - W - H);
            }


        }

        internal bool isInside(int x, int y)
        {
            if (x < _Line.Left || x > _Line.Right)
            {
                return false;
            }
            if (y < _Line.Top || y > _Line.Bottom)
            {
                return false;
            }
            return true;
        }

        internal bool isOver(Line l)
        {
            return l._Line.IntersectsWith(this._Line);
        }
    }
}
