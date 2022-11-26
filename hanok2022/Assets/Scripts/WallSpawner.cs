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

    // Phase End Time
    const float GAMESTART_TIME = 0.0f;
    //readonly float[] PHASE_ENDTIME = { 20.0f, 40.0f, 60.0f, 80.0f, 100.0f, 120.0f }; // SIZE = 6

    const float PHASE_ONE_ENDTIME = 20.0f;
    const float PHASE_TWO_ENDTIME = 40.0f;
    const float PHASE_THREE_ENDTIME = 60.0f;
    const float PHASE_FOUR_ENDTIME = 80.0f;
    const float PHASE_FIVE_ENDTIME = 100.0f;
    const float PHASE_SIX_ENDTIME = 120.0f;

    int _nextStage = 1;
    static WallSpawner _instance = null;
    float _coolTime = 5f;

    const int CREATABLE_MIN_WALL = 0;
    const int CREATABLE_MAX_WALL = 6;


    Dictionary<string, float> coolTimeTable = new Dictionary<string, float>()
    {
        { "Phase1", 5.0f },
        { "Phase2", 4.0f },
        { "Phase3", 3.0f },
        { "Phase4", 2.5f },
        { "Phase5", 2f },
        { "Phase6", 1.5f },
    };

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
        //string curTime = string.Format("{0:0.00}", GameManager.Instance.PlayTime);
        //Debug.Log($"Time : {curTime}");

        // Phase1 벽 생성 시간 조절
        if (
            GAMESTART_TIME <= GameManager.Instance.PlayTime &&
            GameManager.Instance.PlayTime <= PHASE_ONE_ENDTIME &&
            _nextStage == 1
            )
        {
            _coolTime = coolTimeTable["Phase1"];
            Debug.Log("Phase1에 들어왔습니다.");
            _nextStage += 1;
        }

        // Phase2 벽 생성 시간 조절
        else if (
            PHASE_ONE_ENDTIME <= GameManager.Instance.PlayTime &&
            GameManager.Instance.PlayTime <= PHASE_TWO_ENDTIME &&
            _nextStage == 2
            )
        {
            _coolTime = coolTimeTable["Phase2"];
            Debug.Log("Phase2에 들어왔습니다.");
            _nextStage += 1;
        }

        // Phase3 벽 생성 시간 조절
        else if (
            PHASE_TWO_ENDTIME <= GameManager.Instance.PlayTime &&
            GameManager.Instance.PlayTime <= PHASE_THREE_ENDTIME &&
            _nextStage == 3
            )
        {
            _coolTime = coolTimeTable["Phase3"];
            Debug.Log("Phase3에 들어왔습니다.");
            _nextStage += 1;
        }

        // Phase4 벽 생성 시간 조절
        else if (
            PHASE_THREE_ENDTIME <= GameManager.Instance.PlayTime &&
            GameManager.Instance.PlayTime <= PHASE_FOUR_ENDTIME &&
            _nextStage == 4
            )
        {
            _coolTime = coolTimeTable["Phase4"];
            Debug.Log("Phase4에 들어왔습니다.");
            _nextStage += 1;
        }

        // Phase5 벽 생성 시간 조절
        else if (
            PHASE_FOUR_ENDTIME <= GameManager.Instance.PlayTime &&
            GameManager.Instance.PlayTime <= PHASE_FIVE_ENDTIME &&
            _nextStage == 5
            )
        {
            _coolTime = coolTimeTable["Phase5"];
            Debug.Log("Phase5에 들어왔습니다.");
            _nextStage += 1;
        }

        // Phase6 벽 생성 시간 조절
        else if (
            PHASE_FIVE_ENDTIME <= GameManager.Instance.PlayTime &&
            GameManager.Instance.PlayTime <= PHASE_SIX_ENDTIME &&
            _nextStage == 6
            )
        {
            _coolTime = coolTimeTable["Phase6"];
            Debug.Log("Phase6에 들어왔습니다.");
            _nextStage += 1;
        }
    }

    public IEnumerator InstantiateWall()
    {
        while (true)
        {
            EWallDirection eWallDirection = (EWallDirection)Random.Range(CREATABLE_MIN_WALL, CREATABLE_MAX_WALL);
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
