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
        private PlayerController _closestPlayer;
        private float _shortestDistance;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            
            InvokeRepeating(nameof(SetDestination), 1, _checkDestinationIntervals);
        }

        public void SetDestination()
        {
            var shortestDistance = float.MaxValue;

            // It gets the closest player to the bot
            var playerVariablesEnumerable = GameManager.Instance.Players
                .Where(player => player.GetComponentInChildren<PlayerController>().gameObject != gameObject);
            
            foreach (var player in playerVariablesEnumerable)
            {
                _shortestDistance = Vector3.Distance(transform.position, player.transform.position);
                
                if (!(_shortestDistance < shortestDistance)) continue;
                
                shortestDistance = _shortestDistance;
                _closestPlayer = player.GetComponentInChildren<PlayerController>();

            }
            
            // Make it sao that it follows the transform of the player instead of the position
            if (_closestPlayer != null) 
                _destination = _closestPlayer.transform.position;

            SetDestination(_destination);
            
        }

        private void SetDestination(Vector3 destination)
        {
            _destination = destination;
            _agent.SetDestination(_destination);
        }
    }
}
