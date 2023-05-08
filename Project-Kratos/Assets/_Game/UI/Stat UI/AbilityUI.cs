using System.Collections;
using System.Collections.Generic;
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
            if (ability == null)
            {
                gameObject.SetActive(false);
                return;
            }
            
            gameObject.SetActive(true);
            
            _ability = ability;
           
            _image.sprite = _ability.Icon;
            _text.text = _ability.Name;
            
        }
        
        public void ShowAbilityCooldown(float cooldown)
        {
            StartCoroutine(AbilityCooldown(cooldown));
        }

        private IEnumerator AbilityCooldown(float cooldown)
        {
            _image.type = Image.Type.Filled;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.5f);

            var timer = 0f;
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

            while (timer < cooldown)
            {
                timer += Time.deltaTime;
                _image.fillAmount = timer / cooldown;
                
                yield return waitForEndOfFrame;
            }
    
            // Ensure the fill amount is set to 1 when the cooldown is over
            _image.fillAmount = 1;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
        }


    }
}
