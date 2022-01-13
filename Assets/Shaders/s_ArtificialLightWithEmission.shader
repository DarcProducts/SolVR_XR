Shader "Custom/s_ArtificialLightWithEmission"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "black" {}
        _EmissionTex("Emission Texture", 2D) = "white" {} 
        _LightColor("Light Color", Color) = (1.0, 1.0, 1.0, 1.0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                fixed4 vertex : POSITION;
                fixed2 uv : TEXCOORD0;
                fixed3 worldPos : TEXCOORD1;
                fixed3 normal : NORMAL;
            };

            struct v2f
            {
                fixed2 uv : TEXCOORD0;
                fixed3 worldPos : TEXCOORD1;
                fixed3 normal : TEXCOORD2;
                fixed4 vertex : SV_POSITION;

            };

            sampler2D _MainTex;
            sampler2D _EmissionTex;
            fixed4 _MainTex_ST;
            fixed4 _EmissionTex_ST;
            fixed4 _LightColor;
      
            v2f vert (appdata v)
            {
                v2f o;
                o.worldPos = -mul(unity_WorldToObject, v.vertex);
                o.normal = v.normal;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 lImage = tex2D(_MainTex, i.uv) * _LightColor;
                fixed4 eImage = tex2D(_EmissionTex, i.uv);
                fixed4 lDir = normalize(fixed4(0, 0, 0, 1)) - normalize(fixed4(i.worldPos, 1));
                fixed4 lNorm = max(-2, dot(lDir, i.normal));
                fixed4 eLight = fixed4(step(lNorm.xxx, -lNorm.xxx), 1);
                fixed4 iLight = fixed4(lNorm.xyz, 1);
                return lImage * iLight + eImage * eLight;
            }
            ENDCG
        }
    }
}
