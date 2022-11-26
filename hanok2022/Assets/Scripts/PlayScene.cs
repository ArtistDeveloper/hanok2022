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
        SceneManager.LoadScene("Lobby");
    }
}
