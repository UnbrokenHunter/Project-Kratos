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
            
            switch (gamemode)
            {
                case Constants.GameTypes.BattleRoyal:
                    return false;
                case Constants.GameTypes.CaptureTheFlag:
                    return true;
                case Constants.GameTypes.Brawl:
                    return true;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
