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


        private void Start() => tabButtons = GetComponentsInChildren<TabButton>().ToList();

        public void Subscribe(TabButton button) => tabButtons ??= new List<TabButton> { button };

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();

            if (selectedTab != null || button != selectedTab)
            {
                if (selectType == SelectType.Sprite)
                    button.background.texture = tabHover;

                else if (selectType == SelectType.Tint)
                    button.background.color = tabHighlight;
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

            if (selectType == SelectType.Sprite)
                button.background.texture = tabActive;

            else if (selectType == SelectType.Tint)
                button.background.color = tabPressed;


            int index = button.transform.GetSiblingIndex();
            for(int i = 0; i < objectsToSwap.Count; i++)
            {
                if(i == index)
                {
                    objectsToSwap[i].SetActive(true);
                }
                else
                {
                    objectsToSwap[i].SetActive(false);
                }
            }

        }

        public void ResetTabs()
        {
            foreach (var tab in tabButtons)
            {
                if (selectedTab != null && tab == selectedTab) continue;

                if (selectType == SelectType.Sprite)
                    tab.background.texture = tabIdle;

                else if (selectType == SelectType.Tint) 
                    tab.background.color = tab.NormalColor;
            }
        }


        protected enum SelectType {  Tint, Sprite, }
    }
}
