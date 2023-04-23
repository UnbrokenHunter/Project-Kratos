using System.Linq;
using ProjectKratos.Bullet;
using UnityEngine;

namespace ProjectKratos.Player
{
    public sealed class PlayerHitInteractions : MonoBehaviour 
    {
        
        private PlayerVariables _variables;
        private  PlayerInteractions _interactions;

        private void Start()
        {
            _variables = GetComponentInParent<PlayerVariables>();
            _interactions = GetComponentInParent<PlayerInteractions>();
        }
        
        /// <summary>
        /// This method is called on the player that was hit by the bullet shot by someone else.
        /// </summary>
        /// <param name="bullet"></param>
        public void PlayerHit(BulletScript bullet)
        {
            if (bullet.ShooterStats.ShooterGameObject == transform.parent.gameObject) return;

            //print(transform.parent.gameObject.name + " was hit by " + bullet.ShooterStats.ShooterGameObject.name);

            var shooter = bullet.ShooterStats.ShooterGameObject.GetComponentInChildren<PlayerHitInteractions>();
            
            var calculatedDamage = CalculateDamage(bullet.BulletStats.Damage, bullet.ShooterStats.ShooterDamageMultiplier);
            var kill = _interactions.DealDamage(calculatedDamage);

            shooter.AttackResult(transform.parent.gameObject, kill);

        }
        
        private float CalculateDamage(float damage, float multiplier) => damage * multiplier / _variables.Defense;

        private void AttackResult(GameObject shooter, bool wasKill)
        {
            if (shooter == transform.parent.gameObject) return;
            
            if(wasKill) 
                KillSuccessful();
            
            else 
                AttackSuccessful();
        }
        
        /// <summary>
        /// The bullet shot hit a player.
        /// </summary>
        private void AttackSuccessful()
        {
            _variables.MoneyCount = _variables.MoneyPerKill / 10; 
        }
        
        /// <summary>
        /// The bullet shot killed a player.
        /// </summary>
        private void KillSuccessful()
        {
            _variables.MoneyCount = _variables.MoneyPerKill; 
        }
        
    }
}
