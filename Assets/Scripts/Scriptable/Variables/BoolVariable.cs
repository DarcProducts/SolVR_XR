using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewBoolVariable", menuName = "Variables/New Bool Variable")]
public class BoolVariable : ScriptableObject 
{ 
    [SerializeField] bool value; 
    public UnityAction OnValueChanged;
    public UnityAction ValuedChangeTrue;
    public UnityAction ValueChangeFalse;
    public bool resetFalseOnDisable;
    public bool resetTrueOnDisable;
    public bool Value { 
        get { return value; } 
        set {
            OnValueChanged?.Invoke();
            if (value.Equals(true))
                ValuedChangeTrue?.Invoke();
            else if (value.Equals(false))
                ValueChangeFalse?.Invoke();
            this.value = value;
        } 
    }

    void OnDisable()
    {
        if (resetFalseOnDisable)
            value = false;
        if (resetTrueOnDisable)
            value = true;
    }
}
