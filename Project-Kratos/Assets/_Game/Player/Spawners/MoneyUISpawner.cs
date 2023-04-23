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
            _moneyUI = Instantiate(moneyUIPrefab);
            transform.GetComponentInParent<PlayerVariables>().MoneyText = _moneyUI.GetComponentInChildren<TMP_Text>();
        }
        
        public void DestroyMoneyUI()
        {
            Destroy(_moneyUI);
        }
    }
}
