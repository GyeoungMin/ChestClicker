using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour 
{
    // 애니메이션으로 상자 열리며 타이틀 튀어나오기 추가예정

    void Update()
    {
        if (InfoManager.Instance.GameInfo.isLinkGPGS)
        {
            GPGSManager.Instance.Authenticate();
        }
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
