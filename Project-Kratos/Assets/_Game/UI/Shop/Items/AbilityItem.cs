using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class AbilityItem : ShopItem
    {
        [SerializeField] private PlayerAbility _ability;

        public override void BuyItem()
        {
            _variables.SetNewAbility(_ability);
        }
    }
}
