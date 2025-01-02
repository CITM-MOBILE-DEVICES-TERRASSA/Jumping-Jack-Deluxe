Shader "Custom/SpriteOutline"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Range(1, 10)) = 1
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _OutlineThickness;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 texelSize = float2(_OutlineThickness / _ScreenParams.x, _OutlineThickness / _ScreenParams.y);
                float4 mainColor = tex2D(_MainTex, i.texcoord);

                // Compute the outline based on alpha
                float alpha = mainColor.a;
                float outline = 0.0;

                // Sample in 8 directions around the pixel
                outline += tex2D(_MainTex, i.texcoord + texelSize * float2(-1, -1)).a;
                outline += tex2D(_MainTex, i.texcoord + texelSize * float2(0, -1)).a;
                outline += tex2D(_MainTex, i.texcoord + texelSize * float2(1, -1)).a;
                outline += tex2D(_MainTex, i.texcoord + texelSize * float2(-1, 0)).a;
                outline += tex2D(_MainTex, i.texcoord + texelSize * float2(1, 0)).a;
                outline += tex2D(_MainTex, i.texcoord + texelSize * float2(-1, 1)).a;
                outline += tex2D(_MainTex, i.texcoord + texelSize * float2(0, 1)).a;
                outline += tex2D(_MainTex, i.texcoord + texelSize * float2(1, 1)).a;

                // If alpha is low, use outline; otherwise, use the sprite color
                if (outline > 0.0 && alpha == 0.0)
                {
                    return _OutlineColor;
                }

                return mainColor;
            }
            ENDCG
        }
    }
}
