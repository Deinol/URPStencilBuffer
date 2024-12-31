Shader "Deinol/StencilBuffer/StencilBufferMask"
{
	Properties
	{
		[IntRange] _StencilID ("Stencil ID", Range(0,255)) = 1
		[KeywordEnum(Greater, GEqual, Less, LEqual, Equal, NotEqual, Always, Never)] _CompMode ("Comp mode", int) = 6
		[KeywordEnum(Keep, Zero, Replace, IncrSat, DecrSat, Invert, IncrWrap, DecrWrap)] _Action ("Pass action", int) = 2
	}

	SubShader
	{
		Tags {"RenderType"="Opaque" "Queue"="Geometry-1" "RenderPipeline"="UniversalPipeline"}
		Pass
		{
			Blend Zero One
			Zwrite Off

			Stencil
			{
				Ref [_StencilID]
				Comp [_CompMode]
				Pass [_Action]
			}
		}
	}
}
