// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Skybox/Starry Sky"
{
	Properties
	{
		_Color1("Top Color", Color) = (1, 1, 1, 0)
		_Color2("Horizon Color", Color) = (1, 1, 1, 0)
		_Color3("Bottom Color", Color) = (1, 1, 1, 0)
		_Exponent1("Exponent Factor for Top Half", Float) = 1.0
		_Exponent2("Exponent Factor for Bottom Half", Float) = 1.0
		_Intensity("Intensity Amplifier", Float) = 1.0
		
		_StarsSize("Stars Size", Range(0,0.1)) = 0.009
		_StarsDensity("Stars Density", Range(0,1)) = 0.02
		_StarsHash("Stars Hash", Vector) = (641, -113, 271, 1117)
		[HDR]_StarsColor("Stars Color", Color)= (1, 1, 1, 1)
	}

		CGINCLUDE

#include "UnityCG.cginc"

		struct appdata
	{
		float4 position : POSITION;
		float3 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		float4 position : SV_POSITION;
		float3 texcoord : TEXCOORD0;
	};

	half4 _Color1;
	half4 _Color2;
	half4 _Color3;
	half _Intensity;
	half _Exponent1;
	half _Exponent2;
	uniform half _StarsDensity;
	uniform half _StarsSize;
	uniform half4 _StarsHash;
	half4 _StarsColor;

	v2f vert(appdata v)
	{
		v2f o;
		o.position = UnityObjectToClipPos(v.position);
		o.texcoord = v.texcoord;
		return o;
	}

	half4 frag(v2f i) : COLOR
	{
		half3 ray = -i.texcoord.xyz;

		float p = normalize(i.texcoord).y;
		float p1 = 1.0f - pow(min(1.0f, 1.0f - p), _Exponent1);
		float p3 = 1.0f - pow(min(1.0f, 1.0f + p), _Exponent2);
		float p2 = 1.0f - p1 - p3;

		float4 color = (_Color1 * p1 + _Color2 * p2 + _Color3 * p3) * _Intensity;

		//Stars
		half3 pos = ray / _StarsSize;
		half3 center = round(pos);
		half hash = dot(_StarsHash.xyz, center) % _StarsHash.w;
		half threshold = _StarsHash.w * _StarsDensity;
		if (abs(hash) < threshold)
		{
			half dist = length(pos - center);
			half star = saturate(pow(saturate(0.5 - dist * dist) * 2, 14));
			color += star * _StarsColor;
		}
		//End Stars

		return color;
	}

		ENDCG

		SubShader
	{
		Tags{ "RenderType" = "Background" "Queue" = "Background" }
			Pass
		{
			ZWrite Off
			Cull Off
			Fog { Mode Off }
			CGPROGRAM
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}
}