using System;
using MoreMountains.Feedbacks;
using ProjectKratos.Player;
using ProjectKratos.Tabs;
using UnityEngine;

namespace ProjectKratos.Shop
{
    public class ShopMenu : MonoBehaviour
    {
        public PlayerVariables Variables { get => _variables; set => _variables = value; }
        private PlayerVariables _variables;
        
        [SerializeField] private TabGroup _tabGroup;


        private void OnEnable()
        {
            if (_variables == null) return;

            _variables.CanMove = false;
            _variables.CanShoot = false;
            
            MMTimeManager.Instance.SetTimeScaleTo(0f);
        }

        private void OnDisable()
        {
            if (_variables == null) return;

            _variables.CanMove = true;
            _variables.CanShoot = true;
            
            MMTimeManager.Instance.SetTimeScaleTo(1f);
        }
    }
}
