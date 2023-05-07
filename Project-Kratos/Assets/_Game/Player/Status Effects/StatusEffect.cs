using System;
using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public abstract class StatusEffect : MonoBehaviour
    {
        protected PlayerVariables _variables;
        
        [SerializeField] protected float _duration;

        private void Awake()
        {
            _variables = GetComponentInParent<PlayerVariables>();
        }

        public void ApplyStatusEffect()
        {
            StartCoroutine(ApplyEffect());
        }
        
        protected abstract IEnumerator ApplyEffect();
    }
}
