using System;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public abstract class PlayerAbility : MonoBehaviour
    {
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
            
            print("Ability Triggered");
            Ability();
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
