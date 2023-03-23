using UnityEngine;

namespace ProjectKratos.Bullet
{
    public class BulletScript : MonoBehaviour
    {
        private Transform tr;
        private Rigidbody rb;

        [SerializeField] private ScriptableBullet _bulletType;

        private void Awake()
        {
            tr = transform;
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rb.AddForce(_bulletType.BulletSpeed * tr.forward, ForceMode.Impulse);
            print("Added Force" + tr.forward);
        }

    }
}
