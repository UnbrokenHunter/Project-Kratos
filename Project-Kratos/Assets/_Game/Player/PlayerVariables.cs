using QFSW.QC;
using Sirenix.OdinInspector;
using System;
using System.Text;
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
        [SerializeField] private float moneyCount;
        
        [Header("External")]
        [Tooltip("The rate at which external velocity decays")]
        [SerializeField] private int _externalVelocityDecay = 100;

        #region Getters/Setters
        public float MoneyCount { get => moneyCount; set => moneyCount = value; }
        public bool CanMove { get => _canMove; set => _canMove = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }
        public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
        public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        public float Defense { get => _defense; set => _defense = value; }
        public float HealthRegen { get => _healthRegen; set => _healthRegen = value; }
        public bool CanShoot { get => _canShoot; set => _canShoot = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public float ShootingSpeed { get => _shootingSpeed; set => _shootingSpeed = value; }
        public int ExternalVelocityDecay { get => _externalVelocityDecay; set => _externalVelocityDecay = value; }
        #endregion 

        [Command("stats")]
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Player Variables:\n------------------\n");
            sb.Append($"Movement Variables: [CanMove={_canMove}, Speed={_speed}, RotationSpeed={_rotationSpeed}]\n");
            sb.Append($"Health Variables: [CurrentHealth={_currentHealth}, MaxHealth={_maxHealth}, Defense={_defense}, HealthRegen={_healthRegen}]\n");
            sb.Append($"Attack Variables: [CanShoot={_canShoot}, Damage={_damage}, ShootingSpeed={_shootingSpeed}]\n");
            sb.Append($"Economy: [MoneyCount={moneyCount}]\n");
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
