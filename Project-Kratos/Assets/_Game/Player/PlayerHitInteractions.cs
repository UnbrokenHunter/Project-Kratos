using ProjectKratos.Bullet;
using Unity.Netcode;

namespace ProjectKratos.Player
{
    public sealed class PlayerHitInteractions : NetworkBehaviour
    {
        
        private PlayerVariables _variables;
        private  PlayerInteractions _interactions;
        
        public override void OnNetworkSpawn()
        {
            _variables = GetComponentInParent<PlayerVariables>();
            _interactions = GetComponentInParent<PlayerInteractions>();
        }
        
        public void PlayerHit(BulletScript bullet)
        {
            if (!IsOwner) return;
            if (bullet.ShooterStats.ShooterGameObject == transform.parent.gameObject) return;
            
            bool kill = _interactions.DealDamage(CalculateDamage(bullet.BulletStats.Damage, bullet.ShooterStats.ShooterDamageMultiplier));
            MessageAttackResultServerRpc(bullet.ShooterStats.ShooterGameObject.GetComponent<NetworkBehaviour>(), kill); 
            
            Destroy(bullet);
        }
        
        private float CalculateDamage(float damage, float multiplier) => damage * multiplier / _variables.Defense;

        [ServerRpc]
        private void MessageAttackResultServerRpc(NetworkBehaviourReference shooterReference, bool wasKill) => MessageAttackResultClientRpc(shooterReference, wasKill);
        
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
            //if (!IsOwner) return;
            
           
            
            
            print(gameObject.name + "Attack Successful");
        }
        
        /// <summary>
        /// The bullet shot killed a player.
        /// </summary>
        private void KillSuccessful()
        {
            if (!IsOwner) return;

            _variables.MoneyCount += _variables.MoneyPerKill;
            
            print("Kill Successful"); 
        }
        
    }
}
