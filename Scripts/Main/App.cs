using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour 
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.StartGame();
        SceneManager.LoadScene("Title");
    }
}
