﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace BilardSimProject
{
    public class Board
    {
        private PhysicsEngine _physics;
        private Vector2 _position;

        private int _ballRadius;
        private int _ballMargin;

        private int _numOfBalls;
        private int _boardWidth;
        private int _boardHeight;
        private float _halfnWidth;
        private float _baseY;
        private int _ballMass;
        private List<Ball> _balls;

        public Board (Vector2 pos, int boardWidth, int boardHeight, int numOfBalls, int ballRadius, int ballMargin, int ballMass)
        {
            _physics = new PhysicsEngine ();
            _balls = new List<Ball> ();
            _position = pos;
            _numOfBalls = numOfBalls;
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
            _ballRadius = ballRadius;
            _ballMargin = ballMargin;
            _ballMass = ballMass;
            _halfnWidth = _boardWidth / 2 + _position.X;
            _baseY = _boardHeight / 5 + _position.Y;

            _genBalls();
        }

        private void _genBalls ()
        {
            float n = Convert.ToInt32(Math.Ceiling((Math.Sqrt(1 + 8 * _numOfBalls) - 1) / 2)); // calculate number of balls in one row
            for (float j = 0; j < n; j++)
            {
                float halfN = (j - n + 1) / 2;
                float maxI = halfN + n - j;
                for (float i = halfN; i < maxI; i++)
                {
                    float x = _halfnWidth + i * _ballRadius;
                    float y = _baseY + j * _ballRadius;
                    Color color = Color.FromName(_balls.Count % 2 == 0 ? "White" : "Green"); 
                    SolidBrush brush = new SolidBrush(color);
                    Vector2 pos = new Vector2(x, y);
                    Vector2 vel = new Vector2(0, -1);
                    Vector2 acc = Vector2.Zero;

                    _balls.Add(new Ball(pos, vel, acc, _ballMass, _ballRadius, brush));

                    if (_balls.Count == _numOfBalls) return;
                }
            }
        }

        public void Update (float dtime)
        { 
            foreach (Ball ball in _balls)
            {
                ball.Update(dtime);
            }
        }

        public void Draw (Graphics g)
        {
            foreach (Ball ball in _balls)
            {
                ball.Draw(g);
            }
            g.DrawRectangle(Pens.Black, _position.X, _position.Y, _boardWidth, _boardHeight);
        }
    }
}