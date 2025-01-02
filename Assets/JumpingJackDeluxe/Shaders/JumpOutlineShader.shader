Shader "Custom/OutlineBehindSprite"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (0,0,1,1)
        _OutlineThickness ("Outline Thickness", Float) = 0.05
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        // Primer pase: Renderiza solo el outline
        Pass
        {
            Name "Outline"
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineThickness;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Leer el canal alfa
                float alpha = tex2D(_MainTex, i.uv).a;

                // Generar el outline
                float outline =
                    tex2D(_MainTex, i.uv + float2(-_OutlineThickness, 0)).a +
                    tex2D(_MainTex, i.uv + float2(_OutlineThickness, 0)).a +
                    tex2D(_MainTex, i.uv + float2(0, -_OutlineThickness)).a +
                    tex2D(_MainTex, i.uv + float2(0, _OutlineThickness)).a;

                outline = step(0.1, outline); // Determina el borde

                return float4(_OutlineColor.rgb, outline); // Color del outline
            }
            ENDCG
        }

        // Segundo pase: Renderiza el sprite encima
        Pass
        {
            Name "Sprite"
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment fragSprite
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _BaseColor;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 fragSprite (v2f i) : SV_Target {
                // Renderizar el sprite con su color base
                fixed4 texColor = tex2D(_MainTex, i.uv);
                return texColor * _BaseColor;
            }
            ENDCG
        }
    }
}
