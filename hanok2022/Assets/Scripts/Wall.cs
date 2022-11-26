using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

// ���� �׸��ڰ� ���ԵǾ� �־�� �Ѵ�.
public class Wall : MonoBehaviour
{
    [SerializeField] Transform _wallShadowTransform;
    [SerializeField] Transform _humanShadowArrivalTransform;
    [SerializeField] GameObject particle;

    Vector3 _ceter = Vector3.zero;
    ShadowData _shadowData;
    Transform _target;
    

    public ShadowData ShadowData
    {
        get => _shadowData;
        set
        {
            _shadowData = value;
        }
    }

    public Transform Target { get => _target; }

    void Start()
    {
        _target = GameManager.Instance.TargetTransform;
        Debug.Log("_shadowData.Direct" + _shadowData.Direct);
        Debug.Log(string.Format("{0:0.00}", _shadowData.Scale));

        _wallShadowTransform.localScale = new Vector3(_wallShadowTransform.localScale.x, _shadowData.Scale, _wallShadowTransform.localScale.z);
    }

    // Strech�Լ� ȣ�⿹��: StrechBetween(lightTransform.position, target.position);
    void FixedUpdate()
    {
        StretchShadowBetween(_humanShadowArrivalTransform.position, Target.position);

        if (ShadowData.Direct == GameManager.Instance.CurrentShadow.Direct)
        {
            ActivateShadow();
        }
        else
        {
            DeactivateShadow();
        }
    }

    public void StretchShadowBetween(Vector2 point1, Vector2 point2)
    {
        SpriteRenderer render = _humanShadowArrivalTransform.GetComponent<SpriteRenderer>();
        float spriteSize = render.sprite.rect.height / render.sprite.pixelsPerUnit;

        Vector3 scale = _humanShadowArrivalTransform.localScale;
        scale.y = Vector3.Distance(point1, point2) / spriteSize;
        _humanShadowArrivalTransform.localScale = scale;
    }

    public void MoveCenter()
    {
        transform.DOMove(_ceter, WallSpawner.Instance.WallArrivalSpeed).OnComplete(() =>
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
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
        {
            // ���ӿ���?
            SoundManager.Instance.ChangeSFX(SoundManager.ESoundFX.Damaged);
            Destroy(this.gameObject);
            Debug.Log("���ӿ���");
        }
    }

    public void ActivateShadow()
    {
        _humanShadowArrivalTransform.gameObject.SetActive(true);
    }

    public void DeactivateShadow()
    {
        _humanShadowArrivalTransform.gameObject.SetActive(false);
    }
}
