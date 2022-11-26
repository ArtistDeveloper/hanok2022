using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour
{
    [SerializeField] float SceneChangeDelayTime = 1f;

    void Start()
    {
        GameManager.Instance.GameStart();
    }

    public void ChangeToScene()
    {
        Invoke("WaitChangeToScene", SceneChangeDelayTime);
    }

    void WaitChangeToScene()
    {
        FadeEffect.Instance.PlayFadeOut();

        SceneManager.LoadScene("Lobby");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.GameOver();
            ChangeToScene();
        }
    }
}
