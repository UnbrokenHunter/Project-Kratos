using Cinemachine;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        public CinemachineVirtualCamera Camera => _camera;

        public void Start()
        {
            _camera.m_Priority = 10;
        }
    }
}