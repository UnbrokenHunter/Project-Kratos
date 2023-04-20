using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace ProjectKratos.Player
{
    public class PlayerGamemode : NetworkBehaviour 
    {
        #region Internal

        private PlayerVariables _variables;

        #endregion

        public override void OnNetworkSpawn()
        {
            _variables = GetComponentInParent<PlayerVariables>();
            
            
        }

        private void SetupBattleRoyal()
        {
            
        }

        /// <summary>
        /// Return true or false depending on the gamemode
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static bool CanRespawn()
        {
            var gamemode = GameManager.Instance.GameMode;

            return gamemode switch
            {
                Constants.GameTypes.BattleRoyal => false,
                Constants.GameTypes.CaptureTheFlag => true,
                Constants.GameTypes.Brawl => true,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}
