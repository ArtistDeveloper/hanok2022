using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    #region Settings
    [SerializeField] float speed = 10f;
    [SerializeField] float lookSpeed = 100f;

    #endregion Settings

    [SerializeField] Transform spriteTransform;

    Transform target;

    void FixedUpdate()
    {
        Move();
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

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 moveDir = new Vector2(x, y);
        moveDir.Normalize(); // 최대 값을 1로

        if (moveDir == Vector2.zero)
        {
            return;
        }

        transform.Translate(moveDir * speed * Time.deltaTime);
    }
}