using System.Collections;
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
            yield return Helpers.GetWait(0.1f);
            
            DisableMovement();
            
            for (var i = _startCountdownAt; i >= 1; i--)
            {
                _countdownText.text = i.ToString();
                yield return Helpers.GetWait(1);
            }
            
            _countdownText.text = "GO!"; 
            
            yield return Helpers.GetWait(0.5f);

            EnableMovement();

            yield return Helpers.GetWait(0.5f);
            
            gameObject.SetActive(false); 
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
