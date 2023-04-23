using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectKratos
{
    public class ExplosiveAbility : PlayerAbility
    {
        [SerializeField] private GameObject _bombPrefab;
        
        public override void TriggerAbility()
        {
            Instantiate(_bombPrefab, _shoot.Firepoint.position, _shoot.Firepoint.rotation);
        }
    }
}
