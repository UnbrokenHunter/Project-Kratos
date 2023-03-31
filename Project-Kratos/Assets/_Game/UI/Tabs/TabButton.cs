using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace ProjectKratos.Tabs
{

    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        private TabGroup tabGroup;

        internal Image background;
        [SerializeField] private UnityEvent onTabSelect;
        [SerializeField] private UnityEvent onTabDeselect;

        private void Start()
        {
            tabGroup = GetComponentInParent<TabGroup>();

            background = GetComponent<Image>();
            tabGroup.Subscribe(this);
        }


        public void OnPointerClick(PointerEventData eventData) => tabGroup.OnTabSelected(this);

        public void OnPointerEnter(PointerEventData eventData) => tabGroup.OnTabEnter(this);

        public void OnPointerExit(PointerEventData eventData) => tabGroup.OnTabExit(this);

        internal virtual void Select() => onTabSelect?.Invoke();

        internal virtual void Deselect() => onTabDeselect?.Invoke();

    }
}
