using ProjectKratos.Bullet;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerShoot : NetworkBehaviour
    {
        [SerializeField] private Transform _firepoint;
        [SerializeField] private GameObject _bulletPrefab;

        [ServerRpc]
        public void RequestFireServerRpc()
        {
            FireClientRpc();
        }

        [ClientRpc]
        private void FireClientRpc()
        {
            ShootBullet();
        }

        private void ShootBullet()
        {
            // The velocity is done in the awake function on the object In the BulletScript
            Instantiate(
                _bulletPrefab,
                _firepoint.position,
                Quaternion.Euler(transform.rotation.eulerAngles),
                null);

        }
    }
}
