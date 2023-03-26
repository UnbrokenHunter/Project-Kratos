using ProjectKratos.Player;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class HealthBar : NetworkBehaviour
    {
        private PlayerVariables _variables;
        [SerializeField] private SpriteRenderer _healthBarSpriteRender;

        private float _maxWidth;

        public void Start()
        {
            _variables = transform.root.GetComponent<PlayerVariables>();
            _maxWidth = _healthBarSpriteRender.size.x;
        }

        private void FixedUpdate() => transform.rotation = new Quaternion(60, 0, 0, 0);

        public void UpdateBar()
        {
            var width = (_variables.Stats.CurrentHealth / _variables.Stats.MaxHealth) / _maxWidth;
            _healthBarSpriteRender.size = new Vector2(width, _healthBarSpriteRender.size.y);

            UpdateHealthBarServerRpc(width);
        }

        [ServerRpc]
        private void UpdateHealthBarServerRpc(float width)
        {
            UpdateHealthBarClientRpc(width);
        }

        [ClientRpc]
        private void UpdateHealthBarClientRpc(float width)
        {
            _healthBarSpriteRender.size = new Vector2(width, _healthBarSpriteRender.size.y);

        }


    }
}
