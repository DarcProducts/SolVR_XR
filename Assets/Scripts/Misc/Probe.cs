using TMPro;
using UnityEngine;

public class Probe : MonoBehaviour
{
    [SerializeField] ProbeLauncher launcher;
    [SerializeField] DataPanelController dataPanels;
    [SerializeField] TrailRenderer trail;

    void OnEnable()
    {
        if (trail != null)
            trail.Clear();
    }

    void OnCollisionEnter(Collision collision)
    {
        ExitSystem exit = collision.gameObject.GetComponent<ExitSystem>();
        PlanetSystem system = null;
        DataHolder data = collision.gameObject.GetComponent<DataHolder>();
        if (exit != null)
            system = exit.GetPlanetSystem();
        if (system != null)
            system.ActivateLines();
        if (exit != null && system != null && data != null)
        { 
            dataPanels.ActivatePanels();
            dataPanels.StreamData(data.GetObjectData());
            dataPanels.StreamFacts(data.GetObjectFacts());
        }
        launcher.DisableProbe();
    }
}
