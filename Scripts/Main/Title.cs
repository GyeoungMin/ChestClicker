using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour 
{
    // �ִϸ��̼����� ���� ������ Ÿ��Ʋ Ƣ����� �߰�����

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
