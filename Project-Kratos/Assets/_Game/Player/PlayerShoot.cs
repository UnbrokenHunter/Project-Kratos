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
        public void ShootBullet(Quaternion rotation, float damageMultiplier, float speedMultipler)
        {
            if (!IsOwner) return;

            // Fire Locally immediately
            var shooterObject = transform.root.gameObject.GetComponent<PlayerVariables>();
            CreateBullet(rotation, shooterObject, damageMultiplier, speedMultipler);

            // Send off the call to all clients
            RequestFireServerRpc(rotation, shooterObject, damageMultiplier, speedMultipler);
        }

        [ServerRpc]
        private void RequestFireServerRpc(Quaternion bulletRotation, NetworkBehaviourReference referenceToShooter, float damageMultiplier, float speedMultipler)
        {
            FireClientRpc(bulletRotation, referenceToShooter, damageMultiplier, speedMultipler);
        } 

        [ClientRpc]
        private void FireClientRpc(Quaternion bulletRotation, NetworkBehaviourReference referenceToShooter, float damageMultiplier, float speedMultipler)
        {
            if (!IsOwner) 
                CreateBullet(bulletRotation, referenceToShooter, damageMultiplier, speedMultipler);
        }

        private void CreateBullet(Quaternion bulletRotation, NetworkBehaviour networkOfShooter, float damageMultiplier, float speedMultipler)
        {
            // !isOwner is called in the ClientRpc
            
            // The velocity is done in the awake function on the object In the BulletScript
            GameObject bullet = Instantiate(
                _bulletPrefab,
                _firepoint.position,
                bulletRotation,
                null);

            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.CreateBullet(transform.forward, networkOfShooter.gameObject, damageMultiplier, speedMultipler);
        }
    }
}
