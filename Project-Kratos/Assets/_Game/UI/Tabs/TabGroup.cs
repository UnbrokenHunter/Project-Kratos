using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectKratos.Tabs
{
    public class TabGroup : MonoBehaviour
    {
        [Title("Sprites")]
        [SerializeField] private SelectType selectType = SelectType.Tint;

        [SerializeField, ShowIf("@selectType == SelectType.Sprite")] private Texture2D tabIdle;
        [SerializeField, ShowIf("@selectType == SelectType.Sprite")] private Texture2D tabHover;
        [SerializeField, ShowIf("@selectType == SelectType.Sprite")] private Texture2D tabActive;

        [SerializeField, ShowIf("@selectType == SelectType.Tint")] private Color tabHighlight;
        [SerializeField, ShowIf("@selectType == SelectType.Tint")] private Color tabPressed;

        [Title("Debug")]
        [ReadOnly, SerializeField] private List<TabButton> tabButtons;
        [ReadOnly, SerializeField] private TabButton selectedTab;

        [Title("Default Behavior")]
        [SerializeField] private List<GameObject> objectsToSwap;

        private void Start()
        {
            GetTabButtons();
            
            OnTabSelected(tabButtons.FirstOrDefault());
        }
        
        private void GetTabButtons()
        {
            tabButtons = GetComponentsInChildren<TabButton>().ToList();
        }

        public void Subscribe(TabButton button) => tabButtons ??= new List<TabButton> { button };

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();

            if (selectedTab == null && button == selectedTab) return;
            
            switch (selectType)
            {
                case SelectType.Sprite:
                    button.background.texture = tabHover;
                    break;
                
                case SelectType.Tint:
                    button.background.color = tabHighlight;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnTabExit(TabButton button) => ResetTabs();

        public void OnTabSelected(TabButton button)
        {
            if(selectedTab != null)
            {
                selectedTab.Deselect();
            }

            selectedTab = button;
            selectedTab.Select();

            ResetTabs();

            switch (selectType)
            {
                case SelectType.Sprite:
                    button.background.texture = tabActive;
                    break;
                
                case SelectType.Tint:
                    button.background.color = tabPressed;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }


            var index = button.transform.GetSiblingIndex();
            for(var i = 0; i < objectsToSwap.Count; i++)
            {
                objectsToSwap[i].SetActive(i == index);
            }

        }

        private void ResetTabs()
        {
            foreach (var tab in tabButtons.Where(tab => selectedTab == null || tab != selectedTab))
            {
                switch (selectType)
                {
                    case SelectType.Sprite:
                        tab.background.texture = tabIdle;
                        break;
                    
                    case SelectType.Tint:
                        tab.background.color = tab.NormalColor;
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        private enum SelectType {  Tint, Sprite, }
    }
}
