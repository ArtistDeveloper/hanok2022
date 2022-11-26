using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 벽에 그림자가 포함되어 있어야 한다.
public class Wall : MonoBehaviour
{
    //[SerializeField] Transform _shadowScale;
    Vector3 _ceter = Vector3.zero;
    ShadowData shadowData;

    public void MoveCenter()
    {
        transform.DOMove(_ceter, 2).OnComplete(DestoryWall);
    }

    public void DestoryWall()
    {
        Destroy(this.gameObject);
    }
}
