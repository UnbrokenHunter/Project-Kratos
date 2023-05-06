using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectKratos
{
    public class WinScreen : MonoBehaviour
    {

        public static WinScreen Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        
        [SerializeField] private GameObject _winScreen;
        
        [SerializeField] private TMP_Text _gameModeText;
        [SerializeField] private TMP_Text _winLoseText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _killsText;
        [SerializeField] private TMP_Text _scoreText;

        public void SetResults(bool winLose, string description, int kills, int score)
        {
            _gameModeText.text = $"Game Mode: {GameManager.Instance.GameMode}";
            
            _winLoseText.text = winLose ? "You Win!" : "You Lose!";
            
            _descriptionText.text = description;
            
            _killsText.text = $"Kills: {kills}";
            _scoreText.text = $"Score: {score}";
            
            EndGame();
            
            _winScreen.SetActive(true);
        }

        private void EndGame()
        {
            foreach (var player in GameManager.Instance.Players)
            {
                player.CanMove = false;
                player.CanShoot = false;
            }
        }

        public void Continue()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void LobbyMenu()
        {
            // Loads the Lobby scene instead of the Auth Scene
            SceneManager.LoadScene(1);
        }
        
    }
}
