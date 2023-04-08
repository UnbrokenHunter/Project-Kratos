using QFSW.QC;
using System;
using System.Text;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerVariables : NetworkBehaviour
    {

        [Header("Movement Variables")]
        [SerializeField] private bool _canMove = true;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _rotationSpeed = 0.15f;

        [Header("Health Variables")]
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _defense = 1f;
        [SerializeField] private float _healthRegen = 1f;

        [Header("Attack Variables")]
        [SerializeField] private bool _canShoot = true;
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _shootingSpeed = 1f;

        [Header("Economy")] 
        [SerializeField] private float _moneyPerKill = 100f;
        [SerializeField] private float _moneyCount;
        
        [Header("External")]
        [Tooltip("The rate at which external velocity decays")]
        [SerializeField] private int _externalVelocityDecay = 100;

        private Stats _stats;
        
        
        #region Things to do when something is changed

        public TMP_Text MoneyText { get => _moneyText; set => _moneyText = value; }
        private TMP_Text _moneyText;

        /// <summary>
        /// So that we can set the money count and have it update the UI
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        private float SetMoney(float moneyToAdd) {
            
            _stats.MoneyCount += moneyToAdd;
                
            if (MoneyText != null)
                MoneyText.text = "Coins: " + _stats.MoneyCount;
            
            return _stats.MoneyCount;
        }

        #endregion

        #region Getters/Setters
        /// <summary>
        /// Do not use add to this, only set it. The add it implied.
        /// </summary>
        public float MoneyCount { get => _stats.MoneyCount; set => SetMoney(value); }
        public float MoneyPerKill { get => _stats.MoneyPerKill; set => _stats.MoneyPerKill = value; }
        public bool CanMove { get => _stats.CanMove; set => _stats.CanMove = value; }
        public float Speed { get => _stats.Speed; set => _stats.Speed = value; }
        public float RotationSpeed { get => _stats.RotationSpeed; set => _stats.RotationSpeed = value; }
        public float CurrentHealth { get => _stats.CurrentHealth; set => _stats.CurrentHealth = value; }
        public float MaxHealth { get => _stats.MaxHealth; set => _stats.MaxHealth = value; }
        public float Defense { get => _stats.Defense; set => _stats.Defense = value; }
        public float HealthRegen { get => _stats.HealthRegen; set => _stats.HealthRegen = value; }
        public bool CanShoot { get => _stats.CanShoot; set => _stats.CanShoot = value; }
        public float Damage { get => _stats.Damage; set => _stats.Damage = value; }
        public float ShootingSpeed { get => _stats.ShootingSpeed; set => _stats.ShootingSpeed = value; }
        public int ExternalVelocityDecay { get => _stats.ExternalVelocityDecay; set => _stats.ExternalVelocityDecay = value; }
        #endregion
        
        private struct Stats 
        {
            [Header("Movement Variables")]
            public bool CanMove;
            public float Speed;
            public float RotationSpeed;

            [Header("Health Variables")]
            public float CurrentHealth;
            public float MaxHealth;
            public float Defense;
            public float HealthRegen;

            [Header("Attack Variables")]
            public bool CanShoot;
            public float Damage;
            public float ShootingSpeed;

            [Header("Economy")] 
            public float MoneyPerKill;
            public float MoneyCount;
            
            [Header("External")]
            public int ExternalVelocityDecay;
        }
       
        public override void OnNetworkSpawn() => SetStats();

        /// <summary>
        /// Resets all the stats to their default values
        /// </summary>
        public void SetStats()
        {
            print("Reset Stats");
            
            _stats = new Stats
            {
                CanMove = _canMove,
                Speed = _speed,
                RotationSpeed = _rotationSpeed,
                CurrentHealth = _currentHealth,
                MaxHealth = _maxHealth,
                Defense = _defense,
                HealthRegen = _healthRegen,
                CanShoot = _canShoot,
                Damage = _damage,
                ShootingSpeed = _shootingSpeed,
                MoneyPerKill = _moneyPerKill,
                MoneyCount = _moneyCount,
                ExternalVelocityDecay = _externalVelocityDecay
            };
            
            MoneyCount = 0;
        }
        
        [Command("stats")]
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Player Variables:\n------------------\n");
            sb.Append($"Movement Variables: [CanMove={_canMove}, Speed={_speed}, RotationSpeed={_rotationSpeed}]\n");
            sb.Append($"Health Variables: [CurrentHealth={_currentHealth}, MaxHealth={_maxHealth}, Defense={_defense}, HealthRegen={_healthRegen}]\n");
            sb.Append($"Attack Variables: [CanShoot={_canShoot}, Damage={_damage}, ShootingSpeed={_shootingSpeed}]\n");
            sb.Append($"Economy: [MoneyCount={_moneyCount}, MoneyPerKill ={_moneyPerKill}]\n");
            sb.Append($"External: [ExternalVelocityDecay={_externalVelocityDecay}]\n");
            sb.Append("------------------\n");
            return sb.ToString();
        }

        public static implicit operator GameObject(PlayerVariables v)
        {
            throw new NotImplementedException();
        }
    }

}
