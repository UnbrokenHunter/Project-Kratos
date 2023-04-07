using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectKratos.Player
{
    [CreateAssetMenu]
    public class ScriptableStats : ScriptableObject
    {
        [Title("Movement Variables")]
        public bool CanMove = true;
        public float Speed = 10f;
        public float RotationSpeed = 0.15f;

        [Title("Health Variables")]
        public float CurrentHealth;
        public float MaxHealth = 100f;
        public float Defense = 1f;
        public float HealthRegen = 1f;

        [Title("Attack Variables")]
        public bool CanShoot = true;
        public float Damage = 1f;
        public float ShootingSpeed = 1f;

        [Title("External")]
        [Tooltip("The rate at which external velocity decays")]
        public int ExternalVelocityDecay = 100;

        
    }
}
