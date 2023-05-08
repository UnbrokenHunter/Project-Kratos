using System;
using DarkTonic.MasterAudio;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public abstract class PlayerAbility : MonoBehaviour
    {
        public Sprite Icon => _icon;
        [SerializeField] private Sprite _icon;
        public string Name => _name;
        [SerializeField] private string _name;
        
        [SerializeField] private string _abilitySound;
        
        [SerializeField] private protected float _cooldown;
        private bool _canUseAbility = true;
        private protected PlayerVariables _variables;
        private protected PlayerShoot _shoot;
        
        private float _timer;
        
        private void Start()
        {
            _variables = GetComponentInParent<PlayerVariables>();
            _shoot = transform.parent.GetComponentInChildren<PlayerShoot>();
        }

        public void TriggerAbility()
        {
            if (!_canUseAbility) return; 
            
            _canUseAbility = false;
            
            MasterAudio.PlaySound(_abilitySound);
            Ability();
            
            if (_variables.IsBot) return;
            
            AbilityUI.Instance.ShowAbilityCooldown(_cooldown);
            
        }
        
        protected abstract void Ability();

        private void Update()
        {
            if (_canUseAbility) return;

            _timer += Time.deltaTime;

            if (!(_timer >= _cooldown)) return;
            
            _canUseAbility = true;
            _timer = 0;
        }
    }
}
