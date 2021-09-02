using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Variables/New Int Variable")]
public class IntVariable : ScriptableObject
{
    public UnityAction OnValueChanged;
    [SerializeField] int value;
    public bool resetOnDisable;

    void OnDisable()
    {
        if (resetOnDisable)
            value = 0;
    }

    public int Value
    {
        get { return value; }
        set
        {
            OnValueChanged?.Invoke();
            this.value = value;
        }
    }
}
