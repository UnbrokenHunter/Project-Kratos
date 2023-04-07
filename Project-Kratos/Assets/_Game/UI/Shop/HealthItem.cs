using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class HealthItem : ShopItem
    {
        [SerializeField] private float _healthAmount;

        private protected override void BuyItem()
        {
            _variables.MaxHealth += _healthAmount;
            _variables.CurrentHealth += _healthAmount;
        }
    }
}
