using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectKratos
{
    public class StunEffect : StatusEffect
    {
        
        [SerializeField] private ParticleSystem _particles;
        
        protected override IEnumerator ApplyEffect()
        {
            _particles.Play();
            
            _variables.CanMove = false;
            _variables.CanShoot = false;
            yield return Helpers.GetWait(_duration);
            _variables.CanMove = true;
            _variables.CanShoot = true;
        }
    }
}
