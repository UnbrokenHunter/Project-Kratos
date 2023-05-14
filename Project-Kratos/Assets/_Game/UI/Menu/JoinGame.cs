using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectKratos
{
    public class JoinGame : MonoBehaviour
    {
        public void JoinLevel(string sceneName) => SceneManager.LoadScene(sceneName);
        
        public void JoinLevelResetTime(string sceneName)
        {
            MMTimeManager.Instance.SetTimeScaleTo(1f);
            SceneManager.LoadScene(sceneName);
        }
    }
}
