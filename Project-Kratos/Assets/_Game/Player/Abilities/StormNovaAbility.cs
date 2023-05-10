using System;
using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class StormNovaAbility : PlayerAbility
    {
        
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _novaRadius;
        [SerializeField] private float _novaDamage;
        [SerializeField] private StatusEffect _effect;
    
        [SerializeField] private ParticleSystem _particles;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _novaRadius);
        }

        protected override void Ability()
        {
            _particles.gameObject.SetActive(true);
            
            _particles.Play();

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _novaRadius, _layerMask);
        
            foreach (var hitCollider in hitColliders)
            {
                if (!hitCollider.gameObject.CompareTag("Player")) continue;
                var variables = hitCollider.GetComponentInParent<PlayerVariables>();
            
                if (variables == null) continue;
                variables.PlayerInteractions.DealDamage(_novaDamage);
                
                variables.StatusEffect = _effect;
            }
        }
    }
}
