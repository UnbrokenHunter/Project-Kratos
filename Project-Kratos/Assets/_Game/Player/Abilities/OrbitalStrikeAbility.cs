using UnityEngine;

namespace ProjectKratos
{
    public class OrbitalStrikeAbility : PlayerAbility
    {
        [SerializeField] private float _damageRadius;
        [SerializeField] private float _strikeLifespan;
        [SerializeField] private float _strikeOffset;
        [SerializeField] private StatusEffect _effect;
        private RaycastHit hit;
        
        [SerializeField] private GameObject _orbitalStrike;
        [SerializeField] private ParticleSystem _particles;
        
        protected override void Ability()
        {
            var dir = _shoot.GetBulletDirection() * _strikeOffset;
            
            var aura = Instantiate(_orbitalStrike, _variables.RigidBody.position + dir + Vector3.up, Quaternion.identity);
            aura.transform.localScale = Vector3.one * _damageRadius;
            Destroy(aura, _strikeLifespan);
            
        }
    }
}
