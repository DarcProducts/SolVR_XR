using UnityEngine;

public class BoolSwitchSimilar : MonoBehaviour
{
    [SerializeField] BoolVariable activatingBool;
    [SerializeField] BoolVariable[] activatedBools;
    [SerializeField] BoolVariable testAgainstBool;

    void OnEnable() => activatingBool.OnValueChanged += TriggerSwitch;

    void OnDisable() => activatingBool.OnValueChanged -= TriggerSwitch;

    private void TriggerSwitch()
    {
        if (!testAgainstBool.Value)
        {
            if (activatingBool.Value)
            {
                for (int i = 0; i < activatedBools.Length; i++)
                    activatedBools[i].Value = true;
            }
            else
            {
                for (int i = 0; i < activatedBools.Length; i++)
                    activatedBools[i].Value = false;
            }
        }
    }
}