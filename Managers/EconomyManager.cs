using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class EconomyManager
    {
        public static int MoneyAmount { get; private set; } = 50;
        public static event Action OnMoneyAmountChanged;
        public static void UpdateScore(int points)
        {
            MoneyAmount += points;
            OnMoneyAmountChanged?.Invoke();
        }
        public static void ResetScore()
        {
            MoneyAmount = 0;
            OnMoneyAmountChanged?.Invoke();
        }
    }
}
