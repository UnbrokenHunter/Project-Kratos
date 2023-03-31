using ProjectKratos.Shop;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos
{
    public class ShopSpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject _shopPrefab;
        [SerializeField] private PlayerVariables _playerVariables;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;

            GameObject shop = Instantiate(_shopPrefab);

            var shopMenu = shop.GetComponentInChildren<ShopMenu>();
            print(shopMenu + " " + _playerVariables);
            shopMenu.Variables = _playerVariables;

            shopMenu.gameObject.SetActive(false);

        }
    }
}
