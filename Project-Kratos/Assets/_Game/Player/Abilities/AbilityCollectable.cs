using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class AbilityCollectable : CollectableItem
    {
        [SerializeField] private PlayerAbility _ability;
        
        protected override void ItemCollected(PlayerVariables player, GameObject item)
        {
            
            player.SetNewAbility(_ability);
            
        }
    }
}
