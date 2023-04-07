using System;
using UnityEngine;

namespace ProjectKratos.Player
{
    public interface IPlayer 
    {
        public ScriptableStats PlayerStats { get; }
        public Vector2 Input { get; }
        public Vector3 Speed { get; }
        public void ApplyVelocity(Vector3 vel, PlayerForce forceType);

    }
}
