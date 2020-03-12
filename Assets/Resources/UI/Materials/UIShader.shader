Shader "Unlit/UIShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Overlay("Overlay", 2D) = "white" {}
		_Stripes("Stripes", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_ScaleStripes("Stripes Scale", Range(0,100)) = 0.01
		_SpeedStripes("Stripes Speed", Range(0,1000)) = 1000
		_Random("Random", 2D) = "white" {}
		_ScaleRandom("Random Scale", Range(0,10)) = 0.01
		_SpeedRandom("Random Speed", Range(0,1000)) = 1000
		_Transparency("Transparency", Range(0,1)) = 1

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
	}

	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite off
		LOD 100

		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}
		ColorMask[_ColorMask]

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma alpha:fade

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			sampler2D _Overlay;
			sampler2D _Stripes;
			sampler2D _Random;
			sampler2D _Mask;
			float4 _MainTex_ST;
			half _ScaleStripes;
			half _SpeedStripes;
			half _ScaleRandom;
			half _SpeedRandom;
			half _Transparency;
			fixed4 _Color;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.screenPos = ComputeScreenPos(o.vertex);
				return o;
			}


			fixed4 frag(v2f i) : SV_Target
			{
				fixed3 random = (tex2D(_Random, i.screenPos *_ScaleRandom + cos(_Time.r * _SpeedRandom*1.3) + sin(_Time.r * _SpeedRandom))).r*0.5 + 0.5;
				fixed3 stripes = (tex2D(_Stripes, i.screenPos *_ScaleStripes + cos(_Time.r * _SpeedStripes*1.3) + sin(_Time.r * _SpeedStripes))).r*0.5 + 0.5;
				fixed3 col = (tex2D(_MainTex, i.uv)).r*stripes*_Color.rgb - ((1 - (tex2D(_Overlay, i.uv)).r)*0.3)*random;
				col *= 2;
				return fixed4(col.r,col.g,col.b, (tex2D(_Mask, i.uv)).a*_Transparency);
			}
			ENDCG
		}
	}
}
