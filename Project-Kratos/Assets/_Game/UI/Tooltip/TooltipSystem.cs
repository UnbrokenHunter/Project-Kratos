using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectKratos
{
    public class TooltipSystem : MonoBehaviour
    {
        private static TooltipSystem _current;
        
        public Tooltip tooltip;
        
        private void Awake()
        {
            if (_current == null)
            {
                _current = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
        
        public static void Show(string header, string content)
        {
            _current.tooltip.gameObject.SetActive(true);
            
            _current.tooltip.headerField.gameObject.SetActive(!string.IsNullOrEmpty(header));
            _current.tooltip.headerField.text = header;
            
            _current.tooltip.contentField.gameObject.SetActive(!string.IsNullOrEmpty(content));
            _current.tooltip.contentField.text = content;
        }
        
        public static void Hide()
        {
            _current.tooltip.gameObject.SetActive(false);
        }
    }
}
