Shader "Custom/UnitShader"
{
    Properties
    {
        _MainTex ("Albedo", 2D) = "white" {}
		_MetallicGlossMap("Metallic", 2D) = "black" {}
		_RandomTex("Random", 2D) = "black" {}
		_BumpMap("Normal", 2D) = "bump" {}
		_MainColor("Diffuse Color", Color) = (1,1,1,1)
		_BloodColor("Blood Color", Color) = (1,0,0,1)
		_Dist("Shift", Range(-1, 1)) = 0
		_BloodLevel("Blood Level", Range(0, 1)) = 0
    }
    SubShader
    {
		/// first pass

		Tags { "IgnoreProjector" = "True" "RenderType" = "Transparent" "Queue" = "AlphaTest" }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting Off
		ZWrite off
		ZTest Always
		cull Front
		CGPROGRAM

		#pragma surface surf StandardSpecular alphatest:off vertex:vert
		#pragma target 5.0
		#include "UnityCG.cginc"


		float4 _MainColor;
		float _Dist;


		struct Input {
			float2 uv_MainTex;
		};

		void vert(inout appdata_full v) {
			v.vertex.xyz += float3(v.normal.xyz)*_Dist;
		}

		void surf(Input i, inout SurfaceOutputStandardSpecular o) {
			o.Emission = _MainColor.rgb;  // main albedo color
			o.Specular = 0;
			o.Smoothness = 0;
			o.Alpha = 1;
			///////////////
		}
		ENDCG
			//// end first pass


       // Tags { "RenderType"="Opaque" }
		Tags{ "IgnoreProjector" = "True" "RenderType" = "Opaque"}
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting On
		ZWrite On
		ZTest LEqual
		Cull Off
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 5.0

        sampler2D _MainTex;
		sampler2D _MetallicGlossMap;
		sampler2D _RandomTex;
		sampler2D _BumpMap;
		float4 _BloodColor;
		float _BloodLevel;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed r = tex2D(_RandomTex, IN.uv_MainTex).r;
			fixed b = saturate(r - 1 + _BloodLevel.r);
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 m = tex2D(_MetallicGlossMap, IN.uv_MainTex);
			fixed3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
            o.Albedo = lerp(c.rgb,_BloodColor, b);
            o.Metallic = lerp(m.rgb,0,b);
            o.Smoothness = lerp(m.a,0.9, b);
			o.Normal = n;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
