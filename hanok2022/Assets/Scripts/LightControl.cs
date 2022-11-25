using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    #region Settings
    [SerializeField] float horizontalSpeed = 10f;
    [SerializeField] float verticalSpeed = 10f;

    [SerializeField] float zoomMin = -3f;
    [SerializeField] float zoomMax = 0f;

    [SerializeField] float lookSpeed = 100f;

    #endregion Settings

    [SerializeField] Transform spriteTransform;

    Transform target;

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
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
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

                float dist = spriteTransform.localPosition.x + (v * verticalSpeed * Time.deltaTime);
                float clampedDist = Mathf.Clamp(dist, zoomMin, zoomMax);

                Vector3 tempVector = spriteTransform.localPosition;
                tempVector.x = clampedDist;
                spriteTransform.localPosition = tempVector;
            }
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
}