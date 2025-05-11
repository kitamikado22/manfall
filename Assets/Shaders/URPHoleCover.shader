Shader "Custom/URPHoleCover"
{
    SubShader
    {
        Tags {
			"RenderType" = "Opaque"
			"RenderPipeline" = "UniversalPipeline"
			"Queue" = "Background-1"
		}
        LOD 100

        Pass
        {
			Name "HoleCover"

			Cull Back
			ColorMask 0	// êFÇÃï`âÊÇ»Çµ
			ZWrite Off

			Stencil {
				Ref 2
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
