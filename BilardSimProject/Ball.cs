using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace BilardSimProject
{
    public class Ball
    {
        public Vector2 Pos;
        public Vector2 Vel;
        public Vector2 Acc;

        public float Mass;
        public float Diameter;

        private Brush _brush;

        public Ball(Vector2 pos, Vector2 vel, Vector2 acc, float mass, float diameter, Brush brush)
        {
            Pos = pos;
            Vel = vel;
            Acc = acc;
            Mass = mass;
            Diameter = diameter;
            _brush = brush;
        }

        public float calcKinematicEnergy ()
        {
            return Mass * this.Vel.LengthSquared() / 2;
        }   

        public void ApplyForce (Vector2 force)
        {
            Acc += force / Mass;
        }

        public void UpdateVell (Vector2 new_vell)
        {
            this.Vel = new_vell;
        }

        public void Update (float dtime)
        {
            this.Vel += this.Acc * dtime;

            this.Pos += this.Vel * dtime;

            this.Acc.X = 0;
            this.Acc.Y = 0;
        }

        public void Draw (Graphics g)
        {
            float x = this.Pos.X - this.Diameter / 2;
            float y = this.Pos.Y - this.Diameter / 2;
            g.DrawEllipse(new Pen(_brush), x, y, Diameter, Diameter);
        }
    }
}
