using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;


public class WallSpawner : MonoBehaviour
{
    #region Filed
    [SerializeField] double _wallSpped = 0;
    [SerializeField] GameObject _wallPrefab;
    [SerializeField] List<GameObject> wallCreatedPositions = new List<GameObject>(6);
    
    Dictionary<string, float> coolTimeTable = new Dictionary<string, float>()
    {
        { "Phase1", 5.0f },
        { "Phase2", 4.0f },
        { "Phase3", 3.0f },
        { "Phase4", 2.5f },
        { "Phase5", 2f },
        { "Phase6", 1.5f },
    };

    static WallSpawner _instance = null;
    float _coolTime = 1f;

    #endregion Filed

    public enum EWallDirection
    {
        Top,
        TopRight,
        TopLeft,
        Bottom,
        BottomRight,
        BottomLeft
    }


    public static WallSpawner Instance
    {
        get
        {
            if (null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (null == _instance)
        {
            _instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(InstantiateWall());
    }

    void Update()
    {
        if (GameManager.Instance.PlayTime >= 20f)
        {
            _coolTime = coolTimeTable["Phase1"];
        }
        else if (GameManager.Instance.PlayTime >= 40f)
        {
            _coolTime = coolTimeTable["Phase2"];
        }
    }

    public IEnumerator InstantiateWall()
    {
        while (true)
        {
            EWallDirection eWallDirection = (EWallDirection)Random.Range(0, 6);
            Vector3 wallPosition = DecideWallPosition(eWallDirection);
            Vector3 wallRotation = DecideWallRotation(eWallDirection);

            Instantiate(_wallPrefab, wallPosition, Quaternion.Euler(wallRotation));

            yield return MyUtil.WaitForSeconds(_coolTime);
        }
    }


    Vector3 DecideWallPosition(EWallDirection eWallDirection)
    {
        Vector3 wallPosition;

        switch (eWallDirection)
        {
            case EWallDirection.Top:
                wallPosition = wallCreatedPositions[(int)EWallDirection.Top].transform.position;
                break;
            case EWallDirection.TopRight:
                wallPosition = wallCreatedPositions[(int)EWallDirection.TopRight].transform.position;
                break;
            case EWallDirection.TopLeft:
                wallPosition = wallCreatedPositions[(int)EWallDirection.TopLeft].transform.position;
                break;
            case EWallDirection.Bottom:
                wallPosition = wallCreatedPositions[(int)EWallDirection.Bottom].transform.position;
                break;
            case EWallDirection.BottomRight:
                wallPosition = wallCreatedPositions[(int)EWallDirection.BottomRight].transform.position;
                break;
            case EWallDirection.BottomLeft:
                wallPosition = wallCreatedPositions[(int)EWallDirection.BottomLeft].transform.position;
                break;
            default:
                wallPosition = Vector3.zero;
                break;

        }

        return wallPosition;
    }


    Vector3 DecideWallRotation(EWallDirection eWallDirection)
    {
        Vector3 wallEulerAngles;

        switch (eWallDirection)
        {
            case EWallDirection.Top:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.Top].transform.eulerAngles;
                break;
            case EWallDirection.TopRight:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.TopRight].transform.eulerAngles;
                break;
            case EWallDirection.TopLeft:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.TopLeft].transform.eulerAngles;
                break;
            case EWallDirection.Bottom:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.Bottom].transform.eulerAngles;
                break;
            case EWallDirection.BottomRight:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.BottomRight].transform.eulerAngles;
                break;
            case EWallDirection.BottomLeft:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.BottomLeft].transform.eulerAngles;
                break;
            default:
                wallEulerAngles = Vector3.zero;
                break;
        }

        return wallEulerAngles;
    }
}
