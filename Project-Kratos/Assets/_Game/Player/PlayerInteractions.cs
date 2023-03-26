using ProjectKratos.Bullet;
using Unity.Netcode;

namespace ProjectKratos.Player
{
    public class PlayerInteractions : NetworkBehaviour
    {
        #region Internal

        public float CurrentHealth { get => _currentHealth; }
        private float _currentHealth;

        private PlayerVariables _variables;
        private HealthBarManager _healthBarManager;

        #endregion

        public override void OnNetworkSpawn()
        {
            _variables = GetComponentInParent<PlayerVariables>();
            _healthBarManager = transform.root.GetComponentInChildren<HealthBarManager>();

            _currentHealth = _variables.Stats.MaxHealth;
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

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                KillPlayer();
            }
            print(_currentHealth + " " + damage);
            _healthBarManager.SetBar();
        }

        public virtual void AddHealth(float healAmt)
        {
            if (!IsOwner) return;

            _currentHealth += healAmt;

            if(_currentHealth <= _variables.Stats.MaxHealth)
            {
                _currentHealth = _variables.Stats.MaxHealth;
            }

            _healthBarManager.SetBar();
        }

        private void KillPlayer()
        {
            print("Kill Player");
        }


    }
}
