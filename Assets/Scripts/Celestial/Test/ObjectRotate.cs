using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] Vector3 rotateVector;
    [SerializeField] BoolVariable isRotating;

    void FixedUpdate()
    {
        if (isRotating.Value)
            RotateObject();
    }

    public void RotateObject() => transform.rotation *= Quaternion.Euler(rotateVector);
}