using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TowerDefense
{
    public class SelectedTower : GameObject
    {
        // public Tower TowerPrefab { get; private set; }
        private string _type;


        public SelectedTower(string type, Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
        {
            _type = type;
            _origin = GetPrefab()._origin;

            DebugRectangle.Init(GameManager.Device, (int)(texture.Width * _scale), (int)(texture.Height * _scale));

        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color, _rotation, _origin, _scale, _spriteEffect, _layerDepth);

            if(GameManager.UseDebug)
            DebugRectangle.DrawRectangle(spriteBatch, new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * _scale), (int)(texture.Height * _scale)), Color.Red);
        }
        public Tower GetPrefab()
        {
            return _type switch
            {
                "Cannon" => new CannonTower(texture, position, _scale),
                "MG" => new MGTower(texture, position, _scale),
                "Missile" => new MissileTower(texture, position, _scale),
                _ => throw new NotImplementedException()
            };
        }
    }
}
