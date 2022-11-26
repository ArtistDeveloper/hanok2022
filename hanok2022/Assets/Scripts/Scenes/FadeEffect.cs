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

    public void PlayFadeOut() // ��ο���
    {
        Debug.Log("Fade Out");

        //baclBack.DOColor(black, time).From().SetEase(Ease.Linear).OnComplete(() =>
        baclBack.DOFade(1.0f, time).From().SetEase(Ease.OutQuad).OnComplete(() =>
        {
            PlayFadeIn();
        });
    }

    void PlayFadeIn() // �����
    {
        Debug.Log("Fade In");

        baclBack.DOFade(0.0f, time).From().SetEase(Ease.OutQuad);
        //baclBack.DOColor(alpha, time).From().SetEase(Ease.Linear);
    }
}

//public class FadeEffect : MonoBehaviour
//{
//    // �̱��� ��� static ���� ����, �̷��� ��� ���.

//    // �ν����Ϳ��� ������ ��.
//    [SerializeField] private Image blackBack; // ȭ�� �� ä�� �̹��� ������Ʈ. (������.)
//    [SerializeField] private float time = 1.0f;

//    // ���� Static �޼ҵ忡�� ����� ��.
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
//        // ����.
//        sequenceFadeOut = DOTween.Sequence()
//            .SetAutoKill(false) // DoTween Sequence�� �⺻������ ��ȸ����. �����Ϸ��� ������.
//            .OnRewind(() => // ���� ��. OnStart�� unity Start �Լ��� �Ҹ� �� ȣ���. ������ ����.
//            {
//                BlackBack.DOFade(1.0f, 0);
//                BlackBack.enabled = true;
//            })
//            .Append(BlackBack.DOFade(1.0f, Time)) // ��ο���. ���� �� ����.
//            .OnComplete(() => // ���� ��.
//            {
//                Debug.Log("Fade Out");

//                //PlayFadeIn();
//            });

//        sequenceFadeIn = DOTween.Sequence()
//            .SetAutoKill(false) // DoTween Sequence�� �⺻������ ��ȸ����. �����Ϸ��� ������.
//            .OnRewind(() => // ���� ��. OnStart�� unity Start �Լ��� �Ҹ� �� ȣ���. ������ ����.
//            {

//            })
//            .Append(BlackBack.DOFade(0.0f, Time)) // �����. ���� �� ����.
//            .OnComplete(() => // ���� ��.
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
//        sequenceFadeIn.Restart(); // Play()�� �ϸ�, �ѹ� �ۿ� ���� �� ��.
//    }

//    static private void FadeOut()
//    {
//        sequenceFadeOut.Restart(); // Play()�� �ϸ�, �ѹ� �ۿ� ���� �� ��.
//    }
//}
