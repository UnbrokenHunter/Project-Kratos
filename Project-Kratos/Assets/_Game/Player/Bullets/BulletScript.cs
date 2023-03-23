using UnityEngine;

namespace ProjectKratos
{
    public class BulletScript : MonoBehaviour
    {
        private Transform tr;
        private Rigidbody rb;

        private void Awake()
        {
            tr = transform;
            rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

    }
}
