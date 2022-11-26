using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 벽에 그림자가 포함되어 있어야 한다.
public class Wall : MonoBehaviour
{
    [SerializeField] Transform _shadowTransform;
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

        _shadowTransform.localScale = new Vector3(_shadowTransform.localScale.x, _shadowData.Scale, _shadowTransform.localScale.z);
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
