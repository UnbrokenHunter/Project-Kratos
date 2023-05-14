using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class AbilityCooldownItem : ShopItem
    {
        [SerializeField] private float _decreaseAmount = 0.1f;

        public override void BuyItem()
        {
            _variables.AbilityCooldownMultiplier -= _decreaseAmount;
        }
    }
}
