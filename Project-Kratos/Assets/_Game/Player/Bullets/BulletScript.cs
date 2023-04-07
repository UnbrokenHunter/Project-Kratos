using UnityEngine;

namespace ProjectKratos.Bullet
{
    public class BulletScript : MonoBehaviour
    {
        public Vector3 Direction { private get; set; }

        [SerializeField] private ScriptableBullet _bulletType;

        private void Start()
        {
            GetComponent<Rigidbody>().AddForce(_bulletType.BulletSpeed * Direction, ForceMode.Impulse);
        }

    }
}
