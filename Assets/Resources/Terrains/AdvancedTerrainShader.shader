Shader "Terrain/AdvancedTerrainShader"
{
    Properties
    {
		[HideInInspector] _Control("Control (RGBA)", 2D) = "red" {}

		_Random("Random", 2D) = "grey" {}
		_TilingRandom("Tiling Random", Range(0,1000)) = 0.5
		
		_Height("Height", 2D) = "grey" {}
		
		_Albedo3("Layer 3 (A)", 2D) = "white" {}
		_Normal3("Normal 3 (A)", 2D) = "normal" {}
		_Glossiness3("Smoothness 3 (A)", Range(0,1)) = 0.5
		_Blending3("Blending 3 (A)", Range(0,100)) = 1
		_Tiling3("Tiling 3 (A)", Range(0,0.1)) = 0.5

		_Albedo2("Layer 2 (B)", 2D) = "white" {}
		_Normal2("Normal 2 (B)", 2D) = "normal" {}
		_Glossiness2("Smoothness 2 (B)", Range(0,1)) = 0.5
		_Blending2("Blending 2 (B)", Range(0,100)) = 1
		_Tiling2("Tiling 2 (B)", Range(0,0.1)) = 0.5

		_Albedo1("Layer 1 (G)", 2D) = "white" {}
		_Normal1("Normal 1 (G)", 2D) = "normal" {}
		_Glossiness1("Smoothness 1 (G)", Range(0,1)) = 0.5
		_Blending1("Blending 1 (G)", Range(0,100)) = 1
		_Tiling1("Tiling 1 (G)", Range(0,0.1)) = 0.5

		_Albedo0("Layer 0 (R)", 2D) = "white" {}
		_Normal0("Normal 0 (R)", 2D) = "normal" {}
		_Glossiness0("Smoothness 0 (R)", Range(0,1)) = 0.5
		_Tiling0("Tiling 0 (R)", Range(0,0.1)) = 0.5

		_AlbedoRock("Layer Rock", 2D) = "white" {}
		_NormalRock("Normal Rock", 2D) = "normal" {}
		_GlossinessRock("Smoothness Rock", Range(0,1)) = 0.5
		_BlendingRock("Blending Rock", Range(0,100)) = 1
		_TilingRock("Tiling Rock", Range(0,100)) = 0.5
		_RockNormal("Rock Normal", Range(-1,1)) = 0

		_EdgeLength("Edge length", Range(0.1,5)) = 5
		_Phong("Phong Strengh", Range(0,1)) = 0.5
    }
    SubShader
    {
		Tags{ "RenderType" = "Opaque" }
		LOD 300

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows nolightmap tessellate:tess tessphong:_Phong addshadow
		#pragma target 5.0

		#include "Tessellation.cginc"
		#include "UnityCG.cginc"

		sampler2D _Control;

		sampler2D _Height;
		sampler2D _Random;

		sampler2D _Albedo3;
		sampler2D _Albedo2;
		sampler2D _Albedo1;
		sampler2D _Albedo0;
		sampler2D _AlbedoRock;

		sampler2D _Normal3;
		sampler2D _Normal2;
		sampler2D _Normal1;
		sampler2D _Normal0;
		sampler2D _NormalRock;

		half _Blending3;
		half _Blending2;
		half _Blending1;
		half _BlendingRock;

		half _Glossiness3;
		half _Glossiness2;
		half _Glossiness1;
		half _Glossiness0;
		half _GlossinessRock;

		half _Tiling3;
		half _Tiling2;
		half _Tiling1;
		half _Tiling0;
		half _TilingRock;
		half _TilingRandom;

		half _RockNormal;

		float _Phong;
		float _EdgeLength;

		struct appdata
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;
			float4 color: COLOR;
		};

		struct Input
		{
			float2 uv_Control;
			half3 worldPos;
			half3 worldNormal; INTERNAL_DATA
			float4 color: COLOR;
		};

		float4 tess(appdata_full v0, appdata_full v1, appdata_full v2)
		{
			return UnityEdgeLengthBasedTess(v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		fixed3 projectNormal(sampler2D tex, half3 worldPos, float3 projnorm, half scale)
		{
			fixed3 c_1 = UnpackNormal(tex2D(tex, worldPos.yz / scale));
			fixed3 c_2 = UnpackNormal(tex2D(tex, worldPos.xy / scale));
			fixed3 c_3 = UnpackNormal(tex2D(tex, worldPos.xz / scale));
			fixed3 c_4 = lerp(c_1, c_2, projnorm.z);
			fixed3 c = lerp(c_4, c_3, projnorm.y);

			return  c;
		}

		fixed3 projectVector(sampler2D tex, half3 worldPos, float3 projnorm, half scale)
		{
			fixed3 c_1 = tex2D(tex, worldPos.yz / scale);
			fixed3 c_2 = tex2D(tex, worldPos.xy / scale);
			fixed3 c_3 = tex2D(tex, worldPos.xz / scale);
			fixed3 c_4 = lerp(c_1, c_2, projnorm.z);
			fixed3 c = lerp(c_4, c_3, projnorm.y);

			return  c;
		}

		fixed projectSingle(sampler2D tex, half3 worldPos, float3 projnorm, half scale)
		{
			fixed c_1 = tex2D(tex, worldPos.yz / scale).r;
			fixed c_2 = tex2D(tex, worldPos.xy / scale).r;
			fixed c_3 = tex2D(tex, worldPos.xz / scale).r;
			fixed c_4 = lerp(c_1, c_2, projnorm.z);
			fixed c = lerp(c_4, c_3, projnorm.y);

			return  c;
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float3 projnorm = saturate(pow(WorldNormalVector(IN, o.Normal) * 2, 4));
			fixed4 control = tex2D(_Control, IN.uv_Control);
			
			fixed3 random=projectVector(_Random, IN.worldPos, projnorm, _TilingRandom);
			half2 uv1 = IN.uv_Control;
			half2 uv2 = IN.uv_Control + half2(0.3, 0.3);
			half uvRandom = saturate(pow(random.g + 0.5, 10));

			half heightR = projectSingle(_Height, IN.worldPos, projnorm, _TilingRock);
			half heightG = lerp(tex2D(_Height, uv1 / _Tiling1).g, tex2D(_Height, uv2 / _Tiling1).g, uvRandom);
			half heightB = lerp(tex2D(_Height, uv1 / _Tiling1).b, tex2D(_Height, uv2 / _Tiling1).b, uvRandom);
			half heightA = lerp(tex2D(_Height, uv1 / _Tiling1).a, tex2D(_Height, uv2 / _Tiling1).a, uvRandom);

			half control1 = saturate(pow(control.g + (heightG + 0.5)*0.5, _Blending1));
			half control2 = saturate(pow(control.b + (heightB + 0.5)*0.5, _Blending2));
			half control3 = saturate(pow(control.a + (heightA + 0.5)*0.5, _Blending3));
			half controlRock = saturate(pow(WorldNormalVector(IN, o.Normal).g + heightR*0.5 + _RockNormal, _BlendingRock));

			fixed3 albedo0 = lerp(tex2D(_Albedo0, uv1 / _Tiling0), tex2D(_Albedo0, uv2 / _Tiling0), uvRandom);
			fixed3 albedo1 = lerp(tex2D(_Albedo1, uv1 / _Tiling1), tex2D(_Albedo1, uv2 / _Tiling1), uvRandom);
			fixed3 albedo2 = lerp(tex2D(_Albedo2, uv1 / _Tiling2), tex2D(_Albedo2, uv2 / _Tiling2), uvRandom);
			fixed3 albedo3 = lerp(tex2D(_Albedo3, uv1 / _Tiling3), tex2D(_Albedo3, uv2 / _Tiling3), uvRandom);
			fixed3 albedoRock = projectVector(_AlbedoRock, IN.worldPos, projnorm, _TilingRock);

			fixed3 albedo01 = lerp(albedo0, albedo1, control1);
			fixed3 albedo012 = lerp(albedo01, albedo2, control2);
			fixed3 albedo0123 = lerp(albedo012, albedo3, control3);
			fixed3 albedo= lerp(albedoRock, albedo0123, controlRock);

			fixed3 normal0 = lerp(UnpackNormal(tex2D(_Normal3, uv1 / _Tiling0)), UnpackNormal(tex2D(_Normal3, uv2 / _Tiling0)), uvRandom);
			fixed3 normal1 = lerp(UnpackNormal(tex2D(_Normal0, uv1 / _Tiling1)), UnpackNormal(tex2D(_Normal0, uv2 / _Tiling1)), uvRandom);
			fixed3 normal2 = lerp(UnpackNormal(tex2D(_Normal1, uv1 / _Tiling2)), UnpackNormal(tex2D(_Normal1, uv2 / _Tiling2)), uvRandom);
			fixed3 normal3 = lerp(UnpackNormal(tex2D(_Normal2, uv1 / _Tiling3)), UnpackNormal(tex2D(_Normal2, uv2 / _Tiling3)), uvRandom);
			fixed3 normalRock = projectNormal(_NormalRock, IN.worldPos, projnorm, _TilingRock);

			fixed3 normal01 = lerp(normal0, normal1, control1);
			fixed3 normal012 = lerp(normal01, normal2, control2);
			fixed3 normal0123 = lerp(normal012, normal3, control3);
			fixed3 normal = lerp(normalRock, normal0123, controlRock);

			fixed glossiness0 = _Glossiness0;
			fixed glossiness1 = _Glossiness1;
			fixed glossiness2 = _Glossiness2;
			fixed glossiness3 = _Glossiness3;
			fixed glossinessRock = _GlossinessRock;

			fixed glossiness01 = lerp(glossiness0, glossiness1, control1);
			fixed glossiness012 = lerp(glossiness01, glossiness2, control2);
			fixed glossiness0123 = lerp(glossiness012, glossiness3, control3);
			fixed glossiness = lerp(glossinessRock, glossiness0123, control3);

			o.Albedo = albedo;
            o.Metallic = 0;
            o.Smoothness = glossiness;
            o.Normal = normal;
        }
        ENDCG
    }
    FallBack "Diffuse"
}