using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;


public class WallSpawner : MonoBehaviour
{
    #region Filed

    [SerializeField] float _wallArrivalSpeed = 0;
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

    // 생성 가능한 벽의 개수
    const int CREATABLE_MIN_WALL = 0;
    const int CREATABLE_MAX_WALL = 6;

    // 벽의 그림자 스케일
    const float MIN_SCALE = 0.5f;
    const float MAX_SCALE = 0.6f;

#if UNITY_EDITOR
    int _wallIndex = 0;
    [SerializeField] Transform _wallCreationTarget;
#endif

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
        LeftTop,
        Left,
        LeftBottom,
        RightTop,
        Right,
        RightBottom,
        None
    }

    //public enum EWallDirection
    //{
    //    LeftTop,
    //    Left,
    //    LeftBottom,
    //    RightTop,
    //    Right,
    //    RightBottom
    //}


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

    public float WallArrivalSpeed { get => _wallArrivalSpeed; }

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
        //StartCoroutine(InstantiateWall()); // 원본 
        StartCoroutine(IntantiateWallTest()); // 테스트 코드, 생성 위치 고정
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

            var wallObject = Instantiate(_wallPrefab, wallPosition, Quaternion.Euler(wallRotation));
            Wall wallComponent = wallObject.GetComponent<Wall>();
            
            wallComponent.MoveCenter();

            ShadowData shadowData = new ShadowData();
            shadowData.Direct = eWallDirection;
            shadowData.Scale = Random.Range(MIN_SCALE, MAX_SCALE);

            wallComponent.ShadowData = shadowData;

#if UNITY_EDITOR
            wallObject.name = "Wall_" + _wallIndex;
            _wallIndex += 1;
#endif

            yield return MyUtil.WaitForSeconds(_coolTime);
        }
    }


    Vector3 DecideWallPosition(EWallDirection eWallDirection)
    {
        Vector3 wallPosition;

        switch (eWallDirection)
        {
            case EWallDirection.LeftTop:
                wallPosition = wallCreatedPositions[(int)EWallDirection.LeftTop].transform.position;
                break;
            case EWallDirection.Left:
                wallPosition = wallCreatedPositions[(int)EWallDirection.Left].transform.position;
                break;
            case EWallDirection.LeftBottom:
                wallPosition = wallCreatedPositions[(int)EWallDirection.LeftBottom].transform.position;
                break;
            case EWallDirection.RightTop:
                wallPosition = wallCreatedPositions[(int)EWallDirection.RightTop].transform.position;
                break;
            case EWallDirection.Right:
                wallPosition = wallCreatedPositions[(int)EWallDirection.Right].transform.position;
                break;
            case EWallDirection.RightBottom:
                wallPosition = wallCreatedPositions[(int)EWallDirection.RightBottom].transform.position;
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
            case EWallDirection.LeftTop:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.LeftTop].transform.eulerAngles;
                break;
            case EWallDirection.Left:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.Left].transform.eulerAngles;
                break;
            case EWallDirection.LeftBottom:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.LeftBottom].transform.eulerAngles;
                break;
            case EWallDirection.RightTop:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.RightTop].transform.eulerAngles;
                break;
            case EWallDirection.Right:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.Right].transform.eulerAngles;
                break;
            case EWallDirection.RightBottom:
                wallEulerAngles = wallCreatedPositions[(int)EWallDirection.RightBottom].transform.eulerAngles;
                break;
            default:
                wallEulerAngles = Vector3.zero;
                break;
        }

        return wallEulerAngles;
    }

#if UNITY_EDITOR
    void TestPlay()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
    }

    public IEnumerator IntantiateWallTest()
    {
        while (true)
        {
            EWallDirection eWallDirection = (EWallDirection)Random.Range(CREATABLE_MIN_WALL, CREATABLE_MAX_WALL);

            Vector3 wallPosition = _wallCreationTarget.position;

            var wallObject = Instantiate(_wallPrefab, wallPosition, Quaternion.identity);
            Wall wallComponent = wallObject.GetComponent<Wall>();

            wallComponent.MoveCenter();

            ShadowData shadowData = new ShadowData();
            shadowData.Direct = eWallDirection;
            shadowData.Scale = Random.Range(MIN_SCALE, MAX_SCALE);

            wallComponent.ShadowData = shadowData;

            wallObject.name = "Wall_" + _wallIndex;
            _wallIndex += 1;

            yield return MyUtil.WaitForSeconds(_coolTime);
        }
    }
#endif
}
