﻿Shader "Custom/FogShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Alpha("Alpha", 2D) = "white" {}
        _Random ("Random", 2D) = "white" {}
		_Scale("Scale", Range(0,0.2)) = 1
		_Speed("Speed", Range(0,1)) = 1
    }
    SubShader
    {
		Tags { "IgnoreProjector" = "True" "RenderType" = "Transparent" "Queue"="Transparent"}

		Blend SrcAlpha OneMinusSrcAlpha
		Lighting Off
		ZWrite on
		ZTest Always
		Cull Off
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard alphatest:_Cutoff

        #pragma target 5.0

        sampler2D _Alpha;
		sampler2D _Random;

        struct Input
        {
            float2 uv_MainTex;
			half3 worldPos;
			float3 worldNormal;
			float3 viewDir;
			INTERNAL_DATA
        }; 

        fixed4 _Color;
		half _Scale;
		half _Speed;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			half c11 = tex2D(_Random, IN.worldPos.xz*_Scale*1.15 + sin(_Time.r * _Speed)).r;
			half c12 = tex2D(_Random, IN.worldPos.xz*_Scale + cos(_Time.r * _Speed*1.3)).g;
			half c13 = tex2D(_Random, IN.worldPos.xz*_Scale*0.9 + tan(_Time.r * _Speed*0.15)).b;
			half c1 = c11 * c12*c13;
			float fresnel = dot(IN.worldNormal, IN.viewDir);
			fresnel = saturate(fresnel);
			fixed depth = saturate( (_WorldSpaceCameraPos.y-IN.worldPos.y-10)/20);
            fixed4 tex = tex2D (_Alpha, IN.uv_MainTex)*5 *c1;
            o.Albedo = _Color;
            o.Metallic = 0;
            o.Smoothness = 0;
			o.Alpha = clamp(tex.r*fresnel* depth, 0, 0.5);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
