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

    [SerializeField] Transform bodyTransform;
    [SerializeField] Transform lightTransform;
    [SerializeField] Transform circleLineTransform;

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
            Vector2 direction = new Vector2(bodyTransform.position.x - target.position.x, bodyTransform.position.y - target.position.y);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(bodyTransform.rotation, angleAxis, lookSpeed * Time.deltaTime);
            bodyTransform.rotation = rotation;

            StrechBetween(lightTransform.position, target.position);
        }
    }

    void StrechBetween(Vector2 point1, Vector2 point2)
    {
        // refer : https://answers.unity.com/questions/1235760/unity-streching-sprite-gameobject-to-fit-two-posit.html
        SpriteRenderer render = lightTransform.GetComponent<SpriteRenderer>();
        float spriteSize = render.sprite.rect.height / render.sprite.pixelsPerUnit;

        Vector3 scale = lightTransform.localScale;
        scale.y = Vector3.Distance(point1, point2) / spriteSize;
        lightTransform.localScale = scale;
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
                float dist = bodyTransform.localPosition.y - (v * verticalSpeed * Time.deltaTime);
                float clampedDist = Mathf.Clamp(dist, zoomMin, zoomMax);

                Vector3 tempVector = bodyTransform.localPosition;
                tempVector.y = clampedDist;
                bodyTransform.localPosition = tempVector;

                StrechCircleLine(bodyTransform.position, target.position);
            }

            if (isReadyFX == true && (v != 0 || h != 0))
            {
                SoundManager.Instance.ChangeSFX(SoundManager.ESoundFX.LightMove);
                isReadyFX = false;
            }

            isReadyFX = (v == 0 && h == 0);
        }
    }

    void StrechCircleLine(Vector2 point1, Vector2 point2)
    {
        SpriteRenderer render = circleLineTransform.GetComponent<SpriteRenderer>();
        float spriteSize = render.sprite.rect.height / render.sprite.pixelsPerUnit;

        Vector3 scale = circleLineTransform.localScale;
        float radius = Vector3.Distance(point1, point2) / spriteSize * 2f; // 연산 값이 원하는 결과의 절반쯤이어서 2배
        scale.y = radius;
        scale.x = radius;
        circleLineTransform.localScale = scale;
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
}