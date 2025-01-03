Shader "Custom/SpriteOutlineFixed"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (1,1,1,1)
        _OutlineThickness("Outline Thickness", Range(0, 10)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _OutlineThickness;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Definir manualmente los desplazamientos en lugar de usar un array
                float2 offsets[8];
                offsets[0] = float2(-1, -1);
                offsets[1] = float2(-1,  0);
                offsets[2] = float2(-1,  1);
                offsets[3] = float2( 0, -1);
                offsets[4] = float2( 0,  1);
                offsets[5] = float2( 1, -1);
                offsets[6] = float2( 1,  0);
                offsets[7] = float2( 1,  1);

                float4 color = tex2D(_MainTex, i.uv);
                if (color.a > 0.0)
                {
                    return color;
                }

                float alpha = 0.0;
                for (int j = 0; j < 8; j++)
                {
                    float2 offsetUV = i.uv + offsets[j] * (_OutlineThickness / _ScreenParams.xy);
                    alpha += tex2D(_MainTex, offsetUV).a;
                }

                alpha = saturate(alpha);
                return float4(_OutlineColor.rgb, alpha * _OutlineColor.a);
            }
            ENDCG
        }
    }
}
