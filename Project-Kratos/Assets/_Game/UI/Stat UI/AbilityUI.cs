using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectKratos
{
    public class AbilityUI : MonoBehaviour
    {
        // Singleton
        public static AbilityUI Instance { get; private set; }
        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
        
        [SerializeField] private PlayerAbility _ability;
        
        [SerializeField] Image _image;
        [SerializeField] TMP_Text _text;
        
        public void SetAbility(PlayerAbility ability)
        {
            _ability = ability;
           
            _image.sprite = _ability.Icon;
            _text.text = _ability.Name;
            
        }
        
        
    }
}
