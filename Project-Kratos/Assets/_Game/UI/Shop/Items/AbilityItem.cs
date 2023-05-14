using System;
using ProjectKratos.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectKratos.Shop
{
    public class AbilityItem : ShopItem
    {
        [SerializeField] private PlayerAbility _ability;
        [SerializeField] private GameObject _shopMenuRef;

        private void Awake()
        {
            Description = _ability.Description;
        }

        public override void BuyItem()
        {
            _variables.SetNewAbility(_ability);
            
            _shopMenuRef.SetActive(false);
        }
    }
}
