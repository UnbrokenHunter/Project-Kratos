using ProjectKratos.Player;
using System;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos
{
    public class PlayerVariables : NetworkBehaviour
    {
        public ScriptableStats Stats { get => _stats; set => _stats = value; }
        [SerializeField] private ScriptableStats _stats;

        public float MoneyCount { get => moneyCount; set => moneyCount = value; }
        [SerializeField] private float moneyCount;

        public static implicit operator GameObject(PlayerVariables v)
        {
            throw new NotImplementedException();
        }
    }
}
