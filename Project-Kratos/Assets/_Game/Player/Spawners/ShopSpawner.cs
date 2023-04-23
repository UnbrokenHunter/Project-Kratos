using ProjectKratos.Player;
using ProjectKratos.Shop;
using UnityEngine;

namespace ProjectKratos
{
    public class ShopSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _shopPrefab;
        [SerializeField] private PlayerVariables _playerVariables;

        private GameObject _shop;
        
        private void Start()
        {
            _shop = Instantiate(_shopPrefab);

            var shopMenu = _shop.GetComponentInChildren<ShopMenu>();
            shopMenu.Variables = _playerVariables;

            shopMenu.gameObject.SetActive(false);

        }

        public void DestroyShop()
        {
            Destroy(_shop);
        }
    }
}
