using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class PlayerHUD
    {
       // private UIText _text;
        private UIImage _dollarImage;
        public UIText _moneyText;
        private UIText _playerHealth;

        public PlayerHUD()
        {
           // _text = new UIText(ResourceManager.GetSpriteFont("GameText"), "Money: ", new Vector2(50, 20), Color.White, 0.8f, Vector2.Zero, 0.9f);
            _dollarImage = new UIImage(ResourceManager.GetTexture("DollarSign"), new Vector2(260, 10), Color.Gold, 0.08f, Vector2.Zero, new Rectangle(0, 0, 512, 512), 0.9f);
            //  hearts = (int)PlayerController.Instance.Health;
            EconomyManager.OnMoneyAmountChanged += OnMoneyChanged;

            _moneyText = new UIText(ResourceManager.GetSpriteFont("GameText"), $"Money: {EconomyManager.MoneyAmount}$", new Vector2(350, 20), Color.Gold, 0.6f, Vector2.Zero, 0.9f);
            _playerHealth = new UIText(ResourceManager.GetSpriteFont("GameText"), $"Health: {PlayerHealth.HealthAmount}", new Vector2(600, 20), Color.DarkRed, 0.6f, Vector2.Zero, 0.9f);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _dollarImage.Draw(spriteBatch);
            _moneyText.Draw(spriteBatch);
            _playerHealth.Draw(spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
        }
        private void OnMoneyChanged()
        {
            _moneyText.UpdateText($"Money: {EconomyManager.MoneyAmount}");
        }
    }
}
