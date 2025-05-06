using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{

    public string sceneName;
    public void SceneLoad()
    {
        SceneManager.LoadScene(sceneName);
    }
}
