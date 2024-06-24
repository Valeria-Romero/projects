Shader "Unlit/SimpleInkPaint"
{
	Properties{
		_MainTex("MainTex", 2D) = "white" // メインテクスチャ
		_Brush("Brush", 2D) = "white" // ブラシテクスチャ
		_BrushScale("BrushScale", FLOAT) = 0.1 // ブラシサイズ
		_ControlColor("ControlColor", VECTOR) = (0, 0, 0, 0) // ブラシの色
		_PaintUV("Hit UV Position", VECTOR) = (0, 0, 0, 0) // ブラシで塗りたい位置
	}

	SubShader{
			CGINCLUDE
				struct app_data {
					float4 vertex:POSITION;
					float4 uv:TEXCOORD0;
				};

				struct v2f {
					float4 screen:SV_POSITION;
					float4 uv:TEXCOORD0;
				};

				sampler2D _MainTex;
				sampler2D _Brush;
				float4 _PaintUV;
				float _BrushScale;
				float4 _ControlColor;
			ENDCG

			Pass{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				bool IsPaintRange(float2 mainUV, float2 paintUV, float brushScale) {
					return
						paintUV.x - brushScale < mainUV.x &&
						mainUV.x < paintUV.x + brushScale &&
						paintUV.y - brushScale < mainUV.y &&
						mainUV.y < paintUV.y + brushScale;
				}

				v2f vert(app_data i) {
					v2f o;
					o.screen = UnityObjectToClipPos(i.vertex);
					o.uv = i.uv;
					return o;
				}

				float4 frag(v2f i) : SV_TARGET{
					float h = _BrushScale;
					float4 mainColor = tex2D(_MainTex, i.uv.xy);
					float4 brushColor = float4(1, 1, 1, 1);
          
          				// ブラシ範囲に入っているかチェック
					if (IsPaintRange(i.uv, _PaintUV, h)) {
              						// 入力されたuv値をtextureに合わせて変換
							float2 uv = (i.uv - _PaintUV) / h * 0.5 + 0.5;
							brushColor = tex2D(_Brush, uv.xy);
              						// ブラシの形に沿って、ブラシ色にする
							return mainColor * (1 - brushColor.a * _ControlColor.a) + _ControlColor * _ControlColor.a * brushColor.a;
					}
					return mainColor;
				}
				ENDCG
			}
		}
}
