using MoreMountains.Tools;
using ProjectKratos.Player;
using Unity.Netcode;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace ProjectKratos
{
    public class HealthBarManager : NetworkBehaviour
    {
        private PlayerVariables _variables;
        private PlayerInteractions _interaction;
        [SerializeField] private SpriteRenderer _healthBar;

        private float _maxWidth;

        public override void OnNetworkSpawn()
        {
            _variables = transform.root.GetComponent<PlayerVariables>();
            _interaction = GetComponentInParent<PlayerInteractions>();
            _maxWidth = _healthBar.size.x;
        }

        private void FixedUpdate()
        {
            transform.rotation = new Quaternion(60, 0, 0, 0);
        }

        public void SetBar()
        {
            if (!IsOwner) return;
                
            var width = (_interaction.CurrentHealth / _variables.Stats.MaxHealth) / _maxWidth;
            _healthBar.size = new Vector2(width, _healthBar.size.y);

            print($"Current Health: {_interaction.CurrentHealth} \nMax Health: {_variables.Stats.MaxHealth} \n Ratio: {_interaction.CurrentHealth / _variables.Stats.MaxHealth}");
        }


    }
}
