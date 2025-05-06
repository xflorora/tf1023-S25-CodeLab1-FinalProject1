using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleHurt : MonoBehaviour
{
    public string sceneName;
  
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
