using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Bullet
{
    public class BulletScript : MonoBehaviour 
    {
        public ShooterStats ShooterStats { get; private set; }

        public ScriptableBullet BulletStats { get => _bulletStats; }
        [SerializeField] private ScriptableBullet _bulletStats;

        private void Start() => GetComponent<Rigidbody>().AddForce(BulletStats.Speed * ShooterStats.ShooterSpeedMultiplier * ShooterStats.Direction, ForceMode.Impulse);

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
                Destroy(gameObject);
            }

            // Hits Player
            if (!other.gameObject.CompareTag("Player")) return;
            
            // Make sure the player is not the shooter
            if (other.GetComponentInParent<PlayerVariables>().gameObject == ShooterStats.ShooterGameObject) return;
           
            // print("This player : " + this.ShooterStats.ShooterGameObject.name + " hit " + other.transform.parent.gameObject.name + " with a bullet" );
            
            var player = other.transform.GetComponentInParent<PlayerHitInteractions>();

            player.PlayerHit(this);
            
            Destroy(gameObject);

        }
    }

    public struct ShooterStats
    {
        public Vector3 Direction;
        public GameObject ShooterGameObject;
        public float ShooterDamageMultiplier;
        public float ShooterSpeedMultiplier;
    }
}
