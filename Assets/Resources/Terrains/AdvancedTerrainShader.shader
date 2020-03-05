Shader "Terrain/AdvancedTerrainShader"
{
    Properties
    {
		[HideInInspector] _Control("Control (RGBA)", 2D) = "red" {}

		_Albedo3("Layer 3 (A)", 2D) = "white" {}
		_Albedo2("Layer 2 (B)", 2D) = "white" {}
		_Albedo1("Layer 1 (G)", 2D) = "white" {}
		_Albedo0("Layer 0 (R)", 2D) = "white" {}

		_Normal3("Normal 3 (A)", 2D) = "normal" {}
		_Normal2("Normal 2 (B)", 2D) = "normal" {}
		_Normal1("Normal 1 (G)", 2D) = "normal" {}
		_Normal0("Normal 0 (R)", 2D) = "normal" {}

		_Glossiness3("Smoothness 3(A)", Range(0,1)) = 0.5
		_Glossiness2("Smoothness 2(B)", Range(0,1)) = 0.5
		_Glossiness1("Smoothness 1(G)", Range(0,1)) = 0.5
		_Glossiness0("Smoothness 0(R)", Range(0,1)) = 0.5

		_Height3("Height 3(A)", 2D) = "grey" {}
		_Height2("Height 2(B)", 2D) = "grey" {}
		_Height1("Height 1(G)", 2D) = "grey" {}
		_Height0("Height 0(R)", 2D) = "grey" {}

		_Blending3("Blending 3(A)", Range(0,100)) = 1
		_Blending2("Blending 2(B)", Range(0,100)) = 1
		_Blending1("Blending 1(G)", Range(0,100)) = 1
		_Blending0("Blending 0(R)", Range(0,100)) = 1

		_Tiling3("Tiling 3(A)", Range(0,100)) = 0.5
		_Tiling2("Tiling 2(B)", Range(0,100)) = 0.5
		_Tiling1("Tiling 1(G)", Range(0,100)) = 0.5
		_Tiling0("Tiling 0(R)", Range(0,100)) = 0.5

    }
    SubShader
    {
		Tags{ "RenderType" = "Opaque" }
		LOD 300

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows nolightmap addshadow
		#pragma target 5.0

		#include "UnityCG.cginc"

		sampler2D _Control;

		sampler2D _Albedo3;
		sampler2D _Albedo2;
		sampler2D _Albedo1;
		sampler2D _Albedo0;

		sampler2D _Normal3;
		sampler2D _Normal2;
		sampler2D _Normal1;
		sampler2D _Normal0;

		sampler2D _Height3;
		sampler2D _Height2;
		sampler2D _Height1;
		sampler2D _Height0;

		half _Blending3;
		half _Blending2;
		half _Blending1;
		half _Blending0;

		half _Glossiness3;
		half _Glossiness2;
		half _Glossiness1;
		half _Glossiness0;

		half _Tiling3;
		half _Tiling2;
		half _Tiling1;
		half _Tiling0;

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

		fixed3 project0(sampler2D tex, half3 worldPos, float3 projnorm, half scale)
		{
			fixed3 c_1 = UnpackNormal (tex2D(tex, worldPos.yz / scale));
			fixed3 c_2 = UnpackNormal(tex2D(tex, worldPos.xy / scale));
			fixed3 c_3 = UnpackNormal(tex2D(tex, worldPos.xz / scale));
			fixed3 c_4 = lerp(c_1, c_2, projnorm.z);
			fixed3 c = lerp(c_4, c_3, projnorm.y);

			fixed3 b_1 = UnpackNormal(tex2D(tex, worldPos.yz*7.1 / scale));
			fixed3 b_2 = UnpackNormal(tex2D(tex, worldPos.xy*7.1 / scale));
			fixed3 b_3 = UnpackNormal(tex2D(tex, worldPos.xz*7.1 / scale));
			fixed3 b_4 = lerp(b_2, b_3, projnorm.y);
			fixed3 b = lerp(b_4, b_1, projnorm.x);

			return  lerp(c, b, 0.2);
		}

		fixed4 project1(sampler2D tex, half3 worldPos, float3 projnorm, half scale)
		{
			fixed4 c_1 = tex2D(tex, worldPos.yz / scale);
			fixed4 c_2 = tex2D(tex, worldPos.xy / scale);
			fixed4 c_3 = tex2D(tex, worldPos.xz / scale);
			fixed4 c_4 = lerp(c_1, c_2, projnorm.z);
			fixed4 c = lerp(c_4, c_3, projnorm.y);

			fixed4 b_1 = tex2D(tex, worldPos.yz*7.1 / scale);
			fixed4 b_2 = tex2D(tex, worldPos.xy*7.1 / scale);
			fixed4 b_3 = tex2D(tex, worldPos.xz*7.1 / scale);
			fixed4 b_4 = lerp(b_2, b_3, projnorm.y);
			fixed4 b = lerp(b_4, b_1, projnorm.x);

			return  lerp(c, b, 0.2);
		}

		fixed4 project2(sampler2D tex, half3 worldPos, float3 projnorm, half scale)
		{
			half2 worldPosYZ = worldPos.yz / scale;
			half2 worldPosXY = worldPos.xy / scale;
			half2 worldPosXZ = worldPos.xz / scale;
			fixed4 c_1 = tex2Dlod(tex, half4(worldPosYZ.x, worldPosYZ.y, 0, 0));
			fixed4 c_2 = tex2Dlod(tex, half4(worldPosXY.x, worldPosXY.y, 0, 0));
			fixed4 c_3 = tex2Dlod(tex, half4(worldPosXZ.x, worldPosXZ.y, 0, 0));
			fixed4 c_4 = lerp(c_1, c_2, projnorm.z);
			fixed4 c = lerp(c_4, c_3, projnorm.y);

			worldPosYZ = worldPos.yz*7.1 / scale;
			worldPosXY = worldPos.xy*7.1 / scale;
			worldPosXZ = worldPos.xz*7.1 / scale;
			fixed4 b_1 = tex2Dlod(tex, half4(worldPosYZ.x, worldPosYZ.y, 0, 0));
			fixed4 b_2 = tex2Dlod(tex, half4(worldPosXY.x, worldPosXY.y, 0, 0));
			fixed4 b_3 = tex2Dlod(tex, half4(worldPosXZ.x, worldPosXZ.y, 0, 0));
			fixed4 b_4 = lerp(b_2, b_3, projnorm.y);
			fixed4 b = lerp(b_4, b_1, projnorm.x);

			return  lerp(c, b, 0.2);
		}

		fixed project3(sampler2D tex, half3 worldPos, float3 projnorm, half scale)
		{
			fixed c_1 = tex2D(tex, worldPos.yz / scale).r;
			fixed c_2 = tex2D(tex, worldPos.xy / scale).r;
			fixed c_3 = tex2D(tex, worldPos.xz / scale).r;
			fixed c_4 = lerp(c_1, c_2, projnorm.z);
			fixed c = lerp(c_4, c_3, projnorm.y);

			fixed b_1 = tex2D(tex, worldPos.yz*7.1 / scale).r;
			fixed b_2 = tex2D(tex, worldPos.xy*7.1 / scale).r;
			fixed b_3 = tex2D(tex, worldPos.xz*7.1 / scale).r;
			fixed b_4 = lerp(b_2, b_3, projnorm.y);
			fixed b = lerp(b_4, b_1, projnorm.x);

			return  lerp(c, b, 0.2);
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float3 projnorm = saturate(pow(WorldNormalVector(IN, o.Normal) * 2, 4));
			fixed4 control = tex2D(_Control, IN.uv_Control);

			half height0 = project1(_Height0, IN.worldPos, projnorm, _Tiling0).r;
			half height1 = project1(_Height1, IN.worldPos, projnorm, _Tiling1).r;
			half height2 = project1(_Height2, IN.worldPos, projnorm, _Tiling2).r;
			half height3 = project1(_Height3, IN.worldPos, projnorm, _Tiling3).r;

			half controlR = saturate(pow(control.r + (height0 + 0.5)*0.5, _Blending0));
			half controlG = saturate(pow(control.g + (height0 + 0.5)*0.5, _Blending1));
			half controlB = saturate(pow(control.b + (height0 + 0.5)*0.5, _Blending2));
			half controlA = saturate(pow(control.a + (height0 + 0.5)*0.5, _Blending3));

			fixed3 albedo0 =  project1(_Albedo0, IN.worldPos, projnorm, _Tiling0).rgb;
			fixed3 albedo1 = saturate(pow(control.g +  (height1 + 0.5) * 0.5 , _Blending1)) * project1(_Albedo1, IN.worldPos, projnorm, _Tiling1).rgb;
			fixed3 albedo2 = saturate(pow(control.b +  (height2 + 0.5) * 0.5 , _Blending2)) * project1(_Albedo2, IN.worldPos, projnorm, _Tiling2).rgb;
			fixed3 albedo3 = saturate(pow(control.a +  (height3 + 0.5) * 0.5 , _Blending3)) * project1(_Albedo3, IN.worldPos, projnorm, _Tiling3).rgb;

			fixed3 albedo01 = lerp(albedo0, albedo1, controlR);
			fixed3 albedo23 = lerp(albedo2, albedo3, controlG);
			fixed3 albedo = lerp(albedo01, albedo23, controlB);

			fixed3 normal = saturate(pow(control.r + (height0 + 0.5) * 0.5, _Blending0)) * project0(_Normal0, IN.worldPos, projnorm, _Tiling0);
			normal += saturate(pow(control.g + (height1 + 0.5) * 0.5, _Blending1)) *  project0(_Normal1, IN.worldPos, projnorm, _Tiling1);
			normal += saturate(pow(control.b + (height2 + 0.5) * 0.5, _Blending2)) *  project0(_Normal2, IN.worldPos, projnorm, _Tiling2);
			normal += saturate(pow(control.a + (height3 + 0.5) * 0.5, _Blending3)) *  project0(_Normal3, IN.worldPos, projnorm, _Tiling3);

			fixed3 glossiness = clamp(control.r*height0, 0, 1) * _Glossiness0;
			glossiness += clamp(control.g * height1, 0, 1) * _Glossiness1;
			glossiness += clamp(control.b * height2, 0, 1) * _Glossiness2;
			glossiness += clamp(control.a * height3, 0, 1) * _Glossiness3;

			o.Albedo = albedo;
            o.Metallic = 0;
            o.Smoothness = 0;
            //o.Normal = normal;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
