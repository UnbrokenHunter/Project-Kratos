using ProjectKratos.Player;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos
{
    public class MoneyUISpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject moneyUIPrefab;

        public override void OnNetworkSpawn()
        {
            GameObject moneyUI = Instantiate(moneyUIPrefab);
            transform.GetComponentInParent<PlayerVariables>().MoneyText = moneyUI.GetComponentInChildren<TMP_Text>();
        }
    }
}
