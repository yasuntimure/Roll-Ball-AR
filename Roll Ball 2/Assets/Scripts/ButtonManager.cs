using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager: MonoBehaviour
{
   public void PlayBtn(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void AgainBtn(string newGameLevel)
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(newGameLevel);
        Time.timeScale = 1;
    }
   
   public void ExitBtn()
    {
        Application.Quit();
    }
}
