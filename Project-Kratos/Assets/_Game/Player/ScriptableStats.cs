using Sirenix.OdinInspector;
using UnityEngine;
using ProjectKratos.Bullet;

namespace ProjectKratos.Player
{
    [CreateAssetMenu]
    public class ScriptableStats : ScriptableObject
    {
        [Title("Movement Variables")]
        public float Speed = 10f;
        public float RotationSpeed = 0.15f;

        [Title("Health Variables")]
        public float MaxHealth = 100f;
        public float Defense = 100f;
        public float HealthRegen = 1f;

        [Title("Attack Variables")]
        public float Damage = 1f;

        [Title("External")]
        [Tooltip("The rate at which external velocity decays")]
        public int ExternalVelocityDecay = 100;

        
    }
}
