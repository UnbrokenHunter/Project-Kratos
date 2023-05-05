using System;
using System.Collections.Generic;
using System.Linq;
using ProjectKratos.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace ProjectKratos
{
    public class BotNavigation : MonoBehaviour 
    {
        [SerializeField] private Vector3 _destination;

        [SerializeField] private float _checkDestinationIntervals = 1;
        
        private NavMeshAgent _agent;
        private Transform _closestPlayer;
        private float _shortestDistance;

        private PlayerVariables _variables;
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            
            _variables = transform.GetComponentInParent<PlayerVariables>();
            InvokeRepeating(nameof(SetDestination), 1, _checkDestinationIntervals);
        }

        public void SetDestination()
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
            
            // Make it sao that it follows the transform of the player instead of the position
            if (_closestPlayer != null) 
                _destination = _closestPlayer.position;

            SetDestination(_destination);
            
        }

        private void SetDestination(Vector3 destination)
        {
            _destination = destination;
            _agent.SetDestination(_destination);
        }
    }
}
