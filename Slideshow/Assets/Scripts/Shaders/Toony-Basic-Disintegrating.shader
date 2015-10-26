Shader "Toon/Basic-Disintegrating" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		_DisintMult ("Disintegration Progress", Range(-1.1,1)) = 1
		_DisintRamp ("Disintegration Ramp", Range(0,1)) = 0.5
		_DisintColor ("Disintegration Color", Color) = (1,0.353,0,1)
	}


	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			Name "BASE"
			Cull Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			samplerCUBE _ToonShade;
			float4 _MainTex_ST;
			float4 _Color;
			float _DisintMult;
			float _DisintRamp;
			float4 _DisintColor;

			struct appdata {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			struct v2f {
				float4 pos : POSITION;
				float2 texcoord : TEXCOORD0;
				float3 cubenormal : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0));
				return o;
			}

			float4 frag (v2f i) : COLOR
			{
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
			
				//************ DISINTEGRATION CODE ************
				float rand = 0;
				rand = sin(dot(col.xyz / 2 ,float3(15.9845,61.968,16.5628) ));
				float colMult = 1;
				if(rand > _DisintMult)
				{
					discard;
				}
				else if(_DisintMult < 1 && _DisintMult - rand < _DisintRamp)
				{
					col.rgb = lerp(col.rgb, _DisintColor, 1 -(_DisintMult - rand) / _DisintRamp);
				}
				//************ END DISINTEGRATION CODE ************
				
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
				return float4(2.0f * cube.rgb * colMult * col.rgb, col.a);
			}
			ENDCG			
		}
	} 

	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			Name "BASE"
			Cull Off
			SetTexture [_MainTex] {
				constantColor [_Color]
				Combine texture * constant
			} 
			SetTexture [_ToonShade] {
				combine texture * previous DOUBLE, previous
			}
		}
	} 
	
	Fallback "VertexLit"
}
