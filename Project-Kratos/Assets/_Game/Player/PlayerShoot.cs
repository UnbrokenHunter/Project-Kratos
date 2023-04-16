using ProjectKratos.Bullet;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerShoot : NetworkBehaviour
    {
        [SerializeField] private Transform _firepoint;
        [SerializeField] private GameObject _bulletPrefab;
        private PlayerVariables _shooterObject;

        public Transform Firepoint => _firepoint;

        public override void OnNetworkSpawn()
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
            if (!(isBot && (IsServer || IsHost)))
            {
                if (!IsOwner) return;
            }

            // Spawn Bullet 
            CreateBulletServerRpc(rotation, _shooterObject, damageMultiplier, speedMultipler);
        }

        [ServerRpc(RequireOwnership = false)]
        private void CreateBulletServerRpc(Quaternion bulletRotation, NetworkBehaviourReference networkOfShooter, float damageMultiplier, float speedMultipler)
        {
            // !isOwner is called in the ClientRpc
            
            NetworkBehaviour shooterObject = networkOfShooter;
            
            // The velocity is done in the awake function on the object In the BulletScript
            var bullet = Instantiate(
                _bulletPrefab,
                _firepoint.position,
                bulletRotation,
                null);

            var bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.CreateBullet(transform.forward, shooterObject.gameObject, damageMultiplier, speedMultipler);
            
            bullet.GetComponent<NetworkObject>().Spawn(true);
        }
    }
}
