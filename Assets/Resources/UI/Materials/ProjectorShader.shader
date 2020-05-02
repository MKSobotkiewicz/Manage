
Shader "Projector/AdditiveTint" 
{
	Properties
	{
		_Texture("Texture", 2D) = "white" {}
		[HDR]_Color("Color", Color) = (1,1,1,1)
	}
		Subshader
		{
			Tags {"Queue" = "Transparent"}
			Pass
			{
				ZTest Off
				ZWrite Off
				ColorMask RGB
				Blend SrcAlpha One 

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct v2f 
				{
					float4 uv : TEXCOORD0;
					float4 pos : SV_POSITION;
				};

				float4x4 unity_Projector;
				float4x4 unity_ProjectorClip;

				v2f vert(float4 vertex : POSITION)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(vertex);
					o.uv = mul(unity_Projector, vertex);
					return o;
				}

				sampler2D _Texture;
				fixed4 _Color;

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 tex = tex2Dproj(_Texture, UNITY_PROJ_COORD(i.uv));

					return tex* _Color;
				}
				ENDCG
			}
	}
}