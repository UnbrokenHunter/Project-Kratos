using System;
using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class HealingAura : MonoBehaviour
    {
        [SerializeField] private float _regenAmount;
        [SerializeField] private ParticleSystem _playerParticles;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            var t = other.transform;
            Destroy(Instantiate(_playerParticles, t.position, _playerParticles.transform.rotation, t).gameObject, _playerParticles.main.duration);
            
            other.GetComponentInParent<PlayerVariables>().HealthRegen += _regenAmount;
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            other.GetComponentInParent<PlayerVariables>().HealthRegen -= _regenAmount;
        }

        private void OnDestroy()
        {
            // Remove all regen from players
            
        }
    }
}
