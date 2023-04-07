using UnityEngine;

namespace ProjectKratos.Shop
{
    public class DamageItem : ShopItem
    {
        [SerializeField] private float _damageAmount;

        private protected override void BuyItem()
        {
            _variables.Damage += _damageAmount;
        }
    }
}
