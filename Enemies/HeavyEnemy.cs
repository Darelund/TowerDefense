using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class HeavyEnemy : Enemy
    {
        public HeavyEnemy(GraphicsDevice device, Texture2D tex, Vector2 pos, float scale) : base(device, tex, pos, EnemyType.Heavy, scale)
        {
        }
    }
}
