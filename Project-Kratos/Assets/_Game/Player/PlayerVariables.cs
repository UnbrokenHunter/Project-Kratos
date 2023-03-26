using ProjectKratos.Player;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos
{
    public class PlayerVariables : NetworkBehaviour
    {
        public ScriptableStats Stats { get => _stats; set => _stats = value; }
        [SerializeField] private ScriptableStats _stats;

    }
}
