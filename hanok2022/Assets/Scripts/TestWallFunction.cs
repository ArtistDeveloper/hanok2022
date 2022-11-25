using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWallFunction : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            WallSpawner.Instance.InstantiateWall();
        }
    }
}
