using ProjectKratos.Player;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos
{
    public class MoneyUISpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject moneyUIPrefab;
        private GameObject _moneyUI;
        
        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;
            
            _moneyUI = Instantiate(moneyUIPrefab);
            transform.GetComponentInParent<PlayerVariables>().MoneyText = _moneyUI.GetComponentInChildren<TMP_Text>();
        }
        
        public void DestroyMoneyUI()
        {
            if (!IsOwner) return;
            Destroy(_moneyUI);
        }
    }
}
