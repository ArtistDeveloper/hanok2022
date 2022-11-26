using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    public void ChangeToGameScene()
    {
        SceneManager.LoadScene("Proto");
    }
}