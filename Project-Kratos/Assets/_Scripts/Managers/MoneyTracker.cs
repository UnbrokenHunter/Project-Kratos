using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using UnityEngine;

namespace ProjectKratos
{
    public class MoneyTracker : LeaderboardStat 
    {
        public override float GetTrackedStat(PlayerVariables player)
        {
            return player.MoneyCount;
        }
    }
}
