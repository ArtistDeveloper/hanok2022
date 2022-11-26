using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.GameStart();
    }

    public void ChangeToScene()
    {
        Invoke("WaitChangeToScene", 1f);
    }

    void WaitChangeToScene()
    {
        SceneManager.LoadScene("Lobby");
    }
}
