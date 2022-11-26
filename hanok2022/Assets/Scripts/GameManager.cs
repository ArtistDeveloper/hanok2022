using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    #region Settings
    [Header("��ġ��")]
    [SerializeField] public float ConcordanceRate = 0.8f;

    #endregion Settings

    [SerializeField] GameObject target = null;
    [SerializeField] GameObject flashLight = null;

    SpriteRenderer targetSprite = null; // wall�� ǥ�õ� sprite

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

        // todo : wall ���� ����
    }

    public void ChangeTarget()
    {
        // todo : �ð� ��ȭ�� ���� Ÿ�� �̹����� ��ü�Ѵ�.
    }

    public void SetShadowData()
    {
        // todo : �÷��̾� ��ġ�� ���� ������Ʈ
        float val = Vector2.SqrMagnitude(lightSprite.transform.position - target.transform.position);

        Debug.Log("Capture distance^2 : " + val);
    }

    public ShadowData GetShadowData()
    {
        // todo : ������ ����� ���� ����Ʈ�� ǥ���ϴ� �׸����� ��
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