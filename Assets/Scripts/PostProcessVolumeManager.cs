using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessVolumeManager : Singleton<PostProcessVolumeManager>
{

    private UnityEngine.Rendering.PostProcessing.PostProcessVolume postProcessVolume;
    private UnityEngine.Rendering.PostProcessing.ChromaticAberration chromaticAberration;
    private float initialChromaticAberrationValue;
    private float maxChromaticAberrationValue = 1f;

    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume = gameObject.GetComponent<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings<UnityEngine.Rendering.PostProcessing.ChromaticAberration>(out chromaticAberration);
        initialChromaticAberrationValue = chromaticAberration.intensity.value;
        Debug.Log("intensity = " + initialChromaticAberrationValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseChromaticAberration(float value = 0.05f)
    {
        if (chromaticAberration.intensity.value >= maxChromaticAberrationValue || chromaticAberration.intensity.value <= initialChromaticAberrationValue)
            return;

        chromaticAberration.intensity.value += value;
    }

    public void resetChromaticAberration()
    {
        chromaticAberration.intensity.value = initialChromaticAberrationValue;
    }
}
