using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Note: the only ones that work on TMPro are Lighten, Additive, Soft Additive, Linear Burn
public enum BlendingMode
{
    Normal,
    Darken,
    Multiply,
    Lighten,
    Additive,
    Soft_Additive,
    Linear_Burn,
    Overlay,

}
public class BlendModeController : MonoBehaviour
{
    public BlendingMode blendMode;
    public Material material;


    void OnValidate()
    {
        BlendMode srcMode = BlendMode.SrcAlpha,
        dstMode = BlendMode.OneMinusSrcAlpha;
        BlendOp op = 0;
        if (material == null)
        {
            material = GetComponent<Renderer>().sharedMaterial;
        }

        switch (blendMode)
        {
            case BlendingMode.Darken:
                {
                    srcMode = BlendMode.One;
                    dstMode = BlendMode.One;
                    op = BlendOp.Min;
                }
                break;
            case BlendingMode.Multiply:
                {
                    srcMode = BlendMode.DstColor;
                    dstMode = BlendMode.Zero;
                }
                break;
            case BlendingMode.Lighten:
                {
                    srcMode = BlendMode.One;
                    dstMode = BlendMode.One;
                    op = BlendOp.Max;
                }
                break;
            case BlendingMode.Additive:
                {
                    srcMode = BlendMode.One;
                    dstMode = BlendMode.One;
                }
                break;
            case BlendingMode.Soft_Additive:
                {
                    srcMode = BlendMode.OneMinusDstColor;
                    dstMode = BlendMode.One;
                }
                break;
            case BlendingMode.Linear_Burn:
                {
                    srcMode = BlendMode.One;
                    dstMode = BlendMode.One;
                    op = BlendOp.ReverseSubtract;
                }
                break;
            case BlendingMode.Overlay:
                {
                    srcMode = BlendMode.One;
                    dstMode = BlendMode.SrcColor;
                }
                break;
            default: break;
        }
        material.SetInt("_SrcMode", (int)srcMode);
        material.SetInt("_DstMode", (int)dstMode);
        material.SetInt("_BlendOp", (int)op);
    }
}
