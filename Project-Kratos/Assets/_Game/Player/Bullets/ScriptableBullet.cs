using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectKratos.Bullet
{
    [CreateAssetMenu(menuName = "Bullets")]
    public class ScriptableBullet : ScriptableObject
    {

        [TitleGroup("Bullet Settings")]
        [VerticalGroup("Bullet Settings/Vertical")]
        [BoxGroup("Bullet Settings/Vertical/Box A")]
        public GameObject bulletPrefab;

        [BoxGroup("Bullet Settings/Vertical/Box A")]
        [HorizontalGroup("Bullet Settings/Vertical/Box A/Split")]
        public float BulletDamageMultiplier = 1f;

        [HorizontalGroup("Bullet Settings/Vertical/Box A/Split")]
        public float BulletSpeed = 10f;

        [VerticalGroup("Bullet Settings/Vertical/Box B")]
        [BoxGroup("Bullet Settings/Vertical/Box B/Preview")]
        [PreviewField(200, ObjectFieldAlignment.Center)]
        public Mesh BulletMesh;

        [BoxGroup("Bullet Settings/Vertical/Box B/Preview")]
        [PreviewField(200, ObjectFieldAlignment.Center)]
        public Material BulletMaterial;
    }
}
