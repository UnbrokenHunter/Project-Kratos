using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectKratos
{
    public class CheatModes : MonoBehaviour
    {
        public void FindCode(string code)
        {
            if (GameManager.Instance == null) return;
                
            switch (code)
            {
                case "God":
                    GameManager.Instance.MainPlayer.GodMode(true);
                    break;
                
                case "Luca":

                    foreach (var player in GameManager.Instance.Players)
                    {
                        player.Speed = 4000;
                    }
                    
                    break;
            }
        }
    }
}
