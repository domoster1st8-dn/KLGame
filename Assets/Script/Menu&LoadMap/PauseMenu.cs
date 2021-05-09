using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public static bool IsPause = false;
    public GameObject PauseMenuUI;
    SoundBG soundBG;
    private void Awake()
    {
        soundBG = FindObjectOfType<SoundBG>();
    }

    public void PauseGame()
    {
        if (IsPause) Resume(); else Pause();
    }
    void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPause = false;
        soundBG.OnSound();
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPause = true;
        soundBG.PauseSound();
    }
    public void LoadMenu()
    {
        IsPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
