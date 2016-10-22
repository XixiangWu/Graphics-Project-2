﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "ToonShader" {
	Properties{
		_Color("Diffuse Color", Color) = (1,0,0,1)
		_UnlitColor("Unlit Diffuse Color", Color) = (0.5,0.5,0.5,1)
		_DiffuseThreshold("Threshold for Diffuse Colors", Range(0,1))
		= 0.1
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_LitOutlineThickness("Lit Outline Thickness", Range(0,1)) = 0.1
		_UnlitOutlineThickness("Unlit Outline Thickness", Range(0,1))
		= 0.4
		_SpecColor("Specular Color", Color) = (1,1,1,1)
		_Shininess("Shininess", Float) = 10
	}
		SubShader{
		Pass{

		CGPROGRAM

#pragma vertex vert  
#pragma fragment frag 

#include "UnityCG.cginc"

	uniform float3 lightColor;
	uniform float3 lightPosition;
	uniform float4 _Color;
	uniform float4 _UnlitColor;
	uniform float _DiffuseThreshold;
	uniform float4 _OutlineColor;
	uniform float _LitOutlineThickness;
	uniform float _UnlitOutlineThickness;
	uniform float4 _SpecColor;
	uniform float _Shininess;

	struct vIn {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct vOut {
		float4 pos : SV_POSITION;
		float4 posWorld : TEXCOORD0;
		float3 normalDir : TEXCOORD1;
	};

	
	
	vOut vert(vIn input){
		vOut output;

		float4x4 modelMatrix = unity_ObjectToWorld;
		float4x4 modelMatrixInverse = unity_WorldToObject;

		output.posWorld = mul(modelMatrix, input.vertex);
		output.normalDir = normalize(
			mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);
		output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
		return output;
	}

	float4 frag(vOut input) : COLOR{
		float3 normalDirection = normalize(input.normalDir);

		float3 viewDirection = normalize(
			_WorldSpaceCameraPos - input.posWorld.xyz);
		float3 lightDirection;
		float attenuation;


		float3 vertexToLightSource =
			_WorldSpaceLightPos0.xyz - input.posWorld.xyz;
		float distance = length(vertexToLightSource);
		attenuation = 1.0 / distance; // linear attenuation 
		lightDirection = normalize(vertexToLightSource);

		// default: unlit 
		float3 fragmentColor = _UnlitColor.rgb;

		// low priority: diffuse illumination
		if (attenuation
			* max(0.0, dot(normalDirection, lightDirection))
			>= _DiffuseThreshold)
		{
			fragmentColor = lightColor.rgb * _Color.rgb;
		}

		// higher priority: outline
		if (dot(viewDirection, normalDirection)
			< lerp(_UnlitOutlineThickness, _LitOutlineThickness,
				max(0.0, dot(normalDirection, lightDirection))))
		{
			fragmentColor = lightColor.rgb * _OutlineColor.rgb;
		}

		// highest priority: highlights
		if (dot(normalDirection, lightDirection) > 0.0
			// light source on the right side?
			&& attenuation *  pow(max(0.0, dot(
				reflect(-lightDirection, normalDirection),
				viewDirection)), _Shininess) > 0.5)
			// more than half highlight intensity? 
		{
			fragmentColor = _SpecColor.a
				* lightColor.rgb * _SpecColor.rgb
				+ (1.0 - _SpecColor.a) * fragmentColor;
		}
		return float4(fragmentColor, 1.0);
	}


		ENDCG
	}//

	}
	Fallback "Specular"
}