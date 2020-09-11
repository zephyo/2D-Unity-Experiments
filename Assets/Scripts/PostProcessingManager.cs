using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
/// <summary>
/// Manager for post processing effects.
/// </summary>
public class PostProcessingManager : MonoBehaviour
{
    public VolumeProfile volumeProfile;
    private ChromaticAberration aberration;
    private DepthOfField dof;

    private void Awake()
    {
        if (!volumeProfile.TryGet(out aberration))
        {
            Debug.LogError("Aberration doesn't exist");
        }
        if (!volumeProfile.TryGet(out dof))
        {
            Debug.LogError("Depth of Field doesn't exist");
        }
    }

    public void SetAberration(float val)
    {
        aberration.intensity.Override(val);
    }

    public void SetDOF(float val)
    {
        if (dof.mode == DepthOfFieldMode.Bokeh)
        {
            dof.focalLength.Override(val);
        }
        else if (dof.mode == DepthOfFieldMode.Gaussian)
        {
            dof.gaussianMaxRadius.Override(val);
        }
    }
    public float GetDOF()
    {
        if (dof.mode == DepthOfFieldMode.Bokeh)
        {
            return dof.focalLength.GetValue<float>();
        }
        else if (dof.mode == DepthOfFieldMode.Gaussian)
        {
            return dof.gaussianMaxRadius.GetValue<float>();
        }
        return 0;
    }
}