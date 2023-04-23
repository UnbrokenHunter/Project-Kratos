using System;
using System.Collections.Generic;
using System.Linq;
using ProjectKratos.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public List<PlayerVariables> Players => _players;
    [SerializeField] private List<PlayerVariables> _players;

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

    public Transform PickRandomSpawnPoint()
    {
        var spawnPointIndex = Random.Range(0, _playerSpawnPoints.Length - 1);
        return _playerSpawnPoints[spawnPointIndex];
    }
    
    #endregion
   
    public Constants.GameTypes GameMode => _gameMode;
    // create a variable to store the game mode
    [SerializeField] private Constants.GameTypes _gameMode;

}