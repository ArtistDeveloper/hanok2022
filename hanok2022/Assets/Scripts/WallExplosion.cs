using System.Collections;
using UnityEngine;

public class WallExplosion : MonoBehaviour
{
    [SerializeField] float existTime = 1f;

    void Start()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return MyUtil.WaitForSeconds(existTime);

        Destroy(this.gameObject);
    }
}
