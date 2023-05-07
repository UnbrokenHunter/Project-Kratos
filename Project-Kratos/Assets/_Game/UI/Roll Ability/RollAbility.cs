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
        }
        
        [SerializeField] private AbilityCollectable[] _abilitys;
        public PlayerVariables Player { get; set; }
        
        
        [SerializeField] private TMP_Text[] _abilityText;

        private void Start()
        {
            _abilityText = GetComponentsInChildren<TMP_Text>();
            transform.parent.gameObject.SetActive(false);
        }

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
            foreach (var roll in _abilityText)
            {
                var ability = PickAbility();
                roll.text = ability.AbilityName;

                var component = roll.transform.parent.GetComponent<Button>();
                component.onClick.RemoveAllListeners();
                component.onClick.AddListener(() => SelectItem(ability));
                
            }
        }

        private AbilityCollectable PickAbility()
        {
            return _abilitys[Random.Range(0, _abilitys.Length)];
        }
        
        private void SelectItem(AbilityCollectable item)
        {
            item.ItemCollected(Player, null);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
