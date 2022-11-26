using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.ChangeBGM(SoundManager.ESoundBGM.Main);
        SoundManager.Instance.ActiveLowPassFilter(true);
    }

    public void ChangeToGameScene()
    {
        SceneManager.LoadScene("Proto");
        //SceneManager.LoadScene("PlayScene");
    }
}