using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class PointScript : CollectableItem 
    {
        [SerializeField] private float _moneyToGive = 100; 
        
        protected override void ItemCollected(GameObject player, GameObject item)
        {
            if(item != gameObject) return;
            
            print("Money collected");
            player.GetComponent<PlayerVariables>().MoneyCount = _moneyToGive;
        }

    }
}
