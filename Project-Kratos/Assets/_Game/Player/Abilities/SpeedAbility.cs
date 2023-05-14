using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectKratos
{
    public class SpeedAbility : PlayerAbility
    {
        [SerializeField] private float _speedBonus = 100;
        [SerializeField] private float _duration = 5f;
        [SerializeField] private TrailRenderer _speedTrail;
        
        protected override void Ability()
        { 
           StartCoroutine(SpeedUp());
        }
        
        private IEnumerator SpeedUp()
        {
            EnableSpeed();
            yield return Helpers.GetWait(_duration);
        }

        private void EnableSpeed()
        {
            _speedTrail.enabled = true;
            _variables.Speed += _speedBonus;
        }
        
        private void DisableSpeed()
        {
            _variables.Speed -= _speedBonus;
            _speedTrail.enabled = false;
        }
    }
}
