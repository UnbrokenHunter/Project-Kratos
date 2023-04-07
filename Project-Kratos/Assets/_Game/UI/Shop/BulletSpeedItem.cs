using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class BulletSpeedItem : ShopItem
    {
        [SerializeField] private float _bulletSpeedAmount;

        private protected override void BuyItem()
        {
            _variables.ShootingSpeed += _bulletSpeedAmount;
        }
    }
}
