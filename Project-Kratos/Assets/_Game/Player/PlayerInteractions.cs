using ProjectKratos.Bullet;
using QFSW.QC;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

namespace ProjectKratos.Player
{
    public sealed class PlayerInteractions : NetworkBehaviour
    {
        #region Internal

        private PlayerVariables _variables;
        private HealthBar _healthBar;

        private readonly float _regenDivider = 10;

        #endregion

        public override void OnNetworkSpawn()
        {
            _variables = GetComponentInParent<PlayerVariables>();
            _healthBar = transform.GetComponentInChildren<HealthBar>();

            _variables.CurrentHealth = _variables.MaxHealth;

            InvokeRepeating(nameof(AddRegen), 0, 1 / _regenDivider);
        }

        [Command("dmg")]
        public bool DealDamage(float damage)
        {
            if (!IsOwner) return false;

            var kill = false;
            
            _variables.CurrentHealth -= damage;

            if (_variables.CurrentHealth < 0)
            {
                RespawnPlayer();
                kill = true;
            }

            PauseRegen();
            _healthBar.UpdateBar();
            
            return kill;
        }

        [Command("heal")]
        private void AddHealth(float healAmt)
        {
            if (!IsOwner) return;

            _variables.CurrentHealth += healAmt;

            if (_variables.CurrentHealth >= _variables.MaxHealth) 
                _variables.CurrentHealth = _variables.MaxHealth;

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

        private void RespawnPlayer()
        {
            if (!IsOwner) return;
            if (PlayerGamemode.CanRespawn()) return;
            
            
            print("Respawn Player");

            transform.position = GameManager.Instance.PickRandomSpawnPoint().position;
            _variables.SetStats();
            
        }

    }
}