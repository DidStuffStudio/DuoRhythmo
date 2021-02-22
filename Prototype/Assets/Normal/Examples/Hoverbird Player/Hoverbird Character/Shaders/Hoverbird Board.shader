Shader "Normal/Examples/Hoverbird Board" {
	Properties {
		_GradientTexture("GradientTexture", 2D) = "white" {}
		_FlowSpeed("FlowSpeed", Float) = 0.1
		_GradientScale("GradientScale", Float) = 0.1
		_Texture("Texture", 2D) = "white" {}
		[HideInInspector] _texcoord("", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back

		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"

		#pragma target 3.0
		#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
		  #define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
		#else
		  #define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
		#endif

		struct Input {
			float2 uv_texcoord;
			float3 worldPos;
			float3 viewDir;
		};

		UNITY_DECLARE_TEX2D_NOSAMPLER(_Texture);
		uniform float4 _Texture_ST;
		SamplerState sampler_Texture;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_GradientTexture);
		uniform float _GradientScale;
		uniform float _FlowSpeed;
		SamplerState sampler_GradientTexture;

		void surf(Input i , inout SurfaceOutputStandard o) {
			float2 uv_Texture = i.uv_texcoord * _Texture_ST.xy + _Texture_ST.zw;
			float3 vertex3Pos = mul(unity_WorldToObject, float4(i.worldPos , 1));
			float dotResult271 = dot(vertex3Pos , i.viewDir);
			float4 appendResult275 = (float4(dotResult271 , 0.0 , 0.0 , 0.0));
			float mulTime283 = _Time.y * _FlowSpeed;
			o.Albedo = (SAMPLE_TEXTURE2D(_Texture, sampler_Texture, uv_Texture) * SAMPLE_TEXTURE2D(_GradientTexture, sampler_GradientTexture, (appendResult275*_GradientScale + mulTime283).xy)).rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass {
			Name "ShadowCaster"
			Tags { "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2

			#include "HLSLSupport.cginc"
			#if (SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN)
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"

			struct v2f {
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			v2f vert(appdata_full v) {
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				Input customInputData;
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
				return o;
			}
			half4 frag(v2f IN
			#if !defined(CAN_SKIP_VPOS)
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target {
				UNITY_SETUP_INSTANCE_ID(IN);
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT(Input, surfIN);
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				surfIN.viewDir = worldViewDir;
				surfIN.worldPos = worldPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT(SurfaceOutputStandard, o)
				surf(surfIN, o);
				#if defined(CAN_SKIP_VPOS)
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT(IN)
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}
