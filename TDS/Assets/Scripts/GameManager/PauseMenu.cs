using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    
    public void resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void exitToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
