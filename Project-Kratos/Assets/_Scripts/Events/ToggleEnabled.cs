using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectKratos.Events
{
    public class ToggleEnabled : MonoBehaviour
    {

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
        }

        [Button]
        public void ToggleGameobject(GameObject toggle)
        {
            toggle.SetActive(!toggle.activeSelf);

            _text.text = toggle.activeSelf ? "Exit" : "Shop";
        }


    }
}
