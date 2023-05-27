Shader "Custom/SpriteShadow" {
    Properties {
        _ShadowColor ("Shadow Color", Color) = (0, 0, 0, 0.5)
        _ShadowOffset ("Shadow Offset", Vector) = (0, -0.1, 0, 0)
    }
    SubShader {
        Tags { "Queue"="Transparent" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ShadowColor;
            float4 _ShadowOffset;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 shadow = tex2D(_MainTex, i.uv + _ShadowOffset.xy);
                return col * shadow * _ShadowColor;
            }
            ENDCG
        }
    }
}
