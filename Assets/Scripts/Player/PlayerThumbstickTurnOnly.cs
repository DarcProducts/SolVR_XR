using UnityEngine;

public class PlayerThumbstickTurnOnly : MonoBehaviour
{
    [SerializeField] Vector2Variable rotateThumbstickValues;
    [SerializeField] FloatVariable playerRotateSpeed;
    [SerializeField] Transform playerRig;

    void OnEnable() => rotateThumbstickValues.OnValueChanged += RotatePlayer;

    void OnDisable() => rotateThumbstickValues.OnValueChanged -= RotatePlayer;

    void RotatePlayer(Vector2 values) => playerRig.Rotate(playerRotateSpeed.Value * Time.fixedDeltaTime * new Vector2(0, values.x));
}
