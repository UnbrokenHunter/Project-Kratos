using System;
using Cinemachine;
using ProjectKratos.Player;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : NetworkBehaviour {

    public static GameManager Instance { get; private set; }

    private void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    #region  Spawning
    
    [SerializeField] private NetworkBehaviour _playerPrefab;
    [SerializeField] private Transform[] _playerSpawnPoints;
    public override void OnNetworkSpawn() {
        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayerServerRpc(ulong playerId)
    {
        var spawn = Instantiate(_playerPrefab, PickRandomSpawnPoint().position, Quaternion.identity);
        spawn.NetworkObject.SpawnWithOwnership(playerId);
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

    public override async void OnDestroy() {
        base.OnDestroy();
        await MatchmakingService.LeaveLobby();
        if(NetworkManager.Singleton != null )NetworkManager.Singleton.Shutdown();
    }
}