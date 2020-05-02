Shader "Custom/SignShader"
{
    Properties
    {
        [HDR]_Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MetallicGlossMap("Metallic", 2D) = "black" {}
		[MaterialToggle] _isEmissive("Emmision", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

		#pragma target 5.0

		sampler2D _MainTex;
		sampler2D _MetallicGlossMap;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
		float _isEmissive;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 m = tex2D(_MetallicGlossMap, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Metallic = m.rgb;
			o.Smoothness = m.a;
			o.Emission = _isEmissive * _Color;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
