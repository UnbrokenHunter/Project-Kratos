using ProjectKratos.Player;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectKratos.Shop
{
    /// <summary>
    /// Inheritors have access to the PlayerVariables and the cost of the item
    /// </summary>
    public abstract class ShopItem : MonoBehaviour
    {       private ShopMenu _shopMenu;
            private Text _text; 
            private AdvancedShadow _shadow; 
             
            private protected PlayerVariables _variables;
            [SerializeField] private protected float _cost;
             
            public string ItemName { get => _itemName; set => _itemName = value; }
            [SerializeField] private string _itemName;
            
            public Sprite Sprite { get => _sprite; set => _sprite = value; }
            [SerializeField] private Sprite _sprite;

            private void Start()
            {
                _variables = GameManager.Instance.MainPlayer;

                if (GameManager.Instance.GameMode != Constants.GameTypes.Economy) return;
                
                _shopMenu = GetComponentInParent<ShopMenu>();
                _shadow = GetComponentInChildren<AdvancedShadow>();
                _text = _shadow.GetComponent<Text>();
                ItemName = _text.text;
                    
                SetText();
                GetComponent<Button>().onClick.AddListener(TryBuyItem);

            }

            private void TryBuyItem()
            {
                if (!(_variables.MoneyCount >= _cost)) return;
                 
                // The += is implied
                _variables.MoneyCount = -_cost;
                 
                BuyItem();
                SetText();
            }
     
            private void SetText()
            {
                _text.text = $"{ItemName} : {_cost} Coins";
                _shadow.DrawTextShadow();
            }
             
            // Base implementation does nothing
            public abstract void BuyItem(); 


    }
}
