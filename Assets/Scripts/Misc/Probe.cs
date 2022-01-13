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
        if (collision.gameObject.TryGetComponent<ExitSystem>(out ExitSystem exit))
        {
            PlanetSystem system = exit.GetPlanetSystem();
            if (system != null)         
                system.ActivateLines();
            if (exit.TryGetComponent<DataHolder>(out DataHolder data))
            {
                dataPanels.ActivatePanels();
                dataPanels.StreamData(data.GetObjectData());
                dataPanels.StreamFacts(data.GetObjectFacts());
                dataPanels.StreamInfo();
            }
        }
        launcher.DisableProbe();
    }
}
