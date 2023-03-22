using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectKratos
{
    public class PlayerController : MonoBehaviour, IPlayer
    {

        [Title("Movement Variables")]
        [SerializeField] private float playerSpeed = 10f;
        [SerializeField] private float playerRotationSpeed = 0.15f;

        [Title("Attack Variables")]
        [SerializeField] private float playerDamage = 1f;
        [SerializeField] private float playerShootSpeed = 1f;
        [SerializeField] private Bullet bulletType;

        // Cached Variables
        private Transform playerTransform;
        [SerializeField, HideIf("@playerFirepoint != null")] private Transform playerFirepoint;

        // Input Variables
        private Vector2 playerMovementInput;
        private IPlayer.Shoot playerShoot;

        private void Awake()
        {
            playerTransform = transform;

            playerShoot += PlayerShoot;
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }


        public void MovePlayer()
        {
            Vector3 _movement = new (playerMovementInput.x, 0f, playerMovementInput.y);


            playerTransform.Translate(playerSpeed * Time.deltaTime * _movement, Space.World);

            if(_movement != Vector3.zero)
                playerTransform.rotation = Quaternion.Slerp (playerTransform.rotation, Quaternion.LookRotation(_movement), playerRotationSpeed);
        }

        public void PlayerShoot()
        {

            GameObject _bulletObject = new ("Bullet");
            _bulletObject.transform.SetPositionAndRotation(
                playerFirepoint.position,
                Quaternion.Euler(playerTransform.rotation.eulerAngles)
            );

            _bulletObject.AddComponent<MeshFilter>().mesh = bulletType.BulletMesh;
            _bulletObject.AddComponent<MeshRenderer>().material = bulletType.BulletMaterial;
            _bulletObject.AddComponent<MeshCollider>().convex = true;

            _bulletObject.AddComponent<Rigidbody>().AddForce(bulletType.BulletSpeed * _bulletObject.transform.forward, ForceMode.Impulse);
            _bulletObject.AddComponent<BulletScript>();
            
        }

        #region Input 
        public void OnMove(InputValue value) => playerMovementInput = value.Get<Vector2>();
        public void OnShoot(InputValue value) => playerShoot?.Invoke();

        #endregion
    }
}
