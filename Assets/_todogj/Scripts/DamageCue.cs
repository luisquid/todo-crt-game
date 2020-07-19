using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class DamageCue : MonoBehaviour
{
    public UnityEngine.Rendering.VolumeProfile volumeProfile;

    public ChromaticAberration chromaticProperty;
    public DepthOfField depthProperty;

    public float downSpeed;

    private float chromaticLevel;

    private void Start()
    {
        volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;

        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        if (!volumeProfile.TryGet(out chromaticProperty)) throw new System.NullReferenceException(nameof(chromaticProperty));
        if (!volumeProfile.TryGet(out depthProperty)) throw new System.NullReferenceException(nameof(depthProperty));
    }

    public void AddDepth()
    {
        DepthOfFieldModeParameter test = new DepthOfFieldModeParameter(DepthOfFieldMode.Gaussian);
        depthProperty.mode.overrideState = true;
    }

    public void AddChromatic(float _value)
    {
        chromaticLevel += _value;
        chromaticProperty.intensity.Override(chromaticLevel);
    }

    public void GameOver() => StartCoroutine(CallAberration());

    IEnumerator CallAberration()
    {
        chromaticLevel = 1f;
        while (chromaticLevel > 0f)
        {
            chromaticLevel -= Time.deltaTime * downSpeed;
            chromaticProperty.intensity.Override(chromaticLevel);
            yield return null;
        }
    }
}
