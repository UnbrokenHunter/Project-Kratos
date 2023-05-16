using QFSW.QC;
using System;
using System.Text;
using Cinemachine;
using DarkTonic.MasterAudio;
using ProjectKratos.Shop;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectKratos.Player
{
    public class PlayerVariables : MonoBehaviour 
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
        [SerializeField] private bool _hasShop = false;
        [SerializeField] private float _moneyPerKill = 100f;
        [SerializeField] private float _passiveMoneyPerSecond = 1f;
        [SerializeField] private float _moneyCount;
        
        [Header("External")]
        [Tooltip("The rate at which external velocity decays")]
        [SerializeField] private int _externalVelocityDecay = 100;
        [SerializeField] private float _defaultSightRange = 60f;

        [Header("Other")] 
        [SerializeField] private GameObject _defaultBullet;
        [SerializeField] private PlayerAbility _ability;
        [SerializeField] private string _abilitySound;
        [SerializeField] private StatusEffect _statusEffect;

        [SerializeField] private GameObject _abilityPopup;
        [SerializeField] private float _abilityCooldownMultiplier = 1f;
        
        [SerializeField, ReadOnly]
        private Stats _stats;

        private int _totalKillCount = 0;
        private int _killCount = 0;

        #region Things to do when something is changed

        public TMP_Text MoneyText { get; set; }

        /// <summary>
        /// So that we can set the money count and have it update the UI
        /// </summary>
        /// <param name="moneyToAdd"></param>
        /// <returns></returns>
        private float SetMoney(float moneyToAdd) {
            
            _stats.MoneyCount += moneyToAdd;
                
            if (MoneyText != null)
                MoneyText.text = "Coins: " + _stats.MoneyCount.ToString("N0");
            
            return _stats.MoneyCount;
        }
        
        public void AddKill()
        {
            if (IsBot) return;
            
            _totalKillCount++;
            _killCount++;
            
            

            if (GameManager.Instance.GameMode != Constants.GameTypes.Brawl) return;
            
            GameManager.Instance.KillsSlider.value = _killCount;
            GameManager.Instance.KillsSlider.maxValue = GameManager.Instance.BrawlScoreToWin;

            if (_killCount >= GameManager.Instance.BrawlScoreToWin)
            {
                RollAbility.Instance.EnableRoll();
                GameManager.Instance.BrawlScoreToWin++;
                _killCount = 0;
            }
        }

        #endregion

        #region Getters/Setters
        /// <summary>
        /// Do not use add to this, only set it. The add it implied.
        /// </summary>
        public float MoneyCount { get => _stats.MoneyCount; set => SetMoney(value); }
        public float MoneyPerKill { get => _stats.MoneyPerKill; set => _stats.MoneyPerKill = value; }
        public float PassiveMoneyPerSecond { get => _stats.PassiveMoneyPerSecond; set => _stats.PassiveMoneyPerSecond = value; }
        public bool CanMove { get => _stats.CanMove; set => _stats.CanMove = value; }
        public float Speed
        {
            get => _stats.Speed;
            set => _stats.Speed = _stats.Speed + value > 0 ? value : 0;
        }

        public float RotationSpeed
        {
            get => _stats.RotationSpeed; 
            set => _stats.RotationSpeed = _stats.RotationSpeed + value > 0.1 ? value : 0;
        }
        public float CurrentHealth { get => _stats.CurrentHealth; set => _stats.CurrentHealth = value; }
        public float MaxHealth { get => _stats.MaxHealth; set => _stats.MaxHealth = value; }
        public float Defense { get => _stats.Defense; set => _stats.Defense = value; }
        public float HealthRegen { get => _stats.HealthRegen; set => _stats.HealthRegen = value; }
        public bool CanShoot { get => _stats.CanShoot; set => _stats.CanShoot = value; }
        public float Damage { get => _stats.Damage; set => _stats.Damage = value; }
        public float ShootingSpeed { get => _stats.ShootingSpeed; set => _stats.ShootingSpeed = value; }
        public PlayerAbility Ability { get => _stats.Ability; private set => _stats.Ability = value; }
        public GameObject DefaultBullet => _stats.DefaultBullet;
        public int ExternalVelocityDecay { get => _stats.ExternalVelocityDecay; set => _stats.ExternalVelocityDecay = value; }
        public bool HasShop => _hasShop;
        public Rigidbody RigidBody { get; private set; }
        public PlayerController PlayerController { get; private set; }
        public PlayerInteractions PlayerInteractions { get; private set; }
        public int KillCount => _killCount;
        public int TotalKillCount => _totalKillCount;
        public int DeathCount { get; private set; }
        public bool IsBot { get; private set; }
        public StatusEffect StatusEffect
        {
            get => _statusEffect;
            set => SetNewStatusEffect(value);
        }
        public float AbilityCooldownMultiplier { get => _abilityCooldownMultiplier; set => _abilityCooldownMultiplier = value; }
        
        #endregion

        #region Sets

        public void SetNewBullet(GameObject bullet)
        {
            _stats.DefaultBullet = bullet;
        }

        [Button]
        public void GodMode(bool on)
        {
            if (on)
            {
                MaxHealth = 999999;
                CurrentHealth = 999999;
            }
            else
            {
                MaxHealth = 100;
                CurrentHealth = 100;
            }
        }
        
        [Button]
        public void SetNewAbility(PlayerAbility ability)
        {
            var currentAbility = GetComponentInChildren<PlayerAbility>();
            
            if (currentAbility != null) 
                Destroy(currentAbility.gameObject);
            
            var obj = Instantiate(ability, RigidBody.transform);
            
            Ability = obj;

            if (IsBot)
            {
                _ability = obj;
                return;
            }
            
            MasterAudio.PlaySound(_abilitySound);
            
            AbilityUI.Instance.SetAbility(_stats.Ability);
            
            _abilityPopup.SetActive(true);
            
        }

        private void SetNewStatusEffect(StatusEffect statusEffect)
        {
            var currentStatusEffect = GetComponentInChildren<StatusEffect>();
            
            if (currentStatusEffect != null) 
                Destroy(currentStatusEffect.gameObject);
            
            var obj = Instantiate(statusEffect, GetComponentInChildren<Rigidbody>().transform);

            _statusEffect = obj;
            _statusEffect.ApplyStatusEffect();
        }
        
        #endregion

        public void EndGame(bool won)
        {
            WinScreen.Instance.SetResults(
                won, 
                won ? "Nice job! You Won! Play again by pressing continue, or return to the menu." 
                    : "You lost! Try again by pressing continue, or return to the menu.",
                KillCount,
                CalculateScore());
        }

        public event Action<PlayerVariables> OnHit;
        public void Hit(PlayerVariables playerVariables)
        {
            OnHit?.Invoke(playerVariables);
        }
        
        private int CalculateScore()
        {
            var score = TotalKillCount * 100;
            score += (int) MoneyCount;
            score += (int) Random.value * 1000;
            
            return score;
        }
        
        [System.Serializable]
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
            public float PassiveMoneyPerSecond;
            public float MoneyCount;
            
            [Header("External")]
            public int ExternalVelocityDecay;
            public GameObject DefaultBullet;
            public PlayerAbility Ability;
            public float AbilityCooldownMultiplier;
        }

        private void Awake()
        { 
            RigidBody = GetComponentInChildren<Rigidbody>();

            IsBot = RigidBody.gameObject.TryGetComponent(out BotNavigation nav);
            
            InvokeRepeating(nameof(CheckValues), 10f, 10f);
            PlayerInteractions = GetComponentInChildren<PlayerInteractions>();
            
            if (IsBot) return;
            
            GameManager.Instance.MainPlayer = this;
            
            if (GameManager.Instance.GameMode == Constants.GameTypes.Economy)
                _hasShop = true;
            
            PlayerController = GetComponentInChildren<PlayerController>();
            
            InvokeRepeating(nameof(AddPassiveMoney), 0, 1f);
        }

        public void Start()
        {
            SetStats();
        }

        private void AddPassiveMoney()
        {
            SetMoney(PassiveMoneyPerSecond);
        }
        
        private void CheckValues()
        {

            if (CurrentHealth <= 0)
            {
                PlayerInteractions.DealDamage(0f);
            }

            if (Speed < _speed)
            {
                Speed = _speed;
            }
            
            if (HealthRegen < _healthRegen)
            {
                HealthRegen = _healthRegen;
            }
        }
        
        /// <summary>
        /// Resets all the stats to their default values
        /// </summary>
        public void SetStats()
        {
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
                PassiveMoneyPerSecond = _passiveMoneyPerSecond,
                MoneyCount = _moneyCount,
                DefaultBullet = _defaultBullet,
                Ability = _ability,
                ExternalVelocityDecay = _externalVelocityDecay,
                AbilityCooldownMultiplier = _abilityCooldownMultiplier,
            };
            
            MoneyCount = 0;
            DeathCount++;

            if (IsBot)
            {
                GetComponentInChildren<BotShooting>().SetRandomStats();
                return;
            }

            GetComponentInChildren<CameraController>()
                .Camera.GetCinemachineComponent<CinemachineFramingTransposer>()
                .m_CameraDistance = _defaultSightRange;

            if (GameManager.Instance.GameMode == Constants.GameTypes.Brawl)
            {
                RollAbility.Instance.DisableRoll();
                
                GameManager.Instance.ResetBrawlScore();
                _killCount = 0;
                            
                GameManager.Instance.KillsSlider.value = _killCount;
                GameManager.Instance.KillsSlider.maxValue = GameManager.Instance.BrawlScoreToWin;
            }

            else if (GameManager.Instance.GameMode == Constants.GameTypes.Economy)
            {
                if (ShopMenu.Instance != null)
                    ShopMenu.Instance.ResetShop();
            }
            
            AbilityUI.Instance.SetAbility(null);
            print("Player Reset Stats\n" + ToString());
        }
        
        [Command("stats")]
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Stats:");
            sb.AppendFormat("  CanMove: {0}\n", CanMove);
            sb.AppendFormat("  Speed: {0}\n", Speed);
            sb.AppendFormat("  RotationSpeed: {0}\n", RotationSpeed);
            sb.AppendFormat("  CurrentHealth: {0}\n", CurrentHealth);
            sb.AppendFormat("  MaxHealth: {0}\n", MaxHealth);
            sb.AppendFormat("  Defense: {0}\n", Defense);
            sb.AppendFormat("  HealthRegen: {0}\n", HealthRegen);
            sb.AppendFormat("  CanShoot: {0}\n", CanShoot);
            sb.AppendFormat("  Damage: {0}\n", Damage);
            sb.AppendFormat("  ShootingSpeed: {0}\n", ShootingSpeed);
            sb.AppendFormat("  MoneyPerKill: {0}\n", MoneyPerKill);
            sb.AppendFormat("  MoneyCount: {0}\n", MoneyCount);
            sb.AppendFormat("  ExternalVelocityDecay: {0}\n", ExternalVelocityDecay);
            return sb.ToString();
        }

        public static implicit operator GameObject(PlayerVariables v)
        {
            throw new NotImplementedException();
        }
    }

}
