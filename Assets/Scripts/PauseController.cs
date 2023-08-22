using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    GameObject settingsPanel;

    void Start()
    {
        CameraFollower camera = FindAnyObjectByType<CameraFollower>();

    }

    public void Pause()
    {
        AudioManager.Instance.PlaySFX("Button");
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 0.0F;
    }

    public void Home()
    {
        AudioManager.Instance.PlaySFX("Button");
        Time.timeScale = 1.0F;
    }

    public void Resume()
    {
        AudioManager.Instance.PlaySFX("Button");
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
        Time.timeScale = 1.0F;
    }

    public void Reload()
    {
        AudioManager.Instance.PlaySFX("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0F;
    }
}

