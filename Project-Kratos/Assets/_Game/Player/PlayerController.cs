using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerController : MonoBehaviour
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

        private Vector2 Input => _frameInput.Move;

        public virtual void ExternalVelocity(Vector3 vel, PlayerForce forceType)
        {
            if (forceType == PlayerForce.Burst) _speed += vel;
            else _currentExternalVelocity += vel;
        }

        #endregion

        public void Start()
        {
            // Cache Variables
            _transform = transform;
            _variables = GetComponentInParent<PlayerVariables>();
            _rb = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();
            _shoot = GetComponent<PlayerShoot>();
            _interactions = GetComponent<PlayerInteractions>();

            _input.Shoot += HandleShooting;
            _input.Ability += HandleAbility;
        }

        #region Input
        protected virtual void Update()
        {
            GatherInput();
        }
        
        protected virtual void GatherInput()
        {
            _frameInput = _input.FrameInput;
        }
        #endregion

        protected virtual void FixedUpdate()
        {
            if (!_variables.CanMove) return;
            
            _currentExternalVelocity = Vector2.MoveTowards(
                _currentExternalVelocity, 
                Vector2.zero, 
                _variables.ExternalVelocityDecay * Time.fixedDeltaTime);

            HandleMovement();

            ApplyVelocity();
        }

        protected virtual void HandleAbility()
        {
            _variables.Ability.TriggerAbility();
        }
        
        /// <summary>
        /// Gets the player input, and sets the _speed variable equal to it
        /// </summary>
        protected virtual void HandleMovement()
        {
            Vector3 movement = new (Input.x, 0f, Input.y);

            
            _speed = _variables.Speed * Time.fixedDeltaTime * movement;

            if(movement != Vector3.zero)
                _transform.rotation = Quaternion.Slerp (_transform.rotation, 
                    Quaternion.LookRotation(movement), _variables.RotationSpeed);
        }

        /// <summary>
        /// Creates a bullet entirely through script, and syncs it to the server 
        /// using a ClientNetworkTransform. It also adds force to it, and sets 
        /// its direction to the player's
        /// </summary>
        protected virtual void HandleShooting()
        {
            if (!_variables.CanShoot) return;

            _shoot.ShootBullet(_variables.DefaultBullet);

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
