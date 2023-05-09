using ProjectKratos.Player;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectKratos.Shop
{
    /// <summary>
    /// Inheritors have access to the PlayerVariables and the cost of the item
    /// </summary>
    public abstract class ShopItem : MonoBehaviour
    {        private ShopMenu _shopMenu;
             private string _itemName;
             private Text _text;
             private AdvancedShadow _shadow; 
             
             private protected PlayerVariables _variables;
             [SerializeField] private protected float _cost;
             
             public string ItemName => _itemName;
             
             private void Start()
             {
                 _shopMenu = GetComponentInParent<ShopMenu>();
                 _variables = _shopMenu.Variables;
     
                 _shadow = GetComponentInChildren<AdvancedShadow>();
                 _text = _shadow.GetComponent<Text>();
                 _itemName = _text.text;
                 
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
                 _text.text = $"{_itemName} : {_cost} Coins";
                 _shadow.DrawTextShadow();
             }
             
             // Base implementation does nothing
             public abstract void BuyItem(); 


    }
}
