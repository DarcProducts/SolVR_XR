using UnityEngine;

public class PlayerFlight : MonoBehaviour
{
    [SerializeField] Transform rig;
    [SerializeField] Transform head;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;
    [SerializeField] BoolVariable leftThrustActive;
    [SerializeField] BoolVariable rightThrustActive;
    [SerializeField] BoolVariable unableToMove;
    [SerializeField] FloatVariable moveSpeed;

    void Update()
    {
        if (!unableToMove.Value)
        {
            if (leftThrustActive.Value)
                MoveInDirection(leftHand);
            if (rightThrustActive.Value)
                MoveInDirection(rightHand);
            rig.position = head.position + -head.localPosition;
        }
    }

    void MoveInDirection(Transform handTransform)
    {
        if (rig != null && handTransform != null)
        {
            Vector3 dir = handTransform.position - head.position;
            rig.Translate(moveSpeed.Value * Time.fixedDeltaTime * dir.normalized);
        }
    }
}
