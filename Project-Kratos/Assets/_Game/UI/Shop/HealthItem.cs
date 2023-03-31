using ProjectKratos.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class HealthItem : ShopItem
    {
        [SerializeField] private float _healthAmount;

        private protected override void BuyItem()
        {
            _variables.gameObject.GetComponentInChildren<PlayerInteractions>().AddHealth(_healthAmount);
        }
    }
}
