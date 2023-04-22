using System.Collections;
using System.Collections.Generic;
using ProjectKratos.Player;
using TMPro;
using UnityEngine;

namespace ProjectKratos
{
    public class StartCountdown : MonoBehaviour
    {
        [SerializeField] private int _startCountdownAt = 3;
        [SerializeField] private TMP_Text _countdownText;
        
        private IEnumerator Start()
        {
            DisableMovement();
            
            for (int i = _startCountdownAt; i >= 1; i--)
            {
                _countdownText.text = i.ToString(); 
                yield return new WaitForSeconds(1); 
            }
            
            _countdownText.text = "GO!"; 
            
            yield return new WaitForSeconds(1); 
            
            gameObject.SetActive(false); 
            
            EnableMovement();
        }
        
        private static void DisableMovement()
        {
            foreach (var player in GameManager.Instance.Players)
            {
                player.CanMove = false;
                player.CanShoot = false;
            }
        }
        
        private static void EnableMovement()
        {
            foreach (var player in GameManager.Instance.Players)
            {
                player.CanMove = true;
                player.CanShoot = true;
            }
        }
        
        
    }
}
