using ProjectKratos.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectKratos.Bullet
{
    public abstract class BulletScript : MonoBehaviour 
    {
        public ShooterStats ShooterStats { get; private set; }

        public float BulletSpeed => _bulletSpeed;
        [SerializeField] private float _bulletSpeed = 1f;
        
        public float BulletDamage => _bulletDamage;
        [SerializeField] private float _bulletDamage = 1f;
        
        private void Start() => GetComponent<Rigidbody>().AddForce(_bulletSpeed * ShooterStats.ShooterSpeedMultiplier * ShooterStats.Direction, ForceMode.Impulse);

        public void CreateBullet(Vector3 direction, GameObject shooterGameObject, float shooterDamageMultiplier, float shooterSpeedMultiplier)
        {
            ShooterStats = new ShooterStats {
                Direction = direction,
                ShooterGameObject = shooterGameObject,
                ShooterDamageMultiplier = shooterDamageMultiplier,
                ShooterSpeedMultiplier = shooterSpeedMultiplier,
            };
        }

        // Hits something
        private void OnTriggerEnter(Collider other)
        {
            // Hits World
            if (other.CompareTag("World"))
            {
                ContactWorld();
            }

            // Hits Player
            if (!other.gameObject.CompareTag("Player")) return;
            
            // Make sure the player is not the shooter
            if (other.GetComponentInParent<PlayerVariables>().gameObject == ShooterStats.ShooterGameObject) return;
           
            var player = other.transform.GetComponentInParent<PlayerHitInteractions>();

            ContactPlayer(player);
            
            Destroy(gameObject);

        }

        protected virtual void ContactWorld()
        {
            Destroy(gameObject);
        }
        
        protected abstract void ContactPlayer(PlayerHitInteractions player);
        
    }

    public struct ShooterStats
    {
        public Vector3 Direction;
        public GameObject ShooterGameObject;
        public float ShooterDamageMultiplier;
        public float ShooterSpeedMultiplier;
    }
}
