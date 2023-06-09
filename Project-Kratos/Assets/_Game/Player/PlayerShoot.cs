using System;
using ProjectKratos.Bullet;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerShoot : MonoBehaviour 
    {
        [SerializeField] private Transform firePoint;
        private PlayerVariables _variables;
        public Transform FirePoint => firePoint;
        [SerializeField] private float _rotationOffset = 180;

<<<<<<< HEAD
        private Quaternion _bulletRotation;

        /// <summary>
        /// In Future, we may want to also pass in the firepoint position, if there are aiming issues
        /// </summary>
        /// <param name="rotation"></param>
        public void ShootBullet(Quaternion rotation)
        {
            _bulletRotation = rotation;

            RequestFireServerRpc();
        }

        [ServerRpc]
        private void RequestFireServerRpc()
        {
            FireClientRpc();
        }

        [ClientRpc]
        private void FireClientRpc()
        {
            CreateBullet();
=======
        private void Start()
        {
            _variables = transform.gameObject.GetComponentInParent<PlayerVariables>();
        }

        public Quaternion GetBulletRotation()
        {
            // Rotation of the bullet
            var rotation = Quaternion.Euler(transform.rotation.eulerAngles);

            if (_variables.IsBot) return rotation;
            
            var barrel = FirePoint.parent.rotation;
            rotation = Quaternion.Euler(new Vector3(0, barrel.eulerAngles.y + _rotationOffset, 0));

            return rotation;
>>>>>>> add-death
        }
        
        public Vector3 GetBulletDirection()
        {
            // Velocity direction wof the bullet
            var forwardDir = transform.forward;

<<<<<<< HEAD
        private void CreateBullet()
        {
            // The velocity is done in the awake function on the object In the BulletScript
            GameObject bullet = Instantiate(
                _bulletPrefab,
                _firepoint.position,
                _bulletRotation,
=======
            if (_variables.IsBot) return forwardDir;
            
            // Calculate forward direction in terms of x and z axes
            var angle = GetBulletRotation().eulerAngles.y * Mathf.Deg2Rad;
            forwardDir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

            return forwardDir;
        }
        
        /// <summary>
        /// In Future, we may want to also pass in the firepoint position, if there are aiming issues
        /// </summary>
        /// <param name="bulletPrefab"></param>
        public void ShootBullet(GameObject bulletPrefab)
        {
            
            // The velocity is done in the awake function on the object In the BulletScript
            var bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                GetBulletRotation(),
>>>>>>> add-death
                null);
            

<<<<<<< HEAD
            bullet.GetComponent<BulletScript>().Direction = transform.forward;

=======
            var bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.CreateBullet(GetBulletDirection(), _variables.gameObject, _variables.Damage, _variables.ShootingSpeed);
>>>>>>> add-death
        }
    }
}
