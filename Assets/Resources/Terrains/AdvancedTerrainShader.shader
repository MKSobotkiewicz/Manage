Shader "Terrain/AdvancedTerrainShader"
{
    Properties
    {
		[HideInInspector] _Control("Control (RGBA)", 2D) = "red" {}

		_Albedo3("Layer 3 (A)", 2D) = "white" {}
		_Normal3("Normal 3 (A)", 2D) = "normal" {}
		_Height3("Height 3 (A)", 2D) = "grey" {}
		_Glossiness3("Smoothness 3 (A)", Range(0,1)) = 0.5
		_Blending3("Blending 3 (A)", Range(0,100)) = 1
		_Tiling3("Tiling 3 (A)", Range(0,100)) = 0.5

		_Albedo2("Layer 2 (B)", 2D) = "white" {}
		_Normal2("Normal 2 (B)", 2D) = "normal" {}
		_Height2("Height 2 (B)", 2D) = "grey" {}
		_Glossiness2("Smoothness 2 (B)", Range(0,1)) = 0.5
		_Blending2("Blending 2 (B)", Range(0,100)) = 1
		_Tiling2("Tiling 2 (B)", Range(0,100)) = 0.5

		_Albedo1("Layer 1 (G)", 2D) = "white" {}
		_Normal1("Normal 1 (G)", 2D) = "normal" {}
		_Glossiness1("Smoothness 1 (G)", Range(0,1)) = 0.5
		_Blending1("Blending 1 (G)", Range(0,100)) = 1
		_Tiling1("Tiling 1 (G)", Range(0,100)) = 0.5

		_Albedo0("Layer 0 (R)", 2D) = "white" {}
		_Normal0("Normal 0 (R)", 2D) = "normal" {}
		_Glossiness0("Smoothness 0 (R)", Range(0,1)) = 0.5
		_Tiling0("Tiling 0 (R)", Range(0,100)) = 0.5

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

		sampler2D _Height3;
		sampler2D _Height2;

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

			half height2 = projectSingle(_Height2, IN.worldPos, projnorm, _Tiling2);
			half height3 = projectSingle(_Height3, IN.worldPos, projnorm, _Tiling3);

			half control1 = saturate(pow(control.g+0.5 , _Blending1));
			half control2 = saturate(pow(control.b + (height2 + 0.5)*0.5, _Blending2));
			half control3 = saturate(pow(control.a + (height3 + 0.5)*0.5, _Blending3));
			half controlRock = saturate(pow(WorldNormalVector(IN, o.Normal).g + _RockNormal, _BlendingRock));

			fixed3 albedo0 = projectVector(_Albedo0, IN.worldPos, projnorm, _Tiling0);
			fixed3 albedo1 = projectVector(_Albedo1, IN.worldPos, projnorm, _Tiling1);
			fixed3 albedo2 = projectVector(_Albedo2, IN.worldPos, projnorm, _Tiling2);
			fixed3 albedo3 = projectVector(_Albedo3, IN.worldPos, projnorm, _Tiling3);
			fixed3 albedoRock = projectVector(_AlbedoRock, IN.worldPos, projnorm, _TilingRock);

			fixed3 albedo01 = lerp(albedo0, albedo1, control1);
			fixed3 albedo012 = lerp(albedo01, albedo2, control2);
			fixed3 albedo0123 = lerp(albedo012, albedo3, control3);
			fixed3 albedo= lerp(albedoRock, albedo0123, controlRock);

			fixed3 normal0 = projectNormal(_Normal3, IN.worldPos, projnorm, _Tiling0);
			fixed3 normal1 = projectNormal(_Normal0, IN.worldPos, projnorm, _Tiling1);
			fixed3 normal2 = projectNormal(_Normal1, IN.worldPos, projnorm, _Tiling2);
			fixed3 normal3 = projectNormal(_Normal2, IN.worldPos, projnorm, _Tiling3);
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
