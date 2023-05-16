using UnityEngine;

namespace ProjectKratos.Shop
{
    public class FullHealthAbility : ShopItem
    {
        
        [SerializeField] private GameObject _shopMenuRef;

        public override void BuyItem()
        {
            _variables.PlayerInteractions.AddHealth(_variables.MaxHealth);
            
            _shopMenuRef.SetActive(false);

        }
    }
}