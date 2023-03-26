using Cinemachine;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class CameraController : NetworkBehaviour
    {
        [SerializeField] private Transform _objectToFollow;

        public override void OnGainedOwnership()
        {            
            Camera.main.GetComponent<CinemachineBrain>().
                ActiveVirtualCamera.Follow = _objectToFollow;
        }



    }
}
