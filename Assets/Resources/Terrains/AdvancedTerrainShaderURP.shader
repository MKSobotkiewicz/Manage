﻿// When creating shaders for Universal Render Pipeline you can you the ShaderGraph which is super AWESOME!
// However, if you want to author shaders in shading language you can use this teamplate as a base.
// Please note, this shader does not necessarily match perfomance of the built-in URP Lit shader.
// This shader works with URP 7.1.x and above
Shader "Universal Render Pipeline/Custom/Advanced Terrain Shader"
{
	Properties
	{
		// Specular vs Metallic workflow
		[HideInInspector] _WorkflowMode("WorkflowMode", Float) = 1.0

		[MainColor] _BaseColor("Color", Color) = (0.5,0.5,0.5,1)
		[MainTexture] _BaseMap("Albedo", 2D) = "white" {}

		_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

		_Smoothness("Smoothness", Range(0.0, 1.0)) = 0.5
		_GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
		_SmoothnessTextureChannel("Smoothness texture channel", Float) = 0

		[Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
		_MetallicGlossMap("Metallic", 2D) = "white" {}

		_SpecColor("Specular", Color) = (0.2, 0.2, 0.2)
		_SpecGlossMap("Specular", 2D) = "white" {}

		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _EnvironmentReflections("Environment Reflections", Float) = 1.0

		_BumpScale("Scale", Float) = 1.0
		_BumpMap("Normal Map", 2D) = "bump" {}

		_OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
		_OcclusionMap("Occlusion", 2D) = "white" {}

		_EmissionColor("Color", Color) = (0,0,0)
		_EmissionMap("Emission", 2D) = "white" {}

		[HideInInspector] _Control("Control (RGBA)", 2D) = "red" {}

		_Random("Random", 2D) = "grey" {}
		_TilingRandom("Tiling Random", Range(0,1000)) = 0.5

		_Height("Height", 2D) = "grey" {}

		_Albedo3("Layer 3 (A)", 2D) = "white" {}
		_Bump3("Normal 3 (A)", 2D) = "normal" {}
		_Glossiness3("Smoothness 3 (A)", Range(0,1)) = 0.5
		_Blending3("Blending 3 (A)", Range(0,100)) = 1
		_Tiling3("Tiling 3 (A)", Range(0,0.1)) = 0.5

		_Albedo2("Layer 2 (B)", 2D) = "white" {}
		_Bump2("Normal 2 (B)", 2D) = "normal" {}
		_Glossiness2("Smoothness 2 (B)", Range(0,1)) = 0.5
		_Blending2("Blending 2 (B)", Range(0,100)) = 1
		_Tiling2("Tiling 2 (B)", Range(0,0.1)) = 0.5

		_Albedo1("Layer 1 (G)", 2D) = "white" {}
		_Bump1("Normal 1 (G)", 2D) = "normal" {}
		_Glossiness1("Smoothness 1 (G)", Range(0,1)) = 0.5
		_Blending1("Blending 1 (G)", Range(0,100)) = 1
		_Tiling1("Tiling 1 (G)", Range(0,0.1)) = 0.5

		_Albedo0("Layer 0 (R)", 2D) = "white" {}
		_Bump0("Normal 0 (R)", 2D) = "normal" {}
		_Glossiness0("Smoothness 0 (R)", Range(0,1)) = 0.5
		_Tiling0("Tiling 0 (R)", Range(0,0.1)) = 0.5

		_AlbedoRock("Layer Rock", 2D) = "white" {}
		_BumpRock("Normal Rock", 2D) = "normal" {}
		_GlossinessRock("Smoothness Rock", Range(0,1)) = 0.5
		_BlendingRock("Blending Rock", Range(0,100)) = 1
		_TilingRock("Tiling Rock", Range(0,100)) = 0.5
		_RockNormal("Rock Normal", Range(-1,1)) = 0

		// Blending state
		[HideInInspector] _Surface("__surface", Float) = 0.0
		[HideInInspector] _Blend("__blend", Float) = 0.0
		[HideInInspector] _AlphaClip("__clip", Float) = 0.0
		[HideInInspector] _SrcBlend("__src", Float) = 1.0
		[HideInInspector] _DstBlend("__dst", Float) = 0.0
		[HideInInspector] _ZWrite("__zw", Float) = 1.0
		[HideInInspector] _Cull("__cull", Float) = 2.0

		_ReceiveShadows("Receive Shadows", Float) = 1.0

			// Editmode props
			[HideInInspector] _QueueOffset("Queue offset", Float) = 0.0
	}

		SubShader
		{
			Tags{"RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" "IgnoreProjector" = "True"}
			LOD 300
			Pass
			{
			Name "StandardLit"
			Tags{"LightMode" = "UniversalForward"}

			Blend[_SrcBlend][_DstBlend]
			ZWrite[_ZWrite]
			Cull[_Cull]

			HLSLPROGRAM
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.0

			#pragma shader_feature _NORMALMAP
			#pragma shader_feature _ALPHATEST_ON
			#pragma shader_feature _ALPHAPREMULTIPLY_ON
			#pragma shader_feature _EMISSION
			#pragma shader_feature _METALLICSPECGLOSSMAP
			#pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
			#pragma shader_feature _OCCLUSIONMAP

			#pragma shader_feature _SPECULARHIGHLIGHTS_OFF
			#pragma shader_feature _GLOSSYREFLECTIONS_OFF
			#pragma shader_feature _SPECULAR_SETUP
			#pragma shader_feature _RECEIVE_SHADOWS_OFF

			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
			#pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
			#pragma multi_compile _ _SHADOWS_SOFT
			#pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE

			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile_fog

			#pragma multi_compile_instancing

			#pragma vertex LitPassVertex
			#pragma fragment LitPassFragment

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"

			struct Attributes
			{
				float4 positionOS   : POSITION;
				float3 normalOS     : NORMAL;
				float4 tangentOS    : TANGENT;
				float2 uv           : TEXCOORD0;
				float2 uvLM         : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct Varyings
			{
				float2 uv                       : TEXCOORD0;
				float2 uvLM                     : TEXCOORD1;
				float4 positionWSAndFogFactor   : TEXCOORD2; // xyz: positionWS, w: vertex fog factor
				half3  normalWS                 : TEXCOORD3;

#if _NORMALMAP
				half3 tangentWS                 : TEXCOORD4;
				half3 bitangentWS               : TEXCOORD5;
#endif

#ifdef _MAIN_LIGHT_SHADOWS
				float4 shadowCoord              : TEXCOORD6; // compute shadow coord per-vertex for the main light
#endif
				float4 positionCS               : SV_POSITION;
			};

			Varyings LitPassVertex(Attributes input)
			{
				Varyings output;
				VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
				VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);

				// Computes fog factor per-vertex.
				float fogFactor = ComputeFogFactor(vertexInput.positionCS.z);

				output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
				output.uvLM = input.uvLM.xy * unity_LightmapST.xy + unity_LightmapST.zw;

				output.positionWSAndFogFactor = float4(vertexInput.positionWS, fogFactor);
				output.normalWS = vertexNormalInput.normalWS;

#ifdef _NORMALMAP
				output.tangentWS = vertexNormalInput.tangentWS;
				output.bitangentWS = vertexNormalInput.bitangentWS;
#endif

#ifdef _MAIN_LIGHT_SHADOWS
				output.shadowCoord = GetShadowCoord(vertexInput);
#endif
				output.positionCS = vertexInput.positionCS;
				return output;
			}

			half4 LitPassFragment(Varyings input) : SV_Target
			{
				SurfaceData surfaceData;
				InitializeStandardLitSurfaceData(input.uv, surfaceData);

#if _NORMALMAP
				half3 normalWS = TransformTangentToWorld(surfaceData.normalTS,
					half3x3(input.tangentWS, input.bitangentWS, input.normalWS));
#else
				half3 normalWS = input.normalWS;
#endif
				normalWS = normalize(normalWS);

#ifdef LIGHTMAP_ON
				half3 bakedGI = SampleLightmap(input.uvLM, normalWS);
#else
				half3 bakedGI = SampleSH(normalWS);
#endif

				float3 positionWS = input.positionWSAndFogFactor.xyz;
				half3 viewDirectionWS = SafeNormalize(GetCameraPositionWS() - positionWS);

				BRDFData brdfData;
				InitializeBRDFData(surfaceData.albedo, surfaceData.metallic, surfaceData.specular, surfaceData.smoothness, surfaceData.alpha, brdfData);

#ifdef _MAIN_LIGHT_SHADOWS
				Light mainLight = GetMainLight(input.shadowCoord);
#else
				Light mainLight = GetMainLight();
#endif

				// Mix diffuse GI with environment reflections.
				half3 color = GlobalIllumination(brdfData, bakedGI, surfaceData.occlusion, normalWS, viewDirectionWS);

				// LightingPhysicallyBased computes direct light contribution.
				color += LightingPhysicallyBased(brdfData, mainLight, normalWS, viewDirectionWS);

				// Additional lights loop
#ifdef _ADDITIONAL_LIGHTS

				// Returns the amount of lights affecting the object being renderer.
				// These lights are culled per-object in the forward renderer
				int additionalLightsCount = GetAdditionalLightsCount();
				for (int i = 0; i < additionalLightsCount; ++i)
				{
					// Similar to GetMainLight, but it takes a for-loop index. This figures out the
					// per-object light index and samples the light buffer accordingly to initialized the
					// Light struct. If _ADDITIONAL_LIGHT_SHADOWS is defined it will also compute shadows.
					Light light = GetAdditionalLight(i, positionWS);

					// Same functions used to shade the main light.
					color += LightingPhysicallyBased(brdfData, light, normalWS, viewDirectionWS);
				}
#endif
				// Emission
				color += surfaceData.emission;

				float fogFactor = input.positionWSAndFogFactor.w;

				// Mix the pixel color with fogColor. You can optionaly use MixFogColor to override the fogColor
				// with a custom one.
				color = MixFog(color, fogFactor);
				return half4(color, surfaceData.alpha);
			}
			ENDHLSL
		}

			// Used for rendering shadowmaps
			UsePass "Universal Render Pipeline/Lit/ShadowCaster"

				// Used for depth prepass
				// If shadows cascade are enabled we need to perform a depth prepass. 
				// We also need to use a depth prepass in some cases camera require depth texture
				// (e.g, MSAA is enabled and we can't resolve with Texture2DMS
				UsePass "Universal Render Pipeline/Lit/DepthOnly"

				// Used for Baking GI. This pass is stripped from build.
				UsePass "Universal Render Pipeline/Lit/Meta"
		}

			// Uses a custom shader GUI to display settings. Re-use the same from Lit shader as they have the
			// same properties.
				CustomEditor "UnityEditor.Rendering.Universal.ShaderGUI.LitShader"
}