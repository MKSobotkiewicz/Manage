﻿Shader "Hidden/TerrainEngine/Details/WavingDoublePass" {
	Properties{
		_WavingTint("Fade Color", Color) = (.7,.6,.5, 0)
		_MainTex("Base (RGB) Alpha (A)", 2D) = "white" {}
		_WaveAndDistance("Wave and distance", Vector) = (12, 3.6, 1, 1)
	}

		SubShader{
			Tags {
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Grass"
				"DisableBatching" = "True"
			}
			Cull Off
			LOD 200
			ColorMask RGB

		CGPROGRAM
		#pragma surface surf Lambert vertex:WavingGrassVert addshadow fullforwardshadows nolightmap alpha:fade Blend SrcAlpha OneMinusSrcAlpha
		#include "TerrainEngine.cginc"

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			fixed4 color : COLOR;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
		}
		Fallback Off
}