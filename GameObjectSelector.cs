using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class GameObjectSelector
    {
        public Texture2D Icon { get; private set; }
        public GameObject Prefab { get; private set; }
        public Rectangle Bounds { get; private set; }


        public GameObjectSelector(Texture2D icon, GameObject prefab, Vector2 position)
        {
            Icon = icon;
            Prefab = prefab;
            Bounds = new Rectangle((int)position.X, (int)position.Y, icon.Width, icon.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Icon, Bounds, Color.White);
        }
        public bool IsMouseOver()
        {
            var mouse = InputManager.CurrentMouse;
            return Bounds.Contains(mouse.Position);
        }
    }
}
