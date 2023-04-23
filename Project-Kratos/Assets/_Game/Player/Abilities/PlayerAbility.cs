using System;
using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public abstract class PlayerAbility : MonoBehaviour
    {
        [SerializeField] private protected float _numberOfUses;
        private protected PlayerVariables _variables;
        private protected PlayerShoot _shoot;

        private void Start()
        {
            _variables = GetComponent<PlayerVariables>();
            _shoot = GetComponentInChildren<PlayerShoot>();
        }

        public abstract void TriggerAbility();
        
    }
}
