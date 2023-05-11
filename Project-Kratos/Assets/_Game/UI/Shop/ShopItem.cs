using DarkTonic.MasterAudio;
using ProjectKratos.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectKratos.Shop
{
    /// <summary>
    /// Inheritors have access to the PlayerVariables and the cost of the item
    /// </summary>
    public abstract class ShopItem : MonoBehaviour
    { 
        private ShopMenu _shopMenu;
        private TMP_Text _text; 
        private Image _image;
             
        private protected PlayerVariables _variables;
        [SerializeField] private protected float _cost;
        [SerializeField] private protected bool _doesIncrementCost = true;
        [SerializeField] private protected float _costIncrementMultiplier = 1.3f;
             
        public string ItemName { get => _itemName; set => _itemName = value; }
        [SerializeField] private string _itemName;

        public Sprite Sprite { get => _sprite; set => _sprite = value; }
        [SerializeField] private Sprite _sprite;

        [Space] 
        
        [SerializeField] private string _buySound = "Buy";
        [SerializeField] private string _cantBuySound = "Buy Failed";

        private float Cost;

        private void Start()
        {
            _variables = GameManager.Instance.MainPlayer;

            if (GameManager.Instance.GameMode != Constants.GameTypes.Economy) return;
            
            _text = GetComponentInChildren<TMP_Text>();
            _image = GetComponentInChildren<Image>();
            
            Cost = _cost;
            
            SetText();
            GetComponent<Button>().onClick.AddListener(TryBuyItem);

        }

        public void ResetCost()
        {
            Cost = _cost;
            
            SetText();
        }

        private void TryBuyItem()
        {
            if (!(_variables.MoneyCount >= Cost))
            {
                MasterAudio.PlaySound(_cantBuySound);
                return;
            }
             
            // The += is implied
            _variables.MoneyCount = - Cost;
            
            if (_doesIncrementCost)
                Cost *= _costIncrementMultiplier;
            
            MasterAudio.PlaySound(_buySound);
            
            BuyItem();
            SetText();
        }
 
        private void SetText()
        {
            _text.text = $"{ItemName}\n{Cost} Coins";
            _image.sprite = Sprite;
        }
         
        // Base implementation does nothing
        public abstract void BuyItem(); 


    }
}
