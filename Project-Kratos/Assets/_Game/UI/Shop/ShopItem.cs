using ProjectKratos.Player;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectKratos.Shop
{
    public abstract class ShopItem : MonoBehaviour
    {
        private ShopMenu _shopMenu;
        private protected PlayerVariables _variables;
        [SerializeField] private protected float _cost;

        private void Start()
        {
            _shopMenu = GetComponentInParent<ShopMenu>();
            _variables = _shopMenu.Variables;

            GetComponent<Button>().onClick.AddListener(() => TryBuyItem());
        }

        public virtual void TryBuyItem()
        {
            if (_variables.MoneyCount >= _cost)
            {
                BuyItem();
            }
        }

        // Base implementation does nothing
        private protected abstract void BuyItem(); 

    }
}
