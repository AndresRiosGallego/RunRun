using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void CloseUp() { 
        Application.Quit();
        //Debug.Log("Application QUIT");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("LV1");
        Time.timeScale = 1;
    }

    //public void LoadLevel2()
    //{
    //    SceneManager.LoadScene("LV2");
    //    Time.timeScale = 1;
    //}
    //public void LoadLevel3()
    //{
    //    SceneManager.LoadScene("LV3");
    //    Time.timeScale = 1;
    //}
    //public void LoadLevel4()
    //{
    //    SceneManager.LoadScene("LV4");
    //    Time.timeScale = 1;
    //}
}
