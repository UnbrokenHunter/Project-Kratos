using ProjectKratos.Player;
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
                ItemCollected(other.GetComponentInParent<PlayerVariables>(), gameObject);
            
            if (!_destroyOnPickup) return;
                DestroyObject();
        }

        public abstract void ItemCollected(PlayerVariables player, GameObject item);
    }
}
