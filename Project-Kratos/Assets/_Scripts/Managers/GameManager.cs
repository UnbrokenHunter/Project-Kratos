using System.Collections.Generic;
using System.Linq;
using ProjectKratos.Player;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : NetworkBehaviour {

    public static GameManager Instance { get; private set; }

    public List<PlayerVariables> Players => _players;
    [SerializeField] private List<PlayerVariables> _players;
    
    public override void OnNetworkSpawn()
    {
        OnPlayerConnected(NetworkManager.Singleton.LocalClientId);
        
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        print("GameManager spawned");
    }

    #region  Spawning
    
    [SerializeField] private NetworkBehaviour _playerPrefab;
    [SerializeField] private Transform[] _playerSpawnPoints;

    private void OnPlayerConnected(ulong clientId) {
        if(!IsOwner) return;
        
        SpawnPlayerServerRpc(clientId);

        _players = GameObject.FindObjectsByType<PlayerVariables>(FindObjectsSortMode.None).ToList();
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