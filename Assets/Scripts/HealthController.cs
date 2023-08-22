using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    float health = 100.0F;

    public TMP_Text livesText;

    public int pointsToAdd = 100;
    private ScoreController scoreController;

    public void Start()
    {
        scoreController = FindObjectOfType<ScoreController>();
    }

    public void TakeDamage(float damage)
    {
        AudioManager.Instance.PlaySFX("Hit");
        health -= Mathf.Abs(damage);

        if (gameObject.CompareTag("Player"))
        {
            UpdateLifeText();

            if (health <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        else
        {
            if (health <= 0)
            {
                scoreController.AddScore(pointsToAdd);
                Destroy(gameObject);
            }
        }
    }

    private void UpdateLifeText()
    {
        livesText.text = health.ToString() + "%";
    }
}
