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

            var hitColliders = Physics.OverlapSphere(transform.position, _novaRadius, _layerMask);
            
            var isEffectNotNull = _effect != null;
            
            foreach (var hitCollider in hitColliders)
            {
                if (!hitCollider.gameObject.CompareTag("Player")) continue;
                var variables = hitCollider.gameObject.GetComponentInParent<PlayerVariables>();
           
                if (variables == null || variables == _variables) continue;
                
                variables.PlayerInteractions.DealDamage(_novaDamage);
                
                if (isEffectNotNull)
                    variables.StatusEffect = _effect;
            }
        }
    }
}
