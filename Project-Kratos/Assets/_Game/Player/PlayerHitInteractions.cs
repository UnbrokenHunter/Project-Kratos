using ProjectKratos.Bullet;
using Unity.Netcode;

namespace ProjectKratos.Player
{
    public sealed class PlayerHitInteractions : NetworkBehaviour
    {
        
        private PlayerVariables _variables;
        private  PlayerInteractions _interactions;
        private float calculatedDamage;
        private NetworkBehaviour _shooterReference;

        public override void OnNetworkSpawn()
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
            if (!IsOwner) return;
            if (bullet.ShooterStats.ShooterGameObject == transform.parent.gameObject) return;

            calculatedDamage = CalculateDamage(bullet.BulletStats.Damage, bullet.ShooterStats.ShooterDamageMultiplier);
            var kill = _interactions.DealDamage(calculatedDamage);

            _shooterReference = bullet.ShooterStats.ShooterGameObject.GetComponent<NetworkBehaviour>();
            MessageAttackResultServerRpc(_shooterReference, kill); 
            
            Destroy(bullet);
        }
        
        private float CalculateDamage(float damage, float multiplier) => damage * multiplier / _variables.Defense;

        /// <summary>
        /// Call on server
        /// </summary>
        /// <param name="shooterReference"></param>
        /// <param name="wasKill"></param>
        [ServerRpc]
        private void MessageAttackResultServerRpc(NetworkBehaviourReference shooterReference, bool wasKill) => MessageAttackResultClientRpc(shooterReference, wasKill);
        
        /// <summary>
        ///  Call on all clients
        /// </summary>
        /// <param name="shooterReference"></param>
        /// <param name="wasKill"></param>
        [ClientRpc]
        private void MessageAttackResultClientRpc(NetworkBehaviourReference shooterReference, bool wasKill)
        {
            if (IsOwner) return;
            
            shooterReference.TryGet(out PlayerVariables shooter);
            
            if(wasKill) 
                shooter.GetComponentInChildren<PlayerHitInteractions>().KillSuccessful();
            
            else 
                shooter.GetComponentInChildren<PlayerHitInteractions>().AttackSuccessful();
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
