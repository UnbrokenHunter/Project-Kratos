using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectKratos
{
    public class JoinGame : MonoBehaviour
    {
        public void JoinLevel(string sceneName) => SceneManager.LoadScene(sceneName);
    }
}
