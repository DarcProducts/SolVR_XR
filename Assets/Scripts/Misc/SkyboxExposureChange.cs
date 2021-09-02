using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxExposureChange : MonoBehaviour
{
    [SerializeField] float exposureSetValue;
    float exposureOriginalValue;

    private void Start() => exposureOriginalValue = RenderSettings.skybox.GetFloat("_Exposure");

    public void ChangeSkyboxExposure() => RenderSettings.skybox.SetFloat("_Exposure", exposureSetValue);

    public void ResetSkyboxExposure() => RenderSettings.skybox.SetFloat("_Exposure", exposureOriginalValue);
}
