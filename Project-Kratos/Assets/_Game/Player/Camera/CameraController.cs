using Cinemachine;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class CameraController : NetworkBehaviour
    {
        public CinemachineVirtualCamera Camera => _camera;
        [SerializeField] private CinemachineVirtualCamera _camera;

        public override void OnNetworkSpawn()
        {
            if(!IsOwner) return;

            _camera.m_Priority = 10;
        }
    }
}
