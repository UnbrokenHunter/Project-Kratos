using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectKratos
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private static LTDescr delay;
     
        [SerializeField] private string _header;
        
        [TextArea]
        [SerializeField] private string _content;

        public void SetToolTip(string header, string content)
        {
            _header = header;
            _content = content;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Show();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Hide();
        }

        public void Hide()
        {
            if (delay != null)
                LeanTween.cancel(delay.uniqueId);
            
            TooltipSystem.Hide();
        }
        
        public void Show()
        {
            delay = LeanTween.delayedCall(0.5f, () => 
            {
                TooltipSystem.Show(_header, _content);
            });
            delay.setIgnoreTimeScale(true);
        }
    }
}
