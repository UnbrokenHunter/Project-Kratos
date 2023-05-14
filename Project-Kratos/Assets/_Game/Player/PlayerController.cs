<<<<<<< HEAD
using ProjectKratos.Bullet;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectKratos.Player
{
    public class PlayerController : NetworkBehaviour, IPlayer
=======
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectKratos.Player
{
    public class PlayerController : MonoBehaviour
>>>>>>> add-death
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
        [SerializeField] private GameObject _turretGameObject;
        private Camera _camera;

        #endregion

        #region External
        public ScriptableStats PlayerStats => _stats;

        private Vector2 Input => _frameInput.Move;

        public virtual void ExternalVelocity(Vector3 vel, PlayerForce forceType)
        {
            if (forceType == PlayerForce.Burst) _speed += vel;
            else _currentExternalVelocity += vel;
        }

        #endregion

        public void Start()
        {
            _camera = Camera.main;
            // Cache Variables
            _transform = transform;
            _rb = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();
            _shoot = GetComponent<PlayerShoot>();

            _input.Shoot += HandleShooting;
            _input.Ability += HandleAbility;
        }

        protected virtual void Update()
        {
            GatherInput();
        }
        
        protected virtual void GatherInput()
        {
            _frameInput = _input.FrameInput;
        }

        protected virtual void FixedUpdate()
        {
<<<<<<< HEAD
            if (!IsOwner) return;

            _fixedFrame++;
            _currentExternalVelocity = Vector2.MoveTowards(_currentExternalVelocity, Vector2.zero, _stats.ExternalVelocityDecay * Time.fixedDeltaTime);
=======
            if (!_variables.CanMove) return;
            
            _currentExternalVelocity = Vector2.MoveTowards(
                _currentExternalVelocity, 
                Vector2.zero, 
                _variables.ExternalVelocityDecay * Time.fixedDeltaTime);
>>>>>>> add-death

            HandleMovement();

            ApplyVelocity();
        }

<<<<<<< HEAD

=======
        protected virtual void HandleAbility()
        {
            if(_variables.Ability == null) return;
            
            _variables.Ability.TriggerAbility();
        }
        
>>>>>>> add-death
        /// <summary>
        /// Gets the player input, and sets the _speed variable equal to it
        /// </summary>
        protected virtual void HandleMovement()
        {
<<<<<<< HEAD
=======
            Vector3 movement = new (Input.x, 0f, Input.y);
>>>>>>> add-death

            _speed = _variables.Speed * Time.fixedDeltaTime * movement;

<<<<<<< HEAD

            _speed = _stats.Speed * Time.fixedDeltaTime * _movement;

            if(_movement != Vector3.zero)
                _transform.rotation = Quaternion.Slerp (_transform.rotation, 
                    Quaternion.LookRotation(_movement), _stats.RotationSpeed);
=======
            
            // Rotates the base
            if(movement != Vector3.zero)
                _transform.rotation = Quaternion.Slerp (_transform.rotation, 
                    Quaternion.LookRotation(movement), _variables.RotationSpeed);
            
            
            HandleTurretRotation();
        }

        RaycastHit hit;
        protected virtual void HandleTurretRotation()
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out hit, 50000f))
            {
                var rotation = Quaternion.LookRotation(hit.point - _turretGameObject.transform.position).eulerAngles;
                
                rotation -= _transform.rotation.eulerAngles;
                
                var realRotation = Quaternion.Euler(-90f, rotation.y, -180f);

                var slerp = Quaternion.Slerp (_turretGameObject.transform.localRotation, 
                    realRotation, _variables.RotationSpeed);
                
                _turretGameObject.transform.localRotation = slerp;
            }
>>>>>>> add-death
        }

        /// <summary>
        /// Creates a bullet entirely through script, and syncs it to the server 
        /// using a ClientNetworkTransform. It also adds force to it, and sets 
        /// its direction to the player's
        /// </summary>
        protected virtual void HandleShooting()
        {
<<<<<<< HEAD
            if (!IsOwner) return;

            var rotation = Quaternion.Euler(transform.rotation.eulerAngles);
            _shoot.ShootBullet(rotation);
=======
            if (!_variables.CanShoot) return;

            _shoot.ShootBullet(_variables.DefaultBullet);
>>>>>>> add-death

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
