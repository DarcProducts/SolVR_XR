using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemCreator : MonoBehaviour
{
    [SerializeField] private GameObject parentSystem;
    [SerializeField] private GameObject celestialSystem;
    [SerializeField] private GameObject celestialOrbitTargets;
    [SerializeField] private GameObject celesialTilts;
    [SerializeField] private GameObject celestialOrbitLines;
    [SerializeField] private GameObject celestialLabels;
    private List<GameObject> createdMoons = new List<GameObject>();
    private List<GameObject> createdTilts = new List<GameObject>();
    private List<GameObject> createdTargets = new List<GameObject>();
    private List<GameObject> createdOrbitObjects = new List<GameObject>();
    private List<GameObject> createdOrbitTiltObjects = new List<GameObject>();
    private List<GameObject> createdLabels = new List<GameObject>();
    private List<GameObject> createdOrbitLines = new List<GameObject>();

    public GameObject GetSystem(string planetName, string[] moonNames)
    {
        if (celestialSystem != null && celesialTilts != null && celestialOrbitLines != null && celestialLabels != null && celestialOrbitTargets != null && parentSystem != null)
        {
            Debug.Log($"{this.gameObject.name} Started!");
            GameObject newSystem = Instantiate(celestialSystem, Vector3.zero, Quaternion.identity, parentSystem.transform);
            newSystem.name = $"{planetName} System";
            GameObject child1 = newSystem.transform.GetChild(0).gameObject;
            if (child1 != null)
                child1.name = $"{planetName} System Belts";
            GameObject child2 = newSystem.transform.GetChild(1).gameObject;
            if (child2 != null)
                child2.name = $"{planetName} System Labels";
            GameObject child3 = newSystem.transform.GetChild(2).gameObject;
            if (child3 != null)
                child3.name = $"{planetName} System Orbit Lines";
            GameObject child4 = newSystem.transform.GetChild(3).gameObject;
            if (child4 != null)
                child4.name = $"{planetName} System Targets";
            GameObject child5 = newSystem.transform.GetChild(4).gameObject;
            if (child5 != null)
                child5.name = $"{planetName} System Moons";

            for (int i = 0; i < moonNames.Length; i++)
            {
                GameObject newMoonTilt = Instantiate(celesialTilts, Vector3.zero, Quaternion.identity, child5.transform);
                createdTilts.Add(newMoonTilt);
                newMoonTilt.name = $"{moonNames[i]} Tilt";
                GameObject newMoon = newMoonTilt.transform.GetChild(0).gameObject;
                if (newMoon != null)
                    newMoon.name = moonNames[i];
                createdMoons.Add(newMoon);

                GameObject newSystemTargets = Instantiate(celestialOrbitTargets, Vector3.zero, Quaternion.identity, child4.transform);
                newSystemTargets.name = $"{moonNames[i]} Orbit Target";

                GameObject tChild1 = newSystemTargets.transform.GetChild(0).gameObject;
                if (tChild1 != null)
                {
                    tChild1.name = $"{moonNames[i]} Orbit Tilt";
                    createdOrbitTiltObjects.Add(tChild1);
                }
                GameObject tChild2 = newSystemTargets.transform.GetChild(0).GetChild(0).gameObject;
                if (tChild2 != null)
                {
                    tChild2.name = $"{moonNames[i]} Orbit Object";
                    createdOrbitObjects.Add(tChild2);
                }
                GameObject tChild3 = newSystemTargets.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
                if (tChild3 != null)
                {
                    tChild3.name = $"{moonNames[i]} Target";
                    createdTargets.Add(tChild3);
                }

                GameObject newLine = Instantiate(celestialOrbitLines, Vector3.zero, Quaternion.identity, child3.transform);
                newLine.name = $"{moonNames[i]} Orbit Line";
                createdOrbitLines.Add(newLine);

                GameObject newLabel = Instantiate(celestialLabels, Vector3.zero, Quaternion.identity, child2.transform);
                newLabel.name = $"{moonNames[i]} Label";
                createdLabels.Add(newLabel);
            }
            return newSystem;
        }
        return null;
    }

    public GameObject[] GetCreatedMoons() => createdMoons.ToArray();

    public GameObject[] GetCreatedTilts() => createdTilts.ToArray();

    public GameObject[] GetCreatedTargets() => createdTargets.ToArray();

    public GameObject[] GetCreatedOrbitObjects() => createdOrbitObjects.ToArray();

    public GameObject[] GetCreatedOrbitTiltObjects() => createdOrbitTiltObjects.ToArray();

    public TMP_Text[] GetCreatedLabels()
    {
        List<TMP_Text> convert = new List<TMP_Text>();
        for (int i = 0; i < createdLabels.Count; i++)
            convert.Add(createdLabels[i].GetComponent<TMP_Text>());
        return convert.ToArray();
    }

    public LineRenderer[] GetCreatedOrbitLines()
    {
        List<LineRenderer> convert = new List<LineRenderer>();
        for (int i = 0; i < createdOrbitLines.Count; i++)
            convert.Add(createdOrbitLines[i].GetComponent<LineRenderer>());
        return convert.ToArray();
    }
}