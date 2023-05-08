using System.Collections;
using ProjectKratos.Bullet;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class ExplosiveBullet : BulletScript
    {
        [SerializeField] private float _explosionRadius;
        [SerializeField] private ParticleSystem _explosionParticles;
        [SerializeField] private ParticleSystem _missleParticles;

        [SerializeField] private StatusEffect _effect;

        protected override void ContactWorld()
        {
            BlowUp();
        }

        protected override void ContactPlayer(PlayerHitInteractions player)
        {
            BlowUp();
        }

        private const int maxColliders = 50;
        private readonly Collider[] colliders = new Collider[maxColliders];

        private void BlowUp()
        {
            if (Rigidbody != null)
                Rigidbody.velocity = Vector3.zero;

            var numColliders = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, colliders);

            for (var i = 0; i < numColliders; i++)
            {
                var collider = colliders[i];

                if (!collider.CompareTag("Player")) continue;

                var player = collider.GetComponentInParent<PlayerHitInteractions>();
                player.PlayerHit(this);

                if (_effect != null)
                    player.GetComponentInParent<PlayerVariables>().StatusEffect = _effect;
            }

            _explosionParticles.gameObject.SetActive(true);

            GetComponentInChildren<MeshRenderer>().enabled = false;

            if (_missleParticles != null)
            {
                _missleParticles.Stop();
                _missleParticles.gameObject.SetActive(false);
            }

            _explosionParticles.Play();

            // The particle thing is 2 seconds long, so we wait 2 seconds before destroying the bullet
            StartCoroutine(DestroyAfterTime());
        }

        private IEnumerator DestroyAfterTime()
        {
            yield return Helpers.GetWait(2f);
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
    }
}