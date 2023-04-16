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

        public override void OnNetworkSpawn()
        {
            _variables = transform.GetComponentInParent<PlayerVariables>();
            _maxWidth = _healthBarSpriteRender.size.x;
        }

        private void FixedUpdate() => transform.rotation = new Quaternion(60, 0, 0, 0);

        public void UpdateBar()
        {
            var width = (_variables.CurrentHealth / _variables.MaxHealth); 
            width /= _maxWidth;
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
            //if (!IsOwner || (isBot &&  (IsServer || IsHost))) 
                _healthBarSpriteRender.size = new Vector2(width, _healthBarSpriteRender.size.y);

        }
    }
}
