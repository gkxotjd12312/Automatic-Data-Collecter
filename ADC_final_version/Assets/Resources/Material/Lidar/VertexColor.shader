// Upgrade NOTE: 'mul(UNITY_MATRIX_MVP,*)' ��� 'UnityObjectToClipPos(*)'�� ��ü�Ǿ����ϴ�.
Shader "Custom/VertexColor" {
	Properties{
		_MinColor("Min_Color", Color) = (0, 1, 0, 1) // �ּ� ���� �Ӽ� ����
		_MaxColor("Max_Color ����", Color) = (1, 0, 0, 1) // �ִ� ���� �Ӽ� ����
		_MaxDistance("Max_Distance", Float) = 1100 // �ִ� �Ÿ� �Ӽ� ����
	}
	SubShader{
		Pass
		{		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc" // UnityCG.cginc ����

			struct appdata {
				float4 vertex : POSITION; // ���� ��ġ
				float4 color: COLOR; // ���� ����				
			};

			float _MaxDistance;
			float4 _MinColor;
			float4 _MaxColor;

			struct v2f {
				float4 vertex : SV_POSITION; // ��ũ�� ���� ���� ��ġ
				float4 col : COLOR; // ���� ����
			};

			v2f vert(appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex); // ���� ��ġ�� ��ũ�� �������� ��ȯ
				o.col = v.color; // ���� ���� ����

				return o;
			}

			float4 frag(v2f o) : COLOR{
				float mag = abs(length(o.vertex)) / (_MaxDistance); // ���������� �Ÿ� ���
				float4 color = lerp(_MinColor, _MaxColor, mag); // �ּ� ����� �ִ� ���� ���̿��� �����Ͽ� ���� ���
				//float4 color = float4(o.vertex.x/mag, o.vertex.y/mag, 0, 1); // ���� ��ġ�� ����� ���� ��� (�ּ� ó���� �κ�)
				return color; // ���� ���� ��ȯ
			}

				ENDCG
			}
	}
}

