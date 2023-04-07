using Cinemachine;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class SightItem : ShopItem
    {
        [SerializeField] private float _sightAmount;

        private protected override void BuyItem()
        {
            _variables.GetComponentInChildren<CameraController>().Camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance += _sightAmount;
        }
    }
}
