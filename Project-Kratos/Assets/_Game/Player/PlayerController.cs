using ProjectKratos.Bullet;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectKratos.Player
{
    public class PlayerController : NetworkBehaviour, IPlayer
    {
        [SerializeField] private ScriptableStats _stats;

        #region Internal
        private int _fixedFrame;
        private FrameInput _frameInput;
        private Vector3 _speed;
        private Vector3 _currentExternalVelocity;
        
        // Cached Variables
        private Transform _transform;
        private PlayerShoot _shoot;
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
            _shoot = GetComponent<PlayerShoot>();

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


        /// <summary>
        /// Gets the player input, and sets the _speed variable equal to it
        /// </summary>
        protected virtual void HandleMovement()
        {

            Vector3 _movement = new (Input.x, 0f, Input.y);


            _speed = _stats.Speed * Time.fixedDeltaTime * _movement;

            if(_movement != Vector3.zero)
                _transform.rotation = Quaternion.Slerp (_transform.rotation, 
                    Quaternion.LookRotation(_movement), _stats.RotationSpeed);
        }

        /// <summary>
        /// Creates a bullet entirly through script, and syncs it to the server 
        /// using a ClientNetworkTransform. It also adds force to it, and sets 
        /// its direction to the player's
        /// </summary>
        protected virtual void HandleShooting()
        {
            if (!IsOwner) return;

            var rotation = Quaternion.Euler(transform.rotation.eulerAngles);
            _shoot.ShootBullet(rotation);

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
