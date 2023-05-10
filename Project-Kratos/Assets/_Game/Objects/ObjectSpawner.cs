using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectKratos
{
    public class ObjectSpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private CollectableItemStats[] _objectsToSpawn;
        [SerializeField] private float _spawnInterval = 5f;
        
        [Space]
        
        [SerializeField] private SpawnStats[] _spawnStats;

        private float _spawnChanceTotal => _spawnStats.Sum(stat => stat.spawnChance);
        private float _itemSpawnChanceTotal => _objectsToSpawn.Sum(stat => stat.spawnChance);
        
        private void Start()
        {
            InvokeRepeating(nameof(SpawnObject), 0, _spawnInterval);
        }

        private void SpawnObject()
        {
            // The number after removing objects that are already spawned
            var realSpawnNumber = _spawnChanceTotal;
            
            for (int i = 0; i < _spawnStats.Length; i++)
            {
                if (_spawnStats[i].isSpawned)
                {
                    realSpawnNumber -= _spawnStats[i].spawnChance;
                    continue;
                }
            }
            
            // Get a random number between 0 and the total spawn chance - already spawned objects
            var spawnNumber = Random.Range(0, realSpawnNumber); 
            var currentSpawnChance = _spawnStats[0].spawnChance;
            
            for (int i = 0; i < _spawnStats.Length; i++)
            {
                if(_spawnStats[i].isSpawned) continue;

                if (currentSpawnChance >= spawnNumber) 
                {
                    var randomItem = PickRandomItem();
                    
                    CollectableItem item = Instantiate(randomItem.item, _spawnStats[i].position, Quaternion.identity).SpawnItem(this, i);
                    
                    _spawnStats[i].isSpawned = true;
                    return;     
                }        
                
                currentSpawnChance += _spawnStats[i].spawnChance;
                
            }
        }

        private CollectableItemStats PickRandomItem()
        {
            var spawnNumber = Random.Range(0, _itemSpawnChanceTotal);
            var currentSpawnChance = _objectsToSpawn[0].spawnChance;

            for (int i = 0; i < _objectsToSpawn.Length; i++)
            {
                if (currentSpawnChance >= spawnNumber)
                {
                    return _objectsToSpawn[i];
                }
                
                currentSpawnChance += _objectsToSpawn[i].spawnChance;
            }

            print("No item found");
            return _objectsToSpawn[0];
        }

        /// <summary>
        /// Call this method when an object is collected or destroyed to reset the spawn stats
        /// </summary>
        public void ObjectDespawn(int index)
        {
            _spawnStats[index].isSpawned = false;
        }
        
        private void OnDrawGizmos()
        {
            foreach (var obj in _spawnStats)
            {
                Gizmos.color = obj.gizmoColor;
                Gizmos.DrawSphere(obj.position, 7f);
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
        
        [System.Serializable]
        private struct CollectableItemStats
        {
            public CollectableItem item;
            public float spawnChance;
        }
    }
}
