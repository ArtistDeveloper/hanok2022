using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    #region Settings
    [Header("��ġ��")]
    [SerializeField] public float ConcordanceRate = 0.9f;

    #endregion Settings

    [SerializeField] GameObject target = null;
    [SerializeField] GameObject flashLight = null;

    SpriteRenderer targetSprite = null; // wall�� ǥ�õ� sprite
    Transform lightTransform = null;

    public Transform TargetTransform { get => targetSprite.transform; }

    ShadowData shadowData;

    
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
        string curTime = string.Format("{0:0.00}", Time.realtimeSinceStartup - startTime);
        //Debug.Log($"Time : {curTime}");

        if (Input.GetKeyDown(KeyCode.D))
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
        Vector2 lightPos = lightTransform.position;
        float val = Vector2.SqrMagnitude(lightTransform.position - targetSprite.transform.position);

        Debug.Log("distance : " + val);
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