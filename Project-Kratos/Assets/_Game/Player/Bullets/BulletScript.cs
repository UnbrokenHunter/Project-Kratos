<<<<<<< HEAD
=======
using DarkTonic.MasterAudio;
using ProjectKratos.Player;
>>>>>>> add-death
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectKratos.Bullet
{
    public abstract class BulletScript : MonoBehaviour 
    {
<<<<<<< HEAD
        public Vector3 Direction { private get; set; }

        [SerializeField] private ScriptableBullet _bulletType;

        private void Start()
        {
            GetComponent<Rigidbody>().AddForce(_bulletType.BulletSpeed * Direction, ForceMode.Impulse);
        }
=======
        public ShooterStats ShooterStats { get; private set; }
        
        public float BulletSpeed => _bulletSpeed;
        [SerializeField] private float _bulletSpeed = 1f;
        
        public float BulletDamage => _bulletDamage;
        [SerializeField] private float _bulletDamage = 1f;

        [SerializeField] private string _shootAudio;
        [SerializeField] private string _hitAudio;

        protected Rigidbody Rigidbody => _rigidbody;
        private Rigidbody _rigidbody;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddForce(_bulletSpeed * ShooterStats.ShooterSpeedMultiplier * ShooterStats.Direction,
                    ForceMode.Impulse);
        }

        public void CreateBullet(Vector3 direction, GameObject shooterGameObject, float shooterDamageMultiplier, float shooterSpeedMultiplier)
        {
            MasterAudio.PlaySound(_shootAudio);
            
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
                MasterAudio.PlaySound(_hitAudio);
            }

            // Hits Player
            if (!other.gameObject.CompareTag("Player")) return;
            
            // Make sure the player is not the shooter
            if (other.GetComponentInParent<PlayerVariables>().gameObject == ShooterStats.ShooterGameObject) return;
           
            var player = other.transform.GetComponentInParent<PlayerHitInteractions>();

            MasterAudio.PlaySound(_hitAudio);
            
            ContactPlayer(player);
        }

        protected virtual void ContactWorld()
        {
            Destroy(gameObject);
        }
        
        protected abstract void ContactPlayer(PlayerHitInteractions player);
        
    }
>>>>>>> add-death

    }
}
