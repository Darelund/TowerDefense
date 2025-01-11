using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class Enemy : GameObject
    {
        private CatmullRomPath _car; //Draw car

        private float _currentCurvePos = 0;
        private float _currentCurveSpeed = 0.2f;

        //live
        //private float _health = 3;
        public bool IsHit = false; //Gör den till isDead sen

        private GraphicsDevice _device;

        public Enemy(GraphicsDevice device, Texture2D tex, Vector2 pos): base(tex, pos)
        {
            _device = device;

            float tensionRoad = 0.5f;

            _car = new CatmullRomPath(_device, tensionRoad);
            _car.Clear();//Vi vill inte ha default punkter, vi vill göra en egen väg

            LoadPath.LoadPathFromFile(_car, "carpath1.txt");

            //Grejer för hur fett bil ska vara och vad den ska innehålla
            _car.DrawFillSetup(_device, 2, 1, 256);


        }
        public override void Update(GameTime gameTime)
        {
            _currentCurvePos += _currentCurveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_currentCurvePos < 1 && _currentCurvePos > 0)
            {
                //Som den där GetPos eller vad den hette
               Vector2 currentPos = _car.EvaluateAt(_currentCurvePos);
                position = currentPos;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_currentCurvePos < 1 && _currentCurvePos > 0)
            {
                if (!IsHit)
                _car.DrawMovingObject(_currentCurvePos, spriteBatch, texture);
            }
        }
    }
}
