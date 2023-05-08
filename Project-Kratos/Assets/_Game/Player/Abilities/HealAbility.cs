using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class HealAbility : PlayerAbility
    {
        [SerializeField] private float _healRadius;
        [SerializeField] private float _auraLifespan;
        [SerializeField] private StatusEffect _effect;
        private RaycastHit hit;
        
        [SerializeField] private GameObject _healingAura;
        [SerializeField] private ParticleSystem _particles;
        
        protected override void Ability()
        {
            var aura = Instantiate(_healingAura, _variables.RigidBody.position, Quaternion.identity);
            aura.transform.localScale = Vector3.one * _healRadius;
            Destroy(aura, _auraLifespan);
            
        }
        
    }
}
