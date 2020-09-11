//////////////////////////////////////////////
/// 2DxFX v3 - by VETASOFT 2018 //
//////////////////////////////////////////////


//////////////////////////////////////////////

Shader "Custom/DoodleDraw"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        DoodleUV_Size_1("DoodleUV_Size_1", Range(2, 16)) = 7
        DoodleUV_Frame_1("DoodleUV_Frame_1", Range(1, 16)) = 5
        _LerpUV_Fade_1("_LerpUV_Fade_1", Range(0, 1)) = 0.445
        _SpriteFade("SpriteFade", Range(0, 1)) = 1.0

        // required for UI.Mask
        [HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
        [HideInInspector]_Stencil("Stencil ID", Float) = 0
        [HideInInspector]_StencilOp("Stencil Operation", Float) = 0
        [HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
        [HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
        [HideInInspector]_ColorMask("Color Mask", Float) = 15

    }

    SubShader
    {

        Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
        ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off

        // required for UI.Mask
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            struct appdata_t{
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord  : TEXCOORD0;
                float4 vertex   : SV_POSITION;
                float4 color    : COLOR;
            };

            sampler2D _MainTex;
            float _SpriteFade;
            float DoodleUV_Size_1;
            float DoodleUV_Frame_1;
            float _LerpUV_Fade_1;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color;
                return OUT;
            }


            float2 DoodleUV(float2 p, float Size, float Speed)
            {
                float2 co = p;
                Speed = (floor(_Time * 20 * Speed) / Speed) * Speed;
                co.x = sin((co.x * Size + Speed) * 4);
                co.y = cos((co.y * Size + Speed) * 4);
                p = lerp(p, p + co, 0.0005 * Size);
                return p;
            }
            float4 frag (v2f i) : COLOR
            {
                float2 DoodleUV_1 = DoodleUV(i.texcoord,DoodleUV_Size_1,DoodleUV_Frame_1);
                i.texcoord = lerp(i.texcoord,DoodleUV_1,_LerpUV_Fade_1);
                float4 _MainTex_1 = tex2D(_MainTex,i.texcoord);
                float4 FinalResult = _MainTex_1;
                FinalResult.rgb *= i.color.rgb;
                FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
                return FinalResult;
            }

            ENDCG
        }
    }
    Fallback "Sprites/Default"
}
