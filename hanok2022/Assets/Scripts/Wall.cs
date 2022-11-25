using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 벽에 그림자가 포함되어 있어야 한다.
public class Wall : MonoBehaviour
{
    //[SerializeField] Transform _shadowScale;
    Vector3 _ceter = Vector3.zero;

    // TODO: 벽이 생성되면 플레이어 방향으로 이동하는 기능 구현 필요
    void Start()
    {
        MoveCenter();
    }

    void Update()
    {
        
    }

    void MoveCenter()
    {
        transform.DOMove(_ceter, 2);
    }
}
