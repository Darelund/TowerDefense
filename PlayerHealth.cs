using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class PlayerHealth
    {
        public static int HealthAmount { get; private set; } = 100;
        public static event Action OnHealthAmountChanged;
        public static void UpdateScore(int points)
        {
            HealthAmount += points;
            // Debug.WriteLine(points);
            OnHealthAmountChanged?.Invoke();
        }
        public static void ResetScore()
        {
            HealthAmount = 0;
            OnHealthAmountChanged?.Invoke();
        }
    }
}
