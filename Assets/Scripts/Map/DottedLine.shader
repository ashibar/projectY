Shader "Custom/DottedLine" {
    Properties {
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert(appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : SV_Target {
                float2 texCoord = i.vertex.xy * 0.5 + 0.5;
                half4 col = tex2D(_MainTex, texCoord);
                half threshold = 0.5;
                if (col.a < threshold) discard;
                return col;
            }
            ENDCG
        }
    }
}
