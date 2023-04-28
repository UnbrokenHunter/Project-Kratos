using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectKratos
{
    public class SpeedAbility : PlayerAbility
    {
        [SerializeField] private float _speedBonus = 100;
        [SerializeField] private float _duration = 5f;

        protected override void Ability()
        { 
           StartCoroutine(SpeedUp());
        }
        
        private IEnumerator SpeedUp()
        {
            _variables.Speed += _speedBonus;
            yield return Helpers.GetWait(_duration);
            _variables.Speed -= _speedBonus;
        }
    }
}
