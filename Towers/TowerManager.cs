using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public static class TowerManager
    {
        // Store placed towers
        private static List<Tower> _towers = new List<Tower>();

        // Tower settings by level
        private static readonly Dictionary<int, TowerSettings> _cannonSettings = new Dictionary<int, TowerSettings>
        {
            { 1, new TowerSettings(1f, 0.5f, "Cannon") },
            { 2, new TowerSettings(2f, 0.5f, "Cannon2") },
            { 3, new TowerSettings(3f, 0.5f, "Cannon3") },
            { 4, new TowerSettings(2.5f, 0.3f, "MG") },
            { 5, new TowerSettings(3.5f, 0.2f, "MG2") },
            { 6, new TowerSettings(4.5f, 0.1f, "MG3") },
            { 7, new TowerSettings(0.7f, 2f, "Missile_Launcher") },
            { 8, new TowerSettings(0.9f, 2f, "Missile_Launcher2") },
            { 9, new TowerSettings(1f, 1f, "Missile_Launcher3") }
        };

       


        // Register a new tower
        public static void RegisterTower(Tower tower)
        {
            _towers.Add(tower);

            // Apply current settings for the tower's type and level
            ApplySettings(tower);
        }

        // Upgrade all towers of a specific type
        public static void UpgradeTowers()
        {
            foreach (var g in GameObjectPlacer._gameObjects)
            {
                var tower = g as Tower;
                    ApplySettings(tower);
            }
        }

        // Apply settings to a specific tower
        public static void ApplySettings(Tower tower)
        {
            if (tower is CannonTower cannonTower)
            {
                var settings = _cannonSettings[CannonTower.Level];
                cannonTower.BulletSpeed = settings.BulletSpeed;
                cannonTower.fireDelay = settings.FireDelay;
                cannonTower.SetTexture = ResourceManager.GetTexture(settings.TextureName);
            }
            if (tower is MGTower mgTower)
            {
                var settings = _cannonSettings[MGTower.Level + 3];
                mgTower.BulletSpeed = settings.BulletSpeed;
                mgTower.fireDelay = settings.FireDelay;
                mgTower.SetTexture = ResourceManager.GetTexture(settings.TextureName);
            }
            if (tower is MissileTower missileTower)
            {
                var settings = _cannonSettings[MissileTower.Level + 6];
                missileTower.BulletSpeed = settings.BulletSpeed;
                missileTower.fireDelay = settings.FireDelay;
                missileTower.SetTexture = ResourceManager.GetTexture(settings.TextureName);
            }
        }
        public static void UpgradeCannonCost()
        {
            var nextLevel = CannonTower.Level + 1;
            if(nextLevel == 2)
            {
                var price = 10;
                if(EconomyManager.MoneyAmount >=  price)
                {
                    EconomyManager.UpdateScore(-price);
                    CannonTower.Level++;
                    UpgradeTowers();
                }
            }
            if (nextLevel == 3)
            {
                var price = 15;
                if (EconomyManager.MoneyAmount >= price)
                {
                    EconomyManager.UpdateScore(-price);
                    CannonTower.Level++;
                    UpgradeTowers();
                }
            }
        }
        public static void UpgradeMGCost()
        {
            var nextLevel = MGTower.Level + 1;
            if (nextLevel == 2)
            {
                var price = 15;
                if (EconomyManager.MoneyAmount >= price)
                {
                    EconomyManager.UpdateScore(-price);
                    MGTower.Level++;
                    UpgradeTowers();
                }
            }
            if (nextLevel == 3)
            {
                var price = 25;
                if (EconomyManager.MoneyAmount >= price)
                {
                    EconomyManager.UpdateScore(-price);
                    MGTower.Level++;
                    UpgradeTowers();
                }
            }
        }
        public static void UpgradeMissileCost()
        {
            var nextLevel = MissileTower.Level + 1;
            if (nextLevel == 2)
            {
                var price = 30;
                if (EconomyManager.MoneyAmount >= price)
                {
                    EconomyManager.UpdateScore(-price);
                    MissileTower.Level++;
                    UpgradeTowers();
                }
            }
            if (nextLevel == 3)
            {
                var price = 50;
                if (EconomyManager.MoneyAmount >= price)
                {
                    EconomyManager.UpdateScore(-price);
                    MissileTower.Level++;
                    UpgradeTowers();
                }
            }
        }
    }

    // Helper class for tower settings
    public class TowerSettings
    {
        public float BulletSpeed { get; }
        public float FireDelay { get; }
        public string TextureName { get; }

        public TowerSettings(float bulletSpeed, float fireDelay, string textureName)
        {
            BulletSpeed = bulletSpeed;
            FireDelay = fireDelay;
            TextureName = textureName;
        }
    }
}
