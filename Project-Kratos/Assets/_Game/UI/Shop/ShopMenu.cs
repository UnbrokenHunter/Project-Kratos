using UnityEngine;

namespace ProjectKratos.Shop
{
    public class ShopMenu : MonoBehaviour
    {
        public PlayerVariables Variables { get => _variables; set => _variables = value; }
        private PlayerVariables _variables;


        private void OnEnable()
        {
            if (_variables == null) return;
            print("Enable");

            _variables.Stats.CanMove = false;
            _variables.Stats.CanShoot = false;
        }

        private void OnDisable()
        {
            if (_variables == null) return;
            print("Disable");

            _variables.Stats.CanMove = true;
            _variables.Stats.CanShoot = true;

        }
    }
}
