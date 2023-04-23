using ProjectKratos.Bullet;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerShoot : MonoBehaviour 
    {
        [SerializeField] private Transform _firepoint;
        [SerializeField] private GameObject _bulletPrefab;
        private PlayerVariables _shooterObject;

        public Transform Firepoint => _firepoint;

        public void Start()
        {
            _shooterObject = transform.gameObject.GetComponentInParent<PlayerVariables>();
        }

        /// <summary>
        /// In Future, we may want to also pass in the firepoint position, if there are aiming issues
        /// </summary>
        /// <param name="rotation"></param>
        /// <param name="damageMultiplier"></param>
        /// <param name="speedMultipler"></param>
        /// <param name="isBot"></param>
        public void ShootBullet(Quaternion rotation, float damageMultiplier, float speedMultipler, bool isBot)
        {
            // If not a bot and is the owner, go
            // If is a bot and is the server, go
            // Is Bot
            
            // Spawn Bullet 
            CreateBullet(rotation, _shooterObject.gameObject, damageMultiplier, speedMultipler);
        }

        private void CreateBullet(Quaternion bulletRotation, GameObject shooter, float damageMultiplier, float speedMultipler)
        {
            
            // The velocity is done in the awake function on the object In the BulletScript
            var bullet = Instantiate(
                _bulletPrefab,
                _firepoint.position,
                bulletRotation,
                null);

            var bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.CreateBullet(transform.forward, shooter, damageMultiplier, speedMultipler);
        }
    }
}
