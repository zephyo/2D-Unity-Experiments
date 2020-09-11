using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Slider modifies original wind magnitudes by slider's value
/// /// </summary>
public class WindController : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    // windzone to original main
    List<(WindZone, float)> zones = new List<(WindZone, float)>();

    // AreaEffector2D to original magnitude
    List<(AreaEffector2D, float)> effector2Ds = new List<(AreaEffector2D, float)>();

    private void Awake()
    {
        WindZone[] z = GameObject.FindObjectsOfType<WindZone>();
        AreaEffector2D[] e = GameObject.FindObjectsOfType<AreaEffector2D>();

        foreach (var a in z)
        {
            zones.Add((a, a.windMain));
        }
        foreach (var b in e)
        {
            effector2Ds.Add((b, b.forceMagnitude));
        }
    }

    void Start()
    {
        slider.onValueChanged.AddListener(OnValueChanged);
        OnValueChanged(slider.value);
    }

    void OnValueChanged(float v)
    {
        foreach (var a in zones)
        {
            a.Item1.windMain = Mathf.Clamp(a.Item2 + v, 0.05f, Mathf.Infinity);
        }
        foreach (var b in effector2Ds)
        {
            b.Item1.forceMagnitude = Mathf.Clamp(b.Item2 + v, 0, Mathf.Infinity);
            b.Item1.forceVariation = 30 * ((slider.value - slider.minValue) / (slider.maxValue - slider.minValue));
        }
    }
}
