using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectKratos.Shop
{
    public abstract class ShopItem : MonoBehaviour
    {
        private protected PlayerVariables _variables;
        [SerializeField] private protected float _cost;

        private void OnEnable()
        {
            _variables = NetworkManager.Singleton.LocalClient.PlayerObject.
                GetComponent<PlayerVariables>();

            GetComponent<Button>().onClick.AddListener(() => TryBuyItem());
        }

        private void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(() => TryBuyItem());
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
