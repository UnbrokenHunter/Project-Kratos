using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class PointScript : CollectableItem 
    {
        [SerializeField] private float _moneyToGive = 100;

        public override void ItemCollected(PlayerVariables player, GameObject item)
        {
            if(item != gameObject) return;
            
            player.MoneyCount = _moneyToGive;
        }

    }
}
