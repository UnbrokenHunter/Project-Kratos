using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos
{
    public class ObjectSpawner : NetworkBehaviour 
    {
        [Header("Spawn Settings")]
        [SerializeField] private CollectableItem _objectToSpawn;
        [SerializeField] private float _spawnInterval = 5f;
        
        [Space]
        
        [SerializeField] private SpawnStats[] _spawnStats;

        private float spawnChanceTotal => _spawnStats.Sum(stat => stat.spawnChance);
 
        public override void OnNetworkSpawn()
        {
            if (!IsServer) return;
            
            InvokeRepeating(nameof(SpawnObject), 0, _spawnInterval);
        }

        private void SpawnObject()
        {
            if (!IsServer) return; 
            
            // The number after removing objects that are already spawned
            float realSpawnNumber = spawnChanceTotal;
            
            for (int i = 0; i < _spawnStats.Length; i++)
            {
                if (_spawnStats[i].isSpawned)
                {
                    realSpawnNumber -= _spawnStats[i].spawnChance;
                    continue;
                }
            }
            
            // Get a random number between 0 and the total spawn chance - already spawned objects
            float spawnNumber = Random.Range(0, realSpawnNumber);
            float currentSpawnChance = _spawnStats[0].spawnChance;
            
            for (int i = 0; i < _spawnStats.Length; i++)
            {
                if(_spawnStats[i].isSpawned) continue;

                if (currentSpawnChance >= spawnNumber) 
                {
                    CollectableItem item = Instantiate(_objectToSpawn, _spawnStats[i].position, Quaternion.identity).SpawnItem(this, i);
                    // Spawn this object on the server
                    item.GetComponent<NetworkObject>().Spawn();
                    
                    _spawnStats[i].isSpawned = true;
                    return;     
                }        
                
                currentSpawnChance += _spawnStats[i].spawnChance;
                
            }
        }

        /// <summary>
        /// Call this method when an object is collected or destroyed to reset the spawn stats
        /// </summary>
        [ServerRpc(RequireOwnership = false)]
        public void ObjectDespawnServerRpc(int index)
        {
            if (!IsServer) return;
            _spawnStats[index].isSpawned = false;
        }
        
        private void OnDrawGizmos()
        {
            foreach (var obj in _spawnStats)
            {
                Gizmos.color = obj.gizmoColor;
                Gizmos.DrawSphere(obj.position, 0.5f);
            }
        }
        
        [System.Serializable]
        private struct SpawnStats 
        {
            public Vector3 position;
            [Range(0, 100)] public float spawnChance;
            public bool isSpawned;
            public Color gizmoColor;
        }
    }
}
