using ProjectKratos.Player;
using ProjectKratos.Shop;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos
{
    public class ShopSpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject _shopPrefab;
        [SerializeField] private PlayerVariables _playerVariables;

        private GameObject _shop;
        
        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;

            _shop = Instantiate(_shopPrefab);

            var shopMenu = _shop.GetComponentInChildren<ShopMenu>();
            shopMenu.Variables = _playerVariables;

            shopMenu.gameObject.SetActive(false);

        }

        public void DestroyShop()
        {
            if (!IsOwner) return;
            
            Destroy(_shop);
        }
    }
}
