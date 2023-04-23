using System;
using System.Collections;
using System.Collections.Generic; 
using System.Linq;
using ProjectKratos.Player;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
namespace ProjectKratos
{
    public class LeaderBoard : MonoBehaviour
    {
        
        [SerializeField] private LeaderboardStat _trackedStat;
        [SerializeField] private List<PlayerVariables> _players => GameManager.Instance.Players;
        [SerializeField] private TMP_Text _localText;
        [SerializeField] private TMP_Text _highestText;
        
        private void Start()
        {
            _trackedStat = GetComponent<LeaderboardStat>();
        }

        private void Update()
        {
            var values = new float[_players.Count];
            for (var i = 0; i < _players.Count; i++)
            {
                var player = _players[i];
                values[i] = _trackedStat.GetTrackedStat(player);
            }
            
            values.Sort();

            _localText.text = $"{FindLocalPlayerIndex()}. You";
            
            //var highestPlayerName = NetworkManager.Singleton.ConnectedClients.TryGetValue(FindHighestValuePlayer(_players.ToArray()).OwnerClientId, out var client) ? client.PlayerObject.name : "Unknown";
            //_highestText.text = $"{FindHighestValueIndex(values)}. {highestPlayerName}";

        }
         
        // Returns the index of the highest value in the array
        private int FindLocalPlayerIndex()
        {
            // INCOMPLETE
            return -1;
        }
        
        // Returns the index of the highest value in the array
        private int FindHighestValueIndex(float[] values)
        {
            var max = values.Max();
            return Array.IndexOf(values, max);
        }

        private PlayerVariables FindHighestValuePlayer(PlayerVariables[] players)
        {
            var max = players.Max(player => _trackedStat.GetTrackedStat(player));
            return players.First(player => Math.Abs(_trackedStat.GetTrackedStat(player) - max) < 0.1f);
        }
    }
}
