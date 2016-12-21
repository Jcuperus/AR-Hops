Shader "Hidden/Intensify"
{
	// This shader increases contrast by lowering color intensity of colors below a definable threshold and,
	// additionally, raising intensity for colors above that same threshold
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_gsIntensity ("Intensity", Range(0, 1)) = 0
		_gsThreshold ("Threshold", Range(0, 1)) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			uniform float _gsIntensity;
			uniform float _gsThreshold;

			float4 frag (v2f_img i) : COLOR
			{
				// Retrieve pre-existing color
				half4 c = tex2D(_MainTex, i.uv);
				
				//// Natural-looking intensity
				//float lum = c.r*.3 + c.g*.59 + c.b*.11;
				// Numerical intensity chosen instead of natural-looking intensity because of subjective visual preference
				float lum = (c.r + c.g + c.b) / 3;
				
				float thr = lum - clamp(_gsThreshold, 0, 1);
				
				// Intensity curve modification
				float ity = clamp(_gsIntensity, 0, 1);
				// Make a simple curve
				// This increases or decreases intensity more noticeably for higher and lower intensities
				ity = ity * ity + ity;
				
				// thr variable initialization could be incorporated here, removing the need for it
				// Unknown wether or not this affects performance
				c.rgb = lum * ity * thr + c.rgb;
				
				return c;
			}
			ENDCG
		}
	}
}
