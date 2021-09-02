using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Variables/New Material Variable")]
public class MaterialVariable : ScriptableObject
{
    public UnityAction OnValueChanged;
    [SerializeField] Material material;

    public Material Value
    {
        get { return material; }
        set
        {
            OnValueChanged?.Invoke();
            material = value;
        }
    }
}
