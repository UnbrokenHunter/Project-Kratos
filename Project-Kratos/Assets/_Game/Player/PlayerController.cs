using System;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerController : NetworkBehaviour, IPlayer
    {
        [SerializeField] private ScriptableStats _stats;
        [SerializeField] private Transform _firepoint;


        #region Internal
        private int _fixedFrame;
        private FrameInput _frameInput;
        private Vector3 _speed;
        private Vector3 _currentExternalVelocity;
        
        // Cached Variables
        private Transform _transform;
        private PlayerInput _input;
        private Rigidbody _rb;
        #endregion

        #region External
        public ScriptableStats PlayerStats => _stats;

        public Vector2 Input => _frameInput.Move;

        public Vector3 Speed => _speed;

        public virtual void ApplyVelocity(Vector3 vel, PlayerForce forceType)
        {
            if (forceType == PlayerForce.Burst) _speed += vel;
            else _currentExternalVelocity += vel;
        }

        #endregion

        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;

            // Cache Variables
            _transform = transform;
            _rb = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();

            _input.Shoot += HandleShooting;
        }

        protected virtual void Update()
        {
            if (!IsOwner) return;
            
            GatherInput();
        }
        protected virtual void GatherInput()
        {
            _frameInput = _input.FrameInput;
        }

        protected virtual void FixedUpdate()
        {
            if (!IsOwner) return;

            _fixedFrame++;
            _currentExternalVelocity = Vector2.MoveTowards(_currentExternalVelocity, Vector2.zero, _stats.ExternalVelocityDecay * Time.fixedDeltaTime);

            HandleMovement();

            ApplyVelocity();
        }



        protected virtual void HandleMovement()
        {

            Vector3 _movement = new (Input.x, 0f, Input.y);


            _speed = _stats.Speed * Time.fixedDeltaTime * _movement;

            if(_movement != Vector3.zero)
                _transform.rotation = Quaternion.Slerp (_transform.rotation, 
                    Quaternion.LookRotation(_movement), _stats.RotationSpeed);
        }

        protected virtual void HandleShooting()
        {
            if (!IsOwner) return;

            ShootClientRpc();

            GameObject _bulletObject = new ("Bullet");
            _bulletObject.transform.SetPositionAndRotation(
                _firepoint.position,
                Quaternion.Euler(_transform.rotation.eulerAngles)
            );

            _bulletObject.AddComponent<MeshFilter>().mesh = _stats.BulletType.BulletMesh;
            _bulletObject.AddComponent<MeshRenderer>().material = _stats.BulletType.BulletMaterial;
            _bulletObject.AddComponent<MeshCollider>().convex = true;

            _bulletObject.AddComponent<Rigidbody>().AddForce(_stats.BulletType.BulletSpeed * _bulletObject.transform.forward, ForceMode.Impulse);
            _bulletObject.AddComponent<BulletScript>();
            
        }

        protected virtual void ApplyVelocity()
        {
            _rb.velocity = _speed + _currentExternalVelocity;
        }


        #region Servers

        [ClientRpc]
        private void ShootClientRpc()
        {
            Debug.Log($"Shoot Server RPC: {OwnerClientId}");
        }

        #endregion

    }
    public enum PlayerForce
    {
        /// <summary>
        /// Added directly to the players movement speed, to be controlled by the standard deceleration
        /// </summary>
        Burst,

        /// <summary>
        /// An additive force handled by the decay system
        /// </summary>
        Decay
    }

}
