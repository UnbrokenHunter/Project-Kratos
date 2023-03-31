using Cinemachine;
using ProjectKratos.Player;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour {

    [SerializeField] private NetworkBehaviour _playerPrefab;
    public override void OnNetworkSpawn() {
        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayerServerRpc(ulong playerId) {
        var spawn = Instantiate(_playerPrefab);
        spawn.NetworkObject.SpawnWithOwnership(playerId);
    }

    public override async void OnDestroy() {
        base.OnDestroy();
        await MatchmakingService.LeaveLobby();
        if(NetworkManager.Singleton != null )NetworkManager.Singleton.Shutdown();
    }
}