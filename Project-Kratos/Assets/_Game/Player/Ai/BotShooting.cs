using ProjectKratos.Player;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectKratos
{
    public class BotShooting : MonoBehaviour
    {
        [SerializeField] private float _sightRange = 10;
        [SerializeField] private float _shootRate = 2;
        [SerializeField] private LayerMask _layers;
        [SerializeField, Range(0f, 1f)] private float _abilityChance = 0.9f;

        [Space] [SerializeField] private PlayerAbility[] _botAbilities;
        
        private NavMeshAgent _agent;
        
        private RaycastHit[] _hits = new RaycastHit[1];
        private float _waitToShoot = 0;

        private bool _gameStarted = false;
        
        [SerializeField] private PlayerShoot _playerShoot;
        private PlayerVariables _playerVars;
        
        public void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _playerVars = GetComponentInParent<PlayerVariables>();
            _gameStarted = true;
            
            SetRandomStats();
        }

        public void SetRandomStats()
        {
            _playerVars.Speed += Random.Range(-100f, 300f);
            _playerVars.Damage += Random.Range(-1f, 2f);
            _playerVars.MaxHealth += Random.Range(-10f, 30f);
            _playerVars.Defense += Random.Range(-0.3f, 1f);
            _playerVars.ShootingSpeed += Random.Range(-0.5f, 1f);
            _playerVars.HealthRegen += Random.Range(-0.5f, 1f);
            
            _playerVars.SetNewAbility(_botAbilities[Random.Range(0, _botAbilities.Length)]);
        }

        private void FixedUpdate()
        {
            if (!_gameStarted) return;
            
            if (_playerVars.CanMove == false)
            {
                _agent.isStopped = true;
                return;
            }
            
            _agent.speed = _playerVars.Speed / 200;
            
            var hit = Physics.RaycastNonAlloc(_playerShoot.FirePoint.position, transform.forward, _hits,_sightRange, _layers);
            
            _waitToShoot += Time.deltaTime;
            
            if (hit > 0)
            {
                // print("In Sight");
                _agent.isStopped = true;
                AgentShoot();

                if (Random.value > 0.9f)
                {
                    _playerVars.Ability.TriggerAbility();
                }
            }

            // Keep going until in sight
            else
            {
                _agent.isStopped = false;
            }
        }

        private void AgentShoot()
        {
            if (_waitToShoot < _shootRate) return;
            
            _playerShoot.ShootBullet(_playerVars.DefaultBullet);
            _waitToShoot = 0;           
        }
        
        // Draw a gizmo of the ray above
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_playerShoot.FirePoint.position, transform.forward * _sightRange);
        }
    }
}
