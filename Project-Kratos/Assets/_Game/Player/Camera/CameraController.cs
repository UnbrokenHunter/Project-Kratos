using Cinemachine;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class CameraController : NetworkBehaviour
    {
        [SerializeField] private Transform _objectToFollow;
        [SerializeField] private CinemachineVirtualCamera _camera;

        public override void OnNetworkSpawn()
        {
            if(!IsOwner) return;

            _camera.m_Priority = 10;
            print(_camera.m_Priority);
        }
    }
}
