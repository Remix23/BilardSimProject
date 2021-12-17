using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace BilardSimProject
{
    public class PhysicsEngine
    {

        private int _boardWidth;
        private int _boardHeight;
        private int _ballRadius;

        public PhysicsEngine(int boardWidth, int boardHeight, int ballRadius)
        {
            _ballRadius = ballRadius;
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
        }

        public List<Tuple<Ball, Ball>> GetCollidingBalls (List<Ball> balls)
        {

            List<Tuple<Ball, Ball>> colliding_balls = new List<Tuple<Ball, Ball>>();

            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    if (_doBallsCollided(balls[i], balls[j])) colliding_balls.Add(Tuple.Create(balls[i], balls[j]));
                }
            }
            return colliding_balls;
        }

        public void UpdateBallAfterCollision(Ball b1, Ball b2)
        {
            float distance =  (float)(Math.Sqrt(Math.Pow(b2.Pos.X - b1.Pos.X, 2) + Math.Pow(b2.Pos.Y - b1.Pos.Y, 2)));

            // normal 
            float nx = (b2.Pos.X - b1.Pos.X) / distance;
            float ny = (b2.Pos.Y - b1.Pos.Y) / distance;

            // tangent
            float tx = -ny;
            float ty = nx;

            // dot product tangent
            float dpTan1 = b1.Vel.X * tx + b1.Vel.Y * ty;
            float dpTan2 = b2.Vel.X * ty + b2.Vel.Y * tx;

            // dot product normal 
            float dpNorm1 = b1.Vel.X * nx + b1.Vel.Y * ny;
            float dpNorm2 = b2.Vel.X * nx + b2.Vel.Y * ny;

            // conservation of momentum in 1D
            float m1 = (dpNorm1 * (b1.Mass - b2.Mass) + 2 * b2.Mass * dpNorm2) / (b1.Mass + b2.Mass);
            float m2 = (dpNorm2 * (b2.Mass - b1.Mass) + 2 * b1.Mass * dpNorm1) / (b2.Mass + b1.Mass);

            // update ball velocities
            float b1_result_vx = tx * dpTan1 + nx * m1;
            float b1_result_vy = ty * dpTan1 + ny * m1;
            float b2_result_vx = tx * dpTan2 + nx * m2;
            float b2_result_vy = ty * dpTan2 + ny * m2;

            b1.Vel = new Vector2(b1_result_vx, b1_result_vy);
            b2.Vel = new Vector2(b2_result_vx, b2_result_vy);
        }

        public float SignedDistanceToBall (Vector2 b1_pos, Point point, int ball_radius)
        {
            return new Vector2(b1_pos.X - point.X, b1_pos.Y - point.Y).Length() - ball_radius;
        }

        private bool _doBallsCollided (Ball b1, Ball b2)
        {
            return Math.Abs(Math.Pow(b2.Pos.X - b1.Pos.X, 2) + Math.Pow(b2.Pos.Y - b1.Pos.Y, 2)) <= Math.Pow(b1.Diameter / 2 + b2.Diameter / 2, 2);
        }

        public void CollideWithBox (Ball b1, Vector2 board_offset, Vector2 board_dimensions)
        {
            if (b1.Pos.X <= board_offset.X || b1.Pos.X >= board_offset.X + board_dimensions.X)
            {
                b1.Vel.X *= -1;
            }
            if (b1.Pos.Y <= board_offset.Y || b1.Pos.Y >= board_offset.Y + board_dimensions.Y)
            {
                b1.Vel.Y *= -1;
            }
        }
    }
}
