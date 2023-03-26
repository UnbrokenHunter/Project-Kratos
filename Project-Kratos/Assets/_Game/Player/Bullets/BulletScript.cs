using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Bullet
{
    public class BulletScript : MonoBehaviour

    {
        public ShooterStats ShooterStats { get; private set; }

        public ScriptableBullet BulletStats { get => _bulletStats; }
        [SerializeField] private ScriptableBullet _bulletStats;

        public void Start() => GetComponent<Rigidbody>().AddForce(BulletStats.Speed * ShooterStats.Direction, ForceMode.Impulse);

        public void CreateBullet(Vector3 direction, int shooterID, float shooterDamageMultipler)
        {
            ShooterStats = new ShooterStats {
                Direction = direction,
                ShooterID = shooterID,
                ShooterDamageMultipler = shooterDamageMultipler
            };
        }

        // Hits something
        private void OnTriggerEnter(Collider other)
        {
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
        public int ShooterID;
        public float ShooterDamageMultipler;
    }

}
