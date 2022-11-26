using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ���� �׸��ڰ� ���ԵǾ� �־�� �Ѵ�.
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
        transform.DOMove(_ceter, WallSpawner.Instance.WallSpped).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }

    public void ValidateWallShadow()
    {
        bool isDirEqual = _shadowData.Equals(GameManager.Instance.CurrentShadow);
        
        if (isDirEqual)
        {
            // ���ھ� ���?
            SoundManager.Instance.ChangeSFX(SoundManager.ESoundFX.WallBreak);
            Destroy(this.gameObject);
        }
        else
        {
            // ���ӿ���?
            SoundManager.Instance.ChangeSFX(SoundManager.ESoundFX.Demaged);
            Destroy(this.gameObject);
            Debug.Log("���ӿ���");
        }
    }
}
