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
        [SerializeField] Image _backgroundImage;
        [SerializeField] TMP_Text _text;
        [SerializeField] TMP_Text _cooldownText;
        
        [Space]
        
        [SerializeField] private float _alpha = 0.5f;
        
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
            
            ResetAbilityCooldown();
            
        }
        
        public void ShowAbilityCooldown(float cooldown)
        {
            StartCoroutine(AbilityCooldown(cooldown));
        }

        private void ResetAbilityCooldown()
        {
            // Ensure the fill amount is set to 1 when the cooldown is over
            _image.fillAmount = 1;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
            
            _backgroundImage.enabled = false;
            _cooldownText.enabled = false;
        }
        
        private IEnumerator AbilityCooldown(float cooldown)
        {
            _image.type = Image.Type.Filled;
            var imageColor = new Color(_image.color.r, _image.color.g, _image.color.b, _alpha);
            _image.color = imageColor;
            
            _backgroundImage.enabled = true;
            _cooldownText.enabled = true;
            
            var timer = 0f;
            var waitForEndOfFrame = new WaitForEndOfFrame();

            while (timer < cooldown)
            {
                timer += Time.deltaTime;
                var backgroundImageFillAmount = timer / cooldown;
                _image.fillAmount = backgroundImageFillAmount;
                _backgroundImage.fillAmount = backgroundImageFillAmount;
                
                _cooldownText.text = Mathf.CeilToInt(cooldown - timer).ToString();
                
                yield return waitForEndOfFrame;
            }
    
            ResetAbilityCooldown();
        }
    }
}
