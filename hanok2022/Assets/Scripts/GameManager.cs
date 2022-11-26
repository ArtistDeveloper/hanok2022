using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    #region Settings
    [Header("일치율")]
    [SerializeField] public float ConcordanceRate = 0.8f;

    #endregion Settings

    [SerializeField] GameObject target = null;
    [SerializeField] LightControl flashLight = null;

    [Header("HUD")]
    [SerializeField] Text lastTimeText = null;
    [SerializeField] Text bestTimeText = null;
    [SerializeField] GameObject newRecord = null;
    [SerializeField] GameObject GameOverText = null;

    SpriteRenderer targetSprite = null; // wall에 표시될 sprite

    SpriteRenderer lightSprite = null;
    Transform lightTransform = null;

    public Transform TargetTransform { get => targetSprite.transform; }

    ShadowData shadowData = new ShadowData();
    public ShadowData CurrentShadow
    {
        get => shadowData;
        private set => shadowData = value;
    }

    float _playTime;
    public float PlayTime { get => _playTime; }

    bool isGameOver = false;

    readonly float SLOW_TIME_SCALE = 0.2f;
    readonly float SLOW_TIME = 1f;

    Coroutine slowMotionRoutine = null;

    #region Records
    float startTime = 0f;

    #endregion Records

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("Duplicate GameManager Exists");
        }

        Init();
    }

    void Update()
    {
        if (isGameOver == false)
        {
            _playTime = Time.realtimeSinceStartup - startTime;
            string curTime = string.Format("{0:0.00}", _playTime);
        }

        ShowTimeHUD();

        //Debug.Log($"Time : {curTime}");

        //if (Input.GetKeyDown(KeyCode.C))
        {
            SetShadowData();
        }

        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayScene.Instance.ChangeToLobbyScene();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayScene.Instance.ChangeToGameScene();
            }
        }
    }

    public void Init()
    {
        target ??= GameObject.Find("Target");
        targetSprite ??= target.GetComponent<SpriteRenderer>();

        flashLight ??= GameObject.Find("FlashLight").GetComponent<LightControl>();
        lightTransform ??= flashLight.transform;
        lightSprite ??= lightTransform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void GameStart()
    {
        SoundManager.Instance.ChangeBGM(SoundManager.ESoundBGM.Main);
        SoundManager.Instance.ActiveLowPassFilter(false);

        startTime = Time.realtimeSinceStartup;
        isGameOver = false;

        InitHUD();

        // todo : wall 스폰 시작
        WallSpawner.Instance.StartWallSpawner();
    }

    public void GameOver()
    {
        SoundManager.Instance.ChangeBGM(SoundManager.ESoundBGM.None);

        isGameOver = true;
        GameOverUI();

        WallSpawner.Instance.FinishWallSpawner();
    }

    public void PlaySlowMotion()
    {
        if (slowMotionRoutine != null)
        {
            StopCoroutine(slowMotionRoutine);
        }

        slowMotionRoutine = StartCoroutine(SlowMotionRoutine());
    }

    IEnumerator SlowMotionRoutine()
    {
        Time.timeScale = SLOW_TIME_SCALE;

        yield return MyUtil.WaitForSeconds(SLOW_TIME);

        Time.timeScale = 1f;
    }

    public void ChangeTarget()
    {
        // todo : 시간 변화에 따라 타겟 이미지를 교체한다.
    }

    public void SetShadowData()
    {
        // todo : 플레이어 위치에 따라 업데이트
        // 바라보는 방향과 거리에 따라 
        CurrentShadow.Direct = flashLight.GetEDirection();

        float sqrDist = Vector2.SqrMagnitude(lightSprite.transform.position - target.transform.position);
        sqrDist = Vector2.Distance(lightSprite.transform.position, target.transform.position);
        float val1 = 1 / sqrDist; // 거리가 멀면 size 커진다


        CurrentShadow.Scale = sqrDist; // x ~ 0.6097566
     
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Debug.LogError("scale : " + val1);
        //    Debug.Log("Capture distance^2 : " + sqrDist);
        //}
    }

    #region HUD
    readonly string BEST_TIME = "BestTimeKey";
    bool isChallengeNewRecord = true; // false일 땐 최고 기록 갱신을 표시하지 않음
    float savedBestTime = 0;

    public void InitHUD()
    {
        isChallengeNewRecord = true;
        savedBestTime = PlayerPrefs.GetFloat(BEST_TIME, 0);

        GameOverText.SetActive(false);

        lastTimeText.text = string.Format("{0:00.00}", 0);
        bestTimeText.text = string.Format("{0:00.00}", savedBestTime);
    }

    public void GameOverUI()
    {
        GameOverText.SetActive(true);
        RecordTime();
    }

    void RecordTime()
    {
        bool isNewRecord = _playTime > savedBestTime;
        if (isNewRecord)
        {
            PlayerPrefs.SetFloat(BEST_TIME, _playTime);
        }
    }

    public void ShowTimeHUD()
    {
        lastTimeText.text = string.Format("{0:00.00}", _playTime);
        //bestTimeText.text = string.Format("{0:00.00}", savedBestTime);

        bool isNewRecord = _playTime > savedBestTime;

        if (isChallengeNewRecord && isNewRecord)
        {
            isChallengeNewRecord = false;
            newRecord.SetActive(true);
            Invoke("HideNewRecord", 1f);
        }
    }

    void HideNewRecord()
    {
        newRecord.SetActive(false);
    }

    #endregion HUD
}

public class ShadowData
{
    public WallSpawner.EWallDirection Direct;
    public float Scale = 1f;

    public override bool Equals(object obj)
    {
        ShadowData objData = obj as ShadowData;
        if (objData == null)
        {
            return false;
        }

        return objData.Direct == this.Direct;
        //return objData.Direct == this.Direct && objData.Scale == this.Scale;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public class ResultPopup
{
    [SerializeField] Text newRecordText = null;
    [SerializeField] Text playTime = null;
    [SerializeField] Text bestTime = null;

    readonly string BEST_TIME = "BestTimeKey";

    public void SaveBestTime()
    {
        float savedBestTime = PlayerPrefs.GetFloat(BEST_TIME, 0);
        float curTime = GameManager.Instance.PlayTime;

        bool isNewRecord = curTime > savedBestTime;

        if (isNewRecord)
        {
            PlayerPrefs.SetFloat(BEST_TIME, curTime);
        }

        newRecordText.gameObject.SetActive(isNewRecord);
    }
}