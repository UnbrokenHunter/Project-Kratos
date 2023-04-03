using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Bullet
{
    public class BulletScript : MonoBehaviour

    {
        public ShooterStats ShooterStats { get; private set; }

        public ScriptableBullet BulletStats { get => _bulletStats; }
        [SerializeField] private ScriptableBullet _bulletStats;

        public void Start() => GetComponent<Rigidbody>().AddForce(BulletStats.Speed * ShooterStats.ShooterSpeedMultipler * ShooterStats.Direction, ForceMode.Impulse);

        public void CreateBullet(Vector3 direction, GameObject shooterGameObject, float shooterDamageMultipler, float shooterSpeedMultiplier)
        {
            ShooterStats = new ShooterStats {
                Direction = direction,
                ShooterGameObject = shooterGameObject,
                ShooterDamageMultipler = shooterDamageMultipler,
                ShooterSpeedMultipler = shooterSpeedMultiplier
            };
        }

        // Hits something
        private void OnTriggerEnter(Collider other)
        {
            // Hits World
            if (other.CompareTag("World"))
            {
                //Destroy(gameObject);
            }


            // Hits Player
            if (!other.transform.root.CompareTag("Player")) return;
            
            PlayerInteractions player = 
                other.transform.root.GetComponentInChildren<PlayerInteractions>();

            player.PlayerHit(this);

        }
    }

    public struct ShooterStats
    {
        public Vector3 Direction;
        public GameObject ShooterGameObject;
        public float ShooterDamageMultipler;
        public float ShooterSpeedMultipler;
    }

}
