using System.Collections;
using TMPro;
using UnityEngine;

public class SolSystem : MonoBehaviour
{
    public GameEvent OrbitLineActivated;
    public GameEvent OrbitLinesDeactivated;
    [SerializeField] BoolVariable isShowingLinesAndLabels;
    [SerializeField] Transform playerTransform;

    [Header("Planet System Information")]
    [SerializeField] BoolVariable isOrbiting;

    [SerializeField] BoolVariable isRotating;
    [SerializeField] GameObject sun;
    [SerializeField] FontAssetVariable font;
    [SerializeField] TMP_Text sunLabel;
    [SerializeField] GameObject[] planets;
    [SerializeField] GameObject[] planetTargets;
    [SerializeField] Vector3[] planetScales;
    [SerializeField] GameObject[] planetTiltObjects;
    [SerializeField] Vector3[] planetTilts;
    [SerializeField] float[] planetRotationSpeeds;
    [SerializeField] Vector3[] planetRotationVectors;
    [SerializeField] GameObject[] planetOrbitObjects;
    [SerializeField] Vector3[] planetOrbitVectors;
    [SerializeField] GameObject[] planetOrbitTiltObjects;
    [SerializeField] Vector3[] planetOrbitTilts;
    [SerializeField] float[] planetOrbitSpeeds;
    [SerializeField] float[] planetDistanceFromSun;
    [SerializeField] LineRenderer[] planetOrbitLines;
    [SerializeField] TMP_Text[] planetLabels;
    [SerializeField] float planetLabelDistance;
    [SerializeField] float planetOrbitLineWidth;
    [SerializeField] int planetLabelFontSize;
    [SerializeField] Material orbitLineMaterial;
    [SerializeField] float labelLineShowDelay;
    [SerializeField] float labelLineHideDelay;
    bool currentlyShowingLabels;

    void Start()
    {
        SetUpPlanets();
        SetUpOrbitLines();
        SetUpLabels();
        DeactivateLabels();
        ShutOffLines();
        if (sunLabel != null)
            sunLabel.gameObject.SetActive(false);
        sunLabel.font = font.font;
    }

    void FixedUpdate()
    {
        if (isOrbiting != null && isRotating != null)
        {
            for (int i = 0; i < planetTargets.Length; i++)
            {
                if (planetTiltObjects[i] != null && planetTargets[i] != null)
                    planetTiltObjects[i].transform.position = planetTargets[i].transform.position;

                if (planetOrbitObjects[i] != null && isOrbiting.Value)
                    planetOrbitObjects[i].transform.Rotate(planetOrbitSpeeds[i] * Time.fixedDeltaTime * planetOrbitVectors[i]);

                if (planetOrbitObjects[i] != null)
                    planetOrbitObjects[i].transform.position = sun.transform.position;

                if (planetOrbitLines[i] != null && sun != null)
                    planetOrbitLines[i].transform.position = sun.transform.position;

                if (planetLabels[i] != null && planets[i] != null && playerTransform != null)
                {
                    planetLabels[i].transform.position = planets[i].transform.parent.position + (Vector3.up * planetLabelDistance);
                    planetLabels[i].transform.rotation = Quaternion.LookRotation(planetLabels[i].transform.position - playerTransform.position);
                }

                if (planets[i] != null && isRotating.Value)
                    planets[i].transform.Rotate(planetRotationSpeeds[i] * Time.fixedDeltaTime * planetRotationVectors[i]);
            }
        }

        if (isShowingLinesAndLabels != null)
        {
            if (isShowingLinesAndLabels.Value && !currentlyShowingLabels)
            {
                ActivateLines();
                currentlyShowingLabels = true;
                sunLabel.gameObject.SetActive(true);
            }
            else if (!isShowingLinesAndLabels.Value && currentlyShowingLabels)
            {
                DeactivateLines();
                currentlyShowingLabels = false;
                if (sunLabel != null)
                    sunLabel.gameObject.SetActive(false);
            }
            if (currentlyShowingLabels)
                sunLabel.transform.rotation = Quaternion.LookRotation(sunLabel.transform.position - playerTransform.position);
        }
    }

    [ContextMenu("Activate Lines")]
    public void ActivateLines() => StartCoroutine(nameof(ActivateOrbitLines));

