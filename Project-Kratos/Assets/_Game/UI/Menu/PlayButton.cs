using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectKratos
{
    public class PlayButton : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
