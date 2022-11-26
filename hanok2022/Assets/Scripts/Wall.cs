using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ���� �׸��ڰ� ���ԵǾ� �־�� �Ѵ�.
public class Wall : MonoBehaviour
{
    //[SerializeField] Transform _shadowScale;
    Vector3 _ceter = Vector3.zero;
    ShadowData _shadowData;

    public ShadowData ShadowData
    {
        set
        {
            _shadowData = value;
        }
    }

    void Start()
    {
        Debug.Log("_shadowData.Direct" + _shadowData.Direct);
        Debug.Log(string.Format("{0:0.00}", _shadowData.Scale));
    }

    public void MoveCenter()
    {
        transform.DOMove(_ceter, 2).OnComplete(DestoryWall);
    }

    public void DestoryWall()
    {
        Destroy(this.gameObject);
    }
}
