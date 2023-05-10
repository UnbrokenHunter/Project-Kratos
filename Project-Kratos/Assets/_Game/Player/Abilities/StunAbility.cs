using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class StunAbility : PlayerAbility
    {
        [SerializeField] private float _stunDistance;
        [SerializeField] private StatusEffect _effect;
        private RaycastHit hit;
        
        [SerializeField] private ParticleSystem _particles;
        
        protected override void Ability()
        {
            _particles.transform.rotation = _shoot.GetBulletRotation();
            
            _particles.Play();

            // Raycast to find the ob
            Physics.Raycast(_shoot.FirePoint.position, _shoot.GetBulletDirection(), out hit, _stunDistance);
            
            if (hit.collider == null) return;

            var player = hit.collider.GetComponentInParent<PlayerVariables>();
            
            if (player == null) return;
            
            player.StatusEffect = _effect;
        }
        
    }
}
