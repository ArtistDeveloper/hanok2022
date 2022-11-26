using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    #region Settings
    [SerializeField] float horizontalSpeed = 10f;
    [SerializeField] float verticalSpeed = 10f;

    [SerializeField] float zoomMin = -1f;
    [SerializeField] float zoomMax = 3f;

    [SerializeField] float lookSpeed = 100f;

    #endregion Settings

    [SerializeField] Transform spriteTransform;

    Transform target;

    bool isReadyFX = true;

    void FixedUpdate()
    {
        //Move();
        Orbit();
        LookAtTarget();
    }

    void LookAtTarget()
    {
        target ??= GameManager.Instance.TargetTransform;

        if (target != null)
        {
            Vector2 direction = new Vector2(spriteTransform.position.x - target.position.x, spriteTransform.position.y - target.position.y);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(spriteTransform.rotation, angleAxis, lookSpeed * Time.deltaTime);
            spriteTransform.rotation = rotation;
        }
    }

    void Orbit()
    {
        target ??= GameManager.Instance.TargetTransform;

        if (target != null)
        {
            float h = Input.GetAxis("Horizontal");
            if (h != 0)
            {
                Vector3 axis = h < 0 ? Vector3.forward : Vector3.back;
                transform.RotateAround(target.position, axis, horizontalSpeed * Time.deltaTime);
            }

            float v = Input.GetAxis("Vertical");
            if (v != 0)
            {
                float dist = spriteTransform.localPosition.y - (v * verticalSpeed * Time.deltaTime);
                float clampedDist = Mathf.Clamp(dist, zoomMin, zoomMax);

                Vector3 tempVector = spriteTransform.localPosition;
                tempVector.y = clampedDist;
                spriteTransform.localPosition = tempVector;
            }

            if (isReadyFX == true && (v != 0 || h != 0))
            {
                SoundManager.Instance.ChangeSFX(SoundManager.ESoundFX.LightMove);
                isReadyFX = false;
            }

            isReadyFX = (v == 0 && h == 0);
        }
    }

    //void Move()
    //{
    //    float x = Input.GetAxis("Horizontal");
    //    float y = Input.GetAxis("Vertical");

    //    Vector2 moveDir = new Vector2(x, y);
    //    moveDir.Normalize(); // 최대 값을 1로

    //    if (moveDir == Vector2.zero)
    //    {
    //        return;
    //    }

    //    transform.Translate(moveDir * speed * Time.deltaTime);
    //}

    public WallSpawner.EWallDirection GetEDirection()
    {
        float angle = transform.rotation.eulerAngles.z;
        WallSpawner.EWallDirection result = WallSpawner.EWallDirection.None;

        if (22.5 < angle && angle < 67.5)
        {
            result = WallSpawner.EWallDirection.RightBottom;
        }
        else if (67.5 < angle && angle < 112.5)
        {
            result = WallSpawner.EWallDirection.Right;
        }
        else if (112.5 < angle && angle < 157.5)
        {
            result = WallSpawner.EWallDirection.RightTop;
        }
        else if (202.5 < angle && angle < 247.5)
        {
            result = WallSpawner.EWallDirection.LeftTop;
        }
        else if (247.5 < angle && angle < 292.5)
        {
            result = WallSpawner.EWallDirection.Left;
        }
        else if (292.5 < angle && angle < 337.5)
        {
            result = WallSpawner.EWallDirection.LeftBottom;
        }
        else
        {
            result = WallSpawner.EWallDirection.None;
        }

        //Debug.Log($"angle: {angle}, dir: {result}");

        return result;
    }

    public WallSpawner.EWallDirection GetEDirection2()
    {
        float angle = transform.rotation.eulerAngles.z;
        WallSpawner.EWallDirection result = WallSpawner.EWallDirection.LeftTop;

        if (30 < angle && angle < 90)
        {
            result = WallSpawner.EWallDirection.Right;
        }
        else if (90 < angle && angle < 150)
        {
            result = WallSpawner.EWallDirection.Left;
        }
        else if (150 < angle && angle < 210)
        {
            result = WallSpawner.EWallDirection.LeftTop;
        }
        else if (210 < angle && angle < 270)
        {
            result = WallSpawner.EWallDirection.LeftBottom;
        }
        else if (270 < angle && angle < 330)
        {
            result = WallSpawner.EWallDirection.RightBottom;
        }
        else // (0 < angle && angle < 30 || 330 < angle && angle < 360)
        {
            result = WallSpawner.EWallDirection.RightTop;
        }

        //Debug.Log($"angle: {angle}, dir: {result}");

        return result;
    }
}