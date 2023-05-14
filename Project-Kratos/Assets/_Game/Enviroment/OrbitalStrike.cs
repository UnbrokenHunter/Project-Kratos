using System;
using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectKratos
{
    public class OrbitalStrike : MonoBehaviour
    {
        [SerializeField] private float _damageAmount;
        [SerializeField] private ParticleSystem _playerParticles;
        [SerializeField] private float _damageInterval = 1f;
        private float _damageTimer;
        
        private void OnTriggerStay(Collider other)
        {
            _damageTimer += Time.deltaTime;
            if (_damageTimer < _damageInterval) return;
            _damageTimer = 0f;

            if (!other.CompareTag("Player")) return;
            
            var player = other.GetComponentInParent<PlayerVariables>();

            Destroy(Instantiate(_playerParticles, player.RigidBody.position, _playerParticles.transform.rotation).gameObject, _playerParticles.main.duration);
            player.PlayerInteractions.DealDamage(_damageAmount);
        }
    }
}
