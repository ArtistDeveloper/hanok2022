using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour
{
    static PlayScene _instance;
    public static PlayScene Instance { get => _instance; }

    [SerializeField] float SceneChangeDelayTime = 1f;

    void Start()
    {
        _instance = this;

        GameManager.Instance.GameStart();
    }

    public void ChangeToGameScene()
    {
        Invoke("WaitChangeToGameScene", SceneChangeDelayTime);
    }

    void WaitChangeToGameScene()
    {
        FadeEffect.Instance.PlayFadeOut();

        SceneManager.LoadScene("Proto");
    }

    public void ChangeToLobbyScene()
    {
        Invoke("WaitChangeToLobbyScene", SceneChangeDelayTime);
    }

    void WaitChangeToLobbyScene()
    {
        FadeEffect.Instance.PlayFadeOut();

        SceneManager.LoadScene("Lobby");
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        GameManager.Instance.GameOver();
    //        ChangeToScene();
    //    }
    //}
}
