using Cinemachine;
using ProjectKratos.Player;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour {

    [SerializeField] private NetworkBehaviour _playerPrefab;
    [SerializeField] private Transform[] _playerSpawnPoints;
    public override void OnNetworkSpawn() {
        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayerServerRpc(ulong playerId)
    {

        var spawnPointIndex = Random.Range(0, _playerSpawnPoints.Length - 1);
        
        var spawnPoint = _playerSpawnPoints[spawnPointIndex];
        
        var spawn = Instantiate(_playerPrefab, spawnPoint.position, Quaternion.identity);
        spawn.NetworkObject.SpawnWithOwnership(playerId);
    }

    public override async void OnDestroy() {
        base.OnDestroy();
        await MatchmakingService.LeaveLobby();
        if(NetworkManager.Singleton != null )NetworkManager.Singleton.Shutdown();
    }
}