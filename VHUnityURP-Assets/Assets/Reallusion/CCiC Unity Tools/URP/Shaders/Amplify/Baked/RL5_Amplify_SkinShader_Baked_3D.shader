// Made with Amplify Shader Editor v1.9.9.4
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Reallusion/Amplify/RL5_SkinShader_Baked_3D"
{
	Properties
	{
		_MainTex( "Main Tex", 2D ) = "white" {}
		_Color( "Color", Color ) = ( 1, 1, 1, 0 )
		_MetallicGlossMap( "Metallic Gloss Map", 2D ) = "white" {}
		_GlossMapScale( "Gloss Map Scale", Range( 0, 1 ) ) = 1
		_BumpMap( "Bump Map", 2D ) = "bump" {}
		_BumpScale( "Bump Scale", Range( 0, 2 ) ) = 1
		_OcclusionMap( "Occlusion Map", 2D ) = "white" {}
		[ToggleUI] _UseCavity( "Use Cavity", Float ) = 1
		_CavityMap( "CavityMap", 2D ) = "white" {}
		_CavityStrength( "Cavity Strength", Range( 0, 1 ) ) = 1
		_EmissionMap( "Emission Map", 2D ) = "white" {}
		[HDR] _EmissionColor( "Emission Color", Color ) = ( 0, 0, 0, 0 )
		[KeywordEnum( NONE,BUMP,DISPLACEMENT,DISPACEMENT_BUMP )] ENUM_DISPLACEMENT_MODE( "Use Displacement", Float ) = 0
		_DisplacementMap( "Displacement Map", 2D ) = "white" {}
		_DisplacementLevel( "Displacement Level", Range( 0, 1 ) ) = 0.5
		_DisplacementStrength( "Displacement Strength", Range( 0, 1 ) ) = 1
		_BumpStrength( "Bump Strength", Range( 0, 1 ) ) = 1
		_WrinkleDisplacementStrength( "Wrinkle Displacement Strength", Range( 0, 5 ) ) = 0
		[Toggle( BOOLEAN_USE_SSS_ON )] BOOLEAN_USE_SSS( "Use SSS", Float ) = 1
		_SubsurfaceBlurMap( "Subsurface Blur Map", 2D ) = "white" {}
		_SSSThicknessPack( "Subsurface Thickness Pack", 2D ) = "white" {}
		_ThicknessScaleMin( "Thickness Min", Range( 0, 1 ) ) = 0.4
		_ThicknessScale( "Thickness Max", Range( 0, 1 ) ) = 0.9
		_SubsurfaceFalloff1( "Subsurface Falloff", Color ) = ( 0.990566, 0.3446065, 0.2850213, 0 )
		_SSSTransmission( "SSS Transmission", Range( 0, 100 ) ) = 50
		_SSSNormalDistortion( "SSS Normal Distortion", Range( 0, 1 ) ) = 0.5
		_SSSBlurStrength( "SSS Blur Strength", Range( 0, 1 ) ) = 0.5
		_DetailMask( "Detail Mask", 2D ) = "white" {}
		_OcclusionStrength( "Occlusion Strength", Range( 0, 1 ) ) = 1
		_DetailNormalMap( "Detail Normal Map", 2D ) = "bump" {}
		_DetailNormalMapScale( "Detail Normal Map Scale", Range( 0, 2 ) ) = 1
		[KeywordEnum( NONE,WRINKLE,WRINKLE_DISPLACEMENT )] ENUM_WRINKLE_MODE( "Wrinkle Mode", Float ) = 0
		_WrinkleDiffuseArray( "Wrinkle Diffuse Array", 2DArray ) = "white" {}
		_WrinkleMaskSetArray( "Wrinkle Mask Set Array", 2DArray ) = "black" {}
		_WrinkleSmoothnessPack( "Wrinkle Smoothness Pack", 2D ) = "gray" {}
		[Normal] _WrinkleNormalArray( "Wrinkle Normal Array", 2DArray ) = "bump" {}
		_WrinkleFlowPack( "Wrinkle Flow Pack", 2D ) = "white" {}
		_WrinkleDisplacementPack( "Wrinkle Displacement Pack", 2D ) = "linearGrey" {}
		_WrinkleValueSet1AL( "Wrinkle Value Set 1A Left", Vector ) = ( 0, 0, 0, 0 )
		_WrinkleValueSet1BL( "Wrinkle Value Set 1B Left", Vector ) = ( 0, 0, 0, 0 )
		_WrinkleValueSet2L( "Wrinkle Value Set 2 Left", Vector ) = ( 0, 0, 0, 0 )
		_WrinkleValueSet3L( "Wrinkle Value Set 3 Left", Vector ) = ( 0, 0, 0, 0 )
		_WrinkleValueSet12CL( "Wrinkle Value Set 12C Left", Vector ) = ( 0, 0, 0, 0 )
		_WrinkleValueSet1AR( "Wrinkle Value Set 1A Right", Vector ) = ( 0, 0, 0, 0 )
		_WrinkleValueSet1BR( "Wrinkle Value Set 1B Right", Vector ) = ( 0, 0, 0, 0 )
		_WrinkleValueSet2R( "Wrinkle Value Set 2 Right", Vector ) = ( 0, 0, 0, 0 )
		_WrinkleValueSet3R( "Wrinkle Value Set 3 Right", Vector ) = ( 0, 0, 0, 0 )
		_WrinkleValueSet12CR( "Wrinkle Value Set 12C Right", Vector ) = ( 0, 0, 0, 0 )
		_WrinkleValueSet3DB( "Wrinkle Value Set 3D Both", Vector ) = ( 0, 0, 0, 0 )
		_WrinkleValueSetBCCB( "Wrinkle Value Set BCC Both", Vector ) = ( 0, 0, 0, 0 )
		[HideInInspector] _texcoord( "", 2D ) = "white" {}


		//_TransmissionShadow( "Transmission Shadow", Range( 0, 1 ) ) = 0.5
		//_TransStrength( "Trans Strength", Range( 0, 50 ) ) = 1
		//_TransNormal( "Trans Normal Distortion", Range( 0, 1 ) ) = 0.5
		//_TransScattering( "Trans Scattering", Range( 1, 50 ) ) = 2
		//_TransDirect( "Trans Direct", Range( 0, 1 ) ) = 0.9
		//_TransAmbient( "Trans Ambient", Range( 0, 1 ) ) = 0.1
		//_TransShadow( "Trans Shadow", Range( 0, 1 ) ) = 0.5

		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25

		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _GlossyReflections("Reflections", Float) = 1.0

		//_InstancedTerrainNormals("Specular Highlights", Float) = 1.0
	}

	SubShader
	{
		

		

		Tags { "RenderType"="Opaque" "Queue"="Geometry" "DisableBatching"="False" }

	LOD 0

		Cull Back
		AlphaToMask Off
		ZWrite On
		ZTest LEqual
		ColorMask RGBA

		

		Blend Off
		

		CGINCLUDE
			#pragma target 3.5

			float4 FixedTess( float tessValue )
			{
				return tessValue;
			}

			float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
			{
				float3 wpos = mul(o2w,vertex).xyz;
				float dist = distance (wpos, cameraPos);
				float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
				return f;
			}

			float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
			{
				float4 tess;
				tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
				tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
				tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
				tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
				return tess;
			}

			float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
			{
				float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
				float len = distance(wpos0, wpos1);
				float f = max(len * scParams.y / (edgeLen * dist), 1.0);
				return f;
			}

			float DistanceFromPlane (float3 pos, float4 plane)
			{
				float d = dot (float4(pos,1.0f), plane);
				return d;
			}

			bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
			{
				float4 planeTest;
				planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
							  (( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
							  (( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
				planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
							  (( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
							  (( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
				planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
							  (( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
							  (( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
				planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
							  (( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
							  (( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
				return !all (planeTest);
			}

			float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
			{
				float3 f;
				f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
				f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
				f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

				return CalcTriEdgeTessFactors (f);
			}

			float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
			{
				float3 pos0 = mul(o2w,v0).xyz;
				float3 pos1 = mul(o2w,v1).xyz;
				float3 pos2 = mul(o2w,v2).xyz;
				float4 tess;
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
				return tess;
			}

			float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
			{
				float3 pos0 = mul(o2w,v0).xyz;
				float3 pos1 = mul(o2w,v1).xyz;
				float3 pos2 = mul(o2w,v2).xyz;
				float4 tess;

				if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
				{
					tess = 0.0f;
				}
				else
				{
					tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
					tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
					tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
					tess.w = (tess.x + tess.y + tess.z) / 3.0f;
				}
				return tess;
			}

			float4 ComputeClipSpacePosition( float2 screenPosNorm, float deviceDepth )
			{
				float4 positionCS = float4( screenPosNorm * 2.0 - 1.0, deviceDepth, 1.0 );
			#if UNITY_UV_STARTS_AT_TOP
				positionCS.y = -positionCS.y;
			#endif
				return positionCS;
			}
		ENDCG

		
		Pass
		{
			
			Name "ForwardBase"
			Tags { "LightMode"="ForwardBase" }

			Blend One Zero

			CGPROGRAM
				#define ASE_GEOMETRY
				#define ASE_FRAGMENT_NORMAL 0
				#define ASE_RECEIVE_SHADOWS
				#pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
				#pragma shader_feature_local_fragment _GLOSSYREFLECTIONS_OFF
				#pragma multi_compile_instancing
				#pragma multi_compile _ LOD_FADE_CROSSFADE
				#pragma multi_compile_fog
				#define ASE_FOG
				#define ASE_VERSION 19904
				#define ASE_USING_SAMPLING_MACROS 1

				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fwdbase
				#ifndef UNITY_PASS_FORWARDBASE
					#define UNITY_PASS_FORWARDBASE
				#endif
				#include "HLSLSupport.cginc"
				#if defined( ASE_GEOMETRY ) || defined( ASE_IMPOSTOR )
					#ifndef UNITY_INSTANCED_LOD_FADE
						#define UNITY_INSTANCED_LOD_FADE
					#endif
					#ifndef UNITY_INSTANCED_SH
						#define UNITY_INSTANCED_SH
					#endif
					#ifndef UNITY_INSTANCED_LIGHTMAPSTS
						#define UNITY_INSTANCED_LIGHTMAPSTS
					#endif
				#endif
				#include "UnityShaderVariables.cginc"
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"
				#include "AutoLight.cginc"

				#if defined( UNITY_INSTANCING_ENABLED ) && defined( ASE_INSTANCED_TERRAIN ) && ( defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL) || defined(_INSTANCEDTERRAINNORMALS_PIXEL) )
					#define ENABLE_TERRAIN_PERPIXEL_NORMAL
				#endif

				#include "UnityStandardUtils.cginc"
				#include "UnityStandardBRDF.cginc"
				#define ASE_NEEDS_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_NORMAL
				#define ASE_NEEDS_FRAG_TEXTURE_COORDINATES0
				#define ASE_NEEDS_WORLD_POSITION
				#define ASE_NEEDS_FRAG_WORLD_POSITION
				#define ASE_NEEDS_WORLD_NORMAL
				#define ASE_NEEDS_FRAG_WORLD_NORMAL
				#define ASE_NEEDS_WORLD_TANGENT
				#define ASE_NEEDS_FRAG_WORLD_TANGENT
				#define ASE_NEEDS_FRAG_WORLD_BITANGENT
				#define ASE_NEEDS_FRAG_WORLD_VIEW_DIR
				#pragma shader_feature_local ENUM_DISPLACEMENT_MODE_NONE ENUM_DISPLACEMENT_MODE_BUMP ENUM_DISPLACEMENT_MODE_DISPLACEMENT ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP
				#pragma shader_feature_local ENUM_WRINKLE_MODE_NONE ENUM_WRINKLE_MODE_WRINKLE ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT
				#pragma shader_feature_local BOOLEAN_USE_SSS_ON
				#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex.SampleBias(samplerTex,coord,bias)
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex.SampleGrad(samplerTex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#else//ASE Sampling Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex2Dbias(tex,float4(coord,0,bias))
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex2Dgrad(tex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY(tex,samplertex,coord) tex2DArray(tex,coord)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplertex,coord,lod) tex2DArraylod(tex, float4(coord,lod))
				#endif//ASE Sampling Macros
				


				struct appdata
				{
					float4 vertex : POSITION;
					half3 normal : NORMAL;
					half4 tangent : TANGENT;
					float4 texcoord : TEXCOORD0;
					float4 texcoord1 : TEXCOORD1;
					float4 texcoord2 : TEXCOORD2;
					
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 pos : SV_POSITION;
					float4 positionWS : TEXCOORD0; // xyz = positionWS, w = fogCoord
					half3 normalWS : TEXCOORD1;
					float4 tangentWS : TEXCOORD2; // holds terrainUV ifdef ENABLE_TERRAIN_PERPIXEL_NORMAL
					half4 ambientOrLightmapUV : TEXCOORD3;
					UNITY_LIGHTING_COORDS( 4, 5 )
					float4 ase_texcoord6 : TEXCOORD6;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};

				#ifdef ASE_TRANSMISSION
					float _TransmissionShadow;
				#endif
				#ifdef ASE_TRANSLUCENCY
					float _TransStrength;
					float _TransNormal;
					float _TransScattering;
					float _TransDirect;
					float _TransAmbient;
					float _TransShadow;
				#endif
				#ifdef ASE_TESSELLATION
					float _TessPhongStrength;
					float _TessValue;
					float _TessMin;
					float _TessMax;
					float _TessEdgeLength;
					float _TessMaxDisp;
				#endif

				UNITY_DECLARE_TEX2D_NOSAMPLER(_DisplacementMap);
				uniform float4 _DisplacementMap_ST;
				SamplerState sampler_Linear_Repeat;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleDisplacementPack);
				uniform float4 _WrinkleDisplacementPack_ST;
				SamplerState sampler_WrinkleDisplacementPack;
				uniform float _WrinkleDisplacementStrength;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleFlowPack);
				uniform float4 _WrinkleFlowPack_ST;
				uniform float4 _WrinkleValueSet12CL;
				uniform float4 _WrinkleMaskSetArray_ST;
				uniform float4 _WrinkleValueSet1AL;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleMaskSetArray);
				uniform float4 _WrinkleValueSet1BL;
				uniform float4 _WrinkleValueSet1AR;
				uniform float4 _WrinkleValueSet1BR;
				uniform float4 _WrinkleValueSet12CR;
				uniform float4 _WrinkleValueSet2L;
				uniform float4 _WrinkleValueSet2R;
				uniform float4 _WrinkleValueSetBCCB;
				uniform float4 _WrinkleValueSet3DB;
				uniform float4 _WrinkleValueSet3L;
				uniform float4 _WrinkleValueSet3R;
				uniform float _DisplacementLevel;
				uniform float _DisplacementStrength;
				uniform float4 _Color;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_MainTex);
				uniform float4 _MainTex_ST;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleDiffuseArray);
				uniform float4 _WrinkleDiffuseArray_ST;
				uniform float _ThicknessScale;
				uniform float _ThicknessScaleMin;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_SSSThicknessPack);
				uniform float4 _SSSThicknessPack_ST;
				uniform float _SSSTransmission;
				uniform float _SSSNormalDistortion;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_BumpMap);
				uniform float4 _BumpMap_ST;
				uniform float _BumpScale;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleNormalArray);
				uniform float4 _WrinkleNormalArray_ST;
				SamplerState sampler_WrinkleNormalArray;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_DetailNormalMap);
				uniform float4 _DetailNormalMap_ST;
				uniform float _DetailNormalMapScale;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_DetailMask);
				uniform float4 _DetailMask_ST;
				uniform float _BumpStrength;
				uniform float4 _SubsurfaceFalloff1;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_SubsurfaceBlurMap);
				uniform float4 _SubsurfaceBlurMap_ST;
				uniform float _SSSBlurStrength;
				uniform float _UseCavity;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_MetallicGlossMap);
				uniform float4 _MetallicGlossMap_ST;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleSmoothnessPack);
				uniform float4 _WrinkleSmoothnessPack_ST;
				uniform float _GlossMapScale;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_CavityMap);
				uniform float4 _CavityMap_ST;
				uniform float _CavityStrength;
				uniform float _OcclusionStrength;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_OcclusionMap);
				uniform float4 _OcclusionMap_ST;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissionMap);
				uniform float4 _EmissionMap_ST;
				uniform float4 _EmissionColor;


				//This is a late directive
				
				float MinMaxDeltaBlend1245_g1( float In, float In1, float In2, float In3 )
				{
					return max(max(In1, In2), In3) + min(min(In1, In2), In3) + In;
				}
				
				float4 MinMaxDeltaBlend4S181_g1( float4 In, float4 In1, float4 In2, float4 In3 )
				{
					return saturate(max(max(In1, In2), In3) + min(min(In1, In2), In3) + In);
				}
				
				float3 MinMaxDeltaBlend3183_g1( float3 In, float3 In1, float3 In2, float3 In3 )
				{
					return max(max(In1, In2), In3) + min(min(In1, In2), In3) + In;
				}
				
				float3 MyCustomExpression138_g110( float3 In, float Strength )
				{
					return float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
				}
				
				float MinMaxDeltaBlend1S182_g1( float In, float In1, float In2, float In3 )
				{
					return saturate(max(max(In1, In2), In3) + min(min(In1, In2), In3) + In);
				}
				
				float FasterFresnel416( float3 N, float3 V )
				{
					float NdotV = saturate(dot( N, V));
					float om = 1 - NdotV;
					float oms = om * om;
					return oms * oms;
				}
				

				v2f VertexFunction( appdata v  )
				{
					UNITY_SETUP_INSTANCE_ID(v);
					v2f o;
					UNITY_INITIALIZE_OUTPUT(v2f,o);
					UNITY_TRANSFER_INSTANCE_ID(v,o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					float3 _Vector0 = float3(0,0,0);
					float2 uv_DisplacementMap = v.texcoord.xyzw.xy * _DisplacementMap_ST.xy + _DisplacementMap_ST.zw;
					float displacementMap321 = SAMPLE_TEXTURE2D_LOD( _DisplacementMap, sampler_Linear_Repeat, uv_DisplacementMap, 0.0 ).r;
					float displacement229_g1 = displacementMap321;
					float In245_g1 = displacement229_g1;
					float2 uv_WrinkleDisplacementPack = v.texcoord.xyzw.xy * _WrinkleDisplacementPack_ST.xy + _WrinkleDisplacementPack_ST.zw;
					float4 tex2DNode279 = SAMPLE_TEXTURE2D_LOD( _WrinkleDisplacementPack, sampler_WrinkleDisplacementPack, uv_WrinkleDisplacementPack, 0.0 );
					float displacementStrength234_g1 = _WrinkleDisplacementStrength;
					float2 uv_WrinkleFlowPack = v.texcoord.xyzw.xy * _WrinkleFlowPack_ST.xy + _WrinkleFlowPack_ST.zw;
					float4 tex2DNode259 = SAMPLE_TEXTURE2D_LOD( _WrinkleFlowPack, sampler_Linear_Repeat, uv_WrinkleFlowPack, 0.0 );
					float2 texCoord10_g1 = v.texcoord.xyzw.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_1_0_g15 = 0.49;
					float leftMask27_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g15 ) / ( 0.51 - temp_output_1_0_g15 ) ) );
					float4 break107_g1 = _WrinkleValueSet12CL;
					float2 appendResult112_g1 = (float2(break107_g1.x , break107_g1.y));
					float2 uv_WrinkleMaskSetArray = v.texcoord.xyzw.xy;
					float4 break109_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,4.0), 0.0 );
					float2 appendResult115_g1 = (float2(break109_g1.x , break109_g1.y));
					float dotResult121_g1 = dot( appendResult112_g1 , appendResult115_g1 );
					float value1CLeft135_g1 = dotResult121_g1;
					float4 maskSet1A214_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,0.0), 0.0 );
					float dotResult29_g1 = dot( _WrinkleValueSet1AL , maskSet1A214_g1 );
					float4 temp_output_16_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,1.0), 0.0 );
					float dotResult35_g1 = dot( _WrinkleValueSet1BL , temp_output_16_0_g1 );
					float dotResult30_g1 = dot( maskSet1A214_g1 , _WrinkleValueSet1AR );
					float dotResult36_g1 = dot( temp_output_16_0_g1 , _WrinkleValueSet1BR );
					float4 break108_g1 = _WrinkleValueSet12CR;
					float2 appendResult117_g1 = (float2(break108_g1.x , break108_g1.y));
					float dotResult122_g1 = dot( appendResult115_g1 , appendResult117_g1 );
					float value1CRight136_g1 = dotResult122_g1;
					float temp_output_1_0_g14 = 0.51;
					float rightMask28_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g14 ) / ( 0.49 - temp_output_1_0_g14 ) ) );
					float temp_output_16_0_g78 = ( ( leftMask27_g1 * ( value1CLeft135_g1 + dotResult29_g1 + dotResult35_g1 ) ) + ( ( dotResult30_g1 + dotResult36_g1 + value1CRight136_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g78 = ( saturate( ( tex2DNode259.r + temp_output_16_0_g78 ) ) * temp_output_16_0_g78 );
					float In1245_g1 = ( ( tex2DNode279.r - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g78 );
					float2 appendResult113_g1 = (float2(break107_g1.z , break107_g1.w));
					float2 appendResult114_g1 = (float2(break109_g1.z , break109_g1.w));
					float dotResult123_g1 = dot( appendResult113_g1 , appendResult114_g1 );
					float value2CLeft137_g1 = dotResult123_g1;
					float4 temp_output_17_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,2.0), 0.0 );
					float dotResult41_g1 = dot( _WrinkleValueSet2L , temp_output_17_0_g1 );
					float dotResult42_g1 = dot( temp_output_17_0_g1 , _WrinkleValueSet2R );
					float2 appendResult116_g1 = (float2(break108_g1.z , break108_g1.w));
					float dotResult124_g1 = dot( appendResult114_g1 , appendResult116_g1 );
					float value2CRight138_g1 = dotResult124_g1;
					float temp_output_16_0_g80 = ( ( leftMask27_g1 * ( value2CLeft137_g1 + dotResult41_g1 ) ) + ( ( dotResult42_g1 + value2CRight138_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g80 = ( saturate( ( tex2DNode259.g + temp_output_16_0_g80 ) ) * temp_output_16_0_g80 );
					float In2245_g1 = ( ( tex2DNode279.g - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g80 );
					float4 break211_g1 = _WrinkleValueSetBCCB;
					float valueBCCLeft217_g1 = ( break211_g1.x * maskSet1A214_g1.y );
					float4 break118_g1 = _WrinkleValueSet3DB;
					float2 appendResult120_g1 = (float2(break118_g1.x , break118_g1.y));
					float dotResult127_g1 = dot( appendResult120_g1 , appendResult115_g1 );
					float value3DLeft129_g1 = dotResult127_g1;
					float4 temp_output_18_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,3.0), 0.0 );
					float dotResult47_g1 = dot( _WrinkleValueSet3L , temp_output_18_0_g1 );
					float dotResult48_g1 = dot( temp_output_18_0_g1 , _WrinkleValueSet3R );
					float2 appendResult119_g1 = (float2(break118_g1.z , break118_g1.w));
					float dotResult128_g1 = dot( appendResult115_g1 , appendResult119_g1 );
					float value3DRight130_g1 = dotResult128_g1;
					float valueBCCRight219_g1 = ( maskSet1A214_g1.y * break211_g1.y );
					float temp_output_16_0_g81 = ( ( leftMask27_g1 * ( valueBCCLeft217_g1 + value3DLeft129_g1 + dotResult47_g1 ) ) + ( ( dotResult48_g1 + value3DRight130_g1 + valueBCCRight219_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g81 = ( saturate( ( tex2DNode259.b + temp_output_16_0_g81 ) ) * temp_output_16_0_g81 );
					float In3245_g1 = ( ( tex2DNode279.b - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1245_g1 = MinMaxDeltaBlend1245_g1( In245_g1 , In1245_g1 , In2245_g1 , In3245_g1 );
					float displacementWrinkle331 = localMinMaxDeltaBlend1245_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch332 = displacementWrinkle331;
					#else
					float staticSwitch332 = displacementMap321;
					#endif
					float temp_output_319_0 = ( staticSwitch332 - _DisplacementLevel );
					float3 temp_output_327_0 = ( ( temp_output_319_0 * _DisplacementStrength ) * v.normal );
					#if defined( ENUM_DISPLACEMENT_MODE_NONE )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_BUMP )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPLACEMENT )
					float3 staticSwitch330 = temp_output_327_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP )
					float3 staticSwitch330 = temp_output_327_0;
					#else
					float3 staticSwitch330 = _Vector0;
					#endif
					
					o.ase_texcoord6.xy = v.texcoord.xyzw.xy;
					
					//setting value to unused interpolator channels and avoid initialization warnings
					o.ase_texcoord6.zw = 0;

					#ifdef ASE_ABSOLUTE_VERTEX_POS
						float3 defaultVertexValue = v.vertex.xyz;
					#else
						float3 defaultVertexValue = float3(0, 0, 0);
					#endif
					float3 vertexValue = staticSwitch330;
					#ifdef ASE_ABSOLUTE_VERTEX_POS
						v.vertex.xyz = vertexValue;
					#else
						v.vertex.xyz += vertexValue;
					#endif
					v.vertex.w = 1;
					v.normal = v.normal;
					v.tangent = v.tangent;

					float3 positionWS = mul( unity_ObjectToWorld, v.vertex ).xyz;
					half3 normalWS = UnityObjectToWorldNormal( v.normal );
					half3 tangentWS = UnityObjectToWorldDir( v.tangent.xyz );

					o.pos = UnityObjectToClipPos( v.vertex );
					o.positionWS.xyz = positionWS;
					o.normalWS = normalWS;
					o.tangentWS = half4( tangentWS, v.tangent.w );

					o.ambientOrLightmapUV = 0;
					#ifdef LIGHTMAP_ON
						o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
					#elif UNITY_SHOULD_SAMPLE_SH
						#ifdef VERTEXLIGHT_ON
							o.ambientOrLightmapUV.rgb += Shade4PointLights(
								unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
								unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
								unity_4LightAtten0, positionWS, normalWS );
						#endif
						o.ambientOrLightmapUV.rgb = ShadeSHPerVertex( normalWS, o.ambientOrLightmapUV.rgb );
					#endif
					#ifdef DYNAMICLIGHTMAP_ON
						o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
					#endif

					#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
						o.tangentWS.zw = v.texcoord.xy;
						o.tangentWS.xy = v.texcoord.xy * unity_LightmapST.xy + unity_LightmapST.zw;
					#endif

					UNITY_TRANSFER_LIGHTING(o, v.texcoord1.xy);
					#if defined( ASE_FOG )
						o.positionWS.w = o.pos.z;
					#endif
					return o;
				}

				#if defined(ASE_TESSELLATION)
				struct VertexControl
				{
					float4 vertex : INTERNALTESSPOS;
					half4 tangent : TANGENT;
					half3 normal : NORMAL;
					float4 texcoord : TEXCOORD0;
					float4 texcoord1 : TEXCOORD1;
					float4 texcoord2 : TEXCOORD2;
					
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct TessellationFactors
				{
					float edge[3] : SV_TessFactor;
					float inside : SV_InsideTessFactor;
				};

				VertexControl vert ( appdata v )
				{
					VertexControl o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.vertex = v.vertex;
					o.tangent = v.tangent;
					o.normal = v.normal;
					o.texcoord = v.texcoord;
					o.texcoord1 = v.texcoord1;
					o.texcoord2 = v.texcoord2;
					
					return o;
				}

				TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
				{
					TessellationFactors o;
					float4 tf = 1;
					float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
					float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
					#if defined(ASE_FIXED_TESSELLATION)
					tf = FixedTess( tessValue );
					#elif defined(ASE_DISTANCE_TESSELLATION)
					tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
					#elif defined(ASE_LENGTH_TESSELLATION)
					tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
					#elif defined(ASE_LENGTH_CULL_TESSELLATION)
					tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
					#endif
					o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
					return o;
				}

				[domain("tri")]
				[partitioning("fractional_odd")]
				[outputtopology("triangle_cw")]
				[patchconstantfunc("TessellationFunction")]
				[outputcontrolpoints(3)]
				VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
				{
				   return patch[id];
				}

				[domain("tri")]
				v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
				{
					appdata o = (appdata) 0;
					o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
					o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
					o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
					o.texcoord = patch[0].texcoord * bary.x + patch[1].texcoord * bary.y + patch[2].texcoord * bary.z;
					o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
					o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
					
					#if defined(ASE_PHONG_TESSELLATION)
					float3 pp[3];
					for (int i = 0; i < 3; ++i)
						pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
					float phongStrength = _TessPhongStrength;
					o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
					#endif
					UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
					return VertexFunction(o);
				}
				#else
				v2f vert ( appdata v )
				{
					return VertexFunction( v );
				}
				#endif

				half4 frag( v2f IN 
							#if defined( ASE_DEPTH_WRITE_ON )
								, out float outputDepth : SV_Depth
							#endif
							) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(IN);

					#ifdef LOD_FADE_CROSSFADE
						UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
					#endif

					#if defined(ASE_LIGHTING_SIMPLE)
						SurfaceOutput o = (SurfaceOutput)0;
					#else
						#if defined(_SPECULAR_SETUP)
							SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
						#else
							SurfaceOutputStandard o = (SurfaceOutputStandard)0;
						#endif
					#endif

					half atten;
					{
						#if defined( ASE_RECEIVE_SHADOWS )
							UNITY_LIGHT_ATTENUATION( temp, IN, IN.positionWS.xyz )
							atten = temp;
						#else
							atten = 1;
						#endif
					}

					float3 PositionWS = IN.positionWS.xyz;
					half3 ViewDirWS = normalize( UnityWorldSpaceViewDir( PositionWS ) );
					float4 ScreenPosNorm = float4( IN.pos.xy * ( _ScreenParams.zw - 1.0 ), IN.pos.zw );
					float4 ClipPos = ComputeClipSpacePosition( ScreenPosNorm.xy, IN.pos.z ) * IN.pos.w;
					float4 ScreenPos = ComputeScreenPos( ClipPos );
					half3 NormalWS = IN.normalWS;
					half3 TangentWS = IN.tangentWS.xyz;
					half3 BitangentWS = cross( IN.normalWS, IN.tangentWS.xyz ) * IN.tangentWS.w * unity_WorldTransformParams.w;
					half3 LightAtten = atten;
					float FogCoord = IN.positionWS.w;

					#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
						float2 sampleCoords = (IN.tangentWS.zw / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
						NormalWS = UnityObjectToWorldNormal(normalize(tex2D(_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
						TangentWS = -cross(unity_ObjectToWorld._13_23_33, NormalWS);
						BitangentWS = cross(NormalWS, -TangentWS);
					#endif

					float2 uv_MainTex = IN.ase_texcoord6.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					float4 diffuseMap216 = SAMPLE_TEXTURE2D( _MainTex, sampler_Linear_Repeat, uv_MainTex );
					float4 diffuse184_g1 = diffuseMap216;
					float4 In181_g1 = diffuse184_g1;
					float2 uv_WrinkleDiffuseArray = IN.ase_texcoord6.xy;
					float2 uv_WrinkleFlowPack = IN.ase_texcoord6.xy * _WrinkleFlowPack_ST.xy + _WrinkleFlowPack_ST.zw;
					float4 tex2DNode259 = SAMPLE_TEXTURE2D( _WrinkleFlowPack, sampler_Linear_Repeat, uv_WrinkleFlowPack );
					float2 texCoord10_g1 = IN.ase_texcoord6.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_1_0_g15 = 0.49;
					float leftMask27_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g15 ) / ( 0.51 - temp_output_1_0_g15 ) ) );
					float4 break107_g1 = _WrinkleValueSet12CL;
					float2 appendResult112_g1 = (float2(break107_g1.x , break107_g1.y));
					float2 uv_WrinkleMaskSetArray = IN.ase_texcoord6.xy;
					float4 break109_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,4.0) );
					float2 appendResult115_g1 = (float2(break109_g1.x , break109_g1.y));
					float dotResult121_g1 = dot( appendResult112_g1 , appendResult115_g1 );
					float value1CLeft135_g1 = dotResult121_g1;
					float4 maskSet1A214_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,0.0) );
					float dotResult29_g1 = dot( _WrinkleValueSet1AL , maskSet1A214_g1 );
					float4 temp_output_16_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,1.0) );
					float dotResult35_g1 = dot( _WrinkleValueSet1BL , temp_output_16_0_g1 );
					float dotResult30_g1 = dot( maskSet1A214_g1 , _WrinkleValueSet1AR );
					float dotResult36_g1 = dot( temp_output_16_0_g1 , _WrinkleValueSet1BR );
					float4 break108_g1 = _WrinkleValueSet12CR;
					float2 appendResult117_g1 = (float2(break108_g1.x , break108_g1.y));
					float dotResult122_g1 = dot( appendResult115_g1 , appendResult117_g1 );
					float value1CRight136_g1 = dotResult122_g1;
					float temp_output_1_0_g14 = 0.51;
					float rightMask28_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g14 ) / ( 0.49 - temp_output_1_0_g14 ) ) );
					float temp_output_16_0_g78 = ( ( leftMask27_g1 * ( value1CLeft135_g1 + dotResult29_g1 + dotResult35_g1 ) ) + ( ( dotResult30_g1 + dotResult36_g1 + value1CRight136_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g78 = ( saturate( ( tex2DNode259.r + temp_output_16_0_g78 ) ) * temp_output_16_0_g78 );
					float4 In1181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,0.0) ) - diffuse184_g1 ) * temp_output_23_0_g78 );
					float2 appendResult113_g1 = (float2(break107_g1.z , break107_g1.w));
					float2 appendResult114_g1 = (float2(break109_g1.z , break109_g1.w));
					float dotResult123_g1 = dot( appendResult113_g1 , appendResult114_g1 );
					float value2CLeft137_g1 = dotResult123_g1;
					float4 temp_output_17_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,2.0) );
					float dotResult41_g1 = dot( _WrinkleValueSet2L , temp_output_17_0_g1 );
					float dotResult42_g1 = dot( temp_output_17_0_g1 , _WrinkleValueSet2R );
					float2 appendResult116_g1 = (float2(break108_g1.z , break108_g1.w));
					float dotResult124_g1 = dot( appendResult114_g1 , appendResult116_g1 );
					float value2CRight138_g1 = dotResult124_g1;
					float temp_output_16_0_g80 = ( ( leftMask27_g1 * ( value2CLeft137_g1 + dotResult41_g1 ) ) + ( ( dotResult42_g1 + value2CRight138_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g80 = ( saturate( ( tex2DNode259.g + temp_output_16_0_g80 ) ) * temp_output_16_0_g80 );
					float4 In2181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,1.0) ) - diffuse184_g1 ) * temp_output_23_0_g80 );
					float4 break211_g1 = _WrinkleValueSetBCCB;
					float valueBCCLeft217_g1 = ( break211_g1.x * maskSet1A214_g1.y );
					float4 break118_g1 = _WrinkleValueSet3DB;
					float2 appendResult120_g1 = (float2(break118_g1.x , break118_g1.y));
					float dotResult127_g1 = dot( appendResult120_g1 , appendResult115_g1 );
					float value3DLeft129_g1 = dotResult127_g1;
					float4 temp_output_18_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,3.0) );
					float dotResult47_g1 = dot( _WrinkleValueSet3L , temp_output_18_0_g1 );
					float dotResult48_g1 = dot( temp_output_18_0_g1 , _WrinkleValueSet3R );
					float2 appendResult119_g1 = (float2(break118_g1.z , break118_g1.w));
					float dotResult128_g1 = dot( appendResult115_g1 , appendResult119_g1 );
					float value3DRight130_g1 = dotResult128_g1;
					float valueBCCRight219_g1 = ( maskSet1A214_g1.y * break211_g1.y );
					float temp_output_16_0_g81 = ( ( leftMask27_g1 * ( valueBCCLeft217_g1 + value3DLeft129_g1 + dotResult47_g1 ) ) + ( ( dotResult48_g1 + value3DRight130_g1 + valueBCCRight219_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g81 = ( saturate( ( tex2DNode259.b + temp_output_16_0_g81 ) ) * temp_output_16_0_g81 );
					float4 In3181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,2.0) ) - diffuse184_g1 ) * temp_output_23_0_g81 );
					float4 localMinMaxDeltaBlend4S181_g1 = MinMaxDeltaBlend4S181_g1( In181_g1 , In1181_g1 , In2181_g1 , In3181_g1 );
					float4 diffuseWrinkle229 = localMinMaxDeltaBlend4S181_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float4 staticSwitch313 = diffuseMap216;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float4 staticSwitch313 = diffuseWrinkle229;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float4 staticSwitch313 = diffuseWrinkle229;
					#else
					float4 staticSwitch313 = diffuseMap216;
					#endif
					float4 baseColor200 = ( _Color * staticSwitch313 );
					float2 uv_SSSThicknessPack = IN.ase_texcoord6.xy * _SSSThicknessPack_ST.xy + _SSSThicknessPack_ST.zw;
					float4 tex2DNode344 = SAMPLE_TEXTURE2D( _SSSThicknessPack, sampler_Linear_Repeat, uv_SSSThicknessPack );
					float lerpResult356 = lerp( _ThicknessScale , _ThicknessScaleMin , tex2DNode344.a);
					float temp_output_120_0_g110 = lerpResult356;
					float temp_output_144_0_g110 = ( 1.0 - temp_output_120_0_g110 );
					float2 uv_BumpMap = IN.ase_texcoord6.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
					float normalMapScale258 = _BumpScale;
					float3 normalMap218 = UnpackScaleNormal( SAMPLE_TEXTURE2D( _BumpMap, sampler_Linear_Repeat, uv_BumpMap ), normalMapScale258 );
					float3 normal186_g1 = normalMap218;
					float3 In183_g1 = normal186_g1;
					float2 uv_WrinkleNormalArray = IN.ase_texcoord6.xy;
					float3 In1183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,0.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g78 );
					float3 In2183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,1.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g80 );
					float3 In3183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,2.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g81 );
					float3 localMinMaxDeltaBlend3183_g1 = MinMaxDeltaBlend3183_g1( In183_g1 , In1183_g1 , In2183_g1 , In3183_g1 );
					float3 normalWrinkle231 = localMinMaxDeltaBlend3183_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float3 staticSwitch314 = normalMap218;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float3 staticSwitch314 = normalWrinkle231;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float3 staticSwitch314 = normalWrinkle231;
					#else
					float3 staticSwitch314 = normalMap218;
					#endif
					float2 uv_DetailNormalMap = IN.ase_texcoord6.xy * _DetailNormalMap_ST.xy + _DetailNormalMap_ST.zw;
					float2 uv_DetailMask = IN.ase_texcoord6.xy * _DetailMask_ST.xy + _DetailMask_ST.zw;
					float3 temp_output_155_0 = BlendNormals( staticSwitch314 , UnpackScaleNormal( SAMPLE_TEXTURE2D( _DetailNormalMap, sampler_Linear_Repeat, uv_DetailNormalMap ), ( _DetailNormalMapScale * SAMPLE_TEXTURE2D( _DetailMask, sampler_Linear_Repeat, uv_DetailMask ).g ) ) );
					float3 temp_output_111_0_g111 = ddx( PositionWS );
					float3 temp_output_113_0_g111 = cross( ddy( PositionWS ) , NormalWS );
					float dotResult115_g111 = dot( temp_output_111_0_g111 , temp_output_113_0_g111 );
					float2 uv_DisplacementMap = IN.ase_texcoord6.xy * _DisplacementMap_ST.xy + _DisplacementMap_ST.zw;
					float displacementMap321 = SAMPLE_TEXTURE2D( _DisplacementMap, sampler_Linear_Repeat, uv_DisplacementMap ).r;
					float displacement229_g1 = displacementMap321;
					float In245_g1 = displacement229_g1;
					float2 uv_WrinkleDisplacementPack = IN.ase_texcoord6.xy * _WrinkleDisplacementPack_ST.xy + _WrinkleDisplacementPack_ST.zw;
					float4 tex2DNode279 = SAMPLE_TEXTURE2D( _WrinkleDisplacementPack, sampler_WrinkleDisplacementPack, uv_WrinkleDisplacementPack );
					float displacementStrength234_g1 = _WrinkleDisplacementStrength;
					float In1245_g1 = ( ( tex2DNode279.r - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g78 );
					float In2245_g1 = ( ( tex2DNode279.g - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g80 );
					float In3245_g1 = ( ( tex2DNode279.b - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1245_g1 = MinMaxDeltaBlend1245_g1( In245_g1 , In1245_g1 , In2245_g1 , In3245_g1 );
					float displacementWrinkle331 = localMinMaxDeltaBlend1245_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch332 = displacementWrinkle331;
					#else
					float staticSwitch332 = displacementMap321;
					#endif
					float temp_output_319_0 = ( staticSwitch332 - _DisplacementLevel );
					float finalDisplacement322 = temp_output_319_0;
					float temp_output_20_0_g111 = finalDisplacement322;
					float3 normalizeResult130_g111 = normalize( ( ( abs( dotResult115_g111 ) * NormalWS ) - ( _BumpStrength * float3( 0.05,0.05,0.05 ) * sign( dotResult115_g111 ) * ( ( ddx( temp_output_20_0_g111 ) * temp_output_113_0_g111 ) + ( ddy( temp_output_20_0_g111 ) * cross( NormalWS , temp_output_111_0_g111 ) ) ) ) ) );
					float3x3 ase_worldToTangent = float3x3( TangentWS, BitangentWS, NormalWS );
					float3 worldToTangentDir42_g111 = mul( ase_worldToTangent, normalizeResult130_g111 );
					float3 temp_output_340_0 = BlendNormals( temp_output_155_0 , worldToTangentDir42_g111 );
					#if defined( ENUM_DISPLACEMENT_MODE_NONE )
					float3 staticSwitch336 = temp_output_155_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_BUMP )
					float3 staticSwitch336 = temp_output_340_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPLACEMENT )
					float3 staticSwitch336 = temp_output_155_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP )
					float3 staticSwitch336 = temp_output_340_0;
					#else
					float3 staticSwitch336 = temp_output_155_0;
					#endif
					float3 finalTangentNormal369 = staticSwitch336;
					float3 normal162_g110 = finalTangentNormal369;
					float3 tanToWorld0 = float3( TangentWS.x, BitangentWS.x, NormalWS.x );
					float3 tanToWorld1 = float3( TangentWS.y, BitangentWS.y, NormalWS.y );
					float3 tanToWorld2 = float3( TangentWS.z, BitangentWS.z, NormalWS.z );
					float3 tanNormal95_g110 = normal162_g110;
					float3 worldNormal95_g110 = normalize( float3( dot( tanToWorld0, tanNormal95_g110 ), dot( tanToWorld1, tanNormal95_g110 ), dot( tanToWorld2, tanNormal95_g110 ) ) );
					float3 worldSpaceLightDir = Unity_SafeNormalize( UnityWorldSpaceLightDir( PositionWS ) );
					float3 normalizeResult102_g110 = normalize( ( ( _SSSNormalDistortion * worldNormal95_g110 ) + worldSpaceLightDir ) );
					float dotResult106_g110 = dot( -normalizeResult102_g110 , ViewDirWS );
					float dotResult111_g110 = dot( worldNormal95_g110 , ViewDirWS );
					#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
					float4 ase_lightColor = 0;
					#else //aselc
					float4 ase_lightColor = _LightColor0;
					#endif //aselc
					float4 temp_output_124_0_g110 = _SubsurfaceFalloff1;
					float2 uv_SubsurfaceBlurMap = IN.ase_texcoord6.xy * _SubsurfaceBlurMap_ST.xy + _SubsurfaceBlurMap_ST.zw;
					float4 temp_output_123_0_g110 = SAMPLE_TEXTURE2D( _SubsurfaceBlurMap, sampler_Linear_Repeat, uv_SubsurfaceBlurMap );
					float temp_output_127_0_g110 = ( temp_output_120_0_g110 * tex2DNode344.r * _SSSBlurStrength );
					float4 lerpResult142_g110 = lerp( baseColor200 , temp_output_123_0_g110 , ( temp_output_124_0_g110 * temp_output_127_0_g110 ));
					float4 sssDiffuse362 = ( ( ( ( temp_output_144_0_g110 * temp_output_144_0_g110 ) * ( _SSSTransmission * saturate( dotResult106_g110 ) * saturate( ( 1.0 - dotResult111_g110 ) ) ) ) * ( ( ase_lightColor * temp_output_124_0_g110 ) * temp_output_123_0_g110 ) ) + lerpResult142_g110 );
					#ifdef BOOLEAN_USE_SSS_ON
					float4 staticSwitch368 = sssDiffuse362;
					#else
					float4 staticSwitch368 = baseColor200;
					#endif
					
					float3 In138_g110 = normal162_g110;
					float Strength138_g110 = ( 1.0 - temp_output_127_0_g110 );
					float3 localMyCustomExpression138_g110 = MyCustomExpression138_g110( In138_g110 , Strength138_g110 );
					float3 sssNormal361 = localMyCustomExpression138_g110;
					#ifdef BOOLEAN_USE_SSS_ON
					float3 staticSwitch367 = sssNormal361;
					#else
					float3 staticSwitch367 = finalTangentNormal369;
					#endif
					
					float2 uv_MetallicGlossMap = IN.ase_texcoord6.xy * _MetallicGlossMap_ST.xy + _MetallicGlossMap_ST.zw;
					float4 tex2DNode150 = SAMPLE_TEXTURE2D( _MetallicGlossMap, sampler_Linear_Repeat, uv_MetallicGlossMap );
					float smoothnessMap221 = tex2DNode150.a;
					float smoothness185_g1 = smoothnessMap221;
					float In182_g1 = smoothness185_g1;
					float2 uv_WrinkleSmoothnessPack = IN.ase_texcoord6.xy * _WrinkleSmoothnessPack_ST.xy + _WrinkleSmoothnessPack_ST.zw;
					float4 tex2DNode252 = SAMPLE_TEXTURE2D( _WrinkleSmoothnessPack, sampler_Linear_Repeat, uv_WrinkleSmoothnessPack );
					float In1182_g1 = ( ( tex2DNode252.r - smoothness185_g1 ) * temp_output_23_0_g78 );
					float In2182_g1 = ( ( tex2DNode252.g - smoothness185_g1 ) * temp_output_23_0_g80 );
					float In3182_g1 = ( ( tex2DNode252.b - smoothness185_g1 ) * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1S182_g1 = MinMaxDeltaBlend1S182_g1( In182_g1 , In1182_g1 , In2182_g1 , In3182_g1 );
					float smoothnessWrinkle230 = localMinMaxDeltaBlend1S182_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch341 = smoothnessMap221;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch341 = smoothnessWrinkle230;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch341 = smoothnessWrinkle230;
					#else
					float staticSwitch341 = smoothnessMap221;
					#endif
					float temp_output_168_0 = ( staticSwitch341 * _GlossMapScale );
					float2 uv_CavityMap = IN.ase_texcoord6.xy * _CavityMap_ST.xy + _CavityMap_ST.zw;
					float3 N416 = NormalWS;
					float3 V416 = ViewDirWS;
					float localFasterFresnel416 = FasterFresnel416( N416 , V416 );
					float lerpResult389 = lerp( 1.0 , SAMPLE_TEXTURE2D( _CavityMap, sampler_Linear_Repeat, uv_CavityMap ).r , ( _CavityStrength * saturate( ( 1.0 - localFasterFresnel416 ) ) ));
					
					float2 uv_OcclusionMap = IN.ase_texcoord6.xy * _OcclusionMap_ST.xy + _OcclusionMap_ST.zw;
					
					float2 uv_EmissionMap = IN.ase_texcoord6.xy * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
					

					o.Albedo = staticSwitch368.rgb;
					o.Normal = staticSwitch367;

					half3 Specular = half3( 0, 0, 0 );
					half Metallic = 0;
					half Smoothness = (( _UseCavity )?( ( temp_output_168_0 * lerpResult389 ) ):( temp_output_168_0 ));
					half Occlusion = ( 1.0 - ( _OcclusionStrength * ( 1.0 - SAMPLE_TEXTURE2D( _OcclusionMap, sampler_Linear_Repeat, uv_OcclusionMap ).g ) ) );

					#if defined(ASE_LIGHTING_SIMPLE)
						o.Specular = Specular.x;
						o.Gloss = Smoothness;
					#else
						#if defined(_SPECULAR_SETUP)
							o.Specular = Specular;
						#else
							o.Metallic = Metallic;
						#endif
						o.Occlusion = Occlusion;
						o.Smoothness = Smoothness;
					#endif

					o.Emission = ( SAMPLE_TEXTURE2D( _EmissionMap, sampler_Linear_Repeat, uv_EmissionMap ) * _EmissionColor ).rgb;
					o.Alpha = 1;
					half AlphaClipThreshold = 0.5;
					half AlphaClipThresholdShadow = 0.5;
					half3 BakedGI = 0;
					half3 Transmission = 1;
					half3 Translucency = 1;

					#if defined( ASE_DEPTH_WRITE_ON )
						float DeviceDepth = IN.pos.z;
					#endif

					#ifdef _ALPHATEST_ON
						clip( o.Alpha - AlphaClipThreshold );
					#endif

					#if defined( ASE_CHANGES_WORLD_POS )
					{
						#if defined( ASE_RECEIVE_SHADOWS )
							UNITY_LIGHT_ATTENUATION( temp, IN, PositionWS )
							LightAtten = temp;
						#else
							LightAtten = 1;
						#endif
					}
					#endif

					#if ( ASE_FRAGMENT_NORMAL == 0 )
						o.Normal = normalize( o.Normal.x * TangentWS + o.Normal.y * BitangentWS + o.Normal.z * NormalWS );
					#elif ( ASE_FRAGMENT_NORMAL == 1 )
						o.Normal = UnityObjectToWorldNormal( o.Normal );
					#elif ( ASE_FRAGMENT_NORMAL == 2 )
						// @diogo: already in world-space; do nothing
					#endif

					#if defined( ASE_DEPTH_WRITE_ON )
						outputDepth = DeviceDepth;
					#endif

					#ifndef USING_DIRECTIONAL_LIGHT
						half3 lightDir = normalize( UnityWorldSpaceLightDir( PositionWS ) );
					#else
						half3 lightDir = _WorldSpaceLightPos0.xyz;
					#endif

					UnityGI gi;
					UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
					gi.indirect.diffuse = 0;
					gi.indirect.specular = 0;
					gi.light.color = _LightColor0.rgb;
					gi.light.dir = lightDir;

					UnityGIInput giInput;
					UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
					giInput.light = gi.light;
					giInput.worldPos = PositionWS;
					giInput.worldViewDir = ViewDirWS;
					giInput.atten = atten;
					#if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
						giInput.lightmapUV = IN.ambientOrLightmapUV;
					#else
						giInput.lightmapUV = 0.0;
					#endif
					#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
						giInput.ambient = IN.ambientOrLightmapUV.rgb;
					#else
						giInput.ambient.rgb = 0.0;
					#endif
					giInput.probeHDR[0] = unity_SpecCube0_HDR;
					giInput.probeHDR[1] = unity_SpecCube1_HDR;
					#if defined(UNITY_SPECCUBE_BLENDING) || defined(UNITY_SPECCUBE_BOX_PROJECTION)
						giInput.boxMin[0] = unity_SpecCube0_BoxMin;
					#endif
					#ifdef UNITY_SPECCUBE_BOX_PROJECTION
						giInput.boxMax[0] = unity_SpecCube0_BoxMax;
						giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
						giInput.boxMax[1] = unity_SpecCube1_BoxMax;
						giInput.boxMin[1] = unity_SpecCube1_BoxMin;
						giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
					#endif

					#if defined(ASE_LIGHTING_SIMPLE)
						#if defined(_SPECULAR_SETUP)
							LightingBlinnPhong_GI(o, giInput, gi);
						#else
							LightingLambert_GI(o, giInput, gi);
						#endif
					#else
						#if defined(_SPECULAR_SETUP)
							LightingStandardSpecular_GI(o, giInput, gi);
						#else
							LightingStandard_GI(o, giInput, gi);
						#endif
					#endif

					#ifdef ASE_BAKEDGI
						gi.indirect.diffuse = BakedGI;
					#endif

					#if UNITY_SHOULD_SAMPLE_SH && !defined(LIGHTMAP_ON) && defined(ASE_NO_AMBIENT)
						gi.indirect.diffuse = 0;
					#endif

					half4 c = 0;
					#if defined(ASE_LIGHTING_SIMPLE)
						#if defined(_SPECULAR_SETUP)
							c += LightingBlinnPhong (o, ViewDirWS, gi);
						#else
							c += LightingLambert( o, gi );
						#endif
					#else
						#if defined(_SPECULAR_SETUP)
							c += LightingStandardSpecular (o, ViewDirWS, gi);
						#else
							c += LightingStandard(o, ViewDirWS, gi);
						#endif
					#endif

					#ifdef ASE_TRANSMISSION
					{
						half shadow = _TransmissionShadow;
						#ifdef DIRECTIONAL
							half3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, shadow );
						#else
							half3 lightAtten = gi.light.color;
						#endif
						half3 transmission = max(0 , -dot(o.Normal, gi.light.dir)) * lightAtten * Transmission;
						c.rgb += o.Albedo * transmission;
					}
					#endif

					#ifdef ASE_TRANSLUCENCY
					{
						half shadow = _TransShadow;
						half normal = _TransNormal;
						half scattering = _TransScattering;
						half direct = _TransDirect;
						half ambient = _TransAmbient;
						half strength = _TransStrength;

						#ifdef DIRECTIONAL
							half3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, shadow );
						#else
							half3 lightAtten = gi.light.color;
						#endif
						half3 lightDir = gi.light.dir + o.Normal * normal;
						half transVdotL = pow( saturate( dot( ViewDirWS, -lightDir ) ), scattering );
						half3 translucency = lightAtten * (transVdotL * direct + gi.indirect.diffuse * ambient) * Translucency;
						c.rgb += o.Albedo * translucency * strength;
					}
					#endif

					c.rgb += o.Emission;

					#if defined( ASE_FOG )
						UNITY_APPLY_FOG( FogCoord, c );
					#endif
					return c;
				}
			ENDCG
		}

		
		Pass
		{
			
			Name "ForwardAdd"
			Tags { "LightMode"="ForwardAdd" }
			ZWrite Off
			Blend One One

			CGPROGRAM
				#define ASE_GEOMETRY
				#define ASE_FRAGMENT_NORMAL 0
				#define ASE_RECEIVE_SHADOWS
				#pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
				#pragma multi_compile_instancing
				#pragma multi_compile _ LOD_FADE_CROSSFADE
				#pragma multi_compile_fog
				#define ASE_FOG
				#define ASE_VERSION 19904
				#define ASE_USING_SAMPLING_MACROS 1

				#pragma vertex vert
				#pragma fragment frag
				#pragma skip_variants INSTANCING_ON
				#pragma multi_compile_fwdadd_fullshadows
				#ifndef UNITY_PASS_FORWARDADD
					#define UNITY_PASS_FORWARDADD
				#endif
				#include "HLSLSupport.cginc"
				#if defined( ASE_GEOMETRY ) || defined( ASE_IMPOSTOR )
					#ifndef UNITY_INSTANCED_LOD_FADE
						#define UNITY_INSTANCED_LOD_FADE
					#endif
					#ifndef UNITY_INSTANCED_SH
						#define UNITY_INSTANCED_SH
					#endif
					#ifndef UNITY_INSTANCED_LIGHTMAPSTS
						#define UNITY_INSTANCED_LIGHTMAPSTS
					#endif
				#endif
				#include "UnityShaderVariables.cginc"
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"
				#include "AutoLight.cginc"

				#if defined( UNITY_INSTANCING_ENABLED ) && defined( ASE_INSTANCED_TERRAIN ) && ( defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL) || defined(_INSTANCEDTERRAINNORMALS_PIXEL) )
					#define ENABLE_TERRAIN_PERPIXEL_NORMAL
				#endif

				#include "UnityStandardUtils.cginc"
				#include "UnityStandardBRDF.cginc"
				#define ASE_NEEDS_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_NORMAL
				#define ASE_NEEDS_FRAG_TEXTURE_COORDINATES0
				#define ASE_NEEDS_WORLD_POSITION
				#define ASE_NEEDS_FRAG_WORLD_POSITION
				#define ASE_NEEDS_WORLD_NORMAL
				#define ASE_NEEDS_FRAG_WORLD_NORMAL
				#define ASE_NEEDS_FRAG_WORLD_TANGENT
				#define ASE_NEEDS_FRAG_WORLD_BITANGENT
				#define ASE_NEEDS_FRAG_WORLD_VIEW_DIR
				#pragma shader_feature_local ENUM_DISPLACEMENT_MODE_NONE ENUM_DISPLACEMENT_MODE_BUMP ENUM_DISPLACEMENT_MODE_DISPLACEMENT ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP
				#pragma shader_feature_local ENUM_WRINKLE_MODE_NONE ENUM_WRINKLE_MODE_WRINKLE ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT
				#pragma shader_feature_local BOOLEAN_USE_SSS_ON
				#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex.SampleBias(samplerTex,coord,bias)
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex.SampleGrad(samplerTex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#else//ASE Sampling Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex2Dbias(tex,float4(coord,0,bias))
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex2Dgrad(tex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY(tex,samplertex,coord) tex2DArray(tex,coord)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplertex,coord,lod) tex2DArraylod(tex, float4(coord,lod))
				#endif//ASE Sampling Macros
				


				struct appdata
				{
					float4 vertex : POSITION;
					half3 normal : NORMAL;
					half4 tangent : TANGENT;
					float4 texcoord : TEXCOORD0;
					float4 texcoord1 : TEXCOORD1;
					float4 texcoord2 : TEXCOORD2;
					
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 pos : SV_POSITION;
					float4 positionWS : TEXCOORD0; // xyz = positionWS, w = fogCoord
					half3 normalWS : TEXCOORD1;
					float4 tangentWS : TEXCOORD2; // holds terrainUV ifdef ENABLE_TERRAIN_PERPIXEL_NORMAL
					UNITY_LIGHTING_COORDS( 3, 4 )
					float4 ase_texcoord5 : TEXCOORD5;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};

				#ifdef ASE_TRANSMISSION
					float _TransmissionShadow;
				#endif
				#ifdef ASE_TRANSLUCENCY
					float _TransStrength;
					float _TransNormal;
					float _TransScattering;
					float _TransDirect;
					float _TransAmbient;
					float _TransShadow;
				#endif
				#ifdef ASE_TESSELLATION
					float _TessPhongStrength;
					float _TessValue;
					float _TessMin;
					float _TessMax;
					float _TessEdgeLength;
					float _TessMaxDisp;
				#endif

				UNITY_DECLARE_TEX2D_NOSAMPLER(_DisplacementMap);
				uniform float4 _DisplacementMap_ST;
				SamplerState sampler_Linear_Repeat;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleDisplacementPack);
				uniform float4 _WrinkleDisplacementPack_ST;
				SamplerState sampler_WrinkleDisplacementPack;
				uniform float _WrinkleDisplacementStrength;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleFlowPack);
				uniform float4 _WrinkleFlowPack_ST;
				uniform float4 _WrinkleValueSet12CL;
				uniform float4 _WrinkleMaskSetArray_ST;
				uniform float4 _WrinkleValueSet1AL;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleMaskSetArray);
				uniform float4 _WrinkleValueSet1BL;
				uniform float4 _WrinkleValueSet1AR;
				uniform float4 _WrinkleValueSet1BR;
				uniform float4 _WrinkleValueSet12CR;
				uniform float4 _WrinkleValueSet2L;
				uniform float4 _WrinkleValueSet2R;
				uniform float4 _WrinkleValueSetBCCB;
				uniform float4 _WrinkleValueSet3DB;
				uniform float4 _WrinkleValueSet3L;
				uniform float4 _WrinkleValueSet3R;
				uniform float _DisplacementLevel;
				uniform float _DisplacementStrength;
				uniform float4 _Color;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_MainTex);
				uniform float4 _MainTex_ST;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleDiffuseArray);
				uniform float4 _WrinkleDiffuseArray_ST;
				uniform float _ThicknessScale;
				uniform float _ThicknessScaleMin;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_SSSThicknessPack);
				uniform float4 _SSSThicknessPack_ST;
				uniform float _SSSTransmission;
				uniform float _SSSNormalDistortion;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_BumpMap);
				uniform float4 _BumpMap_ST;
				uniform float _BumpScale;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleNormalArray);
				uniform float4 _WrinkleNormalArray_ST;
				SamplerState sampler_WrinkleNormalArray;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_DetailNormalMap);
				uniform float4 _DetailNormalMap_ST;
				uniform float _DetailNormalMapScale;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_DetailMask);
				uniform float4 _DetailMask_ST;
				uniform float _BumpStrength;
				uniform float4 _SubsurfaceFalloff1;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_SubsurfaceBlurMap);
				uniform float4 _SubsurfaceBlurMap_ST;
				uniform float _SSSBlurStrength;
				uniform float _UseCavity;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_MetallicGlossMap);
				uniform float4 _MetallicGlossMap_ST;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleSmoothnessPack);
				uniform float4 _WrinkleSmoothnessPack_ST;
				uniform float _GlossMapScale;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_CavityMap);
				uniform float4 _CavityMap_ST;
				uniform float _CavityStrength;
				uniform float _OcclusionStrength;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_OcclusionMap);
				uniform float4 _OcclusionMap_ST;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissionMap);
				uniform float4 _EmissionMap_ST;
				uniform float4 _EmissionColor;


				//This is a late directive
				
				float MinMaxDeltaBlend1245_g1( float In, float In1, float In2, float In3 )
				{
					return max(max(In1, In2), In3) + min(min(In1, In2), In3) + In;
				}
				
				float4 MinMaxDeltaBlend4S181_g1( float4 In, float4 In1, float4 In2, float4 In3 )
				{
					return saturate(max(max(In1, In2), In3) + min(min(In1, In2), In3) + In);
				}
				
				float3 MinMaxDeltaBlend3183_g1( float3 In, float3 In1, float3 In2, float3 In3 )
				{
					return max(max(In1, In2), In3) + min(min(In1, In2), In3) + In;
				}
				
				float3 MyCustomExpression138_g110( float3 In, float Strength )
				{
					return float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
				}
				
				float MinMaxDeltaBlend1S182_g1( float In, float In1, float In2, float In3 )
				{
					return saturate(max(max(In1, In2), In3) + min(min(In1, In2), In3) + In);
				}
				
				float FasterFresnel416( float3 N, float3 V )
				{
					float NdotV = saturate(dot( N, V));
					float om = 1 - NdotV;
					float oms = om * om;
					return oms * oms;
				}
				

				v2f VertexFunction (appdata v  ) {
					UNITY_SETUP_INSTANCE_ID(v);
					v2f o;
					UNITY_INITIALIZE_OUTPUT(v2f,o);
					UNITY_TRANSFER_INSTANCE_ID(v,o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					float3 _Vector0 = float3(0,0,0);
					float2 uv_DisplacementMap = v.texcoord.xyzw.xy * _DisplacementMap_ST.xy + _DisplacementMap_ST.zw;
					float displacementMap321 = SAMPLE_TEXTURE2D_LOD( _DisplacementMap, sampler_Linear_Repeat, uv_DisplacementMap, 0.0 ).r;
					float displacement229_g1 = displacementMap321;
					float In245_g1 = displacement229_g1;
					float2 uv_WrinkleDisplacementPack = v.texcoord.xyzw.xy * _WrinkleDisplacementPack_ST.xy + _WrinkleDisplacementPack_ST.zw;
					float4 tex2DNode279 = SAMPLE_TEXTURE2D_LOD( _WrinkleDisplacementPack, sampler_WrinkleDisplacementPack, uv_WrinkleDisplacementPack, 0.0 );
					float displacementStrength234_g1 = _WrinkleDisplacementStrength;
					float2 uv_WrinkleFlowPack = v.texcoord.xyzw.xy * _WrinkleFlowPack_ST.xy + _WrinkleFlowPack_ST.zw;
					float4 tex2DNode259 = SAMPLE_TEXTURE2D_LOD( _WrinkleFlowPack, sampler_Linear_Repeat, uv_WrinkleFlowPack, 0.0 );
					float2 texCoord10_g1 = v.texcoord.xyzw.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_1_0_g15 = 0.49;
					float leftMask27_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g15 ) / ( 0.51 - temp_output_1_0_g15 ) ) );
					float4 break107_g1 = _WrinkleValueSet12CL;
					float2 appendResult112_g1 = (float2(break107_g1.x , break107_g1.y));
					float2 uv_WrinkleMaskSetArray = v.texcoord.xyzw.xy;
					float4 break109_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,4.0), 0.0 );
					float2 appendResult115_g1 = (float2(break109_g1.x , break109_g1.y));
					float dotResult121_g1 = dot( appendResult112_g1 , appendResult115_g1 );
					float value1CLeft135_g1 = dotResult121_g1;
					float4 maskSet1A214_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,0.0), 0.0 );
					float dotResult29_g1 = dot( _WrinkleValueSet1AL , maskSet1A214_g1 );
					float4 temp_output_16_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,1.0), 0.0 );
					float dotResult35_g1 = dot( _WrinkleValueSet1BL , temp_output_16_0_g1 );
					float dotResult30_g1 = dot( maskSet1A214_g1 , _WrinkleValueSet1AR );
					float dotResult36_g1 = dot( temp_output_16_0_g1 , _WrinkleValueSet1BR );
					float4 break108_g1 = _WrinkleValueSet12CR;
					float2 appendResult117_g1 = (float2(break108_g1.x , break108_g1.y));
					float dotResult122_g1 = dot( appendResult115_g1 , appendResult117_g1 );
					float value1CRight136_g1 = dotResult122_g1;
					float temp_output_1_0_g14 = 0.51;
					float rightMask28_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g14 ) / ( 0.49 - temp_output_1_0_g14 ) ) );
					float temp_output_16_0_g78 = ( ( leftMask27_g1 * ( value1CLeft135_g1 + dotResult29_g1 + dotResult35_g1 ) ) + ( ( dotResult30_g1 + dotResult36_g1 + value1CRight136_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g78 = ( saturate( ( tex2DNode259.r + temp_output_16_0_g78 ) ) * temp_output_16_0_g78 );
					float In1245_g1 = ( ( tex2DNode279.r - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g78 );
					float2 appendResult113_g1 = (float2(break107_g1.z , break107_g1.w));
					float2 appendResult114_g1 = (float2(break109_g1.z , break109_g1.w));
					float dotResult123_g1 = dot( appendResult113_g1 , appendResult114_g1 );
					float value2CLeft137_g1 = dotResult123_g1;
					float4 temp_output_17_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,2.0), 0.0 );
					float dotResult41_g1 = dot( _WrinkleValueSet2L , temp_output_17_0_g1 );
					float dotResult42_g1 = dot( temp_output_17_0_g1 , _WrinkleValueSet2R );
					float2 appendResult116_g1 = (float2(break108_g1.z , break108_g1.w));
					float dotResult124_g1 = dot( appendResult114_g1 , appendResult116_g1 );
					float value2CRight138_g1 = dotResult124_g1;
					float temp_output_16_0_g80 = ( ( leftMask27_g1 * ( value2CLeft137_g1 + dotResult41_g1 ) ) + ( ( dotResult42_g1 + value2CRight138_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g80 = ( saturate( ( tex2DNode259.g + temp_output_16_0_g80 ) ) * temp_output_16_0_g80 );
					float In2245_g1 = ( ( tex2DNode279.g - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g80 );
					float4 break211_g1 = _WrinkleValueSetBCCB;
					float valueBCCLeft217_g1 = ( break211_g1.x * maskSet1A214_g1.y );
					float4 break118_g1 = _WrinkleValueSet3DB;
					float2 appendResult120_g1 = (float2(break118_g1.x , break118_g1.y));
					float dotResult127_g1 = dot( appendResult120_g1 , appendResult115_g1 );
					float value3DLeft129_g1 = dotResult127_g1;
					float4 temp_output_18_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,3.0), 0.0 );
					float dotResult47_g1 = dot( _WrinkleValueSet3L , temp_output_18_0_g1 );
					float dotResult48_g1 = dot( temp_output_18_0_g1 , _WrinkleValueSet3R );
					float2 appendResult119_g1 = (float2(break118_g1.z , break118_g1.w));
					float dotResult128_g1 = dot( appendResult115_g1 , appendResult119_g1 );
					float value3DRight130_g1 = dotResult128_g1;
					float valueBCCRight219_g1 = ( maskSet1A214_g1.y * break211_g1.y );
					float temp_output_16_0_g81 = ( ( leftMask27_g1 * ( valueBCCLeft217_g1 + value3DLeft129_g1 + dotResult47_g1 ) ) + ( ( dotResult48_g1 + value3DRight130_g1 + valueBCCRight219_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g81 = ( saturate( ( tex2DNode259.b + temp_output_16_0_g81 ) ) * temp_output_16_0_g81 );
					float In3245_g1 = ( ( tex2DNode279.b - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1245_g1 = MinMaxDeltaBlend1245_g1( In245_g1 , In1245_g1 , In2245_g1 , In3245_g1 );
					float displacementWrinkle331 = localMinMaxDeltaBlend1245_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch332 = displacementWrinkle331;
					#else
					float staticSwitch332 = displacementMap321;
					#endif
					float temp_output_319_0 = ( staticSwitch332 - _DisplacementLevel );
					float3 temp_output_327_0 = ( ( temp_output_319_0 * _DisplacementStrength ) * v.normal );
					#if defined( ENUM_DISPLACEMENT_MODE_NONE )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_BUMP )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPLACEMENT )
					float3 staticSwitch330 = temp_output_327_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP )
					float3 staticSwitch330 = temp_output_327_0;
					#else
					float3 staticSwitch330 = _Vector0;
					#endif
					
					o.ase_texcoord5.xy = v.texcoord.xyzw.xy;
					
					//setting value to unused interpolator channels and avoid initialization warnings
					o.ase_texcoord5.zw = 0;

					#ifdef ASE_ABSOLUTE_VERTEX_POS
						float3 defaultVertexValue = v.vertex.xyz;
					#else
						float3 defaultVertexValue = float3(0, 0, 0);
					#endif
					float3 vertexValue = staticSwitch330;
					#ifdef ASE_ABSOLUTE_VERTEX_POS
						v.vertex.xyz = vertexValue;
					#else
						v.vertex.xyz += vertexValue;
					#endif
					v.vertex.w = 1;
					v.normal = v.normal;
					v.tangent = v.tangent;

					float3 positionWS = mul( unity_ObjectToWorld, v.vertex ).xyz;
					half3 normalWS = UnityObjectToWorldNormal( v.normal );
					half3 tangentWS = UnityObjectToWorldDir( v.tangent.xyz );

					o.pos = UnityObjectToClipPos( v.vertex );
					o.positionWS.xyz = positionWS;
					o.normalWS = normalWS;
					o.tangentWS = half4( tangentWS, v.tangent.w );

					UNITY_TRANSFER_LIGHTING(o, v.texcoord1.xy);
					#if defined( ASE_FOG )
						o.positionWS.w = o.pos.z;
					#endif

					#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
						o.tangentWS.zw = v.texcoord.xy;
						o.tangentWS.xy = v.texcoord.xy * unity_LightmapST.xy + unity_LightmapST.zw;
					#endif
					return o;
				}

				#if defined(ASE_TESSELLATION)
				struct VertexControl
				{
					float4 vertex : INTERNALTESSPOS;
					half4 tangent : TANGENT;
					half3 normal : NORMAL;
					float4 texcoord : TEXCOORD0;
					float4 texcoord1 : TEXCOORD1;
					float4 texcoord2 : TEXCOORD2;
					
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct TessellationFactors
				{
					float edge[3] : SV_TessFactor;
					float inside : SV_InsideTessFactor;
				};

				VertexControl vert ( appdata v )
				{
					VertexControl o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.vertex = v.vertex;
					o.tangent = v.tangent;
					o.normal = v.normal;
					o.texcoord = v.texcoord;
					o.texcoord1 = v.texcoord1;
					o.texcoord2 = v.texcoord2;
					
					return o;
				}

				TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
				{
					TessellationFactors o;
					float4 tf = 1;
					float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
					float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
					#if defined(ASE_FIXED_TESSELLATION)
					tf = FixedTess( tessValue );
					#elif defined(ASE_DISTANCE_TESSELLATION)
					tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
					#elif defined(ASE_LENGTH_TESSELLATION)
					tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
					#elif defined(ASE_LENGTH_CULL_TESSELLATION)
					tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
					#endif
					o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
					return o;
				}

				[domain("tri")]
				[partitioning("fractional_odd")]
				[outputtopology("triangle_cw")]
				[patchconstantfunc("TessellationFunction")]
				[outputcontrolpoints(3)]
				VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
				{
				   return patch[id];
				}

				[domain("tri")]
				v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
				{
					appdata o = (appdata) 0;
					o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
					o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
					o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
					o.texcoord = patch[0].texcoord * bary.x + patch[1].texcoord * bary.y + patch[2].texcoord * bary.z;
					o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
					o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
					
					#if defined(ASE_PHONG_TESSELLATION)
					float3 pp[3];
					for (int i = 0; i < 3; ++i)
						pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
					float phongStrength = _TessPhongStrength;
					o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
					#endif
					UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
					return VertexFunction(o);
				}
				#else
				v2f vert ( appdata v )
				{
					return VertexFunction( v );
				}
				#endif

				half4 frag ( v2f IN 
					#if defined( ASE_DEPTH_WRITE_ON )
					, out float outputDepth : SV_Depth
					#endif
					) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(IN);

					#ifdef LOD_FADE_CROSSFADE
						UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
					#endif

					#if defined(ASE_LIGHTING_SIMPLE)
						SurfaceOutput o = (SurfaceOutput)0;
					#else
						#if defined(_SPECULAR_SETUP)
							SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
						#else
							SurfaceOutputStandard o = (SurfaceOutputStandard)0;
						#endif
					#endif

					half atten;
					{
						#if defined( ASE_RECEIVE_SHADOWS )
							UNITY_LIGHT_ATTENUATION( temp, IN, IN.positionWS.xyz )
							atten = temp;
						#else
							atten = 1;
						#endif
					}

					float3 PositionWS = IN.positionWS.xyz;
					half3 ViewDirWS = normalize( UnityWorldSpaceViewDir( PositionWS ) );
					float4 ScreenPosNorm = float4( IN.pos.xy * ( _ScreenParams.zw - 1.0 ), IN.pos.zw );
					float4 ClipPos = ComputeClipSpacePosition( ScreenPosNorm.xy, IN.pos.z ) * IN.pos.w;
					float4 ScreenPos = ComputeScreenPos( ClipPos );
					half3 NormalWS = IN.normalWS;
					half3 TangentWS = IN.tangentWS.xyz;
					half3 BitangentWS = cross( IN.normalWS, IN.tangentWS.xyz ) * IN.tangentWS.w * unity_WorldTransformParams.w;
					half3 LightAtten = atten;
					float FogCoord = IN.positionWS.w;

					#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
						float2 sampleCoords = (IN.tangentWS.zw / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
						NormalWS = UnityObjectToWorldNormal(normalize(tex2D(_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
						TangentWS = -cross(unity_ObjectToWorld._13_23_33, NormalWS);
						BitangentWS = cross(NormalWS, -TangentWS);
					#endif

					float2 uv_MainTex = IN.ase_texcoord5.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					float4 diffuseMap216 = SAMPLE_TEXTURE2D( _MainTex, sampler_Linear_Repeat, uv_MainTex );
					float4 diffuse184_g1 = diffuseMap216;
					float4 In181_g1 = diffuse184_g1;
					float2 uv_WrinkleDiffuseArray = IN.ase_texcoord5.xy;
					float2 uv_WrinkleFlowPack = IN.ase_texcoord5.xy * _WrinkleFlowPack_ST.xy + _WrinkleFlowPack_ST.zw;
					float4 tex2DNode259 = SAMPLE_TEXTURE2D( _WrinkleFlowPack, sampler_Linear_Repeat, uv_WrinkleFlowPack );
					float2 texCoord10_g1 = IN.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_1_0_g15 = 0.49;
					float leftMask27_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g15 ) / ( 0.51 - temp_output_1_0_g15 ) ) );
					float4 break107_g1 = _WrinkleValueSet12CL;
					float2 appendResult112_g1 = (float2(break107_g1.x , break107_g1.y));
					float2 uv_WrinkleMaskSetArray = IN.ase_texcoord5.xy;
					float4 break109_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,4.0) );
					float2 appendResult115_g1 = (float2(break109_g1.x , break109_g1.y));
					float dotResult121_g1 = dot( appendResult112_g1 , appendResult115_g1 );
					float value1CLeft135_g1 = dotResult121_g1;
					float4 maskSet1A214_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,0.0) );
					float dotResult29_g1 = dot( _WrinkleValueSet1AL , maskSet1A214_g1 );
					float4 temp_output_16_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,1.0) );
					float dotResult35_g1 = dot( _WrinkleValueSet1BL , temp_output_16_0_g1 );
					float dotResult30_g1 = dot( maskSet1A214_g1 , _WrinkleValueSet1AR );
					float dotResult36_g1 = dot( temp_output_16_0_g1 , _WrinkleValueSet1BR );
					float4 break108_g1 = _WrinkleValueSet12CR;
					float2 appendResult117_g1 = (float2(break108_g1.x , break108_g1.y));
					float dotResult122_g1 = dot( appendResult115_g1 , appendResult117_g1 );
					float value1CRight136_g1 = dotResult122_g1;
					float temp_output_1_0_g14 = 0.51;
					float rightMask28_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g14 ) / ( 0.49 - temp_output_1_0_g14 ) ) );
					float temp_output_16_0_g78 = ( ( leftMask27_g1 * ( value1CLeft135_g1 + dotResult29_g1 + dotResult35_g1 ) ) + ( ( dotResult30_g1 + dotResult36_g1 + value1CRight136_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g78 = ( saturate( ( tex2DNode259.r + temp_output_16_0_g78 ) ) * temp_output_16_0_g78 );
					float4 In1181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,0.0) ) - diffuse184_g1 ) * temp_output_23_0_g78 );
					float2 appendResult113_g1 = (float2(break107_g1.z , break107_g1.w));
					float2 appendResult114_g1 = (float2(break109_g1.z , break109_g1.w));
					float dotResult123_g1 = dot( appendResult113_g1 , appendResult114_g1 );
					float value2CLeft137_g1 = dotResult123_g1;
					float4 temp_output_17_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,2.0) );
					float dotResult41_g1 = dot( _WrinkleValueSet2L , temp_output_17_0_g1 );
					float dotResult42_g1 = dot( temp_output_17_0_g1 , _WrinkleValueSet2R );
					float2 appendResult116_g1 = (float2(break108_g1.z , break108_g1.w));
					float dotResult124_g1 = dot( appendResult114_g1 , appendResult116_g1 );
					float value2CRight138_g1 = dotResult124_g1;
					float temp_output_16_0_g80 = ( ( leftMask27_g1 * ( value2CLeft137_g1 + dotResult41_g1 ) ) + ( ( dotResult42_g1 + value2CRight138_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g80 = ( saturate( ( tex2DNode259.g + temp_output_16_0_g80 ) ) * temp_output_16_0_g80 );
					float4 In2181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,1.0) ) - diffuse184_g1 ) * temp_output_23_0_g80 );
					float4 break211_g1 = _WrinkleValueSetBCCB;
					float valueBCCLeft217_g1 = ( break211_g1.x * maskSet1A214_g1.y );
					float4 break118_g1 = _WrinkleValueSet3DB;
					float2 appendResult120_g1 = (float2(break118_g1.x , break118_g1.y));
					float dotResult127_g1 = dot( appendResult120_g1 , appendResult115_g1 );
					float value3DLeft129_g1 = dotResult127_g1;
					float4 temp_output_18_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,3.0) );
					float dotResult47_g1 = dot( _WrinkleValueSet3L , temp_output_18_0_g1 );
					float dotResult48_g1 = dot( temp_output_18_0_g1 , _WrinkleValueSet3R );
					float2 appendResult119_g1 = (float2(break118_g1.z , break118_g1.w));
					float dotResult128_g1 = dot( appendResult115_g1 , appendResult119_g1 );
					float value3DRight130_g1 = dotResult128_g1;
					float valueBCCRight219_g1 = ( maskSet1A214_g1.y * break211_g1.y );
					float temp_output_16_0_g81 = ( ( leftMask27_g1 * ( valueBCCLeft217_g1 + value3DLeft129_g1 + dotResult47_g1 ) ) + ( ( dotResult48_g1 + value3DRight130_g1 + valueBCCRight219_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g81 = ( saturate( ( tex2DNode259.b + temp_output_16_0_g81 ) ) * temp_output_16_0_g81 );
					float4 In3181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,2.0) ) - diffuse184_g1 ) * temp_output_23_0_g81 );
					float4 localMinMaxDeltaBlend4S181_g1 = MinMaxDeltaBlend4S181_g1( In181_g1 , In1181_g1 , In2181_g1 , In3181_g1 );
					float4 diffuseWrinkle229 = localMinMaxDeltaBlend4S181_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float4 staticSwitch313 = diffuseMap216;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float4 staticSwitch313 = diffuseWrinkle229;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float4 staticSwitch313 = diffuseWrinkle229;
					#else
					float4 staticSwitch313 = diffuseMap216;
					#endif
					float4 baseColor200 = ( _Color * staticSwitch313 );
					float2 uv_SSSThicknessPack = IN.ase_texcoord5.xy * _SSSThicknessPack_ST.xy + _SSSThicknessPack_ST.zw;
					float4 tex2DNode344 = SAMPLE_TEXTURE2D( _SSSThicknessPack, sampler_Linear_Repeat, uv_SSSThicknessPack );
					float lerpResult356 = lerp( _ThicknessScale , _ThicknessScaleMin , tex2DNode344.a);
					float temp_output_120_0_g110 = lerpResult356;
					float temp_output_144_0_g110 = ( 1.0 - temp_output_120_0_g110 );
					float2 uv_BumpMap = IN.ase_texcoord5.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
					float normalMapScale258 = _BumpScale;
					float3 normalMap218 = UnpackScaleNormal( SAMPLE_TEXTURE2D( _BumpMap, sampler_Linear_Repeat, uv_BumpMap ), normalMapScale258 );
					float3 normal186_g1 = normalMap218;
					float3 In183_g1 = normal186_g1;
					float2 uv_WrinkleNormalArray = IN.ase_texcoord5.xy;
					float3 In1183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,0.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g78 );
					float3 In2183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,1.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g80 );
					float3 In3183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,2.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g81 );
					float3 localMinMaxDeltaBlend3183_g1 = MinMaxDeltaBlend3183_g1( In183_g1 , In1183_g1 , In2183_g1 , In3183_g1 );
					float3 normalWrinkle231 = localMinMaxDeltaBlend3183_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float3 staticSwitch314 = normalMap218;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float3 staticSwitch314 = normalWrinkle231;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float3 staticSwitch314 = normalWrinkle231;
					#else
					float3 staticSwitch314 = normalMap218;
					#endif
					float2 uv_DetailNormalMap = IN.ase_texcoord5.xy * _DetailNormalMap_ST.xy + _DetailNormalMap_ST.zw;
					float2 uv_DetailMask = IN.ase_texcoord5.xy * _DetailMask_ST.xy + _DetailMask_ST.zw;
					float3 temp_output_155_0 = BlendNormals( staticSwitch314 , UnpackScaleNormal( SAMPLE_TEXTURE2D( _DetailNormalMap, sampler_Linear_Repeat, uv_DetailNormalMap ), ( _DetailNormalMapScale * SAMPLE_TEXTURE2D( _DetailMask, sampler_Linear_Repeat, uv_DetailMask ).g ) ) );
					float3 temp_output_111_0_g111 = ddx( PositionWS );
					float3 temp_output_113_0_g111 = cross( ddy( PositionWS ) , NormalWS );
					float dotResult115_g111 = dot( temp_output_111_0_g111 , temp_output_113_0_g111 );
					float2 uv_DisplacementMap = IN.ase_texcoord5.xy * _DisplacementMap_ST.xy + _DisplacementMap_ST.zw;
					float displacementMap321 = SAMPLE_TEXTURE2D( _DisplacementMap, sampler_Linear_Repeat, uv_DisplacementMap ).r;
					float displacement229_g1 = displacementMap321;
					float In245_g1 = displacement229_g1;
					float2 uv_WrinkleDisplacementPack = IN.ase_texcoord5.xy * _WrinkleDisplacementPack_ST.xy + _WrinkleDisplacementPack_ST.zw;
					float4 tex2DNode279 = SAMPLE_TEXTURE2D( _WrinkleDisplacementPack, sampler_WrinkleDisplacementPack, uv_WrinkleDisplacementPack );
					float displacementStrength234_g1 = _WrinkleDisplacementStrength;
					float In1245_g1 = ( ( tex2DNode279.r - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g78 );
					float In2245_g1 = ( ( tex2DNode279.g - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g80 );
					float In3245_g1 = ( ( tex2DNode279.b - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1245_g1 = MinMaxDeltaBlend1245_g1( In245_g1 , In1245_g1 , In2245_g1 , In3245_g1 );
					float displacementWrinkle331 = localMinMaxDeltaBlend1245_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch332 = displacementWrinkle331;
					#else
					float staticSwitch332 = displacementMap321;
					#endif
					float temp_output_319_0 = ( staticSwitch332 - _DisplacementLevel );
					float finalDisplacement322 = temp_output_319_0;
					float temp_output_20_0_g111 = finalDisplacement322;
					float3 normalizeResult130_g111 = normalize( ( ( abs( dotResult115_g111 ) * NormalWS ) - ( _BumpStrength * float3( 0.05,0.05,0.05 ) * sign( dotResult115_g111 ) * ( ( ddx( temp_output_20_0_g111 ) * temp_output_113_0_g111 ) + ( ddy( temp_output_20_0_g111 ) * cross( NormalWS , temp_output_111_0_g111 ) ) ) ) ) );
					float3x3 ase_worldToTangent = float3x3( TangentWS, BitangentWS, NormalWS );
					float3 worldToTangentDir42_g111 = mul( ase_worldToTangent, normalizeResult130_g111 );
					float3 temp_output_340_0 = BlendNormals( temp_output_155_0 , worldToTangentDir42_g111 );
					#if defined( ENUM_DISPLACEMENT_MODE_NONE )
					float3 staticSwitch336 = temp_output_155_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_BUMP )
					float3 staticSwitch336 = temp_output_340_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPLACEMENT )
					float3 staticSwitch336 = temp_output_155_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP )
					float3 staticSwitch336 = temp_output_340_0;
					#else
					float3 staticSwitch336 = temp_output_155_0;
					#endif
					float3 finalTangentNormal369 = staticSwitch336;
					float3 normal162_g110 = finalTangentNormal369;
					float3 tanToWorld0 = float3( TangentWS.x, BitangentWS.x, NormalWS.x );
					float3 tanToWorld1 = float3( TangentWS.y, BitangentWS.y, NormalWS.y );
					float3 tanToWorld2 = float3( TangentWS.z, BitangentWS.z, NormalWS.z );
					float3 tanNormal95_g110 = normal162_g110;
					float3 worldNormal95_g110 = normalize( float3( dot( tanToWorld0, tanNormal95_g110 ), dot( tanToWorld1, tanNormal95_g110 ), dot( tanToWorld2, tanNormal95_g110 ) ) );
					float3 worldSpaceLightDir = Unity_SafeNormalize( UnityWorldSpaceLightDir( PositionWS ) );
					float3 normalizeResult102_g110 = normalize( ( ( _SSSNormalDistortion * worldNormal95_g110 ) + worldSpaceLightDir ) );
					float dotResult106_g110 = dot( -normalizeResult102_g110 , ViewDirWS );
					float dotResult111_g110 = dot( worldNormal95_g110 , ViewDirWS );
					#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
					float4 ase_lightColor = 0;
					#else //aselc
					float4 ase_lightColor = _LightColor0;
					#endif //aselc
					float4 temp_output_124_0_g110 = _SubsurfaceFalloff1;
					float2 uv_SubsurfaceBlurMap = IN.ase_texcoord5.xy * _SubsurfaceBlurMap_ST.xy + _SubsurfaceBlurMap_ST.zw;
					float4 temp_output_123_0_g110 = SAMPLE_TEXTURE2D( _SubsurfaceBlurMap, sampler_Linear_Repeat, uv_SubsurfaceBlurMap );
					float temp_output_127_0_g110 = ( temp_output_120_0_g110 * tex2DNode344.r * _SSSBlurStrength );
					float4 lerpResult142_g110 = lerp( baseColor200 , temp_output_123_0_g110 , ( temp_output_124_0_g110 * temp_output_127_0_g110 ));
					float4 sssDiffuse362 = ( ( ( ( temp_output_144_0_g110 * temp_output_144_0_g110 ) * ( _SSSTransmission * saturate( dotResult106_g110 ) * saturate( ( 1.0 - dotResult111_g110 ) ) ) ) * ( ( ase_lightColor * temp_output_124_0_g110 ) * temp_output_123_0_g110 ) ) + lerpResult142_g110 );
					#ifdef BOOLEAN_USE_SSS_ON
					float4 staticSwitch368 = sssDiffuse362;
					#else
					float4 staticSwitch368 = baseColor200;
					#endif
					
					float3 In138_g110 = normal162_g110;
					float Strength138_g110 = ( 1.0 - temp_output_127_0_g110 );
					float3 localMyCustomExpression138_g110 = MyCustomExpression138_g110( In138_g110 , Strength138_g110 );
					float3 sssNormal361 = localMyCustomExpression138_g110;
					#ifdef BOOLEAN_USE_SSS_ON
					float3 staticSwitch367 = sssNormal361;
					#else
					float3 staticSwitch367 = finalTangentNormal369;
					#endif
					
					float2 uv_MetallicGlossMap = IN.ase_texcoord5.xy * _MetallicGlossMap_ST.xy + _MetallicGlossMap_ST.zw;
					float4 tex2DNode150 = SAMPLE_TEXTURE2D( _MetallicGlossMap, sampler_Linear_Repeat, uv_MetallicGlossMap );
					float smoothnessMap221 = tex2DNode150.a;
					float smoothness185_g1 = smoothnessMap221;
					float In182_g1 = smoothness185_g1;
					float2 uv_WrinkleSmoothnessPack = IN.ase_texcoord5.xy * _WrinkleSmoothnessPack_ST.xy + _WrinkleSmoothnessPack_ST.zw;
					float4 tex2DNode252 = SAMPLE_TEXTURE2D( _WrinkleSmoothnessPack, sampler_Linear_Repeat, uv_WrinkleSmoothnessPack );
					float In1182_g1 = ( ( tex2DNode252.r - smoothness185_g1 ) * temp_output_23_0_g78 );
					float In2182_g1 = ( ( tex2DNode252.g - smoothness185_g1 ) * temp_output_23_0_g80 );
					float In3182_g1 = ( ( tex2DNode252.b - smoothness185_g1 ) * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1S182_g1 = MinMaxDeltaBlend1S182_g1( In182_g1 , In1182_g1 , In2182_g1 , In3182_g1 );
					float smoothnessWrinkle230 = localMinMaxDeltaBlend1S182_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch341 = smoothnessMap221;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch341 = smoothnessWrinkle230;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch341 = smoothnessWrinkle230;
					#else
					float staticSwitch341 = smoothnessMap221;
					#endif
					float temp_output_168_0 = ( staticSwitch341 * _GlossMapScale );
					float2 uv_CavityMap = IN.ase_texcoord5.xy * _CavityMap_ST.xy + _CavityMap_ST.zw;
					float3 N416 = NormalWS;
					float3 V416 = ViewDirWS;
					float localFasterFresnel416 = FasterFresnel416( N416 , V416 );
					float lerpResult389 = lerp( 1.0 , SAMPLE_TEXTURE2D( _CavityMap, sampler_Linear_Repeat, uv_CavityMap ).r , ( _CavityStrength * saturate( ( 1.0 - localFasterFresnel416 ) ) ));
					
					float2 uv_OcclusionMap = IN.ase_texcoord5.xy * _OcclusionMap_ST.xy + _OcclusionMap_ST.zw;
					
					float2 uv_EmissionMap = IN.ase_texcoord5.xy * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
					

					o.Albedo = staticSwitch368.rgb;
					o.Normal = staticSwitch367;

					half3 Specular = half3( 0, 0, 0 );
					half Metallic = 0;
					half Smoothness = (( _UseCavity )?( ( temp_output_168_0 * lerpResult389 ) ):( temp_output_168_0 ));
					half Occlusion = ( 1.0 - ( _OcclusionStrength * ( 1.0 - SAMPLE_TEXTURE2D( _OcclusionMap, sampler_Linear_Repeat, uv_OcclusionMap ).g ) ) );

					#if defined(ASE_LIGHTING_SIMPLE)
						o.Specular = Specular.x;
						o.Gloss = Smoothness;
					#else
						#if defined(_SPECULAR_SETUP)
							o.Specular = Specular;
						#else
							o.Metallic = Metallic;
						#endif
						o.Occlusion = Occlusion;
						o.Smoothness = Smoothness;
					#endif

					o.Emission = ( SAMPLE_TEXTURE2D( _EmissionMap, sampler_Linear_Repeat, uv_EmissionMap ) * _EmissionColor ).rgb;
					o.Alpha = 1;
					half AlphaClipThreshold = 0.5;
					half3 Transmission = 1;
					half3 Translucency = 1;

					#if defined( ASE_DEPTH_WRITE_ON )
						float DeviceDepth = IN.pos.z;
					#endif

					#ifdef _ALPHATEST_ON
						clip( o.Alpha - AlphaClipThreshold );
					#endif

					#if defined( ASE_CHANGES_WORLD_POS )
					{
						#if defined( ASE_RECEIVE_SHADOWS )
							UNITY_LIGHT_ATTENUATION( temp, IN, PositionWS )
							LightAtten = temp;
						#else
							LightAtten = 1;
						#endif
					}
					#endif

					#if ( ASE_FRAGMENT_NORMAL == 0 )
						o.Normal = normalize( o.Normal.x * TangentWS + o.Normal.y * BitangentWS + o.Normal.z * NormalWS );
					#elif ( ASE_FRAGMENT_NORMAL == 1 )
						o.Normal = UnityObjectToWorldNormal( o.Normal );
					#elif ( ASE_FRAGMENT_NORMAL == 2 )
						// @diogo: already in world-space; do nothing
					#endif

					#if defined( ASE_DEPTH_WRITE_ON )
						outputDepth = DeviceDepth;
					#endif

					#ifndef USING_DIRECTIONAL_LIGHT
						half3 lightDir = normalize( UnityWorldSpaceLightDir( PositionWS ) );
					#else
						half3 lightDir = _WorldSpaceLightPos0.xyz;
					#endif

					UnityGI gi;
					UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
					gi.indirect.diffuse = 0;
					gi.indirect.specular = 0;
					gi.light.color = _LightColor0.rgb;
					gi.light.dir = lightDir;
					gi.light.color *= atten;

					half4 c = 0;
					#if defined(ASE_LIGHTING_SIMPLE)
						#if defined(_SPECULAR_SETUP)
							c += LightingBlinnPhong (o, ViewDirWS, gi);
						#else
							c += LightingLambert( o, gi );
						#endif
					#else
						#if defined(_SPECULAR_SETUP)
							c += LightingStandardSpecular(o, ViewDirWS, gi);
						#else
							c += LightingStandard(o, ViewDirWS, gi);
						#endif
					#endif

					#ifdef ASE_TRANSMISSION
					{
						half shadow = _TransmissionShadow;
						#ifdef DIRECTIONAL
							half3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, shadow );
						#else
							half3 lightAtten = gi.light.color;
						#endif
						half3 transmission = max(0 , -dot(o.Normal, gi.light.dir)) * lightAtten * Transmission;
						c.rgb += o.Albedo * transmission;
					}
					#endif

					#ifdef ASE_TRANSLUCENCY
					{
						half shadow = _TransShadow;
						half normal = _TransNormal;
						half scattering = _TransScattering;
						half direct = _TransDirect;
						half ambient = _TransAmbient;
						half strength = _TransStrength;

						#ifdef DIRECTIONAL
							half3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, shadow );
						#else
							half3 lightAtten = gi.light.color;
						#endif
						half3 lightDir = gi.light.dir + o.Normal * normal;
						half transVdotL = pow( saturate( dot( ViewDirWS, -lightDir ) ), scattering );
						half3 translucency = lightAtten * (transVdotL * direct + gi.indirect.diffuse * ambient) * Translucency;
						c.rgb += o.Albedo * translucency * strength;
					}
					#endif

					#if defined( ASE_FOG )
						UNITY_APPLY_FOG( FogCoord, c );
					#endif
					return c;
				}
			ENDCG
		}

		
		Pass
		{
			
			Name "Deferred"
			Tags { "LightMode"="Deferred" }

			AlphaToMask Off

			CGPROGRAM
				#define ASE_GEOMETRY
				#define ASE_FRAGMENT_NORMAL 0
				#define ASE_RECEIVE_SHADOWS
				#pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
				#pragma shader_feature_local_fragment _GLOSSYREFLECTIONS_OFF
				#pragma multi_compile_instancing
				#pragma multi_compile _ LOD_FADE_CROSSFADE
				#define ASE_FOG
				#define ASE_VERSION 19904
				#define ASE_USING_SAMPLING_MACROS 1

				#pragma vertex vert
				#pragma fragment frag
				#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
				#pragma multi_compile_prepassfinal
				#ifndef UNITY_PASS_DEFERRED
					#define UNITY_PASS_DEFERRED
				#endif
				#include "HLSLSupport.cginc"
				#if defined( ASE_GEOMETRY ) || defined( ASE_IMPOSTOR )
					#ifndef UNITY_INSTANCED_LOD_FADE
						#define UNITY_INSTANCED_LOD_FADE
					#endif
					#ifndef UNITY_INSTANCED_SH
						#define UNITY_INSTANCED_SH
					#endif
					#ifndef UNITY_INSTANCED_LIGHTMAPSTS
						#define UNITY_INSTANCED_LIGHTMAPSTS
					#endif
				#endif
				#include "UnityShaderVariables.cginc"
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"

				#if defined( UNITY_INSTANCING_ENABLED ) && defined( ASE_INSTANCED_TERRAIN ) && ( defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL) || defined(_INSTANCEDTERRAINNORMALS_PIXEL) )
					#define ENABLE_TERRAIN_PERPIXEL_NORMAL
				#endif

				#include "UnityStandardUtils.cginc"
				#include "AutoLight.cginc"
				#include "UnityStandardBRDF.cginc"
				#define ASE_NEEDS_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_NORMAL
				#define ASE_NEEDS_FRAG_TEXTURE_COORDINATES0
				#define ASE_NEEDS_WORLD_POSITION
				#define ASE_NEEDS_FRAG_WORLD_POSITION
				#define ASE_NEEDS_WORLD_NORMAL
				#define ASE_NEEDS_FRAG_WORLD_NORMAL
				#define ASE_NEEDS_WORLD_TANGENT
				#define ASE_NEEDS_FRAG_WORLD_TANGENT
				#define ASE_NEEDS_FRAG_WORLD_BITANGENT
				#define ASE_NEEDS_FRAG_WORLD_VIEW_DIR
				#pragma shader_feature_local ENUM_DISPLACEMENT_MODE_NONE ENUM_DISPLACEMENT_MODE_BUMP ENUM_DISPLACEMENT_MODE_DISPLACEMENT ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP
				#pragma shader_feature_local ENUM_WRINKLE_MODE_NONE ENUM_WRINKLE_MODE_WRINKLE ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT
				#pragma shader_feature_local BOOLEAN_USE_SSS_ON
				#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex.SampleBias(samplerTex,coord,bias)
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex.SampleGrad(samplerTex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#else//ASE Sampling Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex2Dbias(tex,float4(coord,0,bias))
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex2Dgrad(tex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY(tex,samplertex,coord) tex2DArray(tex,coord)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplertex,coord,lod) tex2DArraylod(tex, float4(coord,lod))
				#endif//ASE Sampling Macros
				


				struct appdata
				{
					float4 vertex : POSITION;
					half3 normal : NORMAL;
					half4 tangent : TANGENT;
					float4 texcoord : TEXCOORD0;
					float4 texcoord1 : TEXCOORD1;
					float4 texcoord2 : TEXCOORD2;
					
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 pos : SV_POSITION;
					float4 positionWS : TEXCOORD0; // xyz = positionWS, w = fogCoord
					half3 normalWS : TEXCOORD1;
					float4 tangentWS : TEXCOORD2; // holds terrainUV ifdef ENABLE_TERRAIN_PERPIXEL_NORMAL
					half4 ambientOrLightmapUV : TEXCOORD3;
					float4 ase_texcoord4 : TEXCOORD4;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};

				#ifdef LIGHTMAP_ON
				float4 unity_LightmapFade;
				#endif
				half4 unity_Ambient;
				#ifdef ASE_TESSELLATION
					float _TessPhongStrength;
					float _TessValue;
					float _TessMin;
					float _TessMax;
					float _TessEdgeLength;
					float _TessMaxDisp;
				#endif

				UNITY_DECLARE_TEX2D_NOSAMPLER(_DisplacementMap);
				uniform float4 _DisplacementMap_ST;
				SamplerState sampler_Linear_Repeat;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleDisplacementPack);
				uniform float4 _WrinkleDisplacementPack_ST;
				SamplerState sampler_WrinkleDisplacementPack;
				uniform float _WrinkleDisplacementStrength;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleFlowPack);
				uniform float4 _WrinkleFlowPack_ST;
				uniform float4 _WrinkleValueSet12CL;
				uniform float4 _WrinkleMaskSetArray_ST;
				uniform float4 _WrinkleValueSet1AL;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleMaskSetArray);
				uniform float4 _WrinkleValueSet1BL;
				uniform float4 _WrinkleValueSet1AR;
				uniform float4 _WrinkleValueSet1BR;
				uniform float4 _WrinkleValueSet12CR;
				uniform float4 _WrinkleValueSet2L;
				uniform float4 _WrinkleValueSet2R;
				uniform float4 _WrinkleValueSetBCCB;
				uniform float4 _WrinkleValueSet3DB;
				uniform float4 _WrinkleValueSet3L;
				uniform float4 _WrinkleValueSet3R;
				uniform float _DisplacementLevel;
				uniform float _DisplacementStrength;
				uniform float4 _Color;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_MainTex);
				uniform float4 _MainTex_ST;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleDiffuseArray);
				uniform float4 _WrinkleDiffuseArray_ST;
				uniform float _ThicknessScale;
				uniform float _ThicknessScaleMin;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_SSSThicknessPack);
				uniform float4 _SSSThicknessPack_ST;
				uniform float _SSSTransmission;
				uniform float _SSSNormalDistortion;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_BumpMap);
				uniform float4 _BumpMap_ST;
				uniform float _BumpScale;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleNormalArray);
				uniform float4 _WrinkleNormalArray_ST;
				SamplerState sampler_WrinkleNormalArray;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_DetailNormalMap);
				uniform float4 _DetailNormalMap_ST;
				uniform float _DetailNormalMapScale;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_DetailMask);
				uniform float4 _DetailMask_ST;
				uniform float _BumpStrength;
				uniform float4 _SubsurfaceFalloff1;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_SubsurfaceBlurMap);
				uniform float4 _SubsurfaceBlurMap_ST;
				uniform float _SSSBlurStrength;
				uniform float _UseCavity;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_MetallicGlossMap);
				uniform float4 _MetallicGlossMap_ST;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleSmoothnessPack);
				uniform float4 _WrinkleSmoothnessPack_ST;
				uniform float _GlossMapScale;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_CavityMap);
				uniform float4 _CavityMap_ST;
				uniform float _CavityStrength;
				uniform float _OcclusionStrength;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_OcclusionMap);
				uniform float4 _OcclusionMap_ST;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissionMap);
				uniform float4 _EmissionMap_ST;
				uniform float4 _EmissionColor;


				//This is a late directive
				
				float MinMaxDeltaBlend1245_g1( float In, float In1, float In2, float In3 )
				{
					return max(max(In1, In2), In3) + min(min(In1, In2), In3) + In;
				}
				
				float4 MinMaxDeltaBlend4S181_g1( float4 In, float4 In1, float4 In2, float4 In3 )
				{
					return saturate(max(max(In1, In2), In3) + min(min(In1, In2), In3) + In);
				}
				
				float3 MinMaxDeltaBlend3183_g1( float3 In, float3 In1, float3 In2, float3 In3 )
				{
					return max(max(In1, In2), In3) + min(min(In1, In2), In3) + In;
				}
				
				float3 MyCustomExpression138_g110( float3 In, float Strength )
				{
					return float3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
				}
				
				float MinMaxDeltaBlend1S182_g1( float In, float In1, float In2, float In3 )
				{
					return saturate(max(max(In1, In2), In3) + min(min(In1, In2), In3) + In);
				}
				
				float FasterFresnel416( float3 N, float3 V )
				{
					float NdotV = saturate(dot( N, V));
					float om = 1 - NdotV;
					float oms = om * om;
					return oms * oms;
				}
				

				v2f VertexFunction (appdata v  ) {
					UNITY_SETUP_INSTANCE_ID(v);
					v2f o;
					UNITY_INITIALIZE_OUTPUT(v2f,o);
					UNITY_TRANSFER_INSTANCE_ID(v,o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					float3 _Vector0 = float3(0,0,0);
					float2 uv_DisplacementMap = v.texcoord.xyzw.xy * _DisplacementMap_ST.xy + _DisplacementMap_ST.zw;
					float displacementMap321 = SAMPLE_TEXTURE2D_LOD( _DisplacementMap, sampler_Linear_Repeat, uv_DisplacementMap, 0.0 ).r;
					float displacement229_g1 = displacementMap321;
					float In245_g1 = displacement229_g1;
					float2 uv_WrinkleDisplacementPack = v.texcoord.xyzw.xy * _WrinkleDisplacementPack_ST.xy + _WrinkleDisplacementPack_ST.zw;
					float4 tex2DNode279 = SAMPLE_TEXTURE2D_LOD( _WrinkleDisplacementPack, sampler_WrinkleDisplacementPack, uv_WrinkleDisplacementPack, 0.0 );
					float displacementStrength234_g1 = _WrinkleDisplacementStrength;
					float2 uv_WrinkleFlowPack = v.texcoord.xyzw.xy * _WrinkleFlowPack_ST.xy + _WrinkleFlowPack_ST.zw;
					float4 tex2DNode259 = SAMPLE_TEXTURE2D_LOD( _WrinkleFlowPack, sampler_Linear_Repeat, uv_WrinkleFlowPack, 0.0 );
					float2 texCoord10_g1 = v.texcoord.xyzw.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_1_0_g15 = 0.49;
					float leftMask27_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g15 ) / ( 0.51 - temp_output_1_0_g15 ) ) );
					float4 break107_g1 = _WrinkleValueSet12CL;
					float2 appendResult112_g1 = (float2(break107_g1.x , break107_g1.y));
					float2 uv_WrinkleMaskSetArray = v.texcoord.xyzw.xy;
					float4 break109_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,4.0), 0.0 );
					float2 appendResult115_g1 = (float2(break109_g1.x , break109_g1.y));
					float dotResult121_g1 = dot( appendResult112_g1 , appendResult115_g1 );
					float value1CLeft135_g1 = dotResult121_g1;
					float4 maskSet1A214_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,0.0), 0.0 );
					float dotResult29_g1 = dot( _WrinkleValueSet1AL , maskSet1A214_g1 );
					float4 temp_output_16_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,1.0), 0.0 );
					float dotResult35_g1 = dot( _WrinkleValueSet1BL , temp_output_16_0_g1 );
					float dotResult30_g1 = dot( maskSet1A214_g1 , _WrinkleValueSet1AR );
					float dotResult36_g1 = dot( temp_output_16_0_g1 , _WrinkleValueSet1BR );
					float4 break108_g1 = _WrinkleValueSet12CR;
					float2 appendResult117_g1 = (float2(break108_g1.x , break108_g1.y));
					float dotResult122_g1 = dot( appendResult115_g1 , appendResult117_g1 );
					float value1CRight136_g1 = dotResult122_g1;
					float temp_output_1_0_g14 = 0.51;
					float rightMask28_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g14 ) / ( 0.49 - temp_output_1_0_g14 ) ) );
					float temp_output_16_0_g78 = ( ( leftMask27_g1 * ( value1CLeft135_g1 + dotResult29_g1 + dotResult35_g1 ) ) + ( ( dotResult30_g1 + dotResult36_g1 + value1CRight136_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g78 = ( saturate( ( tex2DNode259.r + temp_output_16_0_g78 ) ) * temp_output_16_0_g78 );
					float In1245_g1 = ( ( tex2DNode279.r - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g78 );
					float2 appendResult113_g1 = (float2(break107_g1.z , break107_g1.w));
					float2 appendResult114_g1 = (float2(break109_g1.z , break109_g1.w));
					float dotResult123_g1 = dot( appendResult113_g1 , appendResult114_g1 );
					float value2CLeft137_g1 = dotResult123_g1;
					float4 temp_output_17_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,2.0), 0.0 );
					float dotResult41_g1 = dot( _WrinkleValueSet2L , temp_output_17_0_g1 );
					float dotResult42_g1 = dot( temp_output_17_0_g1 , _WrinkleValueSet2R );
					float2 appendResult116_g1 = (float2(break108_g1.z , break108_g1.w));
					float dotResult124_g1 = dot( appendResult114_g1 , appendResult116_g1 );
					float value2CRight138_g1 = dotResult124_g1;
					float temp_output_16_0_g80 = ( ( leftMask27_g1 * ( value2CLeft137_g1 + dotResult41_g1 ) ) + ( ( dotResult42_g1 + value2CRight138_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g80 = ( saturate( ( tex2DNode259.g + temp_output_16_0_g80 ) ) * temp_output_16_0_g80 );
					float In2245_g1 = ( ( tex2DNode279.g - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g80 );
					float4 break211_g1 = _WrinkleValueSetBCCB;
					float valueBCCLeft217_g1 = ( break211_g1.x * maskSet1A214_g1.y );
					float4 break118_g1 = _WrinkleValueSet3DB;
					float2 appendResult120_g1 = (float2(break118_g1.x , break118_g1.y));
					float dotResult127_g1 = dot( appendResult120_g1 , appendResult115_g1 );
					float value3DLeft129_g1 = dotResult127_g1;
					float4 temp_output_18_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,3.0), 0.0 );
					float dotResult47_g1 = dot( _WrinkleValueSet3L , temp_output_18_0_g1 );
					float dotResult48_g1 = dot( temp_output_18_0_g1 , _WrinkleValueSet3R );
					float2 appendResult119_g1 = (float2(break118_g1.z , break118_g1.w));
					float dotResult128_g1 = dot( appendResult115_g1 , appendResult119_g1 );
					float value3DRight130_g1 = dotResult128_g1;
					float valueBCCRight219_g1 = ( maskSet1A214_g1.y * break211_g1.y );
					float temp_output_16_0_g81 = ( ( leftMask27_g1 * ( valueBCCLeft217_g1 + value3DLeft129_g1 + dotResult47_g1 ) ) + ( ( dotResult48_g1 + value3DRight130_g1 + valueBCCRight219_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g81 = ( saturate( ( tex2DNode259.b + temp_output_16_0_g81 ) ) * temp_output_16_0_g81 );
					float In3245_g1 = ( ( tex2DNode279.b - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1245_g1 = MinMaxDeltaBlend1245_g1( In245_g1 , In1245_g1 , In2245_g1 , In3245_g1 );
					float displacementWrinkle331 = localMinMaxDeltaBlend1245_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch332 = displacementWrinkle331;
					#else
					float staticSwitch332 = displacementMap321;
					#endif
					float temp_output_319_0 = ( staticSwitch332 - _DisplacementLevel );
					float3 temp_output_327_0 = ( ( temp_output_319_0 * _DisplacementStrength ) * v.normal );
					#if defined( ENUM_DISPLACEMENT_MODE_NONE )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_BUMP )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPLACEMENT )
					float3 staticSwitch330 = temp_output_327_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP )
					float3 staticSwitch330 = temp_output_327_0;
					#else
					float3 staticSwitch330 = _Vector0;
					#endif
					
					o.ase_texcoord4.xy = v.texcoord.xyzw.xy;
					
					//setting value to unused interpolator channels and avoid initialization warnings
					o.ase_texcoord4.zw = 0;

					#ifdef ASE_ABSOLUTE_VERTEX_POS
						float3 defaultVertexValue = v.vertex.xyz;
					#else
						float3 defaultVertexValue = float3(0, 0, 0);
					#endif
					float3 vertexValue = staticSwitch330;
					#ifdef ASE_ABSOLUTE_VERTEX_POS
						v.vertex.xyz = vertexValue;
					#else
						v.vertex.xyz += vertexValue;
					#endif
					v.vertex.w = 1;
					v.normal = v.normal;
					v.tangent = v.tangent;

					float3 positionWS = mul( unity_ObjectToWorld, v.vertex ).xyz;
					half3 normalWS = UnityObjectToWorldNormal( v.normal );
					half3 tangentWS = UnityObjectToWorldDir( v.tangent.xyz );

					o.pos = UnityObjectToClipPos( v.vertex );
					o.positionWS.xyz = positionWS;
					o.normalWS = normalWS;
					o.tangentWS = half4( tangentWS, v.tangent.w );

					o.ambientOrLightmapUV = 0;
					#ifdef LIGHTMAP_ON
						o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
					#elif UNITY_SHOULD_SAMPLE_SH
						#ifdef VERTEXLIGHT_ON
							o.ambientOrLightmapUV.rgb += Shade4PointLights(
								unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
								unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
								unity_4LightAtten0, positionWS, normalWS );
						#endif
						o.ambientOrLightmapUV.rgb = ShadeSHPerVertex( normalWS, o.ambientOrLightmapUV.rgb );
					#endif
					#ifdef DYNAMICLIGHTMAP_ON
						o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
					#endif

					#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
						o.tangentWS.zw = v.texcoord.xy;
						o.tangentWS.xy = v.texcoord.xy * unity_LightmapST.xy + unity_LightmapST.zw;
					#endif
					return o;
				}

				#if defined(ASE_TESSELLATION)
				struct VertexControl
				{
					float4 vertex : INTERNALTESSPOS;
					half4 tangent : TANGENT;
					half3 normal : NORMAL;
					float4 texcoord : TEXCOORD0;
					float4 texcoord1 : TEXCOORD1;
					float4 texcoord2 : TEXCOORD2;
					
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct TessellationFactors
				{
					float edge[3] : SV_TessFactor;
					float inside : SV_InsideTessFactor;
				};

				VertexControl vert ( appdata v )
				{
					VertexControl o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.vertex = v.vertex;
					o.tangent = v.tangent;
					o.normal = v.normal;
					o.texcoord = v.texcoord;
					o.texcoord1 = v.texcoord1;
					o.texcoord2 = v.texcoord2;
					
					return o;
				}

				TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
				{
					TessellationFactors o;
					float4 tf = 1;
					float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
					float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
					#if defined(ASE_FIXED_TESSELLATION)
					tf = FixedTess( tessValue );
					#elif defined(ASE_DISTANCE_TESSELLATION)
					tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
					#elif defined(ASE_LENGTH_TESSELLATION)
					tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
					#elif defined(ASE_LENGTH_CULL_TESSELLATION)
					tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
					#endif
					o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
					return o;
				}

				[domain("tri")]
				[partitioning("fractional_odd")]
				[outputtopology("triangle_cw")]
				[patchconstantfunc("TessellationFunction")]
				[outputcontrolpoints(3)]
				VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
				{
				   return patch[id];
				}

				[domain("tri")]
				v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
				{
					appdata o = (appdata) 0;
					o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
					o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
					o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
					o.texcoord = patch[0].texcoord * bary.x + patch[1].texcoord * bary.y + patch[2].texcoord * bary.z;
					o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
					o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
					
					#if defined(ASE_PHONG_TESSELLATION)
					float3 pp[3];
					for (int i = 0; i < 3; ++i)
						pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
					float phongStrength = _TessPhongStrength;
					o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
					#endif
					UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
					return VertexFunction(o);
				}
				#else
				v2f vert ( appdata v )
				{
					return VertexFunction( v );
				}
				#endif

				void frag (v2f IN 
					, out half4 outGBuffer0 : SV_Target0
					, out half4 outGBuffer1 : SV_Target1
					, out half4 outGBuffer2 : SV_Target2
					, out half4 outEmission : SV_Target3
					#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
					, out half4 outShadowMask : SV_Target4
					#endif
					#if defined( ASE_DEPTH_WRITE_ON )
					, out float outputDepth : SV_Depth
					#endif
				)
				{
					UNITY_SETUP_INSTANCE_ID(IN);

					#ifdef LOD_FADE_CROSSFADE
						UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
					#endif

					#if defined(ASE_LIGHTING_SIMPLE)
						SurfaceOutput o = (SurfaceOutput)0;
					#else
						#if defined(_SPECULAR_SETUP)
							SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
						#else
							SurfaceOutputStandard o = (SurfaceOutputStandard)0;
						#endif
					#endif

					float3 PositionWS = IN.positionWS.xyz;
					half3 ViewDirWS = normalize( UnityWorldSpaceViewDir( PositionWS ) );
					float4 ScreenPosNorm = float4( IN.pos.xy * ( _ScreenParams.zw - 1.0 ), IN.pos.zw );
					float4 ClipPos = ComputeClipSpacePosition( ScreenPosNorm.xy, IN.pos.z ) * IN.pos.w;
					float4 ScreenPos = ComputeScreenPos( ClipPos );
					half3 NormalWS = IN.normalWS;
					half3 TangentWS = IN.tangentWS.xyz;
					half3 BitangentWS = cross( IN.normalWS, IN.tangentWS.xyz ) * IN.tangentWS.w * unity_WorldTransformParams.w;

					#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
						float2 sampleCoords = (IN.tangentWS.zw / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
						NormalWS = UnityObjectToWorldNormal(normalize(tex2D(_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
						TangentWS = -cross(unity_ObjectToWorld._13_23_33, NormalWS);
						BitangentWS = cross(NormalWS, -TangentWS);
					#endif

					float2 uv_MainTex = IN.ase_texcoord4.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					float4 diffuseMap216 = SAMPLE_TEXTURE2D( _MainTex, sampler_Linear_Repeat, uv_MainTex );
					float4 diffuse184_g1 = diffuseMap216;
					float4 In181_g1 = diffuse184_g1;
					float2 uv_WrinkleDiffuseArray = IN.ase_texcoord4.xy;
					float2 uv_WrinkleFlowPack = IN.ase_texcoord4.xy * _WrinkleFlowPack_ST.xy + _WrinkleFlowPack_ST.zw;
					float4 tex2DNode259 = SAMPLE_TEXTURE2D( _WrinkleFlowPack, sampler_Linear_Repeat, uv_WrinkleFlowPack );
					float2 texCoord10_g1 = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_1_0_g15 = 0.49;
					float leftMask27_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g15 ) / ( 0.51 - temp_output_1_0_g15 ) ) );
					float4 break107_g1 = _WrinkleValueSet12CL;
					float2 appendResult112_g1 = (float2(break107_g1.x , break107_g1.y));
					float2 uv_WrinkleMaskSetArray = IN.ase_texcoord4.xy;
					float4 break109_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,4.0) );
					float2 appendResult115_g1 = (float2(break109_g1.x , break109_g1.y));
					float dotResult121_g1 = dot( appendResult112_g1 , appendResult115_g1 );
					float value1CLeft135_g1 = dotResult121_g1;
					float4 maskSet1A214_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,0.0) );
					float dotResult29_g1 = dot( _WrinkleValueSet1AL , maskSet1A214_g1 );
					float4 temp_output_16_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,1.0) );
					float dotResult35_g1 = dot( _WrinkleValueSet1BL , temp_output_16_0_g1 );
					float dotResult30_g1 = dot( maskSet1A214_g1 , _WrinkleValueSet1AR );
					float dotResult36_g1 = dot( temp_output_16_0_g1 , _WrinkleValueSet1BR );
					float4 break108_g1 = _WrinkleValueSet12CR;
					float2 appendResult117_g1 = (float2(break108_g1.x , break108_g1.y));
					float dotResult122_g1 = dot( appendResult115_g1 , appendResult117_g1 );
					float value1CRight136_g1 = dotResult122_g1;
					float temp_output_1_0_g14 = 0.51;
					float rightMask28_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g14 ) / ( 0.49 - temp_output_1_0_g14 ) ) );
					float temp_output_16_0_g78 = ( ( leftMask27_g1 * ( value1CLeft135_g1 + dotResult29_g1 + dotResult35_g1 ) ) + ( ( dotResult30_g1 + dotResult36_g1 + value1CRight136_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g78 = ( saturate( ( tex2DNode259.r + temp_output_16_0_g78 ) ) * temp_output_16_0_g78 );
					float4 In1181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,0.0) ) - diffuse184_g1 ) * temp_output_23_0_g78 );
					float2 appendResult113_g1 = (float2(break107_g1.z , break107_g1.w));
					float2 appendResult114_g1 = (float2(break109_g1.z , break109_g1.w));
					float dotResult123_g1 = dot( appendResult113_g1 , appendResult114_g1 );
					float value2CLeft137_g1 = dotResult123_g1;
					float4 temp_output_17_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,2.0) );
					float dotResult41_g1 = dot( _WrinkleValueSet2L , temp_output_17_0_g1 );
					float dotResult42_g1 = dot( temp_output_17_0_g1 , _WrinkleValueSet2R );
					float2 appendResult116_g1 = (float2(break108_g1.z , break108_g1.w));
					float dotResult124_g1 = dot( appendResult114_g1 , appendResult116_g1 );
					float value2CRight138_g1 = dotResult124_g1;
					float temp_output_16_0_g80 = ( ( leftMask27_g1 * ( value2CLeft137_g1 + dotResult41_g1 ) ) + ( ( dotResult42_g1 + value2CRight138_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g80 = ( saturate( ( tex2DNode259.g + temp_output_16_0_g80 ) ) * temp_output_16_0_g80 );
					float4 In2181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,1.0) ) - diffuse184_g1 ) * temp_output_23_0_g80 );
					float4 break211_g1 = _WrinkleValueSetBCCB;
					float valueBCCLeft217_g1 = ( break211_g1.x * maskSet1A214_g1.y );
					float4 break118_g1 = _WrinkleValueSet3DB;
					float2 appendResult120_g1 = (float2(break118_g1.x , break118_g1.y));
					float dotResult127_g1 = dot( appendResult120_g1 , appendResult115_g1 );
					float value3DLeft129_g1 = dotResult127_g1;
					float4 temp_output_18_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,3.0) );
					float dotResult47_g1 = dot( _WrinkleValueSet3L , temp_output_18_0_g1 );
					float dotResult48_g1 = dot( temp_output_18_0_g1 , _WrinkleValueSet3R );
					float2 appendResult119_g1 = (float2(break118_g1.z , break118_g1.w));
					float dotResult128_g1 = dot( appendResult115_g1 , appendResult119_g1 );
					float value3DRight130_g1 = dotResult128_g1;
					float valueBCCRight219_g1 = ( maskSet1A214_g1.y * break211_g1.y );
					float temp_output_16_0_g81 = ( ( leftMask27_g1 * ( valueBCCLeft217_g1 + value3DLeft129_g1 + dotResult47_g1 ) ) + ( ( dotResult48_g1 + value3DRight130_g1 + valueBCCRight219_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g81 = ( saturate( ( tex2DNode259.b + temp_output_16_0_g81 ) ) * temp_output_16_0_g81 );
					float4 In3181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,2.0) ) - diffuse184_g1 ) * temp_output_23_0_g81 );
					float4 localMinMaxDeltaBlend4S181_g1 = MinMaxDeltaBlend4S181_g1( In181_g1 , In1181_g1 , In2181_g1 , In3181_g1 );
					float4 diffuseWrinkle229 = localMinMaxDeltaBlend4S181_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float4 staticSwitch313 = diffuseMap216;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float4 staticSwitch313 = diffuseWrinkle229;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float4 staticSwitch313 = diffuseWrinkle229;
					#else
					float4 staticSwitch313 = diffuseMap216;
					#endif
					float4 baseColor200 = ( _Color * staticSwitch313 );
					float2 uv_SSSThicknessPack = IN.ase_texcoord4.xy * _SSSThicknessPack_ST.xy + _SSSThicknessPack_ST.zw;
					float4 tex2DNode344 = SAMPLE_TEXTURE2D( _SSSThicknessPack, sampler_Linear_Repeat, uv_SSSThicknessPack );
					float lerpResult356 = lerp( _ThicknessScale , _ThicknessScaleMin , tex2DNode344.a);
					float temp_output_120_0_g110 = lerpResult356;
					float temp_output_144_0_g110 = ( 1.0 - temp_output_120_0_g110 );
					float2 uv_BumpMap = IN.ase_texcoord4.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
					float normalMapScale258 = _BumpScale;
					float3 normalMap218 = UnpackScaleNormal( SAMPLE_TEXTURE2D( _BumpMap, sampler_Linear_Repeat, uv_BumpMap ), normalMapScale258 );
					float3 normal186_g1 = normalMap218;
					float3 In183_g1 = normal186_g1;
					float2 uv_WrinkleNormalArray = IN.ase_texcoord4.xy;
					float3 In1183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,0.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g78 );
					float3 In2183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,1.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g80 );
					float3 In3183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,2.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g81 );
					float3 localMinMaxDeltaBlend3183_g1 = MinMaxDeltaBlend3183_g1( In183_g1 , In1183_g1 , In2183_g1 , In3183_g1 );
					float3 normalWrinkle231 = localMinMaxDeltaBlend3183_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float3 staticSwitch314 = normalMap218;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float3 staticSwitch314 = normalWrinkle231;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float3 staticSwitch314 = normalWrinkle231;
					#else
					float3 staticSwitch314 = normalMap218;
					#endif
					float2 uv_DetailNormalMap = IN.ase_texcoord4.xy * _DetailNormalMap_ST.xy + _DetailNormalMap_ST.zw;
					float2 uv_DetailMask = IN.ase_texcoord4.xy * _DetailMask_ST.xy + _DetailMask_ST.zw;
					float3 temp_output_155_0 = BlendNormals( staticSwitch314 , UnpackScaleNormal( SAMPLE_TEXTURE2D( _DetailNormalMap, sampler_Linear_Repeat, uv_DetailNormalMap ), ( _DetailNormalMapScale * SAMPLE_TEXTURE2D( _DetailMask, sampler_Linear_Repeat, uv_DetailMask ).g ) ) );
					float3 temp_output_111_0_g111 = ddx( PositionWS );
					float3 temp_output_113_0_g111 = cross( ddy( PositionWS ) , NormalWS );
					float dotResult115_g111 = dot( temp_output_111_0_g111 , temp_output_113_0_g111 );
					float2 uv_DisplacementMap = IN.ase_texcoord4.xy * _DisplacementMap_ST.xy + _DisplacementMap_ST.zw;
					float displacementMap321 = SAMPLE_TEXTURE2D( _DisplacementMap, sampler_Linear_Repeat, uv_DisplacementMap ).r;
					float displacement229_g1 = displacementMap321;
					float In245_g1 = displacement229_g1;
					float2 uv_WrinkleDisplacementPack = IN.ase_texcoord4.xy * _WrinkleDisplacementPack_ST.xy + _WrinkleDisplacementPack_ST.zw;
					float4 tex2DNode279 = SAMPLE_TEXTURE2D( _WrinkleDisplacementPack, sampler_WrinkleDisplacementPack, uv_WrinkleDisplacementPack );
					float displacementStrength234_g1 = _WrinkleDisplacementStrength;
					float In1245_g1 = ( ( tex2DNode279.r - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g78 );
					float In2245_g1 = ( ( tex2DNode279.g - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g80 );
					float In3245_g1 = ( ( tex2DNode279.b - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1245_g1 = MinMaxDeltaBlend1245_g1( In245_g1 , In1245_g1 , In2245_g1 , In3245_g1 );
					float displacementWrinkle331 = localMinMaxDeltaBlend1245_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch332 = displacementWrinkle331;
					#else
					float staticSwitch332 = displacementMap321;
					#endif
					float temp_output_319_0 = ( staticSwitch332 - _DisplacementLevel );
					float finalDisplacement322 = temp_output_319_0;
					float temp_output_20_0_g111 = finalDisplacement322;
					float3 normalizeResult130_g111 = normalize( ( ( abs( dotResult115_g111 ) * NormalWS ) - ( _BumpStrength * float3( 0.05,0.05,0.05 ) * sign( dotResult115_g111 ) * ( ( ddx( temp_output_20_0_g111 ) * temp_output_113_0_g111 ) + ( ddy( temp_output_20_0_g111 ) * cross( NormalWS , temp_output_111_0_g111 ) ) ) ) ) );
					float3x3 ase_worldToTangent = float3x3( TangentWS, BitangentWS, NormalWS );
					float3 worldToTangentDir42_g111 = mul( ase_worldToTangent, normalizeResult130_g111 );
					float3 temp_output_340_0 = BlendNormals( temp_output_155_0 , worldToTangentDir42_g111 );
					#if defined( ENUM_DISPLACEMENT_MODE_NONE )
					float3 staticSwitch336 = temp_output_155_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_BUMP )
					float3 staticSwitch336 = temp_output_340_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPLACEMENT )
					float3 staticSwitch336 = temp_output_155_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP )
					float3 staticSwitch336 = temp_output_340_0;
					#else
					float3 staticSwitch336 = temp_output_155_0;
					#endif
					float3 finalTangentNormal369 = staticSwitch336;
					float3 normal162_g110 = finalTangentNormal369;
					float3 tanToWorld0 = float3( TangentWS.x, BitangentWS.x, NormalWS.x );
					float3 tanToWorld1 = float3( TangentWS.y, BitangentWS.y, NormalWS.y );
					float3 tanToWorld2 = float3( TangentWS.z, BitangentWS.z, NormalWS.z );
					float3 tanNormal95_g110 = normal162_g110;
					float3 worldNormal95_g110 = normalize( float3( dot( tanToWorld0, tanNormal95_g110 ), dot( tanToWorld1, tanNormal95_g110 ), dot( tanToWorld2, tanNormal95_g110 ) ) );
					float3 worldSpaceLightDir = Unity_SafeNormalize( UnityWorldSpaceLightDir( PositionWS ) );
					float3 normalizeResult102_g110 = normalize( ( ( _SSSNormalDistortion * worldNormal95_g110 ) + worldSpaceLightDir ) );
					float dotResult106_g110 = dot( -normalizeResult102_g110 , ViewDirWS );
					float dotResult111_g110 = dot( worldNormal95_g110 , ViewDirWS );
					#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
					float4 ase_lightColor = 0;
					#else //aselc
					float4 ase_lightColor = _LightColor0;
					#endif //aselc
					float4 temp_output_124_0_g110 = _SubsurfaceFalloff1;
					float2 uv_SubsurfaceBlurMap = IN.ase_texcoord4.xy * _SubsurfaceBlurMap_ST.xy + _SubsurfaceBlurMap_ST.zw;
					float4 temp_output_123_0_g110 = SAMPLE_TEXTURE2D( _SubsurfaceBlurMap, sampler_Linear_Repeat, uv_SubsurfaceBlurMap );
					float temp_output_127_0_g110 = ( temp_output_120_0_g110 * tex2DNode344.r * _SSSBlurStrength );
					float4 lerpResult142_g110 = lerp( baseColor200 , temp_output_123_0_g110 , ( temp_output_124_0_g110 * temp_output_127_0_g110 ));
					float4 sssDiffuse362 = ( ( ( ( temp_output_144_0_g110 * temp_output_144_0_g110 ) * ( _SSSTransmission * saturate( dotResult106_g110 ) * saturate( ( 1.0 - dotResult111_g110 ) ) ) ) * ( ( ase_lightColor * temp_output_124_0_g110 ) * temp_output_123_0_g110 ) ) + lerpResult142_g110 );
					#ifdef BOOLEAN_USE_SSS_ON
					float4 staticSwitch368 = sssDiffuse362;
					#else
					float4 staticSwitch368 = baseColor200;
					#endif
					
					float3 In138_g110 = normal162_g110;
					float Strength138_g110 = ( 1.0 - temp_output_127_0_g110 );
					float3 localMyCustomExpression138_g110 = MyCustomExpression138_g110( In138_g110 , Strength138_g110 );
					float3 sssNormal361 = localMyCustomExpression138_g110;
					#ifdef BOOLEAN_USE_SSS_ON
					float3 staticSwitch367 = sssNormal361;
					#else
					float3 staticSwitch367 = finalTangentNormal369;
					#endif
					
					float2 uv_MetallicGlossMap = IN.ase_texcoord4.xy * _MetallicGlossMap_ST.xy + _MetallicGlossMap_ST.zw;
					float4 tex2DNode150 = SAMPLE_TEXTURE2D( _MetallicGlossMap, sampler_Linear_Repeat, uv_MetallicGlossMap );
					float smoothnessMap221 = tex2DNode150.a;
					float smoothness185_g1 = smoothnessMap221;
					float In182_g1 = smoothness185_g1;
					float2 uv_WrinkleSmoothnessPack = IN.ase_texcoord4.xy * _WrinkleSmoothnessPack_ST.xy + _WrinkleSmoothnessPack_ST.zw;
					float4 tex2DNode252 = SAMPLE_TEXTURE2D( _WrinkleSmoothnessPack, sampler_Linear_Repeat, uv_WrinkleSmoothnessPack );
					float In1182_g1 = ( ( tex2DNode252.r - smoothness185_g1 ) * temp_output_23_0_g78 );
					float In2182_g1 = ( ( tex2DNode252.g - smoothness185_g1 ) * temp_output_23_0_g80 );
					float In3182_g1 = ( ( tex2DNode252.b - smoothness185_g1 ) * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1S182_g1 = MinMaxDeltaBlend1S182_g1( In182_g1 , In1182_g1 , In2182_g1 , In3182_g1 );
					float smoothnessWrinkle230 = localMinMaxDeltaBlend1S182_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch341 = smoothnessMap221;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch341 = smoothnessWrinkle230;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch341 = smoothnessWrinkle230;
					#else
					float staticSwitch341 = smoothnessMap221;
					#endif
					float temp_output_168_0 = ( staticSwitch341 * _GlossMapScale );
					float2 uv_CavityMap = IN.ase_texcoord4.xy * _CavityMap_ST.xy + _CavityMap_ST.zw;
					float3 N416 = NormalWS;
					float3 V416 = ViewDirWS;
					float localFasterFresnel416 = FasterFresnel416( N416 , V416 );
					float lerpResult389 = lerp( 1.0 , SAMPLE_TEXTURE2D( _CavityMap, sampler_Linear_Repeat, uv_CavityMap ).r , ( _CavityStrength * saturate( ( 1.0 - localFasterFresnel416 ) ) ));
					
					float2 uv_OcclusionMap = IN.ase_texcoord4.xy * _OcclusionMap_ST.xy + _OcclusionMap_ST.zw;
					
					float2 uv_EmissionMap = IN.ase_texcoord4.xy * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
					

					o.Albedo = staticSwitch368.rgb;
					o.Normal = staticSwitch367;

					half3 Specular = half3( 0, 0, 0 );
					half Metallic = 0;
					half Smoothness = (( _UseCavity )?( ( temp_output_168_0 * lerpResult389 ) ):( temp_output_168_0 ));
					half Occlusion = ( 1.0 - ( _OcclusionStrength * ( 1.0 - SAMPLE_TEXTURE2D( _OcclusionMap, sampler_Linear_Repeat, uv_OcclusionMap ).g ) ) );

					#if defined(ASE_LIGHTING_SIMPLE)
						o.Specular = Specular.x;
						o.Gloss = Smoothness;
					#else
						#if defined(_SPECULAR_SETUP)
							o.Specular = Specular;
						#else
							o.Metallic = Metallic;
						#endif
						o.Occlusion = Occlusion;
						o.Smoothness = Smoothness;
					#endif

					o.Emission = ( SAMPLE_TEXTURE2D( _EmissionMap, sampler_Linear_Repeat, uv_EmissionMap ) * _EmissionColor ).rgb;
					o.Alpha = 1;
					half AlphaClipThreshold = 0.5;
					half3 BakedGI = 0;

					#if defined( ASE_DEPTH_WRITE_ON )
						float DeviceDepth = IN.pos.z;
					#endif

					#if ( ASE_FRAGMENT_NORMAL == 0 )
						o.Normal = normalize( o.Normal.x * TangentWS + o.Normal.y * BitangentWS + o.Normal.z * NormalWS );
					#elif ( ASE_FRAGMENT_NORMAL == 1 )
						o.Normal = UnityObjectToWorldNormal( o.Normal );
					#elif ( ASE_FRAGMENT_NORMAL == 2 )
						// @diogo: already in world-space; do nothing
					#endif

					#ifdef _ALPHATEST_ON
						clip( o.Alpha - AlphaClipThreshold );
					#endif

					#if defined( ASE_DEPTH_WRITE_ON )
						outputDepth = DeviceDepth;
					#endif

					#ifndef USING_DIRECTIONAL_LIGHT
						half3 lightDir = normalize( UnityWorldSpaceLightDir( PositionWS ) );
					#else
						half3 lightDir = _WorldSpaceLightPos0.xyz;
					#endif

					UnityGI gi;
					UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
					gi.indirect.diffuse = 0;
					gi.indirect.specular = 0;
					gi.light.color = 0;
					gi.light.dir = half3( 0, 1, 0 );

					UnityGIInput giInput;
					UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
					giInput.light = gi.light;
					giInput.worldPos = PositionWS;
					giInput.worldViewDir = ViewDirWS;
					giInput.atten = 1;
					#if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
						giInput.lightmapUV = IN.ambientOrLightmapUV;
					#else
						giInput.lightmapUV = 0.0;
					#endif
					#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
						giInput.ambient = IN.ambientOrLightmapUV.rgb;
					#else
						giInput.ambient.rgb = 0.0;
					#endif
					giInput.probeHDR[0] = unity_SpecCube0_HDR;
					giInput.probeHDR[1] = unity_SpecCube1_HDR;
					#if defined(UNITY_SPECCUBE_BLENDING) || defined(UNITY_SPECCUBE_BOX_PROJECTION)
						giInput.boxMin[0] = unity_SpecCube0_BoxMin;
					#endif
					#ifdef UNITY_SPECCUBE_BOX_PROJECTION
						giInput.boxMax[0] = unity_SpecCube0_BoxMax;
						giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
						giInput.boxMax[1] = unity_SpecCube1_BoxMax;
						giInput.boxMin[1] = unity_SpecCube1_BoxMin;
						giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
					#endif

					#if defined(ASE_LIGHTING_SIMPLE)
						#if defined(_SPECULAR_SETUP)
							LightingBlinnPhong_GI(o, giInput, gi);
						#else
							LightingLambert_GI(o, giInput, gi);
						#endif
					#else
						#if defined(_SPECULAR_SETUP)
							LightingStandardSpecular_GI(o, giInput, gi);
						#else
							LightingStandard_GI(o, giInput, gi);
						#endif
					#endif

					#ifdef ASE_BAKEDGI
						gi.indirect.diffuse = BakedGI;
					#endif

					#if UNITY_SHOULD_SAMPLE_SH && !defined(LIGHTMAP_ON) && defined(ASE_NO_AMBIENT)
						gi.indirect.diffuse = 0;
					#endif

					#if defined(ASE_LIGHTING_SIMPLE)
						#if defined(_SPECULAR_SETUP)
							outEmission = LightingBlinnPhong_Deferred( o, ViewDirWS, gi, outGBuffer0, outGBuffer1, outGBuffer2 );
						#else
							outEmission = LightingLambert_Deferred( o, gi, outGBuffer0, outGBuffer1, outGBuffer2 );
						#endif
					#else
						#if defined(_SPECULAR_SETUP)
							outEmission = LightingStandardSpecular_Deferred( o, ViewDirWS, gi, outGBuffer0, outGBuffer1, outGBuffer2 );
						#else
							outEmission = LightingStandard_Deferred( o, ViewDirWS, gi, outGBuffer0, outGBuffer1, outGBuffer2 );
						#endif
					#endif

					#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
						outShadowMask = UnityGetRawBakedOcclusions( IN.ambientOrLightmapUV.xy, float3( 0, 0, 0 ) );
					#endif
					#ifndef UNITY_HDR_ON
						outEmission.rgb = exp2(-outEmission.rgb);
					#endif
				}
			ENDCG
		}

		
		Pass
		{
			
			Name "Meta"
			Tags { "LightMode"="Meta" }
			Cull Off

			CGPROGRAM
				#define ASE_GEOMETRY
				#define ASE_FRAGMENT_NORMAL 0
				#define ASE_RECEIVE_SHADOWS
				#pragma multi_compile_instancing
				#pragma multi_compile _ LOD_FADE_CROSSFADE
				#define ASE_FOG
				#define ASE_VERSION 19904
				#define ASE_USING_SAMPLING_MACROS 1

				#pragma vertex vert
				#pragma fragment frag
				#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
				#pragma shader_feature EDITOR_VISUALIZATION
				#ifndef UNITY_PASS_META
					#define UNITY_PASS_META
				#endif
				#include "HLSLSupport.cginc"
				#if defined( ASE_GEOMETRY ) || defined( ASE_IMPOSTOR )
					#ifndef UNITY_INSTANCED_LOD_FADE
						#define UNITY_INSTANCED_LOD_FADE
					#endif
					#ifndef UNITY_INSTANCED_SH
						#define UNITY_INSTANCED_SH
					#endif
					#ifndef UNITY_INSTANCED_LIGHTMAPSTS
						#define UNITY_INSTANCED_LIGHTMAPSTS
					#endif
				#endif
				#include "UnityShaderVariables.cginc"
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"
				#include "UnityMetaPass.cginc"

				#include "UnityStandardUtils.cginc"
				#include "AutoLight.cginc"
				#include "UnityStandardBRDF.cginc"
				#define ASE_NEEDS_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_NORMAL
				#define ASE_NEEDS_FRAG_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_TANGENT
				#pragma shader_feature_local ENUM_DISPLACEMENT_MODE_NONE ENUM_DISPLACEMENT_MODE_BUMP ENUM_DISPLACEMENT_MODE_DISPLACEMENT ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP
				#pragma shader_feature_local ENUM_WRINKLE_MODE_NONE ENUM_WRINKLE_MODE_WRINKLE ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT
				#pragma shader_feature_local BOOLEAN_USE_SSS_ON
				#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex.SampleBias(samplerTex,coord,bias)
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex.SampleGrad(samplerTex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#else//ASE Sampling Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex2Dbias(tex,float4(coord,0,bias))
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex2Dgrad(tex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY(tex,samplertex,coord) tex2DArray(tex,coord)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplertex,coord,lod) tex2DArraylod(tex, float4(coord,lod))
				#endif//ASE Sampling Macros
				


				struct appdata
				{
					float4 vertex : POSITION;
					half3 normal : NORMAL;
					half4 tangent : TANGENT;
					float4 texcoord : TEXCOORD0;
					float4 texcoord1 : TEXCOORD1;
					float4 texcoord2 : TEXCOORD2;
					
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 pos : SV_POSITION;
					#ifdef EDITOR_VISUALIZATION
						float2 vizUV : TEXCOORD0;
						float4 lightCoord : TEXCOORD1;
					#endif
					float4 ase_texcoord2 : TEXCOORD2;
					float4 ase_texcoord3 : TEXCOORD3;
					float4 ase_texcoord4 : TEXCOORD4;
					float4 ase_texcoord5 : TEXCOORD5;
					float4 ase_texcoord6 : TEXCOORD6;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};

				#ifdef ASE_TESSELLATION
					float _TessPhongStrength;
					float _TessValue;
					float _TessMin;
					float _TessMax;
					float _TessEdgeLength;
					float _TessMaxDisp;
				#endif

				UNITY_DECLARE_TEX2D_NOSAMPLER(_DisplacementMap);
				uniform float4 _DisplacementMap_ST;
				SamplerState sampler_Linear_Repeat;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleDisplacementPack);
				uniform float4 _WrinkleDisplacementPack_ST;
				SamplerState sampler_WrinkleDisplacementPack;
				uniform float _WrinkleDisplacementStrength;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleFlowPack);
				uniform float4 _WrinkleFlowPack_ST;
				uniform float4 _WrinkleValueSet12CL;
				uniform float4 _WrinkleMaskSetArray_ST;
				uniform float4 _WrinkleValueSet1AL;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleMaskSetArray);
				uniform float4 _WrinkleValueSet1BL;
				uniform float4 _WrinkleValueSet1AR;
				uniform float4 _WrinkleValueSet1BR;
				uniform float4 _WrinkleValueSet12CR;
				uniform float4 _WrinkleValueSet2L;
				uniform float4 _WrinkleValueSet2R;
				uniform float4 _WrinkleValueSetBCCB;
				uniform float4 _WrinkleValueSet3DB;
				uniform float4 _WrinkleValueSet3L;
				uniform float4 _WrinkleValueSet3R;
				uniform float _DisplacementLevel;
				uniform float _DisplacementStrength;
				uniform float4 _Color;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_MainTex);
				uniform float4 _MainTex_ST;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleDiffuseArray);
				uniform float4 _WrinkleDiffuseArray_ST;
				uniform float _ThicknessScale;
				uniform float _ThicknessScaleMin;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_SSSThicknessPack);
				uniform float4 _SSSThicknessPack_ST;
				uniform float _SSSTransmission;
				uniform float _SSSNormalDistortion;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_BumpMap);
				uniform float4 _BumpMap_ST;
				uniform float _BumpScale;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleNormalArray);
				uniform float4 _WrinkleNormalArray_ST;
				SamplerState sampler_WrinkleNormalArray;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_DetailNormalMap);
				uniform float4 _DetailNormalMap_ST;
				uniform float _DetailNormalMapScale;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_DetailMask);
				uniform float4 _DetailMask_ST;
				uniform float _BumpStrength;
				uniform float4 _SubsurfaceFalloff1;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_SubsurfaceBlurMap);
				uniform float4 _SubsurfaceBlurMap_ST;
				uniform float _SSSBlurStrength;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissionMap);
				uniform float4 _EmissionMap_ST;
				uniform float4 _EmissionColor;


				//This is a late directive
				
				float MinMaxDeltaBlend1245_g1( float In, float In1, float In2, float In3 )
				{
					return max(max(In1, In2), In3) + min(min(In1, In2), In3) + In;
				}
				
				float4 MinMaxDeltaBlend4S181_g1( float4 In, float4 In1, float4 In2, float4 In3 )
				{
					return saturate(max(max(In1, In2), In3) + min(min(In1, In2), In3) + In);
				}
				
				float3 MinMaxDeltaBlend3183_g1( float3 In, float3 In1, float3 In2, float3 In3 )
				{
					return max(max(In1, In2), In3) + min(min(In1, In2), In3) + In;
				}
				

				v2f VertexFunction( appdata v  )
				{
					UNITY_SETUP_INSTANCE_ID(v);
					v2f o;
					UNITY_INITIALIZE_OUTPUT(v2f,o);
					UNITY_TRANSFER_INSTANCE_ID(v,o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					float3 _Vector0 = float3(0,0,0);
					float2 uv_DisplacementMap = v.texcoord.xyzw.xy * _DisplacementMap_ST.xy + _DisplacementMap_ST.zw;
					float displacementMap321 = SAMPLE_TEXTURE2D_LOD( _DisplacementMap, sampler_Linear_Repeat, uv_DisplacementMap, 0.0 ).r;
					float displacement229_g1 = displacementMap321;
					float In245_g1 = displacement229_g1;
					float2 uv_WrinkleDisplacementPack = v.texcoord.xyzw.xy * _WrinkleDisplacementPack_ST.xy + _WrinkleDisplacementPack_ST.zw;
					float4 tex2DNode279 = SAMPLE_TEXTURE2D_LOD( _WrinkleDisplacementPack, sampler_WrinkleDisplacementPack, uv_WrinkleDisplacementPack, 0.0 );
					float displacementStrength234_g1 = _WrinkleDisplacementStrength;
					float2 uv_WrinkleFlowPack = v.texcoord.xyzw.xy * _WrinkleFlowPack_ST.xy + _WrinkleFlowPack_ST.zw;
					float4 tex2DNode259 = SAMPLE_TEXTURE2D_LOD( _WrinkleFlowPack, sampler_Linear_Repeat, uv_WrinkleFlowPack, 0.0 );
					float2 texCoord10_g1 = v.texcoord.xyzw.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_1_0_g15 = 0.49;
					float leftMask27_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g15 ) / ( 0.51 - temp_output_1_0_g15 ) ) );
					float4 break107_g1 = _WrinkleValueSet12CL;
					float2 appendResult112_g1 = (float2(break107_g1.x , break107_g1.y));
					float2 uv_WrinkleMaskSetArray = v.texcoord.xyzw.xy;
					float4 break109_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,4.0), 0.0 );
					float2 appendResult115_g1 = (float2(break109_g1.x , break109_g1.y));
					float dotResult121_g1 = dot( appendResult112_g1 , appendResult115_g1 );
					float value1CLeft135_g1 = dotResult121_g1;
					float4 maskSet1A214_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,0.0), 0.0 );
					float dotResult29_g1 = dot( _WrinkleValueSet1AL , maskSet1A214_g1 );
					float4 temp_output_16_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,1.0), 0.0 );
					float dotResult35_g1 = dot( _WrinkleValueSet1BL , temp_output_16_0_g1 );
					float dotResult30_g1 = dot( maskSet1A214_g1 , _WrinkleValueSet1AR );
					float dotResult36_g1 = dot( temp_output_16_0_g1 , _WrinkleValueSet1BR );
					float4 break108_g1 = _WrinkleValueSet12CR;
					float2 appendResult117_g1 = (float2(break108_g1.x , break108_g1.y));
					float dotResult122_g1 = dot( appendResult115_g1 , appendResult117_g1 );
					float value1CRight136_g1 = dotResult122_g1;
					float temp_output_1_0_g14 = 0.51;
					float rightMask28_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g14 ) / ( 0.49 - temp_output_1_0_g14 ) ) );
					float temp_output_16_0_g78 = ( ( leftMask27_g1 * ( value1CLeft135_g1 + dotResult29_g1 + dotResult35_g1 ) ) + ( ( dotResult30_g1 + dotResult36_g1 + value1CRight136_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g78 = ( saturate( ( tex2DNode259.r + temp_output_16_0_g78 ) ) * temp_output_16_0_g78 );
					float In1245_g1 = ( ( tex2DNode279.r - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g78 );
					float2 appendResult113_g1 = (float2(break107_g1.z , break107_g1.w));
					float2 appendResult114_g1 = (float2(break109_g1.z , break109_g1.w));
					float dotResult123_g1 = dot( appendResult113_g1 , appendResult114_g1 );
					float value2CLeft137_g1 = dotResult123_g1;
					float4 temp_output_17_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,2.0), 0.0 );
					float dotResult41_g1 = dot( _WrinkleValueSet2L , temp_output_17_0_g1 );
					float dotResult42_g1 = dot( temp_output_17_0_g1 , _WrinkleValueSet2R );
					float2 appendResult116_g1 = (float2(break108_g1.z , break108_g1.w));
					float dotResult124_g1 = dot( appendResult114_g1 , appendResult116_g1 );
					float value2CRight138_g1 = dotResult124_g1;
					float temp_output_16_0_g80 = ( ( leftMask27_g1 * ( value2CLeft137_g1 + dotResult41_g1 ) ) + ( ( dotResult42_g1 + value2CRight138_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g80 = ( saturate( ( tex2DNode259.g + temp_output_16_0_g80 ) ) * temp_output_16_0_g80 );
					float In2245_g1 = ( ( tex2DNode279.g - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g80 );
					float4 break211_g1 = _WrinkleValueSetBCCB;
					float valueBCCLeft217_g1 = ( break211_g1.x * maskSet1A214_g1.y );
					float4 break118_g1 = _WrinkleValueSet3DB;
					float2 appendResult120_g1 = (float2(break118_g1.x , break118_g1.y));
					float dotResult127_g1 = dot( appendResult120_g1 , appendResult115_g1 );
					float value3DLeft129_g1 = dotResult127_g1;
					float4 temp_output_18_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,3.0), 0.0 );
					float dotResult47_g1 = dot( _WrinkleValueSet3L , temp_output_18_0_g1 );
					float dotResult48_g1 = dot( temp_output_18_0_g1 , _WrinkleValueSet3R );
					float2 appendResult119_g1 = (float2(break118_g1.z , break118_g1.w));
					float dotResult128_g1 = dot( appendResult115_g1 , appendResult119_g1 );
					float value3DRight130_g1 = dotResult128_g1;
					float valueBCCRight219_g1 = ( maskSet1A214_g1.y * break211_g1.y );
					float temp_output_16_0_g81 = ( ( leftMask27_g1 * ( valueBCCLeft217_g1 + value3DLeft129_g1 + dotResult47_g1 ) ) + ( ( dotResult48_g1 + value3DRight130_g1 + valueBCCRight219_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g81 = ( saturate( ( tex2DNode259.b + temp_output_16_0_g81 ) ) * temp_output_16_0_g81 );
					float In3245_g1 = ( ( tex2DNode279.b - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1245_g1 = MinMaxDeltaBlend1245_g1( In245_g1 , In1245_g1 , In2245_g1 , In3245_g1 );
					float displacementWrinkle331 = localMinMaxDeltaBlend1245_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch332 = displacementWrinkle331;
					#else
					float staticSwitch332 = displacementMap321;
					#endif
					float temp_output_319_0 = ( staticSwitch332 - _DisplacementLevel );
					float3 temp_output_327_0 = ( ( temp_output_319_0 * _DisplacementStrength ) * v.normal );
					#if defined( ENUM_DISPLACEMENT_MODE_NONE )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_BUMP )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPLACEMENT )
					float3 staticSwitch330 = temp_output_327_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP )
					float3 staticSwitch330 = temp_output_327_0;
					#else
					float3 staticSwitch330 = _Vector0;
					#endif
					
					float3 ase_positionWS = mul( unity_ObjectToWorld, float4( ( v.vertex ).xyz, 1 ) ).xyz;
					o.ase_texcoord3.xyz = ase_positionWS;
					float3 ase_normalWS = UnityObjectToWorldNormal( v.normal );
					o.ase_texcoord4.xyz = ase_normalWS;
					float3 ase_tangentWS = UnityObjectToWorldDir( v.tangent );
					o.ase_texcoord5.xyz = ase_tangentWS;
					float ase_tangentSign = v.tangent.w * ( unity_WorldTransformParams.w >= 0.0 ? 1.0 : -1.0 );
					float3 ase_bitangentWS = cross( ase_normalWS, ase_tangentWS ) * ase_tangentSign;
					o.ase_texcoord6.xyz = ase_bitangentWS;
					
					o.ase_texcoord2.xy = v.texcoord.xyzw.xy;
					
					//setting value to unused interpolator channels and avoid initialization warnings
					o.ase_texcoord2.zw = 0;
					o.ase_texcoord3.w = 0;
					o.ase_texcoord4.w = 0;
					o.ase_texcoord5.w = 0;
					o.ase_texcoord6.w = 0;

					#ifdef ASE_ABSOLUTE_VERTEX_POS
						float3 defaultVertexValue = v.vertex.xyz;
					#else
						float3 defaultVertexValue = float3(0, 0, 0);
					#endif
					float3 vertexValue = staticSwitch330;
					#ifdef ASE_ABSOLUTE_VERTEX_POS
						v.vertex.xyz = vertexValue;
					#else
						v.vertex.xyz += vertexValue;
					#endif
					v.vertex.w = 1;
					v.normal = v.normal;
					v.tangent = v.tangent;

					#ifdef EDITOR_VISUALIZATION
						o.vizUV = 0;
						o.lightCoord = 0;
						if (unity_VisualizationMode == EDITORVIZ_TEXTURE)
							o.vizUV = UnityMetaVizUV(unity_EditorViz_UVIndex, v.texcoord.xy, v.texcoord1.xy, v.texcoord2.xy, unity_EditorViz_Texture_ST);
						else if (unity_VisualizationMode == EDITORVIZ_SHOWLIGHTMASK)
						{
							o.vizUV = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
							o.lightCoord = mul(unity_EditorViz_WorldToLight, mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1)));
						}
					#endif

					o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
					return o;
				}

				#if defined(ASE_TESSELLATION)
				struct VertexControl
				{
					float4 vertex : INTERNALTESSPOS;
					float4 tangent : TANGENT;
					float3 normal : NORMAL;
					float4 texcoord1 : TEXCOORD1;
					float4 texcoord2 : TEXCOORD2;
					
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct TessellationFactors
				{
					float edge[3] : SV_TessFactor;
					float inside : SV_InsideTessFactor;
				};

				VertexControl vert ( appdata v )
				{
					VertexControl o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.vertex = v.vertex;
					o.tangent = v.tangent;
					o.normal = v.normal;
					o.texcoord1 = v.texcoord1;
					o.texcoord2 = v.texcoord2;
					
					return o;
				}

				TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
				{
					TessellationFactors o;
					float4 tf = 1;
					float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
					float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
					#if defined(ASE_FIXED_TESSELLATION)
					tf = FixedTess( tessValue );
					#elif defined(ASE_DISTANCE_TESSELLATION)
					tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
					#elif defined(ASE_LENGTH_TESSELLATION)
					tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
					#elif defined(ASE_LENGTH_CULL_TESSELLATION)
					tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
					#endif
					o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
					return o;
				}

				[domain("tri")]
				[partitioning("fractional_odd")]
				[outputtopology("triangle_cw")]
				[patchconstantfunc("TessellationFunction")]
				[outputcontrolpoints(3)]
				VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
				{
				   return patch[id];
				}

				[domain("tri")]
				v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
				{
					appdata o = (appdata) 0;
					o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
					o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
					o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
					o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
					o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
					
					#if defined(ASE_PHONG_TESSELLATION)
					float3 pp[3];
					for (int i = 0; i < 3; ++i)
						pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
					float phongStrength = _TessPhongStrength;
					o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
					#endif
					UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
					return VertexFunction(o);
				}
				#else
				v2f vert( appdata v )
				{
					return VertexFunction( v );
				}
				#endif

				half4 frag( v2f IN  ) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(IN);

					#ifdef LOD_FADE_CROSSFADE
						UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
					#endif

					#if defined(ASE_LIGHTING_SIMPLE)
						SurfaceOutput o = (SurfaceOutput)0;
					#else
						#if defined(_SPECULAR_SETUP)
							SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
						#else
							SurfaceOutputStandard o = (SurfaceOutputStandard)0;
						#endif
					#endif

					float2 uv_MainTex = IN.ase_texcoord2.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					float4 diffuseMap216 = SAMPLE_TEXTURE2D( _MainTex, sampler_Linear_Repeat, uv_MainTex );
					float4 diffuse184_g1 = diffuseMap216;
					float4 In181_g1 = diffuse184_g1;
					float2 uv_WrinkleDiffuseArray = IN.ase_texcoord2.xy;
					float2 uv_WrinkleFlowPack = IN.ase_texcoord2.xy * _WrinkleFlowPack_ST.xy + _WrinkleFlowPack_ST.zw;
					float4 tex2DNode259 = SAMPLE_TEXTURE2D( _WrinkleFlowPack, sampler_Linear_Repeat, uv_WrinkleFlowPack );
					float2 texCoord10_g1 = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_1_0_g15 = 0.49;
					float leftMask27_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g15 ) / ( 0.51 - temp_output_1_0_g15 ) ) );
					float4 break107_g1 = _WrinkleValueSet12CL;
					float2 appendResult112_g1 = (float2(break107_g1.x , break107_g1.y));
					float2 uv_WrinkleMaskSetArray = IN.ase_texcoord2.xy;
					float4 break109_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,4.0) );
					float2 appendResult115_g1 = (float2(break109_g1.x , break109_g1.y));
					float dotResult121_g1 = dot( appendResult112_g1 , appendResult115_g1 );
					float value1CLeft135_g1 = dotResult121_g1;
					float4 maskSet1A214_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,0.0) );
					float dotResult29_g1 = dot( _WrinkleValueSet1AL , maskSet1A214_g1 );
					float4 temp_output_16_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,1.0) );
					float dotResult35_g1 = dot( _WrinkleValueSet1BL , temp_output_16_0_g1 );
					float dotResult30_g1 = dot( maskSet1A214_g1 , _WrinkleValueSet1AR );
					float dotResult36_g1 = dot( temp_output_16_0_g1 , _WrinkleValueSet1BR );
					float4 break108_g1 = _WrinkleValueSet12CR;
					float2 appendResult117_g1 = (float2(break108_g1.x , break108_g1.y));
					float dotResult122_g1 = dot( appendResult115_g1 , appendResult117_g1 );
					float value1CRight136_g1 = dotResult122_g1;
					float temp_output_1_0_g14 = 0.51;
					float rightMask28_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g14 ) / ( 0.49 - temp_output_1_0_g14 ) ) );
					float temp_output_16_0_g78 = ( ( leftMask27_g1 * ( value1CLeft135_g1 + dotResult29_g1 + dotResult35_g1 ) ) + ( ( dotResult30_g1 + dotResult36_g1 + value1CRight136_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g78 = ( saturate( ( tex2DNode259.r + temp_output_16_0_g78 ) ) * temp_output_16_0_g78 );
					float4 In1181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,0.0) ) - diffuse184_g1 ) * temp_output_23_0_g78 );
					float2 appendResult113_g1 = (float2(break107_g1.z , break107_g1.w));
					float2 appendResult114_g1 = (float2(break109_g1.z , break109_g1.w));
					float dotResult123_g1 = dot( appendResult113_g1 , appendResult114_g1 );
					float value2CLeft137_g1 = dotResult123_g1;
					float4 temp_output_17_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,2.0) );
					float dotResult41_g1 = dot( _WrinkleValueSet2L , temp_output_17_0_g1 );
					float dotResult42_g1 = dot( temp_output_17_0_g1 , _WrinkleValueSet2R );
					float2 appendResult116_g1 = (float2(break108_g1.z , break108_g1.w));
					float dotResult124_g1 = dot( appendResult114_g1 , appendResult116_g1 );
					float value2CRight138_g1 = dotResult124_g1;
					float temp_output_16_0_g80 = ( ( leftMask27_g1 * ( value2CLeft137_g1 + dotResult41_g1 ) ) + ( ( dotResult42_g1 + value2CRight138_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g80 = ( saturate( ( tex2DNode259.g + temp_output_16_0_g80 ) ) * temp_output_16_0_g80 );
					float4 In2181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,1.0) ) - diffuse184_g1 ) * temp_output_23_0_g80 );
					float4 break211_g1 = _WrinkleValueSetBCCB;
					float valueBCCLeft217_g1 = ( break211_g1.x * maskSet1A214_g1.y );
					float4 break118_g1 = _WrinkleValueSet3DB;
					float2 appendResult120_g1 = (float2(break118_g1.x , break118_g1.y));
					float dotResult127_g1 = dot( appendResult120_g1 , appendResult115_g1 );
					float value3DLeft129_g1 = dotResult127_g1;
					float4 temp_output_18_0_g1 = SAMPLE_TEXTURE2D_ARRAY( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,3.0) );
					float dotResult47_g1 = dot( _WrinkleValueSet3L , temp_output_18_0_g1 );
					float dotResult48_g1 = dot( temp_output_18_0_g1 , _WrinkleValueSet3R );
					float2 appendResult119_g1 = (float2(break118_g1.z , break118_g1.w));
					float dotResult128_g1 = dot( appendResult115_g1 , appendResult119_g1 );
					float value3DRight130_g1 = dotResult128_g1;
					float valueBCCRight219_g1 = ( maskSet1A214_g1.y * break211_g1.y );
					float temp_output_16_0_g81 = ( ( leftMask27_g1 * ( valueBCCLeft217_g1 + value3DLeft129_g1 + dotResult47_g1 ) ) + ( ( dotResult48_g1 + value3DRight130_g1 + valueBCCRight219_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g81 = ( saturate( ( tex2DNode259.b + temp_output_16_0_g81 ) ) * temp_output_16_0_g81 );
					float4 In3181_g1 = ( ( SAMPLE_TEXTURE2D_ARRAY( _WrinkleDiffuseArray, sampler_Linear_Repeat, float3(uv_WrinkleDiffuseArray,2.0) ) - diffuse184_g1 ) * temp_output_23_0_g81 );
					float4 localMinMaxDeltaBlend4S181_g1 = MinMaxDeltaBlend4S181_g1( In181_g1 , In1181_g1 , In2181_g1 , In3181_g1 );
					float4 diffuseWrinkle229 = localMinMaxDeltaBlend4S181_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float4 staticSwitch313 = diffuseMap216;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float4 staticSwitch313 = diffuseWrinkle229;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float4 staticSwitch313 = diffuseWrinkle229;
					#else
					float4 staticSwitch313 = diffuseMap216;
					#endif
					float4 baseColor200 = ( _Color * staticSwitch313 );
					float2 uv_SSSThicknessPack = IN.ase_texcoord2.xy * _SSSThicknessPack_ST.xy + _SSSThicknessPack_ST.zw;
					float4 tex2DNode344 = SAMPLE_TEXTURE2D( _SSSThicknessPack, sampler_Linear_Repeat, uv_SSSThicknessPack );
					float lerpResult356 = lerp( _ThicknessScale , _ThicknessScaleMin , tex2DNode344.a);
					float temp_output_120_0_g110 = lerpResult356;
					float temp_output_144_0_g110 = ( 1.0 - temp_output_120_0_g110 );
					float2 uv_BumpMap = IN.ase_texcoord2.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
					float normalMapScale258 = _BumpScale;
					float3 normalMap218 = UnpackScaleNormal( SAMPLE_TEXTURE2D( _BumpMap, sampler_Linear_Repeat, uv_BumpMap ), normalMapScale258 );
					float3 normal186_g1 = normalMap218;
					float3 In183_g1 = normal186_g1;
					float2 uv_WrinkleNormalArray = IN.ase_texcoord2.xy;
					float3 In1183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,0.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g78 );
					float3 In2183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,1.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g80 );
					float3 In3183_g1 = ( ( UnpackScaleNormal( SAMPLE_TEXTURE2D_ARRAY( _WrinkleNormalArray, sampler_WrinkleNormalArray, float3(uv_WrinkleNormalArray,2.0) ), normalMapScale258 ) - normal186_g1 ) * temp_output_23_0_g81 );
					float3 localMinMaxDeltaBlend3183_g1 = MinMaxDeltaBlend3183_g1( In183_g1 , In1183_g1 , In2183_g1 , In3183_g1 );
					float3 normalWrinkle231 = localMinMaxDeltaBlend3183_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float3 staticSwitch314 = normalMap218;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float3 staticSwitch314 = normalWrinkle231;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float3 staticSwitch314 = normalWrinkle231;
					#else
					float3 staticSwitch314 = normalMap218;
					#endif
					float2 uv_DetailNormalMap = IN.ase_texcoord2.xy * _DetailNormalMap_ST.xy + _DetailNormalMap_ST.zw;
					float2 uv_DetailMask = IN.ase_texcoord2.xy * _DetailMask_ST.xy + _DetailMask_ST.zw;
					float3 temp_output_155_0 = BlendNormals( staticSwitch314 , UnpackScaleNormal( SAMPLE_TEXTURE2D( _DetailNormalMap, sampler_Linear_Repeat, uv_DetailNormalMap ), ( _DetailNormalMapScale * SAMPLE_TEXTURE2D( _DetailMask, sampler_Linear_Repeat, uv_DetailMask ).g ) ) );
					float3 ase_positionWS = IN.ase_texcoord3.xyz;
					float3 temp_output_111_0_g111 = ddx( ase_positionWS );
					float3 ase_normalWS = IN.ase_texcoord4.xyz;
					float3 temp_output_113_0_g111 = cross( ddy( ase_positionWS ) , ase_normalWS );
					float dotResult115_g111 = dot( temp_output_111_0_g111 , temp_output_113_0_g111 );
					float2 uv_DisplacementMap = IN.ase_texcoord2.xy * _DisplacementMap_ST.xy + _DisplacementMap_ST.zw;
					float displacementMap321 = SAMPLE_TEXTURE2D( _DisplacementMap, sampler_Linear_Repeat, uv_DisplacementMap ).r;
					float displacement229_g1 = displacementMap321;
					float In245_g1 = displacement229_g1;
					float2 uv_WrinkleDisplacementPack = IN.ase_texcoord2.xy * _WrinkleDisplacementPack_ST.xy + _WrinkleDisplacementPack_ST.zw;
					float4 tex2DNode279 = SAMPLE_TEXTURE2D( _WrinkleDisplacementPack, sampler_WrinkleDisplacementPack, uv_WrinkleDisplacementPack );
					float displacementStrength234_g1 = _WrinkleDisplacementStrength;
					float In1245_g1 = ( ( tex2DNode279.r - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g78 );
					float In2245_g1 = ( ( tex2DNode279.g - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g80 );
					float In3245_g1 = ( ( tex2DNode279.b - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1245_g1 = MinMaxDeltaBlend1245_g1( In245_g1 , In1245_g1 , In2245_g1 , In3245_g1 );
					float displacementWrinkle331 = localMinMaxDeltaBlend1245_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch332 = displacementWrinkle331;
					#else
					float staticSwitch332 = displacementMap321;
					#endif
					float temp_output_319_0 = ( staticSwitch332 - _DisplacementLevel );
					float finalDisplacement322 = temp_output_319_0;
					float temp_output_20_0_g111 = finalDisplacement322;
					float3 normalizeResult130_g111 = normalize( ( ( abs( dotResult115_g111 ) * ase_normalWS ) - ( _BumpStrength * float3( 0.05,0.05,0.05 ) * sign( dotResult115_g111 ) * ( ( ddx( temp_output_20_0_g111 ) * temp_output_113_0_g111 ) + ( ddy( temp_output_20_0_g111 ) * cross( ase_normalWS , temp_output_111_0_g111 ) ) ) ) ) );
					float3 ase_tangentWS = IN.ase_texcoord5.xyz;
					float3 ase_bitangentWS = IN.ase_texcoord6.xyz;
					float3x3 ase_worldToTangent = float3x3( ase_tangentWS, ase_bitangentWS, ase_normalWS );
					float3 worldToTangentDir42_g111 = mul( ase_worldToTangent, normalizeResult130_g111 );
					float3 temp_output_340_0 = BlendNormals( temp_output_155_0 , worldToTangentDir42_g111 );
					#if defined( ENUM_DISPLACEMENT_MODE_NONE )
					float3 staticSwitch336 = temp_output_155_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_BUMP )
					float3 staticSwitch336 = temp_output_340_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPLACEMENT )
					float3 staticSwitch336 = temp_output_155_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP )
					float3 staticSwitch336 = temp_output_340_0;
					#else
					float3 staticSwitch336 = temp_output_155_0;
					#endif
					float3 finalTangentNormal369 = staticSwitch336;
					float3 normal162_g110 = finalTangentNormal369;
					float3 tanToWorld0 = float3( ase_tangentWS.x, ase_bitangentWS.x, ase_normalWS.x );
					float3 tanToWorld1 = float3( ase_tangentWS.y, ase_bitangentWS.y, ase_normalWS.y );
					float3 tanToWorld2 = float3( ase_tangentWS.z, ase_bitangentWS.z, ase_normalWS.z );
					float3 tanNormal95_g110 = normal162_g110;
					float3 worldNormal95_g110 = normalize( float3( dot( tanToWorld0, tanNormal95_g110 ), dot( tanToWorld1, tanNormal95_g110 ), dot( tanToWorld2, tanNormal95_g110 ) ) );
					float3 worldSpaceLightDir = Unity_SafeNormalize( UnityWorldSpaceLightDir( ase_positionWS ) );
					float3 normalizeResult102_g110 = normalize( ( ( _SSSNormalDistortion * worldNormal95_g110 ) + worldSpaceLightDir ) );
					float3 ase_viewVectorWS = ( _WorldSpaceCameraPos.xyz - ase_positionWS );
					float3 ase_viewDirSafeWS = Unity_SafeNormalize( ase_viewVectorWS );
					float dotResult106_g110 = dot( -normalizeResult102_g110 , ase_viewDirSafeWS );
					float dotResult111_g110 = dot( worldNormal95_g110 , ase_viewDirSafeWS );
					#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
					float4 ase_lightColor = 0;
					#else //aselc
					float4 ase_lightColor = _LightColor0;
					#endif //aselc
					float4 temp_output_124_0_g110 = _SubsurfaceFalloff1;
					float2 uv_SubsurfaceBlurMap = IN.ase_texcoord2.xy * _SubsurfaceBlurMap_ST.xy + _SubsurfaceBlurMap_ST.zw;
					float4 temp_output_123_0_g110 = SAMPLE_TEXTURE2D( _SubsurfaceBlurMap, sampler_Linear_Repeat, uv_SubsurfaceBlurMap );
					float temp_output_127_0_g110 = ( temp_output_120_0_g110 * tex2DNode344.r * _SSSBlurStrength );
					float4 lerpResult142_g110 = lerp( baseColor200 , temp_output_123_0_g110 , ( temp_output_124_0_g110 * temp_output_127_0_g110 ));
					float4 sssDiffuse362 = ( ( ( ( temp_output_144_0_g110 * temp_output_144_0_g110 ) * ( _SSSTransmission * saturate( dotResult106_g110 ) * saturate( ( 1.0 - dotResult111_g110 ) ) ) ) * ( ( ase_lightColor * temp_output_124_0_g110 ) * temp_output_123_0_g110 ) ) + lerpResult142_g110 );
					#ifdef BOOLEAN_USE_SSS_ON
					float4 staticSwitch368 = sssDiffuse362;
					#else
					float4 staticSwitch368 = baseColor200;
					#endif
					
					float2 uv_EmissionMap = IN.ase_texcoord2.xy * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
					

					o.Albedo = staticSwitch368.rgb;
					o.Normal = half3( 0, 0, 1 );
					o.Emission = ( SAMPLE_TEXTURE2D( _EmissionMap, sampler_Linear_Repeat, uv_EmissionMap ) * _EmissionColor ).rgb;
					o.Alpha = 1;
					half AlphaClipThreshold = 0.5;

					#ifdef _ALPHATEST_ON
						clip( o.Alpha - AlphaClipThreshold );
					#endif

					UnityMetaInput metaIN;
					UNITY_INITIALIZE_OUTPUT(UnityMetaInput, metaIN);
					metaIN.Albedo = o.Albedo;
					metaIN.Emission = o.Emission;
					#ifdef EDITOR_VISUALIZATION
						metaIN.VizUV = IN.vizUV;
						metaIN.LightCoord = IN.lightCoord;
					#endif
					return UnityMetaFragment(metaIN);
				}
				ENDCG
			}

			
			Pass
			{
				
				Name "ShadowCaster"
				Tags { "LightMode"="ShadowCaster" }
				ZWrite On
				ZTest LEqual
				AlphaToMask Off

				CGPROGRAM
				#define ASE_GEOMETRY
				#define ASE_FRAGMENT_NORMAL 0
				#define ASE_RECEIVE_SHADOWS
				#pragma multi_compile_instancing
				#pragma multi_compile _ LOD_FADE_CROSSFADE
				#define ASE_FOG
				#define ASE_VERSION 19904
				#define ASE_USING_SAMPLING_MACROS 1

				#pragma vertex vert
				#pragma fragment frag
				#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
				#pragma multi_compile_shadowcaster
				#ifndef UNITY_PASS_SHADOWCASTER
					#define UNITY_PASS_SHADOWCASTER
				#endif
				#include "HLSLSupport.cginc"
				#if defined( ASE_GEOMETRY ) || defined( ASE_IMPOSTOR )
					#ifndef UNITY_INSTANCED_LOD_FADE
						#define UNITY_INSTANCED_LOD_FADE
					#endif
					#ifndef UNITY_INSTANCED_SH
						#define UNITY_INSTANCED_SH
					#endif
					#ifndef UNITY_INSTANCED_LIGHTMAPSTS
						#define UNITY_INSTANCED_LIGHTMAPSTS
					#endif
				#endif
				#include "UnityShaderVariables.cginc"
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"

				#define ASE_NEEDS_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_NORMAL
				#pragma shader_feature_local ENUM_DISPLACEMENT_MODE_NONE ENUM_DISPLACEMENT_MODE_BUMP ENUM_DISPLACEMENT_MODE_DISPLACEMENT ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP
				#pragma shader_feature_local ENUM_WRINKLE_MODE_NONE ENUM_WRINKLE_MODE_WRINKLE ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT
				#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex.SampleBias(samplerTex,coord,bias)
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex.SampleGrad(samplerTex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#else//ASE Sampling Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex2Dbias(tex,float4(coord,0,bias))
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex2Dgrad(tex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplertex,coord,lod) tex2DArraylod(tex, float4(coord,lod))
				#endif//ASE Sampling Macros
				


				struct appdata
				{
					float4 vertex : POSITION;
					half3 normal : NORMAL;
					half4 tangent : TANGENT;
					float4 texcoord1 : TEXCOORD1;
					float4 texcoord2 : TEXCOORD2;
					float4 ase_texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					V2F_SHADOW_CASTER;
					
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};

				#ifdef UNITY_STANDARD_USE_DITHER_MASK
					sampler3D _DitherMaskLOD;
				#endif
				#ifdef ASE_TESSELLATION
					float _TessPhongStrength;
					float _TessValue;
					float _TessMin;
					float _TessMax;
					float _TessEdgeLength;
					float _TessMaxDisp;
				#endif

				UNITY_DECLARE_TEX2D_NOSAMPLER(_DisplacementMap);
				uniform float4 _DisplacementMap_ST;
				SamplerState sampler_Linear_Repeat;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleDisplacementPack);
				uniform float4 _WrinkleDisplacementPack_ST;
				SamplerState sampler_WrinkleDisplacementPack;
				uniform float _WrinkleDisplacementStrength;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleFlowPack);
				uniform float4 _WrinkleFlowPack_ST;
				uniform float4 _WrinkleValueSet12CL;
				uniform float4 _WrinkleMaskSetArray_ST;
				uniform float4 _WrinkleValueSet1AL;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleMaskSetArray);
				uniform float4 _WrinkleValueSet1BL;
				uniform float4 _WrinkleValueSet1AR;
				uniform float4 _WrinkleValueSet1BR;
				uniform float4 _WrinkleValueSet12CR;
				uniform float4 _WrinkleValueSet2L;
				uniform float4 _WrinkleValueSet2R;
				uniform float4 _WrinkleValueSetBCCB;
				uniform float4 _WrinkleValueSet3DB;
				uniform float4 _WrinkleValueSet3L;
				uniform float4 _WrinkleValueSet3R;
				uniform float _DisplacementLevel;
				uniform float _DisplacementStrength;


				float MinMaxDeltaBlend1245_g1( float In, float In1, float In2, float In3 )
				{
					return max(max(In1, In2), In3) + min(min(In1, In2), In3) + In;
				}
				

				v2f VertexFunction( appdata v  )
				{
					UNITY_SETUP_INSTANCE_ID(v);
					v2f o;
					UNITY_INITIALIZE_OUTPUT(v2f,o);
					UNITY_TRANSFER_INSTANCE_ID(v,o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					float3 _Vector0 = float3(0,0,0);
					float2 uv_DisplacementMap = v.ase_texcoord.xy * _DisplacementMap_ST.xy + _DisplacementMap_ST.zw;
					float displacementMap321 = SAMPLE_TEXTURE2D_LOD( _DisplacementMap, sampler_Linear_Repeat, uv_DisplacementMap, 0.0 ).r;
					float displacement229_g1 = displacementMap321;
					float In245_g1 = displacement229_g1;
					float2 uv_WrinkleDisplacementPack = v.ase_texcoord.xy * _WrinkleDisplacementPack_ST.xy + _WrinkleDisplacementPack_ST.zw;
					float4 tex2DNode279 = SAMPLE_TEXTURE2D_LOD( _WrinkleDisplacementPack, sampler_WrinkleDisplacementPack, uv_WrinkleDisplacementPack, 0.0 );
					float displacementStrength234_g1 = _WrinkleDisplacementStrength;
					float2 uv_WrinkleFlowPack = v.ase_texcoord.xy * _WrinkleFlowPack_ST.xy + _WrinkleFlowPack_ST.zw;
					float4 tex2DNode259 = SAMPLE_TEXTURE2D_LOD( _WrinkleFlowPack, sampler_Linear_Repeat, uv_WrinkleFlowPack, 0.0 );
					float2 texCoord10_g1 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_1_0_g15 = 0.49;
					float leftMask27_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g15 ) / ( 0.51 - temp_output_1_0_g15 ) ) );
					float4 break107_g1 = _WrinkleValueSet12CL;
					float2 appendResult112_g1 = (float2(break107_g1.x , break107_g1.y));
					float2 uv_WrinkleMaskSetArray = v.ase_texcoord.xy;
					float4 break109_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,4.0), 0.0 );
					float2 appendResult115_g1 = (float2(break109_g1.x , break109_g1.y));
					float dotResult121_g1 = dot( appendResult112_g1 , appendResult115_g1 );
					float value1CLeft135_g1 = dotResult121_g1;
					float4 maskSet1A214_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,0.0), 0.0 );
					float dotResult29_g1 = dot( _WrinkleValueSet1AL , maskSet1A214_g1 );
					float4 temp_output_16_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,1.0), 0.0 );
					float dotResult35_g1 = dot( _WrinkleValueSet1BL , temp_output_16_0_g1 );
					float dotResult30_g1 = dot( maskSet1A214_g1 , _WrinkleValueSet1AR );
					float dotResult36_g1 = dot( temp_output_16_0_g1 , _WrinkleValueSet1BR );
					float4 break108_g1 = _WrinkleValueSet12CR;
					float2 appendResult117_g1 = (float2(break108_g1.x , break108_g1.y));
					float dotResult122_g1 = dot( appendResult115_g1 , appendResult117_g1 );
					float value1CRight136_g1 = dotResult122_g1;
					float temp_output_1_0_g14 = 0.51;
					float rightMask28_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g14 ) / ( 0.49 - temp_output_1_0_g14 ) ) );
					float temp_output_16_0_g78 = ( ( leftMask27_g1 * ( value1CLeft135_g1 + dotResult29_g1 + dotResult35_g1 ) ) + ( ( dotResult30_g1 + dotResult36_g1 + value1CRight136_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g78 = ( saturate( ( tex2DNode259.r + temp_output_16_0_g78 ) ) * temp_output_16_0_g78 );
					float In1245_g1 = ( ( tex2DNode279.r - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g78 );
					float2 appendResult113_g1 = (float2(break107_g1.z , break107_g1.w));
					float2 appendResult114_g1 = (float2(break109_g1.z , break109_g1.w));
					float dotResult123_g1 = dot( appendResult113_g1 , appendResult114_g1 );
					float value2CLeft137_g1 = dotResult123_g1;
					float4 temp_output_17_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,2.0), 0.0 );
					float dotResult41_g1 = dot( _WrinkleValueSet2L , temp_output_17_0_g1 );
					float dotResult42_g1 = dot( temp_output_17_0_g1 , _WrinkleValueSet2R );
					float2 appendResult116_g1 = (float2(break108_g1.z , break108_g1.w));
					float dotResult124_g1 = dot( appendResult114_g1 , appendResult116_g1 );
					float value2CRight138_g1 = dotResult124_g1;
					float temp_output_16_0_g80 = ( ( leftMask27_g1 * ( value2CLeft137_g1 + dotResult41_g1 ) ) + ( ( dotResult42_g1 + value2CRight138_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g80 = ( saturate( ( tex2DNode259.g + temp_output_16_0_g80 ) ) * temp_output_16_0_g80 );
					float In2245_g1 = ( ( tex2DNode279.g - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g80 );
					float4 break211_g1 = _WrinkleValueSetBCCB;
					float valueBCCLeft217_g1 = ( break211_g1.x * maskSet1A214_g1.y );
					float4 break118_g1 = _WrinkleValueSet3DB;
					float2 appendResult120_g1 = (float2(break118_g1.x , break118_g1.y));
					float dotResult127_g1 = dot( appendResult120_g1 , appendResult115_g1 );
					float value3DLeft129_g1 = dotResult127_g1;
					float4 temp_output_18_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,3.0), 0.0 );
					float dotResult47_g1 = dot( _WrinkleValueSet3L , temp_output_18_0_g1 );
					float dotResult48_g1 = dot( temp_output_18_0_g1 , _WrinkleValueSet3R );
					float2 appendResult119_g1 = (float2(break118_g1.z , break118_g1.w));
					float dotResult128_g1 = dot( appendResult115_g1 , appendResult119_g1 );
					float value3DRight130_g1 = dotResult128_g1;
					float valueBCCRight219_g1 = ( maskSet1A214_g1.y * break211_g1.y );
					float temp_output_16_0_g81 = ( ( leftMask27_g1 * ( valueBCCLeft217_g1 + value3DLeft129_g1 + dotResult47_g1 ) ) + ( ( dotResult48_g1 + value3DRight130_g1 + valueBCCRight219_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g81 = ( saturate( ( tex2DNode259.b + temp_output_16_0_g81 ) ) * temp_output_16_0_g81 );
					float In3245_g1 = ( ( tex2DNode279.b - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1245_g1 = MinMaxDeltaBlend1245_g1( In245_g1 , In1245_g1 , In2245_g1 , In3245_g1 );
					float displacementWrinkle331 = localMinMaxDeltaBlend1245_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch332 = displacementWrinkle331;
					#else
					float staticSwitch332 = displacementMap321;
					#endif
					float temp_output_319_0 = ( staticSwitch332 - _DisplacementLevel );
					float3 temp_output_327_0 = ( ( temp_output_319_0 * _DisplacementStrength ) * v.normal );
					#if defined( ENUM_DISPLACEMENT_MODE_NONE )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_BUMP )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPLACEMENT )
					float3 staticSwitch330 = temp_output_327_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP )
					float3 staticSwitch330 = temp_output_327_0;
					#else
					float3 staticSwitch330 = _Vector0;
					#endif
					

					#ifdef ASE_ABSOLUTE_VERTEX_POS
						float3 defaultVertexValue = v.vertex.xyz;
					#else
						float3 defaultVertexValue = float3(0, 0, 0);
					#endif
					float3 vertexValue = staticSwitch330;
					#ifdef ASE_ABSOLUTE_VERTEX_POS
						v.vertex.xyz = vertexValue;
					#else
						v.vertex.xyz += vertexValue;
					#endif
					v.vertex.w = 1;
					v.normal = v.normal;
					v.tangent = v.tangent;

				#if defined( ASE_IMPOSTOR )
					// Disable "Normal Bias" because we're rendering billboard impostors and there's no vertex normals.
					unity_LightShadowBias.z = 0;
				#endif

					TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
					return o;
				}

				#if defined(ASE_TESSELLATION)
				struct VertexControl
				{
					float4 vertex : INTERNALTESSPOS;
					half4 tangent : TANGENT;
					half3 normal : NORMAL;
					float4 texcoord1 : TEXCOORD1;
					float4 texcoord2 : TEXCOORD2;
					float4 ase_texcoord : TEXCOORD0;

					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct TessellationFactors
				{
					float edge[3] : SV_TessFactor;
					float inside : SV_InsideTessFactor;
				};

				VertexControl vert ( appdata v )
				{
					VertexControl o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.vertex = v.vertex;
					o.tangent = v.tangent;
					o.normal = v.normal;
					o.texcoord1 = v.texcoord1;
					o.texcoord2 = v.texcoord2;
					o.ase_texcoord = v.ase_texcoord;
					return o;
				}

				TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
				{
					TessellationFactors o;
					float4 tf = 1;
					float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
					float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
					#if defined(ASE_FIXED_TESSELLATION)
					tf = FixedTess( tessValue );
					#elif defined(ASE_DISTANCE_TESSELLATION)
					tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
					#elif defined(ASE_LENGTH_TESSELLATION)
					tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
					#elif defined(ASE_LENGTH_CULL_TESSELLATION)
					tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
					#endif
					o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
					return o;
				}

				[domain("tri")]
				[partitioning("fractional_odd")]
				[outputtopology("triangle_cw")]
				[patchconstantfunc("TessellationFunction")]
				[outputcontrolpoints(3)]
				VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
				{
				   return patch[id];
				}

				[domain("tri")]
				v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
				{
					appdata o = (appdata) 0;
					o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
					o.tangent = patch[0].tangent * bary.x + patch[1].tangent * bary.y + patch[2].tangent * bary.z;
					o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
					o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
					o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
					o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
					#if defined(ASE_PHONG_TESSELLATION)
					float3 pp[3];
					for (int i = 0; i < 3; ++i)
						pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
					float phongStrength = _TessPhongStrength;
					o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
					#endif
					UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
					return VertexFunction(o);
				}
				#else
				v2f vert( appdata v )
				{
					return VertexFunction( v );
				}
				#endif

				half4 frag( v2f IN 
							#if defined( ASE_DEPTH_WRITE_ON )
								, out float outputDepth : SV_Depth
							#endif
							) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(IN);

					#ifdef LOD_FADE_CROSSFADE
						UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
					#endif

					#if defined(ASE_LIGHTING_SIMPLE)
						SurfaceOutput o = (SurfaceOutput)0;
					#else
						#if defined(_SPECULAR_SETUP)
							SurfaceOutputStandardSpecular o = (SurfaceOutputStandardSpecular)0;
						#else
							SurfaceOutputStandard o = (SurfaceOutputStandard)0;
						#endif
						o.Occlusion = 1;
					#endif

					

					o.Normal = half3( 0, 0, 1 );

					o.Alpha = 1;
					half AlphaClipThreshold = 0.5;
					half AlphaClipThresholdShadow = 0.5;

					#if defined( ASE_DEPTH_WRITE_ON )
						float DeviceDepth = IN.pos.z;
					#endif

					#ifdef _ALPHATEST_SHADOW_ON
						if (unity_LightShadowBias.z != 0.0)
							clip(o.Alpha - AlphaClipThresholdShadow);
						#ifdef _ALPHATEST_ON
						else
							clip(o.Alpha - AlphaClipThreshold);
						#endif
					#else
						#ifdef _ALPHATEST_ON
							clip(o.Alpha - AlphaClipThreshold);
						#endif
					#endif

					#ifdef UNITY_STANDARD_USE_DITHER_MASK
						half alphaRef = tex3D(_DitherMaskLOD, float3(IN.pos.xy*0.25,o.Alpha*0.9375)).a;
						clip(alphaRef - 0.01);
					#endif

					#if defined( ASE_DEPTH_WRITE_ON )
						outputDepth = DeviceDepth;
					#endif

					SHADOW_CASTER_FRAGMENT(IN)
				}
			ENDCG
		}

		
		Pass
		{
			
			Name "SceneSelectionPass"
			Tags { "LightMode"="SceneSelectionPass" }

			ZWrite On

			CGPROGRAM
				#define ASE_GEOMETRY
				#define ASE_FRAGMENT_NORMAL 0
				#define ASE_RECEIVE_SHADOWS
				#pragma multi_compile_instancing
				#pragma multi_compile _ LOD_FADE_CROSSFADE
				#define ASE_FOG
				#define ASE_VERSION 19904
				#define ASE_USING_SAMPLING_MACROS 1

				#pragma vertex vert
				#pragma fragment frag
				#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2

				#pragma multi_compile_fwdbase
				#ifndef UNITY_PASS_FORWARDBASE
					#define UNITY_PASS_FORWARDBASE
				#endif
				#include "HLSLSupport.cginc"
				#if defined( ASE_GEOMETRY ) || defined( ASE_IMPOSTOR )
					#ifndef UNITY_INSTANCED_LOD_FADE
						#define UNITY_INSTANCED_LOD_FADE
					#endif
					#ifndef UNITY_INSTANCED_SH
						#define UNITY_INSTANCED_SH
					#endif
					#ifndef UNITY_INSTANCED_LIGHTMAPSTS
						#define UNITY_INSTANCED_LIGHTMAPSTS
					#endif
				#endif
				#include "UnityShaderVariables.cginc"
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"
				#include "AutoLight.cginc"

				#define ASE_NEEDS_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_NORMAL
				#pragma shader_feature_local ENUM_DISPLACEMENT_MODE_NONE ENUM_DISPLACEMENT_MODE_BUMP ENUM_DISPLACEMENT_MODE_DISPLACEMENT ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP
				#pragma shader_feature_local ENUM_WRINKLE_MODE_NONE ENUM_WRINKLE_MODE_WRINKLE ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT
				#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex.SampleBias(samplerTex,coord,bias)
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex.SampleGrad(samplerTex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#else//ASE Sampling Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex2Dbias(tex,float4(coord,0,bias))
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex2Dgrad(tex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplertex,coord,lod) tex2DArraylod(tex, float4(coord,lod))
				#endif//ASE Sampling Macros
				


				int _ObjectId;
				int _PassValue;

				struct appdata
				{
					float4 vertex : POSITION;
					half3 normal : NORMAL;
					half4 tangent : TANGENT;
					float4 ase_texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 pos : SV_POSITION;
					float4 positionWS : TEXCOORD0; // xyz = positionWS
					half3 normalWS : TEXCOORD1;
					
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};

				#ifdef ASE_TESSELLATION
					float _TessPhongStrength;
					float _TessValue;
					float _TessMin;
					float _TessMax;
					float _TessEdgeLength;
					float _TessMaxDisp;
				#endif

				UNITY_DECLARE_TEX2D_NOSAMPLER(_DisplacementMap);
				uniform float4 _DisplacementMap_ST;
				SamplerState sampler_Linear_Repeat;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleDisplacementPack);
				uniform float4 _WrinkleDisplacementPack_ST;
				SamplerState sampler_WrinkleDisplacementPack;
				uniform float _WrinkleDisplacementStrength;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleFlowPack);
				uniform float4 _WrinkleFlowPack_ST;
				uniform float4 _WrinkleValueSet12CL;
				uniform float4 _WrinkleMaskSetArray_ST;
				uniform float4 _WrinkleValueSet1AL;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleMaskSetArray);
				uniform float4 _WrinkleValueSet1BL;
				uniform float4 _WrinkleValueSet1AR;
				uniform float4 _WrinkleValueSet1BR;
				uniform float4 _WrinkleValueSet12CR;
				uniform float4 _WrinkleValueSet2L;
				uniform float4 _WrinkleValueSet2R;
				uniform float4 _WrinkleValueSetBCCB;
				uniform float4 _WrinkleValueSet3DB;
				uniform float4 _WrinkleValueSet3L;
				uniform float4 _WrinkleValueSet3R;
				uniform float _DisplacementLevel;
				uniform float _DisplacementStrength;


				float MinMaxDeltaBlend1245_g1( float In, float In1, float In2, float In3 )
				{
					return max(max(In1, In2), In3) + min(min(In1, In2), In3) + In;
				}
				

				v2f VertexFunction( appdata v  )
				{
					UNITY_SETUP_INSTANCE_ID(v);
					v2f o;
					UNITY_INITIALIZE_OUTPUT(v2f,o);
					UNITY_TRANSFER_INSTANCE_ID(v,o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					float3 _Vector0 = float3(0,0,0);
					float2 uv_DisplacementMap = v.ase_texcoord.xy * _DisplacementMap_ST.xy + _DisplacementMap_ST.zw;
					float displacementMap321 = SAMPLE_TEXTURE2D_LOD( _DisplacementMap, sampler_Linear_Repeat, uv_DisplacementMap, 0.0 ).r;
					float displacement229_g1 = displacementMap321;
					float In245_g1 = displacement229_g1;
					float2 uv_WrinkleDisplacementPack = v.ase_texcoord.xy * _WrinkleDisplacementPack_ST.xy + _WrinkleDisplacementPack_ST.zw;
					float4 tex2DNode279 = SAMPLE_TEXTURE2D_LOD( _WrinkleDisplacementPack, sampler_WrinkleDisplacementPack, uv_WrinkleDisplacementPack, 0.0 );
					float displacementStrength234_g1 = _WrinkleDisplacementStrength;
					float2 uv_WrinkleFlowPack = v.ase_texcoord.xy * _WrinkleFlowPack_ST.xy + _WrinkleFlowPack_ST.zw;
					float4 tex2DNode259 = SAMPLE_TEXTURE2D_LOD( _WrinkleFlowPack, sampler_Linear_Repeat, uv_WrinkleFlowPack, 0.0 );
					float2 texCoord10_g1 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_1_0_g15 = 0.49;
					float leftMask27_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g15 ) / ( 0.51 - temp_output_1_0_g15 ) ) );
					float4 break107_g1 = _WrinkleValueSet12CL;
					float2 appendResult112_g1 = (float2(break107_g1.x , break107_g1.y));
					float2 uv_WrinkleMaskSetArray = v.ase_texcoord.xy;
					float4 break109_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,4.0), 0.0 );
					float2 appendResult115_g1 = (float2(break109_g1.x , break109_g1.y));
					float dotResult121_g1 = dot( appendResult112_g1 , appendResult115_g1 );
					float value1CLeft135_g1 = dotResult121_g1;
					float4 maskSet1A214_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,0.0), 0.0 );
					float dotResult29_g1 = dot( _WrinkleValueSet1AL , maskSet1A214_g1 );
					float4 temp_output_16_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,1.0), 0.0 );
					float dotResult35_g1 = dot( _WrinkleValueSet1BL , temp_output_16_0_g1 );
					float dotResult30_g1 = dot( maskSet1A214_g1 , _WrinkleValueSet1AR );
					float dotResult36_g1 = dot( temp_output_16_0_g1 , _WrinkleValueSet1BR );
					float4 break108_g1 = _WrinkleValueSet12CR;
					float2 appendResult117_g1 = (float2(break108_g1.x , break108_g1.y));
					float dotResult122_g1 = dot( appendResult115_g1 , appendResult117_g1 );
					float value1CRight136_g1 = dotResult122_g1;
					float temp_output_1_0_g14 = 0.51;
					float rightMask28_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g14 ) / ( 0.49 - temp_output_1_0_g14 ) ) );
					float temp_output_16_0_g78 = ( ( leftMask27_g1 * ( value1CLeft135_g1 + dotResult29_g1 + dotResult35_g1 ) ) + ( ( dotResult30_g1 + dotResult36_g1 + value1CRight136_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g78 = ( saturate( ( tex2DNode259.r + temp_output_16_0_g78 ) ) * temp_output_16_0_g78 );
					float In1245_g1 = ( ( tex2DNode279.r - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g78 );
					float2 appendResult113_g1 = (float2(break107_g1.z , break107_g1.w));
					float2 appendResult114_g1 = (float2(break109_g1.z , break109_g1.w));
					float dotResult123_g1 = dot( appendResult113_g1 , appendResult114_g1 );
					float value2CLeft137_g1 = dotResult123_g1;
					float4 temp_output_17_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,2.0), 0.0 );
					float dotResult41_g1 = dot( _WrinkleValueSet2L , temp_output_17_0_g1 );
					float dotResult42_g1 = dot( temp_output_17_0_g1 , _WrinkleValueSet2R );
					float2 appendResult116_g1 = (float2(break108_g1.z , break108_g1.w));
					float dotResult124_g1 = dot( appendResult114_g1 , appendResult116_g1 );
					float value2CRight138_g1 = dotResult124_g1;
					float temp_output_16_0_g80 = ( ( leftMask27_g1 * ( value2CLeft137_g1 + dotResult41_g1 ) ) + ( ( dotResult42_g1 + value2CRight138_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g80 = ( saturate( ( tex2DNode259.g + temp_output_16_0_g80 ) ) * temp_output_16_0_g80 );
					float In2245_g1 = ( ( tex2DNode279.g - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g80 );
					float4 break211_g1 = _WrinkleValueSetBCCB;
					float valueBCCLeft217_g1 = ( break211_g1.x * maskSet1A214_g1.y );
					float4 break118_g1 = _WrinkleValueSet3DB;
					float2 appendResult120_g1 = (float2(break118_g1.x , break118_g1.y));
					float dotResult127_g1 = dot( appendResult120_g1 , appendResult115_g1 );
					float value3DLeft129_g1 = dotResult127_g1;
					float4 temp_output_18_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,3.0), 0.0 );
					float dotResult47_g1 = dot( _WrinkleValueSet3L , temp_output_18_0_g1 );
					float dotResult48_g1 = dot( temp_output_18_0_g1 , _WrinkleValueSet3R );
					float2 appendResult119_g1 = (float2(break118_g1.z , break118_g1.w));
					float dotResult128_g1 = dot( appendResult115_g1 , appendResult119_g1 );
					float value3DRight130_g1 = dotResult128_g1;
					float valueBCCRight219_g1 = ( maskSet1A214_g1.y * break211_g1.y );
					float temp_output_16_0_g81 = ( ( leftMask27_g1 * ( valueBCCLeft217_g1 + value3DLeft129_g1 + dotResult47_g1 ) ) + ( ( dotResult48_g1 + value3DRight130_g1 + valueBCCRight219_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g81 = ( saturate( ( tex2DNode259.b + temp_output_16_0_g81 ) ) * temp_output_16_0_g81 );
					float In3245_g1 = ( ( tex2DNode279.b - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1245_g1 = MinMaxDeltaBlend1245_g1( In245_g1 , In1245_g1 , In2245_g1 , In3245_g1 );
					float displacementWrinkle331 = localMinMaxDeltaBlend1245_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch332 = displacementWrinkle331;
					#else
					float staticSwitch332 = displacementMap321;
					#endif
					float temp_output_319_0 = ( staticSwitch332 - _DisplacementLevel );
					float3 temp_output_327_0 = ( ( temp_output_319_0 * _DisplacementStrength ) * v.normal );
					#if defined( ENUM_DISPLACEMENT_MODE_NONE )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_BUMP )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPLACEMENT )
					float3 staticSwitch330 = temp_output_327_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP )
					float3 staticSwitch330 = temp_output_327_0;
					#else
					float3 staticSwitch330 = _Vector0;
					#endif
					

					#ifdef ASE_ABSOLUTE_VERTEX_POS
						float3 defaultVertexValue = v.vertex.xyz;
					#else
						float3 defaultVertexValue = float3(0, 0, 0);
					#endif
					float3 vertexValue = staticSwitch330;
					#ifdef ASE_ABSOLUTE_VERTEX_POS
						v.vertex.xyz = vertexValue;
					#else
						v.vertex.xyz += vertexValue;
					#endif
					v.vertex.w = 1;
					v.normal = v.normal;
					v.tangent = v.tangent;

					float3 positionWS = mul( unity_ObjectToWorld, v.vertex ).xyz;
					half3 normalWS = UnityObjectToWorldNormal( v.normal );

					o.pos = UnityObjectToClipPos( v.vertex );
					o.positionWS.xyz = positionWS;
					o.normalWS = normalWS;
					return o;
				}

				#if defined(ASE_TESSELLATION)
				struct VertexControl
				{
					float4 vertex : INTERNALTESSPOS;
					half3 normal : NORMAL;
					float4 ase_texcoord : TEXCOORD0;

					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct TessellationFactors
				{
					float edge[3] : SV_TessFactor;
					float inside : SV_InsideTessFactor;
				};

				VertexControl vert ( appdata v )
				{
					VertexControl o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.vertex = v.vertex;
					o.normal = v.normal;
					o.ase_texcoord = v.ase_texcoord;
					return o;
				}

				TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
				{
					TessellationFactors o;
					float4 tf = 1;
					float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
					float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
					#if defined(ASE_FIXED_TESSELLATION)
					tf = FixedTess( tessValue );
					#elif defined(ASE_DISTANCE_TESSELLATION)
					tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
					#elif defined(ASE_LENGTH_TESSELLATION)
					tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
					#elif defined(ASE_LENGTH_CULL_TESSELLATION)
					tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
					#endif
					o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
					return o;
				}

				[domain("tri")]
				[partitioning("fractional_odd")]
				[outputtopology("triangle_cw")]
				[patchconstantfunc("TessellationFunction")]
				[outputcontrolpoints(3)]
				VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
				{
				   return patch[id];
				}

				[domain("tri")]
				v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
				{
					appdata o = (appdata) 0;
					o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
					o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
					o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
					#if defined(ASE_PHONG_TESSELLATION)
					float3 pp[3];
					for (int i = 0; i < 3; ++i)
						pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
					float phongStrength = _TessPhongStrength;
					o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
					#endif
					UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
					return VertexFunction(o);
				}
				#else
				v2f vert ( appdata v )
				{
					return VertexFunction( v );
				}
				#endif

				half4 frag( v2f IN 
							#if defined( ASE_DEPTH_WRITE_ON )
								, out float outputDepth : SV_Depth
							#endif
							) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(IN);

					#ifdef LOD_FADE_CROSSFADE
						UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
					#endif

					

					half Alpha = 1;
					half AlphaClipThreshold = 0.5;

					#if defined( ASE_DEPTH_WRITE_ON )
						float DeviceDepth = IN.pos.z;
					#endif

					#ifdef _ALPHATEST_ON
						clip( Alpha - AlphaClipThreshold );
					#endif

					#if defined( ASE_DEPTH_WRITE_ON )
						outputDepth = DeviceDepth;
					#endif

					return float4( _ObjectId, _PassValue, 1.0, 1.0 );
				}
			ENDCG
		}

		
		Pass
		{
			
			Name "ScenePickingPass"
			Tags { "LightMode"="ScenePickingPass" }

			ZWrite On

			CGPROGRAM
				#define ASE_GEOMETRY
				#define ASE_FRAGMENT_NORMAL 0
				#define ASE_RECEIVE_SHADOWS
				#pragma multi_compile_instancing
				#pragma multi_compile _ LOD_FADE_CROSSFADE
				#define ASE_FOG
				#define ASE_VERSION 19904
				#define ASE_USING_SAMPLING_MACROS 1

				#pragma vertex vert
				#pragma fragment frag
				#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2

				#pragma multi_compile_fwdbase
				#ifndef UNITY_PASS_FORWARDBASE
					#define UNITY_PASS_FORWARDBASE
				#endif
				#include "HLSLSupport.cginc"
				#if defined( ASE_GEOMETRY ) || defined( ASE_IMPOSTOR )
					#ifndef UNITY_INSTANCED_LOD_FADE
						#define UNITY_INSTANCED_LOD_FADE
					#endif
					#ifndef UNITY_INSTANCED_SH
						#define UNITY_INSTANCED_SH
					#endif
					#ifndef UNITY_INSTANCED_LIGHTMAPSTS
						#define UNITY_INSTANCED_LIGHTMAPSTS
					#endif
				#endif
				#include "UnityShaderVariables.cginc"
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"
				#include "AutoLight.cginc"

				#define ASE_NEEDS_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_TEXTURE_COORDINATES0
				#define ASE_NEEDS_VERT_NORMAL
				#pragma shader_feature_local ENUM_DISPLACEMENT_MODE_NONE ENUM_DISPLACEMENT_MODE_BUMP ENUM_DISPLACEMENT_MODE_DISPLACEMENT ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP
				#pragma shader_feature_local ENUM_WRINKLE_MODE_NONE ENUM_WRINKLE_MODE_WRINKLE ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT
				#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex.SampleBias(samplerTex,coord,bias)
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex.SampleGrad(samplerTex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
				#else//ASE Sampling Macros
				#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
				#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
				#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex2Dbias(tex,float4(coord,0,bias))
				#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex2Dgrad(tex,coord,ddx,ddy)
				#define SAMPLE_TEXTURE2D_ARRAY_LOD(tex,samplertex,coord,lod) tex2DArraylod(tex, float4(coord,lod))
				#endif//ASE Sampling Macros
				


				float4 _SelectionID;

				struct appdata
				{
					float4 vertex : POSITION;
					half3 normal : NORMAL;
					half4 tangent : TANGENT;
					float4 ase_texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 pos : SV_POSITION;
					float4 positionWS : TEXCOORD0; // xyz = positionWS
					half3 normalWS : TEXCOORD1;
					
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};

				#ifdef ASE_TESSELLATION
					float _TessPhongStrength;
					float _TessValue;
					float _TessMin;
					float _TessMax;
					float _TessEdgeLength;
					float _TessMaxDisp;
				#endif

				UNITY_DECLARE_TEX2D_NOSAMPLER(_DisplacementMap);
				uniform float4 _DisplacementMap_ST;
				SamplerState sampler_Linear_Repeat;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleDisplacementPack);
				uniform float4 _WrinkleDisplacementPack_ST;
				SamplerState sampler_WrinkleDisplacementPack;
				uniform float _WrinkleDisplacementStrength;
				UNITY_DECLARE_TEX2D_NOSAMPLER(_WrinkleFlowPack);
				uniform float4 _WrinkleFlowPack_ST;
				uniform float4 _WrinkleValueSet12CL;
				uniform float4 _WrinkleMaskSetArray_ST;
				uniform float4 _WrinkleValueSet1AL;
				UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(_WrinkleMaskSetArray);
				uniform float4 _WrinkleValueSet1BL;
				uniform float4 _WrinkleValueSet1AR;
				uniform float4 _WrinkleValueSet1BR;
				uniform float4 _WrinkleValueSet12CR;
				uniform float4 _WrinkleValueSet2L;
				uniform float4 _WrinkleValueSet2R;
				uniform float4 _WrinkleValueSetBCCB;
				uniform float4 _WrinkleValueSet3DB;
				uniform float4 _WrinkleValueSet3L;
				uniform float4 _WrinkleValueSet3R;
				uniform float _DisplacementLevel;
				uniform float _DisplacementStrength;


				float MinMaxDeltaBlend1245_g1( float In, float In1, float In2, float In3 )
				{
					return max(max(In1, In2), In3) + min(min(In1, In2), In3) + In;
				}
				

				v2f VertexFunction( appdata v  )
				{
					UNITY_SETUP_INSTANCE_ID(v);
					v2f o;
					UNITY_INITIALIZE_OUTPUT(v2f,o);
					UNITY_TRANSFER_INSTANCE_ID(v,o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					float3 _Vector0 = float3(0,0,0);
					float2 uv_DisplacementMap = v.ase_texcoord.xy * _DisplacementMap_ST.xy + _DisplacementMap_ST.zw;
					float displacementMap321 = SAMPLE_TEXTURE2D_LOD( _DisplacementMap, sampler_Linear_Repeat, uv_DisplacementMap, 0.0 ).r;
					float displacement229_g1 = displacementMap321;
					float In245_g1 = displacement229_g1;
					float2 uv_WrinkleDisplacementPack = v.ase_texcoord.xy * _WrinkleDisplacementPack_ST.xy + _WrinkleDisplacementPack_ST.zw;
					float4 tex2DNode279 = SAMPLE_TEXTURE2D_LOD( _WrinkleDisplacementPack, sampler_WrinkleDisplacementPack, uv_WrinkleDisplacementPack, 0.0 );
					float displacementStrength234_g1 = _WrinkleDisplacementStrength;
					float2 uv_WrinkleFlowPack = v.ase_texcoord.xy * _WrinkleFlowPack_ST.xy + _WrinkleFlowPack_ST.zw;
					float4 tex2DNode259 = SAMPLE_TEXTURE2D_LOD( _WrinkleFlowPack, sampler_Linear_Repeat, uv_WrinkleFlowPack, 0.0 );
					float2 texCoord10_g1 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_1_0_g15 = 0.49;
					float leftMask27_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g15 ) / ( 0.51 - temp_output_1_0_g15 ) ) );
					float4 break107_g1 = _WrinkleValueSet12CL;
					float2 appendResult112_g1 = (float2(break107_g1.x , break107_g1.y));
					float2 uv_WrinkleMaskSetArray = v.ase_texcoord.xy;
					float4 break109_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,4.0), 0.0 );
					float2 appendResult115_g1 = (float2(break109_g1.x , break109_g1.y));
					float dotResult121_g1 = dot( appendResult112_g1 , appendResult115_g1 );
					float value1CLeft135_g1 = dotResult121_g1;
					float4 maskSet1A214_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,0.0), 0.0 );
					float dotResult29_g1 = dot( _WrinkleValueSet1AL , maskSet1A214_g1 );
					float4 temp_output_16_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,1.0), 0.0 );
					float dotResult35_g1 = dot( _WrinkleValueSet1BL , temp_output_16_0_g1 );
					float dotResult30_g1 = dot( maskSet1A214_g1 , _WrinkleValueSet1AR );
					float dotResult36_g1 = dot( temp_output_16_0_g1 , _WrinkleValueSet1BR );
					float4 break108_g1 = _WrinkleValueSet12CR;
					float2 appendResult117_g1 = (float2(break108_g1.x , break108_g1.y));
					float dotResult122_g1 = dot( appendResult115_g1 , appendResult117_g1 );
					float value1CRight136_g1 = dotResult122_g1;
					float temp_output_1_0_g14 = 0.51;
					float rightMask28_g1 = saturate( ( ( texCoord10_g1.x - temp_output_1_0_g14 ) / ( 0.49 - temp_output_1_0_g14 ) ) );
					float temp_output_16_0_g78 = ( ( leftMask27_g1 * ( value1CLeft135_g1 + dotResult29_g1 + dotResult35_g1 ) ) + ( ( dotResult30_g1 + dotResult36_g1 + value1CRight136_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g78 = ( saturate( ( tex2DNode259.r + temp_output_16_0_g78 ) ) * temp_output_16_0_g78 );
					float In1245_g1 = ( ( tex2DNode279.r - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g78 );
					float2 appendResult113_g1 = (float2(break107_g1.z , break107_g1.w));
					float2 appendResult114_g1 = (float2(break109_g1.z , break109_g1.w));
					float dotResult123_g1 = dot( appendResult113_g1 , appendResult114_g1 );
					float value2CLeft137_g1 = dotResult123_g1;
					float4 temp_output_17_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,2.0), 0.0 );
					float dotResult41_g1 = dot( _WrinkleValueSet2L , temp_output_17_0_g1 );
					float dotResult42_g1 = dot( temp_output_17_0_g1 , _WrinkleValueSet2R );
					float2 appendResult116_g1 = (float2(break108_g1.z , break108_g1.w));
					float dotResult124_g1 = dot( appendResult114_g1 , appendResult116_g1 );
					float value2CRight138_g1 = dotResult124_g1;
					float temp_output_16_0_g80 = ( ( leftMask27_g1 * ( value2CLeft137_g1 + dotResult41_g1 ) ) + ( ( dotResult42_g1 + value2CRight138_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g80 = ( saturate( ( tex2DNode259.g + temp_output_16_0_g80 ) ) * temp_output_16_0_g80 );
					float In2245_g1 = ( ( tex2DNode279.g - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g80 );
					float4 break211_g1 = _WrinkleValueSetBCCB;
					float valueBCCLeft217_g1 = ( break211_g1.x * maskSet1A214_g1.y );
					float4 break118_g1 = _WrinkleValueSet3DB;
					float2 appendResult120_g1 = (float2(break118_g1.x , break118_g1.y));
					float dotResult127_g1 = dot( appendResult120_g1 , appendResult115_g1 );
					float value3DLeft129_g1 = dotResult127_g1;
					float4 temp_output_18_0_g1 = SAMPLE_TEXTURE2D_ARRAY_LOD( _WrinkleMaskSetArray, sampler_Linear_Repeat, float3(uv_WrinkleMaskSetArray,3.0), 0.0 );
					float dotResult47_g1 = dot( _WrinkleValueSet3L , temp_output_18_0_g1 );
					float dotResult48_g1 = dot( temp_output_18_0_g1 , _WrinkleValueSet3R );
					float2 appendResult119_g1 = (float2(break118_g1.z , break118_g1.w));
					float dotResult128_g1 = dot( appendResult115_g1 , appendResult119_g1 );
					float value3DRight130_g1 = dotResult128_g1;
					float valueBCCRight219_g1 = ( maskSet1A214_g1.y * break211_g1.y );
					float temp_output_16_0_g81 = ( ( leftMask27_g1 * ( valueBCCLeft217_g1 + value3DLeft129_g1 + dotResult47_g1 ) ) + ( ( dotResult48_g1 + value3DRight130_g1 + valueBCCRight219_g1 ) * rightMask28_g1 ) );
					float temp_output_23_0_g81 = ( saturate( ( tex2DNode259.b + temp_output_16_0_g81 ) ) * temp_output_16_0_g81 );
					float In3245_g1 = ( ( tex2DNode279.b - displacement229_g1 ) * displacementStrength234_g1 * temp_output_23_0_g81 );
					float localMinMaxDeltaBlend1245_g1 = MinMaxDeltaBlend1245_g1( In245_g1 , In1245_g1 , In2245_g1 , In3245_g1 );
					float displacementWrinkle331 = localMinMaxDeltaBlend1245_g1;
					#if defined( ENUM_WRINKLE_MODE_NONE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE )
					float staticSwitch332 = displacementMap321;
					#elif defined( ENUM_WRINKLE_MODE_WRINKLE_DISPLACEMENT )
					float staticSwitch332 = displacementWrinkle331;
					#else
					float staticSwitch332 = displacementMap321;
					#endif
					float temp_output_319_0 = ( staticSwitch332 - _DisplacementLevel );
					float3 temp_output_327_0 = ( ( temp_output_319_0 * _DisplacementStrength ) * v.normal );
					#if defined( ENUM_DISPLACEMENT_MODE_NONE )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_BUMP )
					float3 staticSwitch330 = _Vector0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPLACEMENT )
					float3 staticSwitch330 = temp_output_327_0;
					#elif defined( ENUM_DISPLACEMENT_MODE_DISPACEMENT_BUMP )
					float3 staticSwitch330 = temp_output_327_0;
					#else
					float3 staticSwitch330 = _Vector0;
					#endif
					

					#ifdef ASE_ABSOLUTE_VERTEX_POS
						float3 defaultVertexValue = v.vertex.xyz;
					#else
						float3 defaultVertexValue = float3(0, 0, 0);
					#endif
					float3 vertexValue = staticSwitch330;
					#ifdef ASE_ABSOLUTE_VERTEX_POS
						v.vertex.xyz = vertexValue;
					#else
						v.vertex.xyz += vertexValue;
					#endif
					v.vertex.w = 1;
					v.normal = v.normal;
					v.tangent = v.tangent;

					float3 positionWS = mul( unity_ObjectToWorld, v.vertex ).xyz;
					half3 normalWS = UnityObjectToWorldNormal( v.normal );

					o.pos = UnityObjectToClipPos( v.vertex );
					o.positionWS.xyz = positionWS;
					o.normalWS = normalWS;
					return o;
				}

				#if defined(ASE_TESSELLATION)
				struct VertexControl
				{
					float4 vertex : INTERNALTESSPOS;
					half3 normal : NORMAL;
					float4 ase_texcoord : TEXCOORD0;

					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct TessellationFactors
				{
					float edge[3] : SV_TessFactor;
					float inside : SV_InsideTessFactor;
				};

				VertexControl vert ( appdata v )
				{
					VertexControl o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.vertex = v.vertex;
					o.normal = v.normal;
					o.ase_texcoord = v.ase_texcoord;
					return o;
				}

				TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
				{
					TessellationFactors o;
					float4 tf = 1;
					float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
					float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
					#if defined(ASE_FIXED_TESSELLATION)
					tf = FixedTess( tessValue );
					#elif defined(ASE_DISTANCE_TESSELLATION)
					tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, UNITY_MATRIX_M, _WorldSpaceCameraPos );
					#elif defined(ASE_LENGTH_TESSELLATION)
					tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams );
					#elif defined(ASE_LENGTH_CULL_TESSELLATION)
					tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, UNITY_MATRIX_M, _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
					#endif
					o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
					return o;
				}

				[domain("tri")]
				[partitioning("fractional_odd")]
				[outputtopology("triangle_cw")]
				[patchconstantfunc("TessellationFunction")]
				[outputcontrolpoints(3)]
				VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
				{
				   return patch[id];
				}

				[domain("tri")]
				v2f DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
				{
					appdata o = (appdata) 0;
					o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
					o.normal = patch[0].normal * bary.x + patch[1].normal * bary.y + patch[2].normal * bary.z;
					o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
					#if defined(ASE_PHONG_TESSELLATION)
					float3 pp[3];
					for (int i = 0; i < 3; ++i)
						pp[i] = o.vertex.xyz - patch[i].normal * (dot(o.vertex.xyz, patch[i].normal) - dot(patch[i].vertex.xyz, patch[i].normal));
					float phongStrength = _TessPhongStrength;
					o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
					#endif
					UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
					return VertexFunction(o);
				}
				#else
				v2f vert ( appdata v )
				{
					return VertexFunction( v );
				}
				#endif

				half4 frag( v2f IN 
							#if defined( ASE_DEPTH_WRITE_ON )
								, out float outputDepth : SV_Depth
							#endif
							) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(IN);

					#ifdef LOD_FADE_CROSSFADE
						UNITY_APPLY_DITHER_CROSSFADE(IN.pos.xy);
					#endif

					

					half Alpha = 1;
					half AlphaClipThreshold = 0.5;

					#if defined( ASE_DEPTH_WRITE_ON )
						float DeviceDepth = IN.pos.z;
					#endif

					#ifdef _ALPHATEST_ON
						clip( Alpha - AlphaClipThreshold );
					#endif

					#if defined( ASE_DEPTH_WRITE_ON )
						outputDepth = DeviceDepth;
					#endif

					return _SelectionID;
				}
			ENDCG
		}
		
	}
	CustomEditor "AmplifyShaderEditor.MaterialInspector"
	
	Fallback "Shader Graphs/RL5_SkinShader_Baked_3D"
}
/*ASEBEGIN
Version=19904
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;176;-2368,-960;Inherit;False;1855.013;669.454;Comment;21;369;336;155;340;314;339;148;374;338;337;156;219;375;147;154;218;262;146;335;258;153;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;178;-2224,688;Inherit;False;1764.486;534.3745;Comment;13;397;398;394;168;167;341;224;164;165;222;221;150;265;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;175;-2208,-1472;Inherit;False;1705.688;500.4623;Comment;9;379;200;174;313;173;215;216;378;377;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;153;-2304,-864;Inherit;False;Property;_BumpScale;Bump Scale;6;0;Create;True;0;0;0;False;0;False;1;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;225;-5196.062,-617.9283;Inherit;False;2668.702;1991.226;;71;260;257;255;236;235;240;239;256;234;233;238;237;304;303;302;301;300;309;308;307;306;305;311;310;232;251;261;278;277;299;298;297;294;295;296;259;279;291;292;293;276;288;289;290;252;284;282;283;281;280;285;286;287;273;274;268;269;270;271;275;272;231;230;229;253;226;228;227;312;331;334;Wrinkle System;1,0.5137255,0.7779443,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;265;-2192,960;Inherit;False;261;mainSamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;258;-2032,-736;Inherit;False;normalMapScale;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;335;-2016,-896;Inherit;False;261;mainSamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;379;-2128,-1152;Inherit;False;261;mainSamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.TexturePropertyNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;377;-2128,-1360;Inherit;True;Property;_MainTex;Main Tex;0;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;False;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;150;-1952,832;Inherit;True;Property;_MetallicGlossMap;Metallic Gloss Map;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;232;-5120,272;Inherit;False;258;normalMapScale;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;146;-1760,-896;Inherit;True;Property;_BumpMap;Bump Map;5;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;378;-1840,-1200;Inherit;True;Property;_TextureSample0;Texture Sample 0;56;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;221;-1600,912;Inherit;False;smoothnessMap;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;272;-4482.697,-435.7407;Inherit;True;Property;_WrinkleMaskSetArray;Wrinkle Mask Set Array;33;0;Fetch;True;0;0;0;False;0;False;-1;None;None;True;0;False;black;LockedToTexture2DArray;False;Object;-1;Auto;Texture2DArray;False;8;0;SAMPLER2DARRAY;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;4;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;275;-4482.697,-163.7407;Inherit;True;Property;_WrinkleDiffuseArray;Wrinkle Diffuse Array;34;0;Fetch;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;LockedToTexture2DArray;False;Object;-1;Auto;Texture2DArray;False;8;0;SAMPLER2DARRAY;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;2;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;271;-4498.697,-467.7407;Inherit;True;Property;_WrinkleMaskSetArray;Wrinkle Mask Set Array;57;0;Fetch;True;0;0;0;False;0;False;-1;None;None;True;0;False;black;LockedToTexture2DArray;False;Object;-1;Auto;Texture2DArray;False;8;0;SAMPLER2DARRAY;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;3;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;270;-4514.697,-499.7407;Inherit;True;Property;_WrinkleMaskSetArray;Wrinkle Mask Set Array;56;0;Fetch;True;0;0;0;False;0;False;-1;None;None;True;0;False;black;LockedToTexture2DArray;False;Object;-1;Auto;Texture2DArray;False;8;0;SAMPLER2DARRAY;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;2;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;269;-4530.697,-531.7407;Inherit;True;Property;_WrinkleMaskSetArray;Wrinkle Mask Set Array;56;0;Fetch;True;0;0;0;False;0;False;-1;None;None;True;0;False;black;LockedToTexture2DArray;False;Object;-1;Auto;Texture2DArray;False;8;0;SAMPLER2DARRAY;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;1;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;268;-4546.697,-563.7407;Inherit;True;Property;_WrinkleMaskSetArray;Wrinkle Mask Set Array;31;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;black;LockedToTexture2DArray;False;Object;-1;Auto;Texture2DArray;False;8;0;SAMPLER2DARRAY;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;274;-4498.697,-195.7407;Inherit;True;Property;_WrinkleDiffuseArray;Wrinkle Diffuse Array;59;0;Fetch;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;LockedToTexture2DArray;False;Object;-1;Auto;Texture2DArray;False;8;0;SAMPLER2DARRAY;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;1;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;273;-4514.697,-227.7407;Inherit;True;Property;_WrinkleDiffuseArray;Wrinkle Diffuse Array;32;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;LockedToTexture2DArray;False;Object;-1;Auto;Texture2DArray;False;8;0;SAMPLER2DARRAY;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;252;-4480,32;Inherit;True;Property;_WrinkleSmoothnessPack;Wrinkle Smoothness Pack;35;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;276;-4496,240;Inherit;True;Property;_WrinkleNormalArray;Wrinkle Normal Array;36;1;[Normal];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;bump;LockedToTexture2DArray;True;Object;-1;Auto;Texture2DArray;False;8;0;SAMPLER2DARRAY;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;279;-4480,496;Inherit;True;Property;_WrinkleDisplacementPack;Wrinkle Displacement Pack;38;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;linearGrey;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;259;-4480,688;Inherit;True;Property;_WrinkleFlowPack;Wrinkle Flow Pack;37;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;277;-4480,272;Inherit;True;Property;_WrinkleNormalArray;Wrinkle Normal Array;62;1;[Normal];Fetch;True;0;0;0;False;0;False;-1;None;None;True;0;False;bump;LockedToTexture2DArray;True;Object;-1;Auto;Texture2DArray;False;8;0;SAMPLER2DARRAY;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;1;False;7;SAMPLERSTATE;;False;6;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;278;-4464,304;Inherit;True;Property;_WrinkleNormalArray;Wrinkle Normal Array;61;1;[Normal];Fetch;True;0;0;0;False;0;False;-1;None;None;True;0;False;bump;LockedToTexture2DArray;True;Object;-1;Auto;Texture2DArray;False;8;0;SAMPLER2DARRAY;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;2;False;7;SAMPLERSTATE;;False;6;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;237;-4640,960;Inherit;False;Property;_WrinkleValueSet1AL;Wrinkle Value Set 1A Left;39;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;238;-4624,992;Inherit;False;Property;_WrinkleValueSet1BL;Wrinkle Value Set 1B Left;40;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;233;-4608,1024;Inherit;False;Property;_WrinkleValueSet2L;Wrinkle Value Set 2 Left;41;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;234;-4592,1056;Inherit;False;Property;_WrinkleValueSet3L;Wrinkle Value Set 3 Left;42;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;256;-4576,1088;Inherit;False;Property;_WrinkleValueSet12CL;Wrinkle Value Set 12C Left;43;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;239;-4336,960;Inherit;False;Property;_WrinkleValueSet1AR;Wrinkle Value Set 1A Right;44;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;240;-4320,992;Inherit;False;Property;_WrinkleValueSet1BR;Wrinkle Value Set 1B Right;45;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;235;-4304,1024;Inherit;False;Property;_WrinkleValueSet2R;Wrinkle Value Set 2 Right;46;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;236;-4288,1056;Inherit;False;Property;_WrinkleValueSet3R;Wrinkle Value Set 3 Right;47;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;255;-4272,1088;Inherit;False;Property;_WrinkleValueSet12CR;Wrinkle Value Set 12C Right;48;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;257;-3952,960;Inherit;False;Property;_WrinkleValueSet3DB;Wrinkle Value Set 3D Both;49;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;260;-3936,992;Inherit;False;Property;_WrinkleValueSetBCCB;Wrinkle Value Set BCC Both;50;0;Create;False;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerStateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;251;-5088,112;Inherit;False;0;0;0;1;2;None;1;0;SAMPLER2D;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;216;-1504,-1200;Inherit;False;diffuseMap;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;218;-1456,-896;Inherit;False;normalMap;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;315;-3040,2544;Inherit;False;2475.808;713.9033;Comment;14;330;327;326;325;324;323;322;321;320;319;318;317;316;332;Vertex Displacement;1,1,1,1;0;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;287;-3792,48;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;286;-3792,48;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;285;-3792,48;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;280;-3792,-80;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;281;-3792,-80;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;283;-3792,-80;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;282;-3792,-80;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;284;-3792,-80;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;290;-3792,176;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;289;-3792,176;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;288;-3792,176;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;293;-3792,304;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;292;-3792,304;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;291;-3792,304;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;296;-3792,432;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;295;-3792,432;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;294;-3792,432;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;297;-3792,496;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;298;-3792,496;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;299;-3792,496;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;310;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;311;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;305;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;306;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;307;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;308;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;309;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;300;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;301;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;302;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;303;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;304;-3520,608;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;312;-3712,-192;Inherit;False;Property;_WrinkleDisplacementStrength;Wrinkle Displacement Strength;18;0;Create;True;0;0;0;False;0;False;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;228;-3648,-384;Inherit;False;221;smoothnessMap;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;226;-3616,-448;Inherit;False;216;diffuseMap;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;227;-3616,-320;Inherit;False;218;normalMap;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;334;-3648,-256;Inherit;False;321;displacementMap;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;261;-5120,192;Inherit;False;mainSamplerState;-1;True;1;0;SAMPLERSTATE;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;318;-2992,2640;Inherit;False;261;mainSamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;253;-3270.997,-186.733;Inherit;False;RL5_Amplify_WrinkleMapSystem;-1;;1;ada0dcb2336c5f74995ee8fbcf5e9cf2;0;37;7;COLOR;0,0,0,0;False;8;FLOAT;0;False;9;FLOAT3;0,0,0;False;228;FLOAT;0;False;233;FLOAT;0;False;15;FLOAT4;0,0,0,0;False;16;FLOAT4;0,0,0,0;False;17;FLOAT4;0,0,0,0;False;18;FLOAT4;0,0,0,0;False;105;FLOAT4;0,0,0,0;False;67;COLOR;0,0,0,0;False;71;COLOR;0,0,0,0;False;76;COLOR;0,0,0,0;False;68;FLOAT;0;False;72;FLOAT;0;False;77;FLOAT;0;False;69;FLOAT3;0,0,0;False;73;FLOAT3;0,0,0;False;78;FLOAT3;0,0,0;False;235;FLOAT;0;False;236;FLOAT;0;False;241;FLOAT;0;False;151;FLOAT;0;False;155;FLOAT;0;False;156;FLOAT;0;False;19;FLOAT4;0,0,0,0;False;20;FLOAT4;0,0,0,0;False;23;FLOAT4;0,0,0,0;False;24;FLOAT4;0,0,0,0;False;103;FLOAT4;0,0,0,0;False;21;FLOAT4;0,0,0,0;False;22;FLOAT4;0,0,0,0;False;25;FLOAT4;0,0,0,0;False;26;FLOAT4;0,0,0,0;False;104;FLOAT4;0,0,0,0;False;106;FLOAT4;0,0,0,0;False;210;FLOAT4;0,0,0,0;False;4;FLOAT4;0;FLOAT;1;FLOAT3;6;FLOAT;247
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;331;-2784,32;Inherit;False;displacementWrinkle;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;320;-2752,2592;Inherit;True;Property;_DisplacementMap;Displacement Map;14;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;316;-2448,2816;Inherit;False;331;displacementWrinkle;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;321;-2432,2640;Inherit;False;displacementMap;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;332;-2144,2720;Inherit;False;Property;ENUM_WRINKLE_MODE2;Use Wrinkle;32;0;Create;False;0;0;0;False;0;False;0;0;0;True;BOOLEAN_USE_WRINKLE_ON;KeywordEnum;3;NONE;WRINKLE;WRINKLE_DISPLACEMENT;Reference;313;True;False;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;317;-1776,2880;Inherit;False;Property;_DisplacementLevel;Displacement Level;15;0;Create;True;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;319;-1456,2768;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;323;-1776,2960;Inherit;False;Property;_DisplacementStrength;Displacement Strength;16;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;324;-1280,2816;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;325;-1328,3072;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;326;-1344,2624;Inherit;False;Constant;_Vector0;Vector 0;82;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;327;-1120,2976;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;380;-2432,80;Inherit;False;1076;576;;9;389;388;387;386;385;384;383;395;416;Cavity Map;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;342;-2384,1664;Inherit;False;1811.315;782.4224;;20;362;361;373;358;371;359;357;356;354;353;352;351;350;372;348;347;346;345;344;343;Subsurface;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;179;-1824,1264;Inherit;False;1257.896;320.3456;Comment;6;172;171;169;170;151;266;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;177;-1312,128;Inherit;False;822.292;467.8168;Comment;4;149;157;158;376;;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;330;-928,2800;Inherit;False;Property;ENUM_DISPLACEMENT_MODE;Use Displacement;13;0;Create;False;0;0;0;False;0;False;0;0;0;True;BOOLEAN_USE_WRINKLE_ON;KeywordEnum;4;NONE;BUMP;DISPLACEMENT;DISPACEMENT_BUMP;Create;True;False;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;231;-2785.533,-31.98799;Inherit;False;normalWrinkle;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;322;-944,2672;Inherit;False;finalDisplacement;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;343;-2336,1840;Inherit;False;261;mainSamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;384;-2368,176;Inherit;False;261;mainSamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;385;-2064,352;Inherit;False;Property;_CavityStrength;Cavity Strength;10;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;344;-2336,1920;Inherit;True;Property;_SSSThicknessPack;Subsurface Thickness Pack;21;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;345;-2336,2192;Inherit;True;Property;_SubsurfaceBlurMap;Subsurface Blur Map;20;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;229;-2784,-160;Inherit;False;diffuseWrinkle;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;387;-2096,128;Inherit;True;Property;_CavityMap;CavityMap;9;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;388;-1728,384;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;266;-1792,1408;Inherit;False;261;mainSamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;346;-2000,1840;Inherit;False;Property;_ThicknessScale;Thickness Max;23;0;Create;False;0;0;0;False;0;False;0.9;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;347;-2000,1904;Inherit;False;Property;_ThicknessScaleMin;Thickness Min;22;0;Create;False;0;0;0;False;0;False;0.4;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;348;-1760,2064;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;372;-1760,2064;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;350;-1808,2176;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;230;-2784,-96;Inherit;False;smoothnessWrinkle;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;389;-1536,144;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;151;-1520,1344;Inherit;True;Property;_OcclusionMap;Occlusion Map;7;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;351;-1392,2192;Inherit;False;200;baseColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;352;-2000,1776;Inherit;False;Property;_SSSTransmission;SSS Transmission;25;0;Create;True;0;0;0;False;0;False;50;0;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;353;-2000,1712;Inherit;False;Property;_SSSNormalDistortion;SSS Normal Distortion;26;0;Create;True;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;354;-2000,1968;Inherit;False;Property;_SSSBlurStrength;SSS Blur Strength;27;0;Create;True;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;356;-1616,1872;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;357;-1744,2192;Inherit;False;Property;_SubsurfaceFalloff1;Subsurface Falloff;24;0;Create;True;0;0;0;False;0;False;0.990566,0.3446065,0.2850213,0;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;359;-1456,2272;Inherit;False;369;finalTangentNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;371;-1408,1952;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;358;-1680,2112;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;222;-1616,1024;Inherit;False;230;smoothnessWrinkle;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;395;-1392,592;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;170;-1136,1456;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;169;-1216,1312;Inherit;False;Property;_OcclusionStrength;Occlusion Strength;29;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;373;-1136,1856;Inherit;False;RL5_Amplify_Function_Skin_SSS;-1;;110;3a15041bfe742e24d9003ae8b3e9eee0;0;9;99;FLOAT;0;False;118;FLOAT;0;False;120;FLOAT;0;False;121;FLOAT;0;False;122;FLOAT;0;False;123;COLOR;0,0,0,0;False;124;COLOR;0,0,0,0;False;125;COLOR;0,0,0,0;False;126;FLOAT3;0,0,0;False;2;COLOR;0;FLOAT3;140
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;376;-1296,224;Inherit;False;261;mainSamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;164;-1312,1088;Inherit;False;Property;_GlossMapScale;Gloss Map Scale;4;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;341;-1328,928;Inherit;False;Property;ENUM_WRINKLE_MODE3;Use Wrinkle;32;0;Create;False;0;0;0;False;0;False;0;0;0;True;BOOLEAN_USE_WRINKLE_ON;KeywordEnum;3;NONE;WRINKLE;WRINKLE_DISPLACEMENT;Reference;313;True;False;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;394;-1024,720;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;171;-912,1360;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;165;-1312,736;Inherit;False;Property;_Metallic;Metallic;3;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;224;-1600,832;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;361;-816,1904;Inherit;False;sssNormal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;362;-816,1824;Inherit;False;sssDiffuse;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;157;-1056,384;Inherit;False;Property;_EmissionColor;Emission Color;12;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;149;-1072,176;Inherit;True;Property;_EmissionMap;Emission Map;11;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;168;-944,928;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;398;-944,1072;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;167;-944,800;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;158;-704,288;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;172;-736,1360;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;397;-720,928;Inherit;False;Property;_UseCavity;Use Cavity;8;0;Create;True;0;0;0;False;0;False;1;False;Create;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;262;-2352,-480;Inherit;False;261;mainSamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;154;-2112,-640;Inherit;False;Property;_DetailNormalMapScale;Detail Normal Map Scale;31;0;Create;True;0;0;0;False;0;False;1;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;147;-2112,-544;Inherit;True;Property;_DetailMask;Detail Mask;28;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;375;-2160,-544;Inherit;False;1;0;SAMPLERSTATE;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;219;-1456,-800;Inherit;False;231;normalWrinkle;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;156;-1728,-672;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;337;-1744,-512;Inherit;False;322;finalDisplacement;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;338;-1744,-432;Inherit;False;Property;_BumpStrength;Bump Strength;17;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;374;-1696,-560;Inherit;False;1;0;SAMPLERSTATE;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.SamplerNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;148;-1456,-720;Inherit;True;Property;_DetailNormalMap;Detail Normal Map;30;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;False;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;339;-1424,-480;Inherit;False;Normal From Height;-1;;111;1942fe2c5f1a1f94881a33d532e4afeb;0;2;20;FLOAT;0;False;110;FLOAT;1;False;2;FLOAT3;40;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;314;-1232,-880;Inherit;False;Property;ENUM_WRINKLE_MODE1;Use Wrinkle;32;0;Create;False;0;0;0;False;0;False;0;0;0;True;BOOLEAN_USE_WRINKLE_ON;KeywordEnum;3;NONE;WRINKLE;WRINKLE_DISPLACEMENT;Reference;313;True;False;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BlendNormalsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;340;-1088,-528;Inherit;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BlendNormalsNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;155;-1088,-704;Inherit;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;336;-832,-656;Inherit;False;Property;ENUM_DISPLACEMENT_MODE1;Use Displacement;13;0;Create;False;0;0;0;False;0;False;0;0;0;True;BOOLEAN_USE_WRINKLE_ON;KeywordEnum;4;NONE;BUMP;DISPLACEMENT;DISPACEMENT_BUMP;Reference;330;True;False;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;369;-800,-496;Inherit;False;finalTangentNormal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;215;-1504,-1104;Inherit;False;229;diffuseWrinkle;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ColorNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;173;-1280,-1424;Inherit;False;Property;_Color;Color;1;0;Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;313;-1280,-1168;Inherit;False;Property;ENUM_WRINKLE_MODE;Wrinkle Mode;32;0;Create;False;0;0;0;False;0;False;0;0;0;True;BOOLEAN_USE_WRINKLE_ON;KeywordEnum;3;NONE;WRINKLE;WRINKLE_DISPLACEMENT;Create;True;False;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;174;-944,-1248;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;200;-704,-1200;Inherit;False;baseColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;363;-1024,0;Inherit;False;361;sssNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;364;-1024,-160;Inherit;False;362;sssDiffuse;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;365;-1024,-240;Inherit;False;200;baseColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;366;-1088,-80;Inherit;False;369;finalTangentNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;368;-784,-160;Inherit;False;Property;BOOLEAN_USE_SSS;Use SSS;19;0;Create;False;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;367;-768,-64;Inherit;False;Property;BOOLEAN_USE_SSS1;BOOLEAN_USE_SSS;19;0;Create;False;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Reference;368;True;True;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;410;-240,1168;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;409;-240,1168;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;408;-240,1168;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;411;-240,1168;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;412;-240,1168;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;413;-240,1168;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldNormalVector, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;414;-2400,336;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ViewDirInputsCoordNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;415;-2400,496;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.OneMinusNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;383;-2032,464;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;386;-1872,512;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CustomExpressionNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;416;-2192,432;Inherit;False;float NdotV = saturate(dot( N, V))@$float om = 1 - NdotV@$float oms = om * om@$return oms * oms@;1;Create;2;True;N;FLOAT3;0,0,0;In;;Inherit;False;True;V;FLOAT3;0,0,0;In;;Inherit;False;Faster Fresnel;True;False;0;;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;400;322.3807,257.9512;Float;False;False;-1;3;AmplifyShaderEditor.MaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ExtraPrePass;0;0;ExtraPrePass;6;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;3;False;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=ForwardBase;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;402;322.3807,257.9512;Float;False;False;-1;3;AmplifyShaderEditor.MaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ForwardAdd;0;2;ForwardAdd;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;3;False;0;False;True;4;1;False;;1;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;True;1;LightMode=ForwardAdd;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;403;322.3807,257.9512;Float;False;False;-1;3;AmplifyShaderEditor.MaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;Deferred;0;3;Deferred;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;3;False;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Deferred;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;404;322.3807,257.9512;Float;False;False;-1;3;AmplifyShaderEditor.MaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;Meta;0;4;Meta;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;3;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;405;322.3807,257.9512;Float;False;False;-1;3;AmplifyShaderEditor.MaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ShadowCaster;0;5;ShadowCaster;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;3;False;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=ShadowCaster;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;406;322.3807,257.9512;Float;False;False;-1;3;AmplifyShaderEditor.MaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;SceneSelectionPass;0;6;SceneSelectionPass;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;3;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;False;False;True;1;LightMode=SceneSelectionPass;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;407;322.3807,257.9512;Float;False;False;-1;3;AmplifyShaderEditor.MaterialInspector;0;1;New Amplify Shader;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ScenePickingPass;0;7;ScenePickingPass;0;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;3;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;False;False;True;1;LightMode=ScenePickingPass;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode, AmplifyShaderEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;401;32,1056;Float;False;True;-1;3;AmplifyShaderEditor.MaterialInspector;0;4;Reallusion/Amplify/RL5_SkinShader_Baked_3D;ed95fe726fd7b4644bb42f4d1ddd2bcd;True;ForwardBase;0;1;ForwardBase;17;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;False;True;3;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;DisableBatching=False=DisableBatching;True;3;False;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;False;0;Shader Graphs/RL5_SkinShader_Baked_3D;0;0;Standard;44;Category;0;0;  Instanced Terrain Normals;1;0;Workflow;1;0;Surface;0;0;  Blend;0;0;  Dither Shadows;1;0;Two Sided;1;0;Alpha Clipping;0;0;  Use Shadow Threshold;0;0;Deferred Pass;1;0;Normal Space;0;0;Transmission;0;0;  Transmission Shadow;0.5,False,;0;Translucency;0;0;  Translucency Strength;1,False,;0;  Normal Distortion;0.5,False,;0;  Scattering;2,False,;0;  Direct;0.9,False,;0;  Ambient;0.1,False,;0;  Shadow;0.5,False,;0;Cast Shadows;1;0;Receive Shadows;1;0;Receive Specular;2;0;Receive Reflections;2;0;GPU Instancing;1;0;LOD CrossFade;1;0;Built-in Fog;1;0;Ambient Light;1;0;Meta Pass;1;0;Add Pass;1;0;Override Baked GI;0;0;Write Depth;0;0;Extra Pre Pass;0;0;Tessellation;0;0;  Phong;0;0;  Strength;0.5,False,;0;  Type;0;0;  Tess;16,False,;0;  Min;10,False,;0;  Max;25,False,;0;  Edge Length;16,False,;0;  Max Displacement;25,False,;0;Disable Batching;0;0;Vertex Position;1;0;0;8;False;True;True;True;True;True;True;True;False;;True;0
WireConnection;258;0;153;0
WireConnection;150;7;265;0
WireConnection;146;5;258;0
WireConnection;146;7;335;0
WireConnection;378;0;377;0
WireConnection;378;7;379;0
WireConnection;221;0;150;4
WireConnection;272;7;251;0
WireConnection;275;7;251;0
WireConnection;271;7;251;0
WireConnection;270;7;251;0
WireConnection;269;7;251;0
WireConnection;268;7;251;0
WireConnection;274;7;251;0
WireConnection;273;7;251;0
WireConnection;252;7;251;0
WireConnection;276;5;232;0
WireConnection;259;7;251;0
WireConnection;277;5;232;0
WireConnection;278;5;232;0
WireConnection;216;0;378;0
WireConnection;218;0;146;0
WireConnection;287;0;275;0
WireConnection;286;0;274;0
WireConnection;285;0;273;0
WireConnection;280;0;268;0
WireConnection;281;0;269;0
WireConnection;283;0;270;0
WireConnection;282;0;271;0
WireConnection;284;0;272;0
WireConnection;290;0;252;3
WireConnection;289;0;252;2
WireConnection;288;0;252;1
WireConnection;293;0;278;0
WireConnection;292;0;277;0
WireConnection;291;0;276;0
WireConnection;296;0;279;3
WireConnection;295;0;279;2
WireConnection;294;0;279;1
WireConnection;297;0;259;1
WireConnection;298;0;259;2
WireConnection;299;0;259;3
WireConnection;310;0;257;0
WireConnection;311;0;260;0
WireConnection;305;0;239;0
WireConnection;306;0;240;0
WireConnection;307;0;235;0
WireConnection;308;0;236;0
WireConnection;309;0;255;0
WireConnection;300;0;237;0
WireConnection;301;0;238;0
WireConnection;302;0;233;0
WireConnection;303;0;234;0
WireConnection;304;0;256;0
WireConnection;261;0;251;0
WireConnection;253;7;226;0
WireConnection;253;8;228;0
WireConnection;253;9;227;0
WireConnection;253;228;334;0
WireConnection;253;233;312;0
WireConnection;253;15;280;0
WireConnection;253;16;281;0
WireConnection;253;17;283;0
WireConnection;253;18;282;0
WireConnection;253;105;284;0
WireConnection;253;67;285;0
WireConnection;253;71;286;0
WireConnection;253;76;287;0
WireConnection;253;68;288;0
WireConnection;253;72;289;0
WireConnection;253;77;290;0
WireConnection;253;69;291;0
WireConnection;253;73;292;0
WireConnection;253;78;293;0
WireConnection;253;235;294;0
WireConnection;253;236;295;0
WireConnection;253;241;296;0
WireConnection;253;151;297;0
WireConnection;253;155;298;0
WireConnection;253;156;299;0
WireConnection;253;19;300;0
WireConnection;253;20;301;0
WireConnection;253;23;302;0
WireConnection;253;24;303;0
WireConnection;253;103;304;0
WireConnection;253;21;305;0
WireConnection;253;22;306;0
WireConnection;253;25;307;0
WireConnection;253;26;308;0
WireConnection;253;104;309;0
WireConnection;253;106;310;0
WireConnection;253;210;311;0
WireConnection;331;0;253;247
WireConnection;320;7;318;0
WireConnection;321;0;320;1
WireConnection;332;1;321;0
WireConnection;332;0;321;0
WireConnection;332;2;316;0
WireConnection;319;0;332;0
WireConnection;319;1;317;0
WireConnection;324;0;319;0
WireConnection;324;1;323;0
WireConnection;327;0;324;0
WireConnection;327;1;325;0
WireConnection;330;1;326;0
WireConnection;330;0;326;0
WireConnection;330;2;327;0
WireConnection;330;3;327;0
WireConnection;231;0;253;6
WireConnection;322;0;319;0
WireConnection;344;7;343;0
WireConnection;345;7;343;0
WireConnection;229;0;253;0
WireConnection;387;7;384;0
WireConnection;388;0;385;0
WireConnection;388;1;386;0
WireConnection;348;0;344;4
WireConnection;372;0;344;1
WireConnection;350;0;345;0
WireConnection;230;0;253;1
WireConnection;389;1;387;1
WireConnection;389;2;388;0
WireConnection;151;7;266;0
WireConnection;356;0;346;0
WireConnection;356;1;347;0
WireConnection;356;2;348;0
WireConnection;371;0;372;0
WireConnection;358;0;350;0
WireConnection;395;0;389;0
WireConnection;170;0;151;2
WireConnection;373;99;353;0
WireConnection;373;118;352;0
WireConnection;373;120;356;0
WireConnection;373;121;371;0
WireConnection;373;122;354;0
WireConnection;373;123;358;0
WireConnection;373;124;357;0
WireConnection;373;125;351;0
WireConnection;373;126;359;0
WireConnection;341;1;221;0
WireConnection;341;0;222;0
WireConnection;341;2;222;0
WireConnection;394;0;395;0
WireConnection;171;0;169;0
WireConnection;171;1;170;0
WireConnection;224;0;150;2
WireConnection;361;0;373;140
WireConnection;362;0;373;0
WireConnection;149;7;376;0
WireConnection;168;0;341;0
WireConnection;168;1;164;0
WireConnection;398;0;168;0
WireConnection;398;1;394;0
WireConnection;167;0;165;0
WireConnection;167;1;224;0
WireConnection;158;0;149;0
WireConnection;158;1;157;0
WireConnection;172;0;171;0
WireConnection;397;0;168;0
WireConnection;397;1;398;0
WireConnection;147;7;262;0
WireConnection;375;0;262;0
WireConnection;156;0;154;0
WireConnection;156;1;147;2
WireConnection;374;0;375;0
WireConnection;148;5;156;0
WireConnection;148;7;374;0
WireConnection;339;20;337;0
WireConnection;339;110;338;0
WireConnection;314;1;218;0
WireConnection;314;0;219;0
WireConnection;314;2;219;0
WireConnection;340;0;155;0
WireConnection;340;1;339;40
WireConnection;155;0;314;0
WireConnection;155;1;148;0
WireConnection;336;1;155;0
WireConnection;336;0;340;0
WireConnection;336;2;155;0
WireConnection;336;3;340;0
WireConnection;369;0;336;0
WireConnection;313;1;216;0
WireConnection;313;0;215;0
WireConnection;313;2;215;0
WireConnection;174;0;173;0
WireConnection;174;1;313;0
WireConnection;200;0;174;0
WireConnection;368;1;365;0
WireConnection;368;0;364;0
WireConnection;367;1;366;0
WireConnection;367;0;363;0
WireConnection;410;0;158;0
WireConnection;409;0;367;0
WireConnection;408;0;368;0
WireConnection;411;0;397;0
WireConnection;412;0;172;0
WireConnection;413;0;330;0
WireConnection;383;0;416;0
WireConnection;386;0;383;0
WireConnection;416;0;414;0
WireConnection;416;1;415;0
WireConnection;401;0;408;0
WireConnection;401;1;409;0
WireConnection;401;5;411;0
WireConnection;401;6;412;0
WireConnection;401;2;410;0
WireConnection;401;15;413;0
ASEEND*/
//CHKSM=B4F1DCECC6B36A6D829C8F91CD029874E4EBA74B