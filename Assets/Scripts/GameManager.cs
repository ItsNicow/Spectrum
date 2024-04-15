using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
