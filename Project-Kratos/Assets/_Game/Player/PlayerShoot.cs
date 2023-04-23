using ProjectKratos.Bullet;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerShoot : MonoBehaviour 
    {
        [SerializeField] private Transform firePoint;
        private PlayerVariables _variables;
        public Transform FirePoint => firePoint;

        private void Start()
        {
            _variables = transform.gameObject.GetComponentInParent<PlayerVariables>();
        }

        /// <summary>
        /// In Future, we may want to also pass in the firepoint position, if there are aiming issues
        /// </summary>
        /// <param name="bulletPrefab"></param>
        public void ShootBullet(GameObject bulletPrefab)
        {
            var rotation = Quaternion.Euler(transform.rotation.eulerAngles);

            // The velocity is done in the awake function on the object In the BulletScript
            var bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                rotation,
                null);

            var bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.CreateBullet(transform.forward, _variables.gameObject, _variables.Damage, _variables.ShootingSpeed);
        }
    }
}
