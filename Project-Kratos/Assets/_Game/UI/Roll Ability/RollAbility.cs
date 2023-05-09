using System;
using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using ProjectKratos.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ProjectKratos
{
    public class RollAbility : MonoBehaviour
    {
        public static RollAbility Instance { get; private set; }
        
        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            
            _abilityText = GetComponentsInChildren<TMP_Text>();
            _shopItems = _shopItems[0].GetComponents<ShopItem>();
            transform.parent.gameObject.SetActive(false);
        }
        
        [SerializeField] private PlayerAbility[] _abilitys;
        [SerializeField] private ShopItem[] _shopItems;
        public PlayerVariables Player { get; set; }
        
        
        [SerializeField] private TMP_Text[] _abilityText;
        [SerializeField] private Image[] _abilityImages;

        public void EnableRoll()
        {
            transform.parent.gameObject.SetActive(true);
        }
        
        private void OnEnable()
        {
            Roll();
        }

        private void Roll()
        {
            var statIndex = Random.Range(0, 3);
        
            for (int i = 0; i < _abilityText.Length; i++) 
            {
                var roll = _abilityText[i];
                var image = _abilityImages[i];
                
                var component = roll.transform.parent.GetComponent<Button>();
                component.onClick.RemoveAllListeners();
                
                if (i == statIndex) {
                    
                    var stat = PickStat();
                    roll.text = stat.ItemName;
                    image.sprite = stat.Sprite;

                    component.onClick.AddListener(() => SelectItem(stat));
                }
                
                else 
                {
                    var ability = PickAbility();
                    roll.text = ability.Name;
                    image.sprite = ability.Icon;

                    component.onClick.AddListener(() => SelectItem(ability));
                }
            }
        }

        private PlayerAbility PickAbility()
        {
            return _abilitys[Random.Range(0, _abilitys.Length)];
        }
        
        private ShopItem PickStat() {
            return _shopItems[Random.Range(0, _shopItems.Length)];
        }
        
        private void SelectItem(PlayerAbility item)
        {
            Player.SetNewAbility(item);
            DisableRoll();
        }
        
        private void SelectItem(ShopItem item)
        {
            item.BuyItem();
            DisableRoll();
        }
        
        public void DisableRoll()
        {
            transform.parent.gameObject.SetActive(false);
        }

    }
}
