using ProjectKratos.Bullet;
using QFSW.QC;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerInteractions : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            _variables = GetComponentInParent<PlayerVariables>();
            _healthBar = transform.root.GetComponentInChildren<HealthBar>();

            _variables.CurrentHealth = _variables.MaxHealth;

            InvokeRepeating(nameof(AddRegen), 0, 1 / _regenDivider);
        }

        public virtual void PlayerHit(BulletScript bullet)
        {
            if (!IsOwner) return;
            if (bullet.ShooterStats.ShooterGameObject == transform.parent.gameObject) return;
            
            
            bool kill = DealDamage(CalculateDamage(bullet.BulletStats.Damage, bullet.ShooterStats.ShooterDamageMultipler));
             
            MessageAttackResultServerRpc(bullet.ShooterStats.ShooterGameObject.GetComponent<NetworkBehaviour>(), kill); 
            
            Destroy(bullet);
        }

        [ServerRpc]
        private void MessageAttackResultServerRpc(NetworkBehaviourReference shooterReference, bool wasKill)
        {
            MessageAttackResultClientRpc(shooterReference, wasKill);
        }
        
        [ClientRpc]
        private void MessageAttackResultClientRpc(NetworkBehaviourReference shooterReference, bool wasKill)
        {
            shooterReference.TryGet(out PlayerVariables shooter);
            
            if(wasKill) 
                shooter.GetComponentInChildren<PlayerInteractions>().KillSuccessful();
            
            else 
                shooter.GetComponentInChildren<PlayerInteractions>().AttackSuccessful();
        }

        private float CalculateDamage(float damage, float multiplier)
        {
            return damage * multiplier / _variables.Defense;
        }

        [Command("dmg")]
        protected virtual bool DealDamage(float damage)
        {
            if (!IsOwner) return false;

            _variables.CurrentHealth -= damage;

            if (_variables.CurrentHealth <= 0)
            {
                _variables.CurrentHealth = 0;
                KillPlayer();
                return true;
            }

            PauseRegen();
            _healthBar.UpdateBar();
            return false;
        }

        [Command("heal")]
        protected virtual void AddHealth(float healAmt)
        {
            if (!IsOwner) return;

            _variables.CurrentHealth += healAmt;

            if (_variables.CurrentHealth >= _variables.MaxHealth) _variables.CurrentHealth = _variables.MaxHealth;

            _healthBar.UpdateBar();
        }

        private void PauseRegen()
        {
            CancelInvoke(nameof(AddRegen));

            // Name     Time to cancel    Frequency
            InvokeRepeating(nameof(AddRegen), 3, 1 / _regenDivider);
        }

        private void AddRegen()
        {
            if (!IsOwner) return;

            AddHealth(_variables.HealthRegen / _regenDivider);
        }

        /// <summary>
        /// The bullet shot hit a player.
        /// </summary>
        public void AttackSuccessful()
        {
            //if (!IsOwner) return;
            
            
                
            
            print(gameObject.name + "Attack Successful");
        }
        
        /// <summary>
        /// The bullet shot killed a player.
        /// </summary>
        public void KillSuccessful()
        {
            if (!IsOwner) return;

            _variables.MoneyCount += _variables.MoneyPerKill;
            
            print("Kill Successful"); 
        }

        private void KillPlayer()
        {
            print("Kill Player");
        }

        #region Internal

        private PlayerVariables _variables;
        private HealthBar _healthBar;

        private readonly float _regenDivider = 10;

        #endregion
    }
}