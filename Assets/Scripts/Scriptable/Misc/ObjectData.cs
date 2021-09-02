using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "New Object Data")]
public class ObjectData : ScriptableObject
{
    public UnityAction<string[]> DataStream;
    [SerializeField] string objectName;
    [SerializeField] string mass;
    [SerializeField] string diameter;
    [SerializeField] string density;
    [SerializeField] string gravity;
    [SerializeField] string escapeVelocity;
    [SerializeField] string rotationalPeriod;
    [SerializeField] string lengthOfDay;
    [SerializeField] string distanceFromSun;
    [SerializeField] string perihelion;
    [SerializeField] string aphelion;
    [SerializeField] string orbitalPeriod;
    [SerializeField] string orbitalVelocity;
    [SerializeField] string orbitalInclination;
    [SerializeField] string orbitalEccetricity;
    [SerializeField] string obliquityToOrbit;
    [SerializeField] string meanTemperature;
    [SerializeField] string surfacePressure;
    [SerializeField] string numberOfMoons;
    [SerializeField] string hasRingSystem;
    [SerializeField] string hasGlobalMagneticField;
    [TextArea(1, 10)] [SerializeField] string facts;
    [SerializeField] bool isPlanet;
    string data = "";

    void OnEnable()
    {
        if (isPlanet)
        {
            data =
                $"Name:    {objectName}\n\n" +
                $"Mass:    {mass}\n\n" +
                $"Diameter:    {diameter}\n\n" +
                $"Density:    {density}\n\n" +
                $"Gravity:    {gravity}\n\n" +
                $"Escape Velocity:    {escapeVelocity}\n\n" +
                $"Rotational Period:    {rotationalPeriod}\n\n" +
                $"Length Of Day:    {lengthOfDay}\n\n" +
                $"DistanceFromSun:    {distanceFromSun}\n\n" +
                $"Perihelion:    {perihelion}\n\n" +
                $"Aphelion:    {aphelion}\n\n" +
                $"Orbital Period:    {orbitalPeriod}\n\n" +
                $"Orbital Velocity:    {orbitalVelocity}\n\n" +
                $"Orbital Inclination:    {orbitalInclination}\n\n" +
                $"Orbital Eccentricity:    {orbitalEccetricity}\n\n" +
                $"Obliquity To Orbit:    {obliquityToOrbit}\n\n" +
                $"Mean Temperature:    {meanTemperature}\n\n" +
                $"Surface Pressure:    {surfacePressure}\n\n" +
                $"Number Of Moons:    {numberOfMoons}\n\n" +
                $"Has Ring System:    {hasRingSystem}\n\n" +
                $"Has Magnetic Field:    {hasGlobalMagneticField}";
        }
        else
        {
            data =
                $"Name:    {objectName}\n\n" +
                $"Mass:    {mass}\n\n" +
                $"Diameter:    {diameter}\n\n" +
                $"Density:    {density}\n\n" +
                $"Gravity:    {gravity}\n\n" +
                $"Escape Velocity:    {escapeVelocity}\n\n" +
                $"Rotational Period:    {rotationalPeriod}\n\n" +
                $"Length Of Day:    {lengthOfDay}\n\n" +
                $"Distance From Moon:    {distanceFromSun}\n\n" +
                $"Orbital Period:    {orbitalPeriod}\n\n" +
                $"Orbital Velocity:    {orbitalVelocity}\n\n" +
                $"Orbital Inclination:    {orbitalInclination}\n\n" +
                $"Orbital Eccentricity:    {orbitalEccetricity}\n\n" +
                $"Obliquity To Orbit:    {obliquityToOrbit}\n\n" +
                $"Mean Temperature:    {meanTemperature}\n\n" +
                $"Surface Pressure:    {surfacePressure}\n\n" +
                $"Has Magnetic Field:    {hasGlobalMagneticField}";
        }
    }

    public string GetData() => data;

    public string GetFacts() => facts;
}