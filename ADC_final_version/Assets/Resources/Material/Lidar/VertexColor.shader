// Upgrade NOTE: 'mul(UNITY_MATRIX_MVP,*)' 대신 'UnityObjectToClipPos(*)'로 대체되었습니다.
Shader "Custom/VertexColor" {
	Properties{
		_MinColor("Min_Color", Color) = (0, 1, 0, 1) // 최소 색상 속성 정의
		_MaxColor("Max_Color 색상", Color) = (1, 0, 0, 1) // 최대 색상 속성 정의
		_MaxDistance("Max_Distance", Float) = 1100 // 최대 거리 속성 정의
	}
	SubShader{
		Pass
		{		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc" // UnityCG.cginc 포함

			struct appdata {
				float4 vertex : POSITION; // 정점 위치
				float4 color: COLOR; // 정점 색상				
			};

			float _MaxDistance;
			float4 _MinColor;
			float4 _MaxColor;

			struct v2f {
				float4 vertex : SV_POSITION; // 스크린 공간 정점 위치
				float4 col : COLOR; // 정점 색상
			};

			v2f vert(appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex); // 정점 위치를 스크린 공간으로 변환
				o.col = v.color; // 정점 색상 설정

				return o;
			}

			float4 frag(v2f o) : COLOR{
				float mag = abs(length(o.vertex)) / (_MaxDistance); // 정점까지의 거리 계산
				float4 color = lerp(_MinColor, _MaxColor, mag); // 최소 색상과 최대 색상 사이에서 보간하여 색상 계산
				//float4 color = float4(o.vertex.x/mag, o.vertex.y/mag, 0, 1); // 정점 위치에 기반한 색상 계산 (주석 처리된 부분)
				return color; // 계산된 색상 반환
			}

				ENDCG
			}
	}
}

