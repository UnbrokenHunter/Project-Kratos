using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos
{
    public abstract class CollectableItem : NetworkBehaviour
    {
        private ObjectSpawner _spawner;
        private int _spawnIndex;
        
        public CollectableItem SpawnItem(ObjectSpawner spawner, int spawnIndex)
        {
            _spawner = spawner;
            _spawnIndex = spawnIndex;
            
            return this;
        }

        public void OnDisable()
        {
            _spawner.ObjectDespawn(_spawnIndex);
        }

        private void OnTriggerEnter(Collider other)
        { 
            if (!other.CompareTag("Player")) return;
            ItemCollected(other.transform.root.gameObject); 
        }

        protected abstract void ItemCollected(GameObject player);
    }
}
