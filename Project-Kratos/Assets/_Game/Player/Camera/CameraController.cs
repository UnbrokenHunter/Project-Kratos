using Cinemachine;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class CameraController : NetworkBehaviour
    {
        [SerializeField] private Transform _objectToFollow;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;

            Camera.main.GetComponent<CinemachineBrain>().
                ActiveVirtualCamera.Follow = _objectToFollow;
        }



    }
}
