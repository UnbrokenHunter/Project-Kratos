using System;
using System.Collections.Generic;
using System.Linq;
using ProjectKratos.Player;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public List<PlayerVariables> Players => _players;
    [SerializeField] private List<PlayerVariables> _players;
    
    public PlayerVariables MainPlayer { get; set; }

    public void Awake()
    { 
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        SpawnPlayer();
        
        print("GameManager spawned");
    }

    #region  Spawning
    
    [SerializeField] private PlayerVariables _playerPrefab;
    [SerializeField] private Transform[] _playerSpawnPoints;

    private void SpawnPlayer() {
        
        var spawn = Instantiate(_playerPrefab, PickRandomSpawnPoint().position, Quaternion.identity);

        _players = GameObject.FindObjectsByType<PlayerVariables>(FindObjectsSortMode.None).ToList();
    }
    
    public void DespawnPlayer(PlayerVariables player)
    {
        _players.Remove(player);
        Destroy(player.gameObject);
        
        if (GameMode != Constants.GameTypes.BattleRoyal) return;
        
        if (_players.Count <= 0) {
            if (_players[0].IsBot) return;
            
            _players[0].EndGame(true);
        }
            
    }

    public Transform PickRandomSpawnPoint()
    {
        var spawnPointIndex = Random.Range(0, _playerSpawnPoints.Length - 1);
        return _playerSpawnPoints[spawnPointIndex];
    }
    
    #endregion
   
    public Constants.GameTypes GameMode => _gameMode;
    // create a variable to store the game mode
    [SerializeField] private Constants.GameTypes _gameMode;

    public void ResetBrawlScore()
    {
        _brawlScore = _brawlScoreToWin;
        
    }
    public int BrawlScoreToWin { get => _brawlScore; set => _brawlScore += _brawlScoreIncrement; }

    [SerializeField] private int _brawlScoreToWin = 10; 
    private int _brawlScore = 10;
    [SerializeField] private int _brawlScoreIncrement = 3;
    public Slider KillsSlider => _killsSlider;
    [SerializeField] private Slider _killsSlider;
    
}
