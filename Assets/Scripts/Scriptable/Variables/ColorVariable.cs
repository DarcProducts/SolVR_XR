using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Variables/New Color Variable")]
public class ColorVariable : ScriptableObject
{
    public UnityAction OnValueChanged;
    [SerializeField] Color color;

    public Color Value 
    {
        get { return color; }
        set
        {
            OnValueChanged?.Invoke();
            color = value;
        }
    }
}
