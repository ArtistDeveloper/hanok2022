using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ���� �׸��ڰ� ���ԵǾ� �־�� �Ѵ�.
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
