using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ���� �׸��ڰ� ���ԵǾ� �־�� �Ѵ�.
public class Wall : MonoBehaviour
{
    //[SerializeField] Transform _shadowScale;
    Vector3 _ceter = Vector3.zero;

    // TODO: ���� �����Ǹ� �÷��̾� �������� �̵��ϴ� ��� ���� �ʿ�
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
