using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class DefenseItem : ShopItem
    {
        
        [SerializeField] private float _defenseAmount;

        public override void BuyItem()
        {
            _variables.Defense += _defenseAmount;
        }
    }
}