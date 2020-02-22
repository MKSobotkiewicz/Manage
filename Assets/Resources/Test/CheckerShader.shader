Shader "Custom/CheckerShader" {
	Properties {
		_Texture("Albedo (RGB)", 2D) = "white" {}
		_Scale("Scale", Range(0,100)) = 1.0
	}
	SubShader {
			Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows
		//#pragma surface surf Lambert alpha

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 5.0

	sampler2D _Texture;


	struct Input {
		half3 worldPos;
		INTERNAL_DATA
	};

	half _Scale;

	void surf(Input IN, inout SurfaceOutputStandard o) {


		float3 projnorm = saturate(pow(WorldNormalVector(IN, o.Normal) * 2, 4));

		fixed4 c_1 = tex2D(_Texture, IN.worldPos.yz / _Scale);
		fixed4 c_2 = tex2D(_Texture, IN.worldPos.xy / _Scale);
		fixed4 c_3 = tex2D(_Texture, IN.worldPos.xz / _Scale);
		fixed4 c_4 = lerp(c_1, c_2, projnorm.z);
		fixed4 c = lerp(c_4, c_3,  projnorm.y);


		//o.Alpha = 1;
		o.Albedo = c.rgb;
		//o.Normal = 0;
		o.Metallic = 0;
		o.Smoothness = 0;
	}
	ENDCG
	}
			FallBack "Diffuse"
}
