using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Bullet
{
    public class BulletScript : MonoBehaviour
    {
        public ShooterStats ShooterStats { get; private set; }

        public ScriptableBullet BulletStats { get => _bulletStats; }
        [SerializeField] private ScriptableBullet _bulletStats;

        public void Start() => GetComponent<Rigidbody>().AddForce(BulletStats.Speed * ShooterStats.ShooterSpeedMultiplier * ShooterStats.Direction, ForceMode.Impulse);

        public void CreateBullet(Vector3 direction, GameObject shooterGameObject, float shooterDamageMultiplier, float shooterSpeedMultiplier)
        {
            ShooterStats = new ShooterStats {
                Direction = direction,
                ShooterGameObject = shooterGameObject,
                ShooterDamageMultiplier = shooterDamageMultiplier,
                ShooterSpeedMultiplier = shooterSpeedMultiplier
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
            if (!other.transform.root.CompareTag("Player")) return;
            
            PlayerHitInteractions player = 
                other.transform.root.GetComponentInChildren<PlayerHitInteractions>();

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
