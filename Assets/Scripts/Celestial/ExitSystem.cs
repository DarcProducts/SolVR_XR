using UnityEngine;
using UnityEngine.Events;

public class ExitSystem : MonoBehaviour
{
    public UnityAction OnEnter;
    public UnityAction OnExit;
    [SerializeField] PlanetSystem planetSystem;

    private void OnTriggerExit(Collider other)
    {
        planetSystem.DeactivateLines();
        OnExit?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        planetSystem.ActivateLines();
        OnEnter?.Invoke();
    }

    public PlanetSystem GetPlanetSystem() => planetSystem;
}