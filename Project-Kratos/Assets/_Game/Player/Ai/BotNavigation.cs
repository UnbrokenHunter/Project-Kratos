using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectKratos.Player;
using Sirenix.OdinInspector;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace ProjectKratos
{
    public class BotNavigation : MonoBehaviour 
    {
        [SerializeField] private bool _debug;
        private void OnDrawGizmos()
        {
            if (!_debug) return;
            
            Gizmos.color = Color.blue;
            
            Gizmos.DrawSphere(_destination, 0.5f);
            
            Gizmos.color = _state == States.Moving ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position, _tooCloseDistance);
        }

        [SerializeField] private Vector3 _destination;

        [SerializeField] private float _checkDestinationIntervals = 1;
        
        [Header("Nav Settings")]
        [SerializeField] private float _tooCloseDistance = 1f;
        [SerializeField] private float _revengeTime = 5f;
        [SerializeField] private float _playerDistanceBias = 10f;
        
        private enum States
        {
            Idle,
            Moving,
            Revenge,
        }
        private States _state = States.Moving;
        
        private NavMeshAgent _agent;
        private Transform _closestPlayer;
        private float _shortestDistance;
        private PlayerVariables _attacker;

        private PlayerVariables _variables;
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            
            _variables = transform.GetComponentInParent<PlayerVariables>();
            _variables.OnHit += PlayerAttacked;
            
            InvokeRepeating(nameof(SetDestination), 1, _checkDestinationIntervals);
        }

        public void SetDestination()
        {

            if (_state == States.Moving)
                MovingState();
            
            else if (_state == States.Revenge)
                RevengeState();

            SetDestination(_destination);
        }

        private bool MovingState()
        {
            var closestPlayer = GetClosestPlayer();
            var position = transform.position;
            var distance = Vector3.Distance(position, closestPlayer.position);
            
            if (distance <= _tooCloseDistance)
            {
                _destination = position + (position - closestPlayer.position);
                SetDestination(_destination);
                return true;
            }

            return false;
        }

        private Transform GetClosestPlayer()
        {
            var shortestDistance = float.MaxValue;

            // It gets the closest player to the bot
            var playerList = GameManager.Instance.Players;

            foreach (var player in playerList.Where(player => player.gameObject != transform.parent.gameObject))
            {
                _shortestDistance = Vector3.Distance(transform.position, player.RigidBody.transform.position);
                
                if (!(_shortestDistance < shortestDistance)) continue;
                
                shortestDistance = _shortestDistance;
                _closestPlayer = player.RigidBody.transform;
            }
            
            var _mainPlayerDistance = Vector3.Distance(transform.position, GameManager.Instance.MainPlayer
                .RigidBody.transform.position);
                
            if (shortestDistance < _mainPlayerDistance - _playerDistanceBias)
                _closestPlayer = GameManager.Instance.MainPlayer.RigidBody.transform;
            
            // Make it sao that it follows the transform of the player instead of the position
            if (_closestPlayer != null) 
                _destination = _closestPlayer.position;

            return _closestPlayer;
        }

        private void PlayerAttacked(PlayerVariables attacker)
        {
            _attacker = attacker;
            _state = States.Revenge;
            StartCoroutine(AngerTimer());
        }

        private IEnumerator AngerTimer()
        {
            yield return Helpers.GetWait(_revengeTime);
            
            _state = States.Moving;
        }

        private void RevengeState()
        {
            if (_attacker == _variables) return;
            
            var destination = _attacker.RigidBody.transform.position;
            
            SetDestination(destination);
        }
        
        private void SetDestination(Vector3 destination)
        {
            _destination = destination;
            _agent.SetDestination(_destination);
        }
    }
}
