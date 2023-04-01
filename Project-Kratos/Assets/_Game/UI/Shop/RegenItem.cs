using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class RegenItem : ShopItem
    {
        [SerializeField] private float _regenAmount;

        private protected override void BuyItem()
        {
            _variables.HealthRegen += _regenAmount;
        }
    }
}
