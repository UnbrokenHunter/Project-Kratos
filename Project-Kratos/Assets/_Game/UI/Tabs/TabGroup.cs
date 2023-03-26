using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectKratos.Tabs
{
    public class TabGroup : MonoBehaviour
    {
        [Title("Sprites")]
        [SerializeField] private Sprite tabIdle;
        [SerializeField] private Sprite tabHover;
        [SerializeField] private Sprite tabActive;

        [Title("Debug")]
        [ReadOnly, SerializeField] private List<TabButton> tabButtons;
        [ReadOnly, SerializeField] private TabButton selectedTab;

        [Title("Default Behavior")]
        [SerializeField] private List<GameObject> objectsToSwap;

        private void Start() => tabButtons = GetComponentsInChildren<TabButton>().ToList();

        public void Subscribe(TabButton button)
        {
            tabButtons ??= new List<TabButton>
            {
                button
            };
        }

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();
            if(selectedTab != null || button != selectedTab)
                button.background.sprite = tabHover;
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();

        }

        public void OnTabSelected(TabButton button)
        {
            if(selectedTab != null)
            {
                selectedTab.Deselect();
            }

            selectedTab = button;
            selectedTab.Select();

            ResetTabs();
            button.background.sprite = tabActive;

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

                tab.background.sprite = tabIdle;
            }
        }

    }
}
