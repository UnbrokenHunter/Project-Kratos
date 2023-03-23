using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectKratos.Player
{
    public class PlayerInput : NetworkBehaviour
    {
        public FrameInput FrameInput { get; private set; }

        private Vector2 _move;
        public event Action Shoot;

        public void OnMove(InputValue value)
        {
            _move = value.Get<Vector2>();
            Gather();
        }

        public void OnShoot(InputValue value) => Shoot?.Invoke();

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
