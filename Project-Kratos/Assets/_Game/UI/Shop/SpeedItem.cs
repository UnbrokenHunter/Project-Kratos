using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class SpeedItem : ShopItem
    {
        [SerializeField] private float _speedAmount;

        private protected override void BuyItem()
        {
            _variables.Speed += _speedAmount;
        }
    }
}
