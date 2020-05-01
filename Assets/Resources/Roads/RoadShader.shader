Shader "Custom/RoadShader"
{
    Properties
    {
		_MainTex("Albedo", 2D) = "white" {}
		_MetallicGlossMap("Metallic", 2D) = "black" {}
		_BumpMap("Normal", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "Queue" = "AlphaTest"
			   "IgnoreProjector" = "True"
			   "RenderType" = "Opaque" }
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting On
		Cull Off
		//Blend One OneMinusSrcAlpha
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows nofog  alphatest:_Cutoff addshadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 5.0

		sampler2D _MainTex;
		sampler2D _MetallicGlossMap;
		sampler2D _BumpMap;

        struct Input
        {
            float2 uv_MainTex;
        };


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 m = tex2D(_MetallicGlossMap, IN.uv_MainTex);
			fixed3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
			o.Albedo = c.rgb;
			o.Metallic = m.rgb;
			o.Smoothness = m.a;
			o.Normal = n;
			o.Alpha = c.a*0.9;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
