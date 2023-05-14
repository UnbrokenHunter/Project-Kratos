using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ProjectKratos
{
    [ExecuteInEditMode]
    public class Tooltip : MonoBehaviour
    {
        public TextMeshProUGUI headerField;
        public TextMeshProUGUI contentField;
        public LayoutElement layoutElement;
        public int characterWrapLimit;
        public RectTransform rectTransform;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        
        private void Update()
        {
            if (Application.isPlaying)
            {
                var position = Mouse.current.position.ReadValue();
                
                var pivotX = position.x / Screen.width;
                var pivotY = position.y / Screen.height;
                
                rectTransform.pivot = new Vector2(pivotX, pivotY);
                transform.position = position;
            }
            
            if (!Application.isEditor) return;
            
            var headerLength = headerField.text.Length;
            var contentLength = contentField.text.Length;
            
            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }
    }
}
