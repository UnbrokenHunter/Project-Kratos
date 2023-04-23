using ProjectKratos.Bullet;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class PowerUpScript : CollectableItem
    {
        [SerializeField] private GameObject _bullet;
        
        protected override void ItemCollected(PlayerVariables player, GameObject item)
        {
            player.SetNewBullet(_bullet);
        }
    }
}
