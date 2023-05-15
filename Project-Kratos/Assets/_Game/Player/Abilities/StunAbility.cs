using System;
using ProjectKratos.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectKratos
{
    public class StunAbility : PlayerAbility
    {
        [SerializeField] private float _stunDistance;
        [SerializeField] private StatusEffect _effect;
        private RaycastHit[] _hits = new RaycastHit[20];
        
        [SerializeField] private LayerMask _layerMask;
        
        [SerializeField] private ParticleSystem[] _particles;

        protected override void Ability()
        {
            _particles[0].transform.parent.rotation = _shoot.GetBulletRotation();

            foreach (var p in _particles)
            {
                p.Play();

                // Raycast to find the object to stun
                var size = Physics.SphereCastNonAlloc(_shoot.FirePoint.position, 
                    2f, 
                    _shoot.GetBulletDirection(), //+ p.transform.localRotation.eulerAngles,
                    _hits,
                    _stunDistance,
                    _layerMask);

                for (int i = 0; i < size; i++)
                {
                    if (_hits[i].collider == null) continue;
                    
                    if (!_hits[i].collider.gameObject.CompareTag("Player")) continue;
                    
                    var player = _hits[i].collider.gameObject.GetComponentInParent<PlayerVariables>();

                    player.StatusEffect = _effect;
                }
            }
        }
    }
}
