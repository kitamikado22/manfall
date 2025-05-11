Shader "Custom/URPHoleMask"
{
    SubShader
    {
		Tags {
			"RenderType" = "Opaque"
			"RenderPipeline" = "UniversalPipeline"
			"Queue" = "Geometry-1"
		}
        LOD 100

        Pass
        {
			Name "HoleMask"

			ZWrite Off
			ColorMask 0	// êFÇÃï`âÊÇ»Çµ
			Stencil {
				Ref 1
				Comp Always
				Pass replace
			}

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			struct Attributes {
				float4 positionOS : POSITION;
			};
			struct Varyings {
				float4 positionHCS : SV_POSITION;
			};

			Varyings vert (Attributes IN)
			{
				Varyings OUT;
				OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
				return OUT;
			}

			half4 frag (Varyings IN) : SV_Target
			{
				return 0;
			}
			ENDHLSL
        }
    }
}
