using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectKratos.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public FrameInput FrameInput { get; private set; }

        private Vector2 _move;
        public event Action Shoot;
        public event Action Ability;

        public void OnMove(InputValue value)
        {
            _move = value.Get<Vector2>();
            Gather();
        }

        
        public void OnShoot(InputValue value) => Shoot?.Invoke();

        public void OnAbility(InputValue value) => Ability?.Invoke();
        
        
        private void Gather()
        {
            FrameInput = new FrameInput
            {
                Move = _move,
            };
        }
    }
    public struct FrameInput
    {
        public Vector2 Move;
    }

}
