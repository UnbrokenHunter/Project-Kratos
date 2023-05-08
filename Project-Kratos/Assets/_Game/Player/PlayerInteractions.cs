using DarkTonic.MasterAudio;
using ProjectKratos.Bullet;
using QFSW.QC;
using UnityEditor;
using UnityEngine;

namespace ProjectKratos.Player
{
    public sealed class PlayerInteractions : MonoBehaviour
    {
        #region Internal

        private PlayerVariables _variables;
        private HealthBar _healthBar;

        private readonly float _regenDivider = 10;
        private bool _canRespawn;

        #endregion

        public void Start()
        {
            _variables = GetComponentInParent<PlayerVariables>();
            _healthBar = transform.GetComponentInChildren<HealthBar>();

            _variables.CurrentHealth = _variables.MaxHealth;

            InvokeRepeating(nameof(AddRegen), 0, 1 / _regenDivider);
        }

        [Command("dmg")]
        public bool DealDamage(float damage)
        {
            var kill = false;
            
            _variables.CurrentHealth -= damage;
            
            if (_variables.CurrentHealth < 0)
            {
                RespawnPlayer();
                kill = true;
            }

            PauseRegen();
            _healthBar.UpdateBar();

            if (!_variables.IsBot)
                MasterAudio.PlaySound("Damaged");
            
            return kill;
        }

        [Command("heal")]
        private void AddHealth(float healAmt)
        {
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
            AddHealth(_variables.HealthRegen / _regenDivider);
        }

        private void RespawnPlayer()
        {
            _canRespawn = PlayerGamemode.CanRespawn();

            if (!_canRespawn)
            {
                // Player died, end game
                if (!_variables.IsBot)
                    _variables.EndGame(false);
                
                else
                    GameManager.Instance.DespawnPlayer(_variables);
                
                return;
            } 
            
            
            print("Respawn Player");

            transform.position = GameManager.Instance.PickRandomSpawnPoint().position;
            _variables.SetStats();
            
        }

    }
}