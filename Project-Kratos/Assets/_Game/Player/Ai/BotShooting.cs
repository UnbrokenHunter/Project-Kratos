using System;
using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectKratos
{
    public class BotShooting : NetworkBehaviour
    {
        [SerializeField] private float _sightRange = 10;
        [SerializeField] private float _shootRate = 2;
        [SerializeField] private LayerMask _layers;

        private NavMeshAgent _agent;
        
        private RaycastHit[] _hits = new RaycastHit[1];
        private float _waitToShoot = 0;

        private bool _gameStarted = false;
        
        [SerializeField] private PlayerShoot _playerShoot;
        private PlayerVariables _playerVars;
        
        
        public override void OnNetworkSpawn()
        {
            _agent = GetComponent<NavMeshAgent>();
            _playerVars = GetComponentInParent<PlayerVariables>();
            _gameStarted = true;
        }

        private void FixedUpdate()
        {
            if (!_gameStarted) return;

            if (_playerVars.CanMove == false)
            {
                _agent.isStopped = true;
                return;
            }
            
            int hit = Physics.RaycastNonAlloc(_playerShoot.Firepoint.position, transform.forward, _hits,_sightRange, _layers);
            
            _waitToShoot += Time.deltaTime;
            
            if (hit > 0)
            {
                // print("In Sight");
                _agent.isStopped = true;
                AgentShoot(); 
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
            
            _playerShoot.ShootBullet(transform.rotation, 1, 1, true);
            _waitToShoot = 0;           
        }
        
        // Draw a gizmo of the ray above
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_playerShoot.Firepoint.position, transform.forward * _sightRange);
        }
    }
}
