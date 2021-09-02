using UnityEngine;

public class SunColorChanger : MonoBehaviour
{
    [SerializeField] BoolVariable activatingBool;
    [SerializeField] ParticleSystem.MinMaxGradient particleColorChange;
    [SerializeField] Color sunMatColor;
    [SerializeField] ParticleSystem sunCorona;
    [SerializeField] ParticleSystem sunAura;
    [SerializeField] Material sunMaterial;
    ParticleSystem.MinMaxGradient coronaGradeOrig;
    ParticleSystem.MinMaxGradient auraGradeOrig;
    ParticleSystem.ColorOverLifetimeModule coronaC;
    ParticleSystem.ColorOverLifetimeModule auraC;

    void OnEnable()
    {
        activatingBool.ValuedChangeTrue += ChangeColorNew;
        activatingBool.ValueChangeFalse += ChangeColorOld;
    }

    void OnDisable()
    {
        activatingBool.ValuedChangeTrue -= ChangeColorNew;
        activatingBool.ValueChangeFalse -= ChangeColorOld;
    }

    void Start()
    {
        coronaC = sunCorona.colorOverLifetime;
        auraC = sunAura.colorOverLifetime;
        coronaGradeOrig = coronaC.color;
    }

    void ChangeColorNew()
    {
        sunMaterial.SetColor("_emissionColor", sunMatColor);
        coronaC.color = particleColorChange.gradient;
        auraC.color = particleColorChange.gradient;
    }

    void ChangeColorOld()
    {
        sunMaterial.SetColor("_emissionColor", Color.white);
        coronaC.color = coronaGradeOrig.gradient;
        auraC.color = auraGradeOrig.gradient;
    }
}