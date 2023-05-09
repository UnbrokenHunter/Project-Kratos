using System;
using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
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
            transform.parent.gameObject.SetActive(false);
        }
        
        [SerializeField] private AbilityCollectable[] _abilitys;
        [SerializeField] private ShopItem[] _shopItems;
        public PlayerVariables Player { get; set; }
        
        
        [SerializeField] private TMP_Text[] _abilityText;


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
            int statIndex = Random.Range(0, 3);
        
            for (int i = 0; i < _abilityText.Length; i++) 
            {
                var roll = _abilityText[i];
                var component = roll.transform.parent.GetComponent<Button>();
                component.onClick.RemoveAllListeners();
                
                if (i == statIndex) {
                    
                    var stat = PickStat();
                    roll.text = stat.ItemName; // TODO Add a Getter
                    
                    component.onClick.AddListener(() => stat.BuyItem()); // TODO Make Public
                }
                
                else 
                {
                    var ability = PickAbility();
                    roll.text = ability.AbilityName;

                    component.onClick.AddListener(() => SelectItem(ability));
                }
            }
        }

        private AbilityCollectable PickAbility()
        {
            return _abilitys[Random.Range(0, _abilitys.Length)];
        }
        
        private ShopItem PickStat() {
            return _shopItems[Random.Range(0, _shopItems.Length)];
        }
        
        private void SelectItem(AbilityCollectable item)
        {
            item.ItemCollected(Player, null);
            DisableRoll();
        }
        
        public void DisableRoll()
        {
            transform.parent.gameObject.SetActive(false);
        }

    }
}
