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
        
        [Space]
        
        [SerializeField] private int _maxBuyCount = 0;
        private int _buyCounter = 0;
        private bool _cantBuy = false;
        
        [Space]
        
        [SerializeField] private string _itemName;
        public string ItemName { get => _itemName; set => _itemName = value; }

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
            _buyCounter = 0;
            
            SetText();
        }
        
        private void MaxLevel()
        {
            _cantBuy = true;
            
            _text.text = $"{ItemName}\nMAX";
            
            MasterAudio.PlaySound(_cantBuySound);
        }

        private void TryBuyItem()
        {
            if (!(_variables.MoneyCount >= Cost))
            {
                MasterAudio.PlaySound(_cantBuySound);
                return;
            }

            if (_maxBuyCount > 0 && _buyCounter >= _maxBuyCount)
            {
                MaxLevel();
                return;
            }
            
            _buyCounter++;
            
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
            if (_cantBuy) return;
            
            _text.text = $"{ItemName}\n{Cost:N0} Coins";
            _image.sprite = Sprite;
        }
         
        // Base implementation does nothing
        public abstract void BuyItem(); 
        
    }
}
