using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtil : MonoBehaviour
{
    public static IEnumerator WaitForSeconds(float time)
    {
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            yield return null;
        }
    }
}
