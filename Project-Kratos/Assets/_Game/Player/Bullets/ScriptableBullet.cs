using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectKratos.Bullet
{
    [CreateAssetMenu(menuName = "Bullets")]
    public class ScriptableBullet : ScriptableObject
    {

        public float Damage = 1f;

        public float Speed = 10f;

    }
}
