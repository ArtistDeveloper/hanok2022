using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeEffect : MonoBehaviour
{
    static FadeEffect _instance;
    public static FadeEffect Instance { get => _instance; }


    [SerializeField] Image baclBack;
    [SerializeField] float time = 1f;

    readonly Color black = new Color(0, 0, 0, 255);
    readonly Color alpha = new Color(0, 0, 0, 0);

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("Duplicate FadeEffect Exists");
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);    
    }

    public void PlayFadeOut() // 어두워짐
    {
        Debug.Log("Fade Out");

        //baclBack.DOColor(black, time).From().SetEase(Ease.Linear).OnComplete(() =>
        baclBack.DOFade(1.0f, time).From().SetEase(Ease.OutQuad).OnComplete(() =>
        {
            PlayFadeIn();
        });
    }

    void PlayFadeIn() // 밝아짐
    {
        Debug.Log("Fade In");

        baclBack.DOFade(0.0f, time).From().SetEase(Ease.OutQuad);
        //baclBack.DOColor(alpha, time).From().SetEase(Ease.Linear);
    }
}

//public class FadeEffect : MonoBehaviour
//{
//    // 싱글톤 대신 static 쓰기 위해, 이러한 방법 사용.

//    // 인스펙터에서 수정할 값.
//    [SerializeField] private Image blackBack; // 화면 꽉 채운 이미지 컴포넌트. (검은색.)
//    [SerializeField] private float time = 1.0f;

//    // 실제 Static 메소드에서 사용할 값.
//    static private Image BlackBack;
//    static private float Time;

//    static Sequence sequenceFadeOut;
//    static Sequence sequenceFadeIn;


//    private void Awake()
//    {
//        BlackBack = blackBack;
//        Time = time;

//        BlackBack.enabled = false;

//        DontDestroyOnLoad(this.gameObject);
//    }

//    private void Start()
//    {
//        // 구성.
//        sequenceFadeOut = DOTween.Sequence()
//            .SetAutoKill(false) // DoTween Sequence는 기본적으로 일회용임. 재사용하려면 써주자.
//            .OnRewind(() => // 실행 전. OnStart는 unity Start 함수가 불릴 때 호출됨. 낚이지 말자.
//            {
//                BlackBack.DOFade(1.0f, 0);
//                BlackBack.enabled = true;
//            })
//            .Append(BlackBack.DOFade(1.0f, Time)) // 어두워짐. 알파 값 조정.
//            .OnComplete(() => // 실행 후.
//            {
//                Debug.Log("Fade Out");

//                //PlayFadeIn();
//            });

//        sequenceFadeIn = DOTween.Sequence()
//            .SetAutoKill(false) // DoTween Sequence는 기본적으로 일회용임. 재사용하려면 써주자.
//            .OnRewind(() => // 실행 전. OnStart는 unity Start 함수가 불릴 때 호출됨. 낚이지 말자.
//            {

//            })
//            .Append(BlackBack.DOFade(0.0f, Time)) // 밝아짐. 알파 값 조정.
//            .OnComplete(() => // 실행 후.
//            {
//                BlackBack.enabled = false;
//                Debug.Log("Fade In");
//            });
//    }

//    static public void PlayFadeIn()
//    {
//        FadeIn();
//    }

//    static public void PlayFadeOut()
//    {
//        FadeOut();
//    }

//    static private void FadeIn()
//    {
//        sequenceFadeIn.Restart(); // Play()로 하면, 한번 밖에 실행 안 됨.
//    }

//    static private void FadeOut()
//    {
//        sequenceFadeOut.Restart(); // Play()로 하면, 한번 밖에 실행 안 됨.
//    }
//}
