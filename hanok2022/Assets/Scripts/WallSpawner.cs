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

    static WallSpawner _instance = null;

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

    public void InstantiateWall()
    {
        // TODO: DecideWallRotation을 호출해서 벽의 각도를 결정한 뒤에, Instantiate를 할 때 벽의 각도를 정해준다.
        EWallDirection eWallDirection = (EWallDirection)Random.Range(0, 6);
        Vector3 wallPosition = DecideWallPosition(eWallDirection);
        Vector3 wallRotation = DecideWallRotation(eWallDirection);

        Instantiate(_wallPrefab, wallPosition, Quaternion.Euler(wallRotation));
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


    // TODO: transform.eulerAngles을 사용하여 벽을 돌려서 생성할 예정.
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
