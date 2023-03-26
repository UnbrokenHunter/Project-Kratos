using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class ShopMenu : MonoBehaviour
    {
        private PlayerVariables _variables;

        private void OnEnable()
        {
            _variables = NetworkManager.Singleton.LocalClient.PlayerObject.
                GetComponent<PlayerVariables>();

            _variables.Stats.CanMove = false;
            _variables.Stats.CanShoot = false;
        }

        private void OnDisable()
        {
            _variables.Stats.CanMove = true;
            _variables.Stats.CanShoot = true;

        }
    }
}
