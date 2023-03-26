using ProjectKratos.Bullet;
using Unity.Netcode;

namespace ProjectKratos.Player
{
    public class PlayerInteractions : NetworkBehaviour
    {
        #region Internal

        private PlayerVariables _variables;
        private HealthBar _healthBar;

        #endregion

        public override void OnNetworkSpawn()
        {
            _variables = GetComponentInParent<PlayerVariables>();
            _healthBar = transform.root.GetComponentInChildren<HealthBar>();

            _variables.Stats.CurrentHealth = _variables.Stats.MaxHealth;
        }

        public virtual void PlayerHit(BulletScript bullet)
        {            
            if (!IsOwner) return;
            if (bullet.ShooterStats.ShooterID == transform.parent.GetInstanceID()) return;            

            DealDamage(CalculateDamage(bullet.BulletStats.Damage, bullet.ShooterStats.ShooterDamageMultipler));
            Destroy(bullet);
        }

        private float CalculateDamage(float damage, float multiplier)
        {
            return (damage * multiplier) / _variables.Stats.Defense;
        }

        public virtual void DealDamage(float damage)
        {
            if (!IsOwner) return;

            _variables.Stats.CurrentHealth -= damage;

            if (_variables.Stats.CurrentHealth <= 0)
            {
                _variables.Stats.CurrentHealth = 0;
                KillPlayer();
            }
            print(_variables.Stats.CurrentHealth + " " + damage);

            _healthBar.UpdateBar();
        }

        public virtual void AddHealth(float healAmt)
        {
            if (!IsOwner) return;

            _variables.Stats.CurrentHealth += healAmt;

            if(_variables.Stats.CurrentHealth <= _variables.Stats.MaxHealth)
            {
                _variables.Stats.CurrentHealth = _variables.Stats.MaxHealth;
            }
            
            _healthBar.UpdateBar();
        }

        private void KillPlayer()
        {
            print("Kill Player");
        }


    }
}
