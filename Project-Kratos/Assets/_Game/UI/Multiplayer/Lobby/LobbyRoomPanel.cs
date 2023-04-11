using System;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyRoomPanel : MonoBehaviour {
    [SerializeField] private float _difficultyDialMaxAngle = 100f;

    [SerializeField] private TMP_Text _nameText, _typeText, _playerCountText;

    public Lobby Lobby { get; private set; }

    public static event Action<Lobby> LobbySelected;

    public void Init(Lobby lobby) {
        UpdateDetails(lobby);
    }

    public void UpdateDetails(Lobby lobby) {
        Lobby = lobby;
        _nameText.text = lobby.Name;
        var list = Constants.GameTypesList();
        _typeText.text = list[GetValue(Constants.GameTypeKey)].ToString();

        var point = Mathf.InverseLerp(0, Constants.Difficulties.Count - 1, GetValue(Constants.DifficultyKey));

        _playerCountText.text = $"{lobby.Players.Count}/{lobby.MaxPlayers}";

        int GetValue(string key) {
            return int.Parse(lobby.Data[key].Value);
        }
    }

    public void Clicked() {
        LobbySelected?.Invoke(Lobby);
    }
}