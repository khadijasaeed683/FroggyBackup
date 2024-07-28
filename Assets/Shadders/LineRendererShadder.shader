Shader "Unlit/LineRendererShadder"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _DotSize ("Dot Size", Float) = 1.0
        _GlowIntensity ("Glow Intensity", Float) = 1.0
        _TransitionSpeed ("Transition Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _DotSize;
            float _GlowIntensity;
            float _TransitionSpeed;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Calculate the dotted pattern
                float linePos = frac(i.uv.x * _DotSize + _Time.y * _TransitionSpeed);
                float mask = step(0.5, linePos);
                
                // Create a glow effect
                float4 glow = _Color * _GlowIntensity;
                glow.a = _Color.a;

                // Apply the texture and color
                half4 texcol = tex2D(_MainTex, i.uv) * _Color;

                // Combine the dotted pattern with the glow effect
                half4 finalColor = lerp(texcol, glow, mask);

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
