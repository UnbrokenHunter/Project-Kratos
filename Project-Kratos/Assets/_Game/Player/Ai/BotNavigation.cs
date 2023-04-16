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
