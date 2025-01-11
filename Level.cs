using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class Level
    {
        private CatmullRomPath _road; //Draw road

        //Deras position?
        private float _curveCurrentPos = 0;
        //Deras speed?
        private float _curveSpeed = 0;

        private GraphicsDevice _device;

        //OM jag lägger till fler banor
        public static int LevelNbr;

        private Texture2D _texture;
        private string _levelMap;
        public string LevelMap => _levelMap;
        public Level(GraphicsDevice device, Texture2D tex, string map)
        {
            _texture = tex;
            _device = device;
            LevelNbr++;
            _levelMap = map;

            LoadMap();
        }
        public void Update(GameTime gameTime)
        {
            //_curveCurrentPos += _curveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (_curveCurrentPos > 1.0f)
            //{
            //    _curveCurrentPos = 0.0f;
            //}
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _road.DrawFill(_device, _texture);
        }
        public void LoadMap()
        {
            float tensionRoad = 0.5f;//Default tension för nu

            _road = new CatmullRomPath(_device, tensionRoad);
            _road.Clear();//Vi vill inte ha default punkter, vi vill göra en egen väg

            //Skapa path
            LoadPath.LoadPathFromFile(_road, _levelMap);

            //Grejer för hur fett road ska vara och vad den ska innehålla
            _road.DrawFillSetup(_device, 30, 5, 26);
        }
    }
}
