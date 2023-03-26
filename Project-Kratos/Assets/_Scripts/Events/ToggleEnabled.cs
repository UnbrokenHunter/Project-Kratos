using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectKratos.Events
{
    public class ToggleEnabled : MonoBehaviour
    {

        [Button]
        public void ToggleGameobject(GameObject toggle) => toggle.SetActive(!toggle.activeSelf);

    }
}
