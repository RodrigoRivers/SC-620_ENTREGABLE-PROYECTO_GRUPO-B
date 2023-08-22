using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelController : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject == player)
        {
           
            SceneManager.LoadScene("EndLevel");
        }
    }
}






