Shader "Custom/tracks_shader"
{
    Properties
    {
        _MainTex ("Albedo", 2D) = "white" {}
		_MetallicTex("Matallic", 2D) = "white" {}
        _NormalTex ("Normal",2D) = "normal" {}
		_Speed ("Speed", Range(-10,10)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _MetallicTex;
		sampler2D _NormalTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
			half _Speed;
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed2 offset = fixed2(0, -_Speed*_Time.g%1);
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex+ offset);
			fixed4 m = tex2D (_MetallicTex, IN.uv_MainTex + offset);
			fixed3 n = UnpackNormal (tex2D (_NormalTex, IN.uv_MainTex + offset));
            o.Albedo = c.rgb;
            o.Metallic = m;
            o.Smoothness = m.a;
			o.Normal = n;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
