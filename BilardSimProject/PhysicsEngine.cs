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

        public List<KeyValuePair<Ball, Ball>> GetCollidingBalls (List<Ball> balls)
        {

            List<KeyValuePair<Ball, Ball>> colliding_balls = new List<KeyValuePair<Ball, Ball>>();

            foreach (Ball b1 in balls)
            {
                foreach (Ball b2 in balls)
                {
                    if (b2 == b1) continue;

                    float d = _signedDistanceToBall(b1.Pos, b2.Pos, _ballRadius);
                    if (d <= 0) colliding_balls.Add(new KeyValuePair<Ball, Ball>(b1, b2));
                }
            }
            return colliding_balls;
        }

        public Vector2 GetForceOfCollision(Ball ball, List<Ball> balls)
        {
            Vector2 forces = Vector2.Zero;


            return forces;
        }

        private float _signedDistanceToBall (Vector2 pos, Vector2 circlePos, int ballRsdius)
        {
            return new Vector2(circlePos.X - pos.X, circlePos.Y - pos.Y).Length() - ballRsdius;
        }

        static Vector2 DistanceToPlane (Vector2 pos, int x1, int y1, int x2, int y2)
        {
            if (pos.X < x1 || pos.X > x2 || pos.Y < y1 || pos.Y > y2)
            {
                return new Vector2(-1, -1);
            }
            return new Vector2(Math.Abs(x1 - pos.X), Math.Abs(y1 - pos.Y));
        }
    }
}
