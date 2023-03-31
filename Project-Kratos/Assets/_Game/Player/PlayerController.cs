using Cinemachine;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerController : NetworkBehaviour
    {
        #region Internal
        private FrameInput _frameInput;
        private Vector3 _speed;
        private Vector3 _currentExternalVelocity;
        
        // Cached Variables
        private Transform _transform;
        private PlayerVariables _variables;
        private PlayerShoot _shoot;
        private PlayerInteractions _interactions;
        private PlayerInput _input;
        private Rigidbody _rb;
        #endregion

        #region External

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
            _variables = GetComponentInParent<PlayerVariables>();
            _rb = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();
            _shoot = GetComponent<PlayerShoot>();
            _interactions = GetComponent<PlayerInteractions>();

            _input.Shoot += HandleShooting;
        }

        #region Input
        protected virtual void Update()
        {
            if (!IsOwner) return;
            
            GatherInput();
        }
        
        protected virtual void GatherInput()
        {
            _frameInput = _input.FrameInput;
        }
        #endregion

        protected virtual void FixedUpdate()
        {
            if (!IsOwner) return;

            _currentExternalVelocity = Vector2.MoveTowards(
                _currentExternalVelocity, 
                Vector2.zero, 
                _variables.Stats.ExternalVelocityDecay * Time.fixedDeltaTime);

            HandleMovement();

            ApplyVelocity();
        }

        /// <summary>
        /// Gets the player input, and sets the _speed variable equal to it
        /// </summary>
        protected virtual void HandleMovement()
        {
            if (!_variables.Stats.CanMove) return;

            Vector3 _movement = new (Input.x, 0f, Input.y);


            _speed = _variables.Stats.Speed * Time.fixedDeltaTime * _movement;

            if(_movement != Vector3.zero)
                _transform.rotation = Quaternion.Slerp (_transform.rotation, 
                    Quaternion.LookRotation(_movement), _variables.Stats.RotationSpeed);
        }

        /// <summary>
        /// Creates a bullet entirly through script, and syncs it to the server 
        /// using a ClientNetworkTransform. It also adds force to it, and sets 
        /// its direction to the player's
        /// </summary>
        protected virtual void HandleShooting()
        {
            if (!IsOwner) return;
            if (!_variables.Stats.CanShoot) return;

            var rotation = Quaternion.Euler(transform.rotation.eulerAngles);
            _shoot.ShootBullet(rotation, _variables.Stats.Damage);

        }

        protected virtual void ApplyVelocity()
        {
            _rb.velocity = _speed + _currentExternalVelocity;
        }

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
