using System;
using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class PointScript : CollectableItem 
    {
        [SerializeField] private float _moneyToGive = 100; 
        
        protected override void ItemCollected(GameObject player)
        {
            player.GetComponent<PlayerVariables>().MoneyCount += _moneyToGive;
            Destroy(gameObject);
        }

    }
}
