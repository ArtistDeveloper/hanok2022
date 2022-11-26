using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    #region Settings
    [Header("일치율")]
    [SerializeField] public float ConcordanceRate = 0.8f;

    #endregion Settings

    [SerializeField] GameObject target = null;
    [SerializeField] GameObject flashLight = null;

    SpriteRenderer targetSprite = null; // wall에 표시될 sprite

    SpriteRenderer lightSprite = null;
    Transform lightTransform = null;

    public Transform TargetTransform { get => targetSprite.transform; }

    ShadowData shadowData;

    float _playTime;
    public float PlayTime { get => _playTime; }

    
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
        _playTime = Time.realtimeSinceStartup - startTime;
        string curTime = string.Format("{0:0.00}", _playTime);
        //Debug.Log($"Time : {curTime}");

        if (Input.GetKeyDown(KeyCode.C))
        {
            SetShadowData();
        }
    }

    public void Init()
    {
        target ??= GameObject.Find("Target");
        targetSprite ??= target.GetComponent<SpriteRenderer>();

        flashLight ??= GameObject.Find("FlashLight");
        lightTransform ??= flashLight.transform;
        lightSprite ??= lightTransform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void GameStart()
    {
        startTime = Time.realtimeSinceStartup;

        // todo : wall 스폰 시작
    }

    public void ChangeTarget()
    {
        // todo : 시간 변화에 따라 타겟 이미지를 교체한다.
    }

    public void SetShadowData()
    {
        // todo : 플레이어 위치에 따라 업데이트
        float val = Vector2.SqrMagnitude(lightSprite.transform.position - target.transform.position);

        Debug.Log("Capture distance^2 : " + val);
    }

    public ShadowData GetShadowData()
    {
        // todo : 벽에서 사용할 현재 라이트가 표현하는 그림자의 값
        return null;
    }
}

public class ShadowData
{
    public EDirection Direct;
    public float Size = 1f;

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public enum EDirection
{
    Top,
    TopRight,
    Right,
    BottomRight,
    Bottom,
    BottomLeft,
    Left,
    TopLeft,
}