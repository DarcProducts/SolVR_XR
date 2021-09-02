using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Variables/New Float Variable")]
public class FloatVariable : ScriptableObject 
{
    public UnityAction OnValueChanged;
    [SerializeField] float value;
    public bool resetOnDisable;

    void OnDisable()
    {
        if (resetOnDisable)
            value = 0;
    }

    public float Value
    {
        get { return value; }
        set
        {
            OnValueChanged?.Invoke();
            this.value = value;
        }
    }
}
