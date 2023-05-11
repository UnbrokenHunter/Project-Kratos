using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class StunAbility : PlayerAbility
    {
        [SerializeField] private float _stunDistance;
        [SerializeField] private StatusEffect _effect;
        private RaycastHit hit;
        
        [SerializeField] private ParticleSystem[] _particles;

        protected override void Ability()
        {
            _particles[0].transform.parent.rotation = _shoot.GetBulletRotation();

            foreach (var p in _particles)
            {
                p.Play();

                // Raycast to find the ob
                var hits = Physics.RaycastAll(_shoot.FirePoint.position, _shoot.GetBulletDirection(), _stunDistance);

                // Draw the ray above
                Debug.DrawRay(_shoot.FirePoint.position, _shoot.GetBulletDirection() * _stunDistance, Color.red, 3f);

                foreach (var h in hits)
                {
                    if (h.collider == null) return;

                    var player = h.collider.GetComponentInParent<PlayerVariables>();

                    if (player == null) return;

                    player.StatusEffect = _effect;
                }
            }
        }
    }
}
