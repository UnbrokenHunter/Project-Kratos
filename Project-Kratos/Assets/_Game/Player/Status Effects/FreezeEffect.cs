using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectKratos
{
    public class FreezeEffect : StatusEffect
    {
        [SerializeField] private float _slowAmount;
        [SerializeField] private float _slowRotationAmount;
        
        [SerializeField] private ParticleSystem _particles;
        
        protected override IEnumerator ApplyEffect()
        {
            _particles.Play();

            var hasDied = _variables.DeathCount;
            
            _variables.Speed -= _slowAmount;
            _variables.RotationSpeed -= _slowRotationAmount;
            yield return Helpers.GetWait(_duration);
            
            if (hasDied != _variables.DeathCount) yield break;
            
            _variables.RotationSpeed += _slowRotationAmount;
            _variables.Speed += _slowAmount;
        }
    }
}
