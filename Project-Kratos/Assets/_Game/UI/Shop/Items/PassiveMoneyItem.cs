using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Shop;
using UnityEngine;

namespace ProjectKratos
{
    public class PassiveMoneyItem : ShopItem
    {

        [SerializeField] private float _multiplierAmount = 1.5f;
        
        public override void BuyItem()
        {
            _variables.PassiveMoneyPerSecond *= _multiplierAmount;
        }
    }
}
