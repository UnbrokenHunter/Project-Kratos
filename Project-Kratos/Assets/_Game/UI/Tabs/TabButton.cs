using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace ProjectKratos.Tabs
{
    [RequireComponent(typeof(RawImage))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        private TabGroup tabGroup;

        internal RawImage background;

        public Color NormalColor { get => normalColor; private set => normalColor = value; }
        private Color normalColor;

        [SerializeField] private UnityEvent onTabSelect;
        [SerializeField] private UnityEvent onTabDeselect;


        private void Awake()
        {
            tabGroup = GetComponentInParent<TabGroup>();

            background = GetComponent<RawImage>();

            normalColor = background.color;

            tabGroup.Subscribe(this);
        }


        public void OnPointerClick(PointerEventData eventData) => tabGroup.OnTabSelected(this);

        public void OnPointerEnter(PointerEventData eventData)
        {
            tabGroup.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData) => tabGroup.OnTabExit(this);

        internal virtual void Select() => onTabSelect?.Invoke();

        internal virtual void Deselect() => onTabDeselect?.Invoke();

    }
}
