using ProjectKratos.Player;
using ProjectKratos.Shop;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectKratos
{
    public class ShopSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _shopPrefab;
        [SerializeField] private PlayerVariables _variables;

        private GameObject _shop;
        
        private void Start()
        {
            if (!_variables.HasShop) return;
            
            _shop = Instantiate(_shopPrefab);

            var shopMenu = _shop.GetComponentInChildren<ShopMenu>();
            shopMenu.Variables = _variables;

            shopMenu.gameObject.SetActive(false);

        }

        public void DestroyShop()
        {
            Destroy(_shop);
        }
    }
}
