using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class HealthBar : MonoBehaviour
    {
        private PlayerVariables _variables;
        [SerializeField] private SpriteRenderer _healthBarSpriteRender;
        [SerializeField] private float _extraOffset = 0.15f;

        private float _maxWidth;

        public void Start()
        {
            _variables = transform.GetComponentInParent<PlayerVariables>();
            _maxWidth = _healthBarSpriteRender.size.x;
        }

        private void FixedUpdate() => transform.rotation = new Quaternion(60, 0, 0, 0);

        public void UpdateBar()
        {
            var width = (_variables.CurrentHealth / _variables.MaxHealth); 
            width /= _maxWidth;
            width += _extraOffset;
            _healthBarSpriteRender.size = new Vector2(width, _healthBarSpriteRender.size.y);

            UpdateHealthBar(width);
        }

        private void UpdateHealthBar(float width)
        {
            _healthBarSpriteRender.size = new Vector2(width, _healthBarSpriteRender.size.y);
        }
    }
}
