using ProjectKratos.Bullet;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerShoot : NetworkBehaviour
    {
        [SerializeField] private Transform _firepoint;
        [SerializeField] private GameObject _bulletPrefab;

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
        }

        private void CreateBullet()
        {
            // The velocity is done in the awake function on the object In the BulletScript
            GameObject bullet = Instantiate(
                _bulletPrefab,
                _firepoint.position,
                _bulletRotation,
                null);

            bullet.GetComponent<BulletScript>().Direction = transform.forward;

        }
    }
}
