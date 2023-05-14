using ProjectKratos.Player;
using TMPro;
using UnityEngine;

namespace ProjectKratos
{
    public class MoneyUISpawner : MonoBehaviour
    {
        [SerializeField] private GameObject moneyUIPrefab;
        private GameObject _moneyUI;
        
        public void Start()
        {
            var variables = transform.GetComponentInParent<PlayerVariables>();
            if (!variables.HasShop) return;
            
            _moneyUI = Instantiate(moneyUIPrefab);
            variables.MoneyText = _moneyUI.GetComponentInChildren<TMP_Text>();
        }
        
        public void DestroyMoneyUI()
        {
            Destroy(_moneyUI);
        }
    }
}
