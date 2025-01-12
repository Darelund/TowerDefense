using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class GameObjectSelector
    {
        public Texture2D Icon { get; private set; }
        public SelectedTower Prefab { get; private set; }
        public Rectangle Bounds { get; private set; }
        private float _scale;

        public GameObjectSelector(Texture2D icon, SelectedTower prefab, Vector2 position, float scale)
        {
            Icon = icon;
            Prefab = prefab;
            _scale = scale;
            Bounds = new Rectangle((int)position.X, (int)position.Y, (int)(icon.Width * _scale), (int)(icon.Height * _scale));
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
        public SelectedTower SelectedObject()
        {
            GameObjectPlacer.OnObjectPlaced += Deselected;
            return Prefab;
        }
        public void Deselected()
        {
            GameManager._selectedObject = null;
            Debug.WriteLine("Trying to place object - GameObjectSelector");
            GameObjectPlacer.OnObjectPlaced -= Deselected;
        }
    }
}
