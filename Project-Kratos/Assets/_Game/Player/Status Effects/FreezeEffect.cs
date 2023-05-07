using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectKratos
{
    public class FreezeEffect : StatusEffect
    {
        [SerializeField] private float _slowAmount;
        
        protected override IEnumerator ApplyEffect()
        {
            _variables.Speed -= _slowAmount;
            yield return Helpers.GetWait(_duration);
            _variables.Speed += _slowAmount;
        }
    }
}
