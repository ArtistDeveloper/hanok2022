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
            // 스코어 상승?
            SoundManager.Instance.ChangeSFX(SoundManager.ESoundFX.WallBreak);
            Destroy(this.gameObject);
        }
        else
        {
            // 게임오버?
            SoundManager.Instance.ChangeSFX(SoundManager.ESoundFX.Demaged);
            Destroy(this.gameObject);
            Debug.Log("게임오버");
        }
    }
}
