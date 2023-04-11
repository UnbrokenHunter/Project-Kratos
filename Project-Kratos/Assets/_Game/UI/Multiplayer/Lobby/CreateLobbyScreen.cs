using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateLobbyScreen : MonoBehaviour
{
    [SerializeField] private InputField _nameInput;
    [SerializeField] private TMP_InputField _maxPlayersInput;
    [SerializeField] private TMP_Dropdown _typeDropdown;

    private void Start() {
        SetOptions(_typeDropdown);

        void SetOptions(TMP_Dropdown dropdown)
        {
            var values = Constants.GameTypesList();
            
            dropdown.options = values.Select(type => new TMP_Dropdown.OptionData { text = type.ToString() }).ToList();
        }
    }

    public static event Action<LobbyData> LobbyCreated;

    public void OnCreateClicked() {
        var lobbyData = new LobbyData {
            Name = _nameInput.text,
            MaxPlayers = int.Parse(_maxPlayersInput.text),
            Type = _typeDropdown.value
        };

        LobbyCreated?.Invoke(lobbyData);
    }
}

public struct LobbyData {
    public string Name;
    public int MaxPlayers;
    public int Difficulty;
    public int Type;
}