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
    public class GameObjectPlacer
    {
        private static Texture2D _transparent;
        private static RenderTarget2D _renderTarget; //Map to render
      //  private GameObject _selectedGameObject;//Selected gameobject if you can afford
        private static List<GameObject> _gameObjects;

        private static SpriteBatch _spriteBatch;
        private static GraphicsDevice _graphicsDevice;
        public static event Action OnObjectPlaced;

        public static void SetUp(GraphicsDevice graphicsDevice, GameWindow window, SpriteBatch spriteBatch)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;
            _transparent = ResourceManager.GetTexture("transparentSquareBackground");
            _renderTarget = new RenderTarget2D(_graphicsDevice, window.ClientBounds.Width, window.ClientBounds.Height);
            _gameObjects = new List<GameObject>();
        }

        public static void Update(GameTime gameTime, SelectedTower selectedGameObject)
        {

            selectedGameObject.Position = new Vector2((-selectedGameObject.GetTexture.Width * selectedGameObject._scale) / 2, (-selectedGameObject.GetTexture.Height * selectedGameObject._scale) / 2) + Mouse.GetState().Position.ToVector2();
           // selectedGameObject.Update(gameTime); //Need to Update the position so it follows the mouse

            if (InputManager.CurrentMouse.LeftButton == ButtonState.Released && CanPlace(selectedGameObject))
            {
                PlaceDownObject(selectedGameObject);
            }
        }
        private static void PlaceDownObject(SelectedTower selectedGameObject)
        {
            var tower = selectedGameObject.GetPrefab();
            Texture2D tex = tower.GetTexture;
            Vector2 vec = tower.Position = new Vector2((-selectedGameObject.GetTexture.Width * selectedGameObject._scale) / 2, (-selectedGameObject.GetTexture.Height * selectedGameObject._scale) / 2) + Mouse.GetState().Position.ToVector2();
            float scale = tower._scale;
            _gameObjects.Add(new Tower(tex, vec, scale));
            OnObjectPlaced?.Invoke();
            Debug.WriteLine(_gameObjects.Count);
            DrawOnRenderTarget();
        }

        public static void Draw(GameObject selectedGameObject)
        {
            _spriteBatch.Draw(_renderTarget, new Vector2(0, 0), Color.White);

            selectedGameObject.Draw(_spriteBatch);
        }
        public static void Draw()
        {
            _spriteBatch.Draw(_renderTarget, new Vector2(0, 0), Color.White);
        }
        public static void DrawOnRenderTarget()
        {
            //Ändra så att GraphicsDevice ritar mot vårt render target
            _graphicsDevice.SetRenderTarget(_renderTarget);
            _graphicsDevice.Clear(Color.Transparent);
            _spriteBatch.Begin();
            Debug.WriteLine("Changed render");
            //Rita ut texturen. Den ritas nu ut till vårt render target istället
            //för på skärmen.
            if (_gameObjects != null && _gameObjects.Count > 0)
            {
                foreach (var gameObject in _gameObjects)
                {
                    gameObject.Draw(_spriteBatch);
                }
            }

          //  _spriteBatch.Draw(_transparent, Vector2.Zero, Color.White);
            _spriteBatch.End();

            //Sätt GraphicsDevice att åter igen peka på skärmen
            _graphicsDevice.SetRenderTarget(null);
        }

        //private static bool CanPlace(GameObject g)
        //{
        //    Color[] pixels = new Color[g.GetTexture.Width * g.GetTexture.Height];
        //    Color[] pixels2 = new Color[g.GetTexture.Width * g.GetTexture.Height];
        //    g.GetTexture.GetData<Color>(pixels2);
        //    _renderTarget.GetData(0, g.Hitbox, pixels, 0, pixels.Length);
        //    for (int i = 0; i < pixels.Length; ++i)
        //    {
        //        if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        private static bool CanPlace(GameObject g)
        {
            // Get the hitbox of the GameObject
            Rectangle hitbox = g.Hitbox;

            // Validate and clamp hitbox to the render target bounds
            hitbox.Width = Math.Min(hitbox.Width, _renderTarget.Width - hitbox.X);
            hitbox.Height = Math.Min(hitbox.Height, _renderTarget.Height - hitbox.Y);

            if (hitbox.X < 0 || hitbox.Y < 0 || hitbox.Width <= 0 || hitbox.Height <= 0)
            {
                return false; // Invalid placement
            }

            // Create pixel arrays based on the hitbox size
            Color[] pixels = new Color[hitbox.Width * hitbox.Height];
            Color[] pixels2 = new Color[hitbox.Width * hitbox.Height];

            // Resize texture data to match the hitbox
            Texture2D scaledTexture = ScaleTexture(g.GetTexture, hitbox.Width, hitbox.Height);

            // Retrieve scaled texture and render target data
            scaledTexture.GetData(pixels2);
            _renderTarget.GetData(0, hitbox, pixels, 0, pixels.Length);

            // Check for collisions
            for (int i = 0; i < pixels.Length; ++i)
            {
                if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f)
                {
                    return false; // Overlap detected
                }
            }
            return true; // No overlap, placement valid
        }
        private static Texture2D ScaleTexture(Texture2D texture, int width, int height)
        {
            RenderTarget2D renderTarget = new RenderTarget2D(_graphicsDevice, width, height);

            // Set the render target
            _graphicsDevice.SetRenderTarget(renderTarget);
            _graphicsDevice.Clear(Color.Transparent);

            // Draw the scaled texture
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, new Rectangle(0, 0, width, height), Color.White);
            _spriteBatch.End();

            // Reset render target
            _graphicsDevice.SetRenderTarget(null);

            // Return the scaled texture
            return renderTarget;
        }
    }
}
