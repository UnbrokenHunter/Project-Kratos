using UnityEngine;

namespace ProjectKratos
{
    public abstract class CollectableItem : MonoBehaviour
    {
        private ObjectSpawner _spawner; 
        private int _spawnIndex; 
        
        [SerializeField] private bool _destroyOnPickup = true;
        
        public CollectableItem SpawnItem(ObjectSpawner spawner, int spawnIndex)
        {
            _spawner = spawner;
            _spawnIndex = spawnIndex;
            
            return this;
        }

        private void DestroyObject()
        {
            _spawner.ObjectDespawn(_spawnIndex);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        { 
            if (!other.CompareTag("Player")) return;
                ItemCollected(other.transform.root.gameObject, gameObject);
            
            if (!_destroyOnPickup) return;
                DestroyObject();
        }

        /// <summary>
        /// Make sure to check that the item that is being collected is the correct item. Otherwise it will call this method on all items.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        protected abstract void ItemCollected(GameObject player, GameObject item);
    }
}
