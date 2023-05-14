using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class AbilityCollectable : CollectableItem
    {
        [SerializeField] private PlayerAbility _ability;

        // Return from the beginning to the first space
        public string AbilityName => _ability.name.Split(' ')[0];
        
        public override void ItemCollected(PlayerVariables player, GameObject item)
        {
            player.SetNewAbility(_ability);
        }
    }
}
