using ProjectKratos.Bullet;
using QFSW.QC;
using System.Collections;
using System.Diagnostics;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerInteractions : NetworkBehaviour
    {
        #region Internal

        private PlayerVariables _variables;
        private HealthBar _healthBar;

        private float _regenDivider = 10;

        #endregion

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
            if (bullet.ShooterStats.ShooterID == transform.parent.GetInstanceID()) return;            

            DealDamage(CalculateDamage(bullet.BulletStats.Damage, bullet.ShooterStats.ShooterDamageMultipler));
            Destroy(bullet);
        }

        private float CalculateDamage(float damage, float multiplier)
        {
            return (damage * multiplier) / _variables.Defense;
        }

        [Command("dmg")]
        public virtual void DealDamage(float damage)
        {
            if (!IsOwner) return;

            _variables.CurrentHealth -= damage;

            if (_variables.CurrentHealth <= 0)
            {
                _variables.CurrentHealth = 0;
                KillPlayer();
            }

            PauseRegen();
            _healthBar.UpdateBar();

            print($"Current Health: {_variables.CurrentHealth} \nDamage Taken: {damage} \nPlayer Name: {name}");
        }

        [Command("heal")]
        public virtual void AddHealth(float healAmt)
        {
            if (!IsOwner) return;

            _variables.CurrentHealth += healAmt;

            if(_variables.CurrentHealth >= _variables.MaxHealth)
            {
                _variables.CurrentHealth = _variables.MaxHealth;
            }
            
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

        private void KillPlayer()
        {
            print("Kill Player");
        }


    }
}
