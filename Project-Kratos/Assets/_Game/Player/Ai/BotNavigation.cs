using System;
using System.Collections.Generic;
using System.Linq;
using ProjectKratos.Player;
using Sirenix.OdinInspector;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectKratos
{
    public class BotNavigation : NetworkBehaviour 
    {
        [SerializeField] private Vector3 _destination;

        [SerializeField] private float _checkDestinationIntervaks = 10;
        
        private NavMeshAgent _agent;
        private ClientNetworkTransform _closestPlayer;
        private float _shortestDistance;

        public override void OnNetworkSpawn()
        {
            _agent = GetComponent<NavMeshAgent>();
            
            InvokeRepeating(nameof(SetDestinationServerRpc), 1, _checkDestinationIntervaks);
            
            if (IsServer || IsHost)
                AgentServerRpc();
        }

        /// <summary>
        /// This C# code defines a public method called "SetDestinationServerRpc"
        /// which finds the closest player to the current game object and sets
        /// their position as the destination. It then sends an RPC to the server
        /// with that destination. The code achieves this by iterating through a 
        /// list of players, calculating their distance to the game object and updating 
        /// the closest player accordingly. If a closest player is found, the method 
        /// sets the destination to the position of the closest player and calls the 
        /// SetDestinationServerRpc method with that destination.
        /// </summary>
        [ServerRpc(RequireOwnership = false)]
        public void SetDestinationServerRpc()
        {
            var shortestDistance = float.MaxValue;

            // It gets the closest player to the bot
            var playerVariablesEnumerable = GameManager.Instance.Players
                .Where(player => player.GetComponentInChildren<ClientNetworkTransform>().gameObject != gameObject);
            
            foreach (var player in playerVariablesEnumerable)
            {
                _shortestDistance = Vector3.Distance(transform.position, player.transform.position);
                
                if (!(_shortestDistance < shortestDistance)) continue;
                
                shortestDistance = _shortestDistance;
                _closestPlayer = player.GetComponentInChildren<ClientNetworkTransform>();

            }
            
            // Make it sao that it follows the transform of the player instead of the position
            if (_closestPlayer != null) 
                _destination = _closestPlayer.transform.position;

            SetDestinationServerRpc(_destination);
            
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetDestinationServerRpc(Vector3 destination)
        {
            _destination = destination;
            AgentServerRpc();
        }
        
        [ServerRpc]
        private void AgentServerRpc()
        {
            _agent.SetDestination(_destination);
        }
    }
}
