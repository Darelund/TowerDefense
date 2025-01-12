using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class SelectedTower : GameObject
    {
        public Tower TowerPrefab { get; private set; }
        public SelectedTower(string type, Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
        {
            TowerPrefab = type switch
            {
                "Cannon" => new CannonTower(tex, pos, scale),
                "MG" => new MGTower(tex, pos, scale),
                "Missile" => new MissileTower(tex, pos, scale),
                _ => throw new NotImplementedException()
            };
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color, _rotation, _origin, _scale, _spriteEffect, _layerDepth);
        }
        public Tower GetPrefab()
        {
            return TowerPrefab;
        }
    }
}
