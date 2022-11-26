using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    [Header("Credit")]
    [SerializeField] GameObject credit;
    [SerializeField] GameObject creditContents;
    [SerializeField] float duration = 10f;
    [SerializeField] Ease easeType = Ease.Linear;

    void Start()
    {
        SoundManager.Instance.ChangeBGM(SoundManager.ESoundBGM.Main);
        SoundManager.Instance.ActiveLowPassFilter(true);
    }

    public void ChangeToGameScene()
    {
        Invoke("WaitChangeToGameScene", 1f);
    }

    void WaitChangeToGameScene()
    {
        SoundManager.Instance.ChangeBGM(SoundManager.ESoundBGM.None);
        SceneManager.LoadScene("Proto");
        //SceneManager.LoadScene("PlayScene");
    }

    public void PlayCredit()
    {
        credit.SetActive(true);

        var startPos = creditContents.transform.position;
        startPos.y = -800f;
        creditContents.transform.position = startPos;
        creditContents.transform.DOMoveY(2000f, duration).SetEase(easeType).OnComplete(() =>
        {
            credit.SetActive(false);
        });
    }

    public void ReadTitle()
    {
        SoundManager.Instance.ChangeSFX(SoundManager.ESoundFX.DuneoFULLGadong);
    }
}