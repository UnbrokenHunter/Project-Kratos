using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class RollAbility : MonoBehaviour
    {
        public static RollAbility Instance { get; private set; }
        
        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
        
        [SerializeField] private CollectableItem[] _abilitys;
        public PlayerVariables Player { get; set; }
        
        public CollectableItem PickAbility()
        {
            return _abilitys[Random.Range(0, _abilitys.Length - 1)];
        }
        
        public void SelectItem(CollectableItem item)
        {
            item.
        }
        
    }
}
