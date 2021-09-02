using UnityEngine;

public class BoolSwitchOpposite : MonoBehaviour
{
    [SerializeField] BoolVariable activatingBool;
    [SerializeField] BoolVariable[] activatedBools;

    void OnEnable() => activatingBool.OnValueChanged += TriggerSwitch;

    void OnDisable() => activatingBool.OnValueChanged -= TriggerSwitch;

    private void TriggerSwitch()
    {
        if (activatingBool.Value)
        {
            for (int i = 0; i < activatedBools.Length; i++)
                activatedBools[i].Value = false;
        }
        else
        {
            for (int i = 0; i < activatedBools.Length; i++)
                activatedBools[i].Value = true;
        }
    }
}
