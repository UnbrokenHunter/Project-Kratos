using System;
using MoreMountains.Feedbacks;
using ProjectKratos.Player;
using ProjectKratos.Tabs;
using TMPro;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class ShopMenu : MonoBehaviour
    {
        public static ShopMenu Instance { get; private set; }
        
        [SerializeField] private TabGroup _tabGroup;
        [SerializeField] private TMP_Text _shopButtonText;

        private ShopItem[] _shopItems;

        private void Awake()
        {
            _shopItems = GetComponentsInChildren<ShopItem>();
            
            // Singleton
            if (Instance == null)
                Instance = this;
            
            else
                Destroy(gameObject);
        }

        public void ResetShop()
        {
            foreach (var item in _shopItems)
            {
                item.ResetCost();
            }
        }
        
        private void OnEnable()
        {
            
            if (GameManager.Instance.MainPlayer == null) return;

            GameManager.Instance.MainPlayer.CanMove = false;
            GameManager.Instance.MainPlayer.CanShoot = false;
            
            _shopButtonText.text = "Exit";
            
            MMTimeManager.Instance.SetTimeScaleTo(0f);
        }

        private void OnDisable()
        {
            if (GameManager.Instance.MainPlayer == null) return;

            GameManager.Instance.MainPlayer.CanMove = true;
            GameManager.Instance.MainPlayer.CanShoot = true;
            
            _shopButtonText.text = "Shop";
            
            MMTimeManager.Instance.SetTimeScaleTo(1f);
        }
    }
}
