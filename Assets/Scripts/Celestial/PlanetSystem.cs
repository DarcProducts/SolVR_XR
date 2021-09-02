using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlanetSystem : MonoBehaviour
{
    public static UnityAction<PlanetSystem> LinesActivated;
    public GameEvent OrbitLineActivated;
    public GameEvent OrbitLinesDeactivated;
    SystemCreator systemCreator;
    GameObject thisSystem;
    bool linesActive;
    [SerializeField] float disableLineDistance = 0;
    [SerializeField] Transform playerTransform;
    [SerializeField] BoolVariable isOrbiting;
    [SerializeField] BoolVariable isRotating;
    [SerializeField] BoolVariable unableToDisable;
    [SerializeField] GameObject planet;
    [SerializeField] Material moonDefaultMat;
    [SerializeField] string[] moonNames;
    GameObject[] moons = new GameObject[0];
    GameObject[] moonTargets = new GameObject[0];
    [SerializeField] Vector3[] moonScales;
    GameObject[] moonTiltObjects = new GameObject[0];
    [SerializeField] Vector3[] moonTilts;
    [SerializeField] float[] moonRotationSpeeds;
    [SerializeField] Vector3[] moonRotationVectors;
    GameObject[] moonOrbitObjects = new GameObject[0];
    [SerializeField] Vector3[] moonOrbitVectors;
    GameObject[] moonOrbitTiltObjects = new GameObject[0];
    [SerializeField] Vector3[] moonOrbitTilts;
    [SerializeField] float[] moonOrbitSpeeds;
    [SerializeField] float[] moonDistanceFromPlanet;
    LineRenderer[] moonOrbitLines = new LineRenderer[0];
    [SerializeField] Material[] moonMaterials = new Material[0];
    [SerializeField] ObjectData[] objectData = new ObjectData[0];

    [Header("Label Stuff")]
    TMP_Text[] moonLabels = new TMP_Text[0];
    [SerializeField] FontAssetVariable font;
    [SerializeField] FloatVariable moonLabelDistance;
    [SerializeField] FloatVariable moonOrbitLineWidth;
    [SerializeField] IntVariable moonLabelFontSize;
    [SerializeField] MaterialVariable orbitLineMaterial;
    [SerializeField] float labelLineShowDelay;
    [SerializeField] float labelLineHideDelay;

    [Header("Asteroid Belt Stuff")]
    [SerializeField] private bool hasAsteroidRingSystem;

    [SerializeField] GameObject asteroid;
    [SerializeField] GameObject[] asteroidBelts;
    [SerializeField] int[] asteroidsToSpawn;
    [SerializeField] Vector3[] asteroidBeltsTilts;
    [SerializeField] Vector2[] asteroidMinMaxScale;
    [SerializeField] float[] asteroidBeltThickness;
    [SerializeField] float[] asteroidBeltsRadius;
    [SerializeField] Vector2[] asteroidBeltsMinMaxHeight;
    [SerializeField] float[] asteroidBeltRotationSpeed;

    [Header("For Replacing Planet")]
    [SerializeField] bool replacePlanet = false;
    [SerializeField] GameObject planetLabelForReplace;

    private void Awake()
    {
        systemCreator = GetComponent<SystemCreator>();
        if (systemCreator != null)
            thisSystem = systemCreator.GetSystem(planet.name, moonNames);
    }

    void Start()
    {
        if (thisSystem != null)
        {
            moonTargets = systemCreator.GetCreatedTargets();
            moonTiltObjects = systemCreator.GetCreatedTilts();
            moonOrbitTiltObjects = systemCreator.GetCreatedOrbitTiltObjects();
            moonOrbitObjects = systemCreator.GetCreatedOrbitObjects();

            SetUpOrbitLines();
            SetUpMoons();
            SetUpLabels();
        }
        if (replacePlanet && planet != null && planetLabelForReplace != null)
        {
            planet.SetActive(false);
            planetLabelForReplace.SetActive(false);
        }
        if (hasAsteroidRingSystem)
            SetUpAsteroidBelts();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < moonTargets.Length; i++)
        {
            if (moonTiltObjects[i] != null && moonTargets[i] != null)
                moonTiltObjects[i].transform.position = moonTargets[i].transform.position;

            if (isOrbiting.Value && moonOrbitObjects[i] != null)
                moonOrbitObjects[i].transform.Rotate(moonOrbitSpeeds[i] * Time.fixedDeltaTime * moonOrbitVectors[i]);

            if (isRotating.Value && moons[i] != null)
                moons[i].transform.Rotate(moonRotationSpeeds[i] * moonRotationVectors[i] * Time.fixedDeltaTime);

            if (i < moonLabels.Length && i < moons.Length && playerTransform != null && moonLabelDistance != null)
            {
                moonLabels[i].transform.rotation = Quaternion.LookRotation(moonLabels[i].transform.position - playerTransform.position);
                moonLabels[i].transform.position = moons[i].transform.parent.position + (Vector3.up * moonLabelDistance.Value);
            }

            if (moonOrbitObjects[i] != null)
                moonOrbitObjects[i].transform.position = planet.transform.position;

            if (moonOrbitLines[i] != null && planet != null)
                moonOrbitLines[i].transform.position = planet.transform.position;
        }

        if (hasAsteroidRingSystem && planet != null)
        {
            for (int i = 0; i < asteroidBelts.Length; i++)
            {
                if (asteroidBelts[i] != null)
                {
                    asteroidBelts[i].transform.position = planet.transform.position;
                    if (isOrbiting.Value)
                        asteroidBelts[i].transform.Rotate(asteroidBeltRotationSpeed[i] * Time.fixedDeltaTime * Vector3.up, Space.World);
                }
            }
        }

        if (linesActive && disableLineDistance > 0 && !unableToDisable.Value)
        {
            if (Vector3.Distance(planet.transform.position, playerTransform.position) > disableLineDistance)
                DeactivateLines();
        }
    }

    [ContextMenu("Activate Lines")]
    public void ActivateLines() => StartCoroutine(nameof(ActivateOrbitLines));

    IEnumerator ActivateOrbitLines()
    {
        StopCoroutine(nameof(DeactivateOrbitLines));
        LinesActivated?.Invoke(this);
        linesActive = true;
        for (int i = 0; i < moonOrbitLines.Length; i++)
        {
            moonOrbitLines[i].gameObject.SetActive(true);
            moonLabels[i].gameObject.SetActive(true);
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
        linesActive = false;
        if (OrbitLinesDeactivated != null)
            OrbitLinesDeactivated?.Invoke();
        for (int i = moonOrbitLines.Length - 1; i > -1; i--)
        {
            moonLabels[i].gameObject.SetActive(false);
            moonOrbitLines[i].gameObject.SetActive(false);
            yield return new WaitForSeconds(labelLineHideDelay);
        }
    }

    void SetUpMoons()
    {
        if (systemCreator != null)
            moons = systemCreator.GetCreatedMoons();
        if (moons != null)
        {
            for (int i = 0; i < moons.Length; i++)
            {
                if (moons[i] != null)
                {
                    if (i < objectData.Length)
                    {
                        DataHolder dH = moons[i].GetComponent<DataHolder>();
                        if (dH != null && objectData[i] != null)
                            dH.SetObjectData(objectData[i]);
                    }
                    moons[i].layer = (int)planet.layer;
                    moons[i].transform.position = moons[i].transform.parent.position;
                }

                if (i < moonTargets.Length)
                    if (moonTargets[i] != null)
                        moonTargets[i].transform.position = new Vector3(moonDistanceFromPlanet[i], moonTargets[i].transform.position.y,
                            moonTargets[i].transform.position.z);

                if (i < moonScales.Length)
                    if (moons[i] != null)
                        moons[i].transform.localScale = moonScales[i];

                if (i < moonOrbitTiltObjects.Length && i < moonOrbitLines.Length && i < moonOrbitTilts.Length)
                {
                    if (moonOrbitTiltObjects[i] != null && moonOrbitLines[i] != null)
                    {
                        moonOrbitTiltObjects[i].transform.rotation = Quaternion.Euler(moonOrbitTilts[i]);
                        moonOrbitLines[i].transform.rotation = Quaternion.Euler(moonOrbitTilts[i]);
                    }
                }

                if (i < moonOrbitObjects.Length)
                    if (moonOrbitObjects[i] != null)
                        moonOrbitObjects[i].transform.rotation *= Quaternion.Euler(0, Random.Range(-180f, 180f), 0);

                if (i < moonOrbitSpeeds.Length) /// --------------------------------------------------<<<< // remove random speeds, just for testing
                    moonOrbitSpeeds[i] = Random.Range(2, 11); /// ------------------------------------<<<<
                if (i < moonTiltObjects.Length && i < moonDistanceFromPlanet.Length && i < moonTilts.Length)
                {
                    if (moonTiltObjects[i] != null)
                    {
                        moonTiltObjects[i].transform.position = new Vector3(moonDistanceFromPlanet[i], moonTiltObjects[i].transform.position.y,
                        moonTiltObjects[i].transform.position.z);
                        moonTiltObjects[i].transform.rotation = Quaternion.Euler(moonTilts[i]);
                    }
                }

                if (i < moonMaterials.Length && i < moons.Length)
                {
                    if (moonMaterials[i] != null && moons[i] != null)
                    {
                        if (moonMaterials[i] != null)
                            moons[i].GetComponentInChildren<Renderer>().material = moonMaterials[i];
                        else if (moonMaterials[i] == null && moonDefaultMat != null)
                            moons[i].GetComponentInChildren<Renderer>().material = moonDefaultMat;
                    }
                }
            }
        }
    }

    void SetUpOrbitLines()
    {
        if (systemCreator != null)
            moonOrbitLines = systemCreator.GetCreatedOrbitLines();
        if (orbitLineMaterial != null && moonOrbitLineWidth != null)
        {
            for (int i = 0; i < moonOrbitLines.Length; i++)
            {
                if (moonOrbitLines[i] != null && i < moonOrbitLines.Length && i < moonDistanceFromPlanet.Length)
                {
                    Utility.DrawCircle(moonOrbitLines[i], moonDistanceFromPlanet[i], moonOrbitLineWidth.Value);
                    moonOrbitLines[i].material = orbitLineMaterial.Value;
                    moonOrbitLines[i].startWidth = moonOrbitLineWidth.Value;
                    moonOrbitLines[i].endWidth = moonOrbitLineWidth.Value;
                    moonOrbitLines[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void SetUpLabels()
    {
        if (systemCreator != null)
            moonLabels = systemCreator.GetCreatedLabels();
        if (moonLabels != null && moonLabelDistance != null && moonLabelFontSize != null)
        {
            for (int i = 0; i < moons.Length; i++)
            {
                if (i < moonLabels.Length && i < moonTilts.Length)
                {
                    if (moons[i] != null && moonLabels[i] != null && moonTilts[i] != null)
                    {
                        moonLabels[i].font = font.font;
                        moonLabels[i].transform.position = moonTilts[i] + Vector3.up * moonLabelDistance.Value;
                        moonLabels[i].fontSize = moonLabelFontSize.Value;
                        moonLabels[i].text = moons[i].name;
                        moonLabels[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    void SetUpAsteroidBelts()
    {
        for (int i = 0; i < asteroidBelts.Length; i++)
        {
            if (asteroidBelts[i] != null)
                SpawnAsteroids(i);
        }
    }

    void SpawnAsteroids(int i)
    {
        int currentAsteroids = 0;
        do
        {
            GameObject myAsteroid = Instantiate(asteroid, Utility.RandomAsteroidCircle(planet.transform.localPosition, asteroidBeltsRadius[i], asteroidBeltsMinMaxHeight, asteroidBeltThickness, i),
                Quaternion.identity, asteroidBelts[i].transform);
            myAsteroid.transform.LookAt(-Vector3.zero);

            float newScale = UnityEngine.Random.Range(asteroidMinMaxScale[i].x, asteroidMinMaxScale[i].y);
            myAsteroid.transform.localScale = new Vector3(newScale, newScale, newScale);
            currentAsteroids++;
        }
        while (currentAsteroids < asteroidsToSpawn[i]);

        for (int aR = 0; aR < asteroidBelts.Length; aR++)
        {
            if (asteroidBelts[aR] != null)
                asteroidBelts[aR].transform.rotation = Quaternion.Euler(asteroidBeltsTilts[aR]);
        }
    }

    public GameObject GetThisSystem() => thisSystem;
}