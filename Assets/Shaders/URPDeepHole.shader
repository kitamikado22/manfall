Shader "Custom/URPDeepHole"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags {
			"RenderType" = "Opaque"
			"RenderPipeline" = "UniversalPipeline"
			"Queue" = "Background"
		}
        LOD 100

        Pass
        {
			Name "DeepHole"

			Cull Front

			Stencil {
				Ref 2
				Comp NotEqual
				Pass Keep
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

			half4 _BaseColor : COLOR;
			half4 frag (Varyings IN) : SV_Target
			{
				return _BaseColor;
			}
			ENDHLSL
        }
    }
}
