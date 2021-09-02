using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThumbstickMove : MonoBehaviour
{
    [SerializeField] Transform rig;
    [SerializeField] Transform head;
    [SerializeField] Vector2Variable leftThumbstick;
    [SerializeField] Vector2Variable rightThumbstick;
    [SerializeField] FloatVariable moveSpeed;
    [SerializeField] FloatVariable rotateSpeed;
    [Range(0f, 1f)] [SerializeField] float deadZone;

    void Update()
    {
        if (leftThumbstick != null && rightThumbstick != null)
        {
            if (leftThumbstick.value.y > deadZone)
                MoveInDirection(head.forward, false);
            if (leftThumbstick.value.y < -deadZone)
                MoveInDirection(-head.forward, false);
            if (leftThumbstick.value.x < -deadZone)
                RotateInDirection(Vector3.down);
            if (leftThumbstick.value.x > deadZone)
                RotateInDirection(Vector3.up);

            if (rightThumbstick.value.y > deadZone)
                MoveInDirection(Vector3.up, true);
            if (rightThumbstick.value.y < -deadZone)
                MoveInDirection(Vector3.down, true);
            if (rightThumbstick.value.x < -deadZone)
                MoveInDirection(Vector3.left, false);
            if (rightThumbstick.value.x > deadZone)
                MoveInDirection(Vector3.right, false);
        }
    }

    void MoveInDirection(Vector3 direction, bool halfValue)
    {
        if (head != null && moveSpeed != null && rig != null)
        {
            float mS;
            if (halfValue)
                mS = moveSpeed.Value * .5f;
            else
                mS = moveSpeed.Value;

            rig.Translate(mS * Time.fixedDeltaTime * direction.normalized);
        }
    }

    void RotateInDirection(Vector3 direction)
    {
        if (head != null && rotateSpeed != null && rig != null)
        {
            rig.Rotate(rotateSpeed.Value * Time.fixedDeltaTime * direction.normalized);
        }
    }
}
