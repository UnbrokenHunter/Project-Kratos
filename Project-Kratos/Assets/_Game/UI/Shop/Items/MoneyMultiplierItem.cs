using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Shop;
using UnityEngine;

namespace ProjectKratos
{
    public class MoneyMultiplierItem : ShopItem
    {
        [SerializeField] private float _moneyMultiplierAmount;
        
        public override void BuyItem()
        {
            _variables.MoneyPerKill += _moneyMultiplierAmount;
        }
    }
}