    IEnumerator ActivateOrbitLines()
    {
        StopCoroutine(nameof(DeactivateOrbitLines));
        for (int i = 0; i < planetOrbitLines.Length; i++)
        {
            planetOrbitLines[i].gameObject.SetActive(true);
            planetLabels[i].gameObject.SetActive(true);
            if (OrbitLineActivated != null)
                OrbitLineActivated?.Invoke();
            yield return new WaitForSeconds(labelLineShowDelay);
        }
    }

    [ContextMenu("Deactivate Lines")]
    public void DeactivateLines() => StartCoroutine(nameof(DeactivateOrbitLines));

    IEnumerator DeactivateOrbitLines()
    {
        StopCoroutine(nameof(ActivateOrbitLines));
        for (int i = planetOrbitLines.Length - 1; i > -1; i--)
        {
            planetLabels[i].gameObject.SetActive(false);
            planetOrbitLines[i].gameObject.SetActive(false);
            yield return new WaitForSeconds(labelLineHideDelay);
        }
        if (OrbitLinesDeactivated != null)
            OrbitLinesDeactivated?.Invoke();
    }

    void SetUpPlanets()
    {
        if (planets != null)
        {
            for (int i = 0; i < planets.Length; i++)
            {
                if (i < planetTiltObjects.Length && i < planetDistanceFromSun.Length && i < planetTilts.Length)
                {
                    if (planetTiltObjects[i] != null)
                    {
                        planetTiltObjects[i].transform.position = new Vector3(planetDistanceFromSun[i], planetTiltObjects[i].transform.position.y,
                        planetTiltObjects[i].transform.position.z);
                        planetTiltObjects[i].transform.rotation = Quaternion.Euler(planetTilts[i]);
                    }
                }

                if (planets[i] != null)
                    planets[i].transform.position = planets[i].transform.parent.position;

                if (i < planetTargets.Length)
                    if (planetTargets[i] != null)
                        planetTargets[i].transform.position = new Vector3(planetDistanceFromSun[i], planetTargets[i].transform.position.y,
                            planetTargets[i].transform.position.z);

                if (i < planetScales.Length)
                    if (planets[i] != null)
                        planets[i].transform.localScale = planetScales[i];

                if (i < planetOrbitTiltObjects.Length && i < planetOrbitLines.Length && i < planetOrbitTilts.Length)
                {
                    if (planetOrbitTiltObjects[i] != null && planetOrbitLines[i] != null)
                    {
                        Vector3 newStartOrbitLoc = new Vector3(planetOrbitTilts[i].x, Random.Range(-180f, 180f), planetOrbitTilts[i].z);
                        planetOrbitTiltObjects[i].transform.rotation = Quaternion.Euler(newStartOrbitLoc);
                        planetOrbitLines[i].transform.rotation = Quaternion.Euler(planetOrbitTilts[i]);
                    }
                }
            }
        }
    }

    void SetUpOrbitLines()
    {
        if (orbitLineMaterial != null && planetOrbitLines != null)
        {
            for (int i = 0; i < planetOrbitLines.Length; i++)
            {
                if (planetTargets[i] != null && planetOrbitLines[i] != null)
                {
                    Utility.DrawCircle(planetOrbitLines[i], planetDistanceFromSun[i], planetOrbitLineWidth);
                    planetOrbitLines[i].material = orbitLineMaterial;
                    planetOrbitLines[i].startWidth = planetOrbitLineWidth;
                    planetOrbitLines[i].endWidth = planetOrbitLineWidth;
                }
            }
        }
    }

    void SetUpLabels()
    {
        if (planetLabels != null)
        {
            for (int i = 0; i < planets.Length; i++)
            {
                if (i < planetLabels.Length && i < planetTilts.Length)
                {
                    if (planets[i] != null && planetLabels[i] != null && planetTilts[i] != null)
                    {
                        planetLabels[i].font = font.font;
                        planetLabels[i].transform.position = planetTilts[i] + Vector3.up * planetLabelDistance;
                        planetLabels[i].fontSize = planetLabelFontSize;
                        planetLabels[i].text = planets[i].name;
                    }
                }
            }
        }
    }

    public void DeactivateLabels()
    {
        for (int i = 0; i < planetLabels.Length; i++)
        {
            if (planetLabels[i] != null)
                planetLabels[i].gameObject.SetActive(false);
        }
    }

    public void ShutOffLines()
    {
        for (int i = 0; i < planetLabels.Length; i++)
        {
            if (planetOrbitLines[i] != null)
                planetOrbitLines[i].gameObject.SetActive(false);
        }
    }
}