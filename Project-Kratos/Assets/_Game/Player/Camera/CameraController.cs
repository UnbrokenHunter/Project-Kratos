using Cinemachine;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class CameraController : MonoBehaviour
    {
        public CinemachineVirtualCamera Camera => _camera;
        [SerializeField] private CinemachineVirtualCamera _camera;

        public void Start()
        {
            _camera.m_Priority = 10;
        }
    }
}
