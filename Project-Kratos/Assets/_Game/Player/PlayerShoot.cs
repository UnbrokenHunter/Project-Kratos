using ProjectKratos.Bullet;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerShoot : NetworkBehaviour
    {
        [SerializeField] private Transform _firepoint;
        [SerializeField] private GameObject _bulletPrefab;

        /// <summary>
        /// In Future, we may want to also pass in the firepoint position, if there are aiming issues
        /// </summary>
        /// <param name="rotation"></param>
        public void ShootBullet(Quaternion rotation, float damageMultiplier)
        {
            if (!IsOwner) return;

            // Fire Locally immedietly
            CreateBullet(rotation, transform.parent.GetInstanceID(), damageMultiplier);

            // Send off the call to all clients
            RequestFireServerRpc(rotation, transform.parent.GetInstanceID(), damageMultiplier);
        }

        [ServerRpc]
        private void RequestFireServerRpc(Quaternion bulletRotation, int instanceIdOfShooter, float damageMultiplier)
        {
            FireClientRpc(bulletRotation, instanceIdOfShooter, damageMultiplier);
        }

        [ClientRpc]
        private void FireClientRpc(Quaternion bulletRotation, int instanceIdOfShooter, float damageMultiplier)
        {
            if (!IsOwner) 
                CreateBullet(bulletRotation, instanceIdOfShooter, damageMultiplier);
        }


        private void CreateBullet(Quaternion bulletRotation, int instanceIdOfShooter, float damageMultiplier)
        {
            // The velocity is done in the awake function on the object In the BulletScript
            GameObject bullet = Instantiate(
                _bulletPrefab,
                _firepoint.position,
                bulletRotation,
                null);

            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.CreateBullet(transform.forward, instanceIdOfShooter, damageMultiplier);
        }
    }
}
