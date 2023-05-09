using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace ProjectKratos
{
    public class PauseButton : MonoBehaviour
    {
        
        [SerializeField] private GameObject _pauseMenu;
        
        public void Pause()
        {
            MMTimeManager.Current.SetTimeScaleTo(0f);
            _pauseMenu.SetActive(true);
        }
        
        public void Unpause()
        {
            MMTimeManager.Current.SetTimeScaleTo(1f);
            _pauseMenu.SetActive(false);
        }
        
    }
}
