Shader "Custom/s_ArtificialLight"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "black" {}
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
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 normal : TEXCOORD2;
                float4 vertex : SV_POSITION;

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LightColor;
      
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
                float4 image = tex2D(_MainTex, i.uv);
                float4 lightDir = normalize(float4(0,0,0,1)) - normalize(float4(i.worldPos, 1));
                float4 lightNormals = max(-2, dot(lightDir, i.normal));
                float4 light = float4(lightNormals.xxx * _LightColor, 1);
                return image * light;
            }
            ENDCG
        }
    }
}
