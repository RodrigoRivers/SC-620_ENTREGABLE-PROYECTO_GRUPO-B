using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    
    public void Restart()
    {
        AudioManager.Instance.PlaySFX("Button");
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        AudioManager.Instance.PlaySFX("Button");
        SceneManager.LoadScene("MainMenu");
    }
}
