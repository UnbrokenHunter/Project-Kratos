using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public abstract class LeaderboardStat : MonoBehaviour
    {
        public abstract float GetTrackedStat(PlayerVariables player);
    }
}
