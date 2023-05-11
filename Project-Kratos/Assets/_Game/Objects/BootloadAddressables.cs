using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ProjectKratos
{
    public class BootloadAddressables : MonoBehaviour
    {

        [SerializeField] private string[] _addressables;

        private void Awake()
        {
            foreach (var addressable in _addressables)
            {
                Addressables.LoadAssetAsync<GameObject>(addressable);
            }
        }
    }
}