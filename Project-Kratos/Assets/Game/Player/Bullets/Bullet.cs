using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectKratos
{
    [CreateAssetMenu(menuName = "Bullets")]
    public class Bullet : ScriptableObject
    {
        public float BulletDamageMultiplier = 1f;
        public float BulletSpeed = 10f;
        [PreviewField] public Mesh BulletMesh;
        [PreviewField] public Material BulletMaterial;

    }
}
