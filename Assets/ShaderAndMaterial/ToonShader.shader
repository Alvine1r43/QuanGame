Shader "Unlit/ToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_DisplacementTex("Displacement Texture", 2D) = "white" {}
		_Magnitude("Magnitude", Range(0, 1)) = 0
		_Color("Color",Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags
		{
			"Queue" = "Transparent"
		}
        LOD 100

        Pass
        {	
			Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
            };

            sampler2D _MainTex;
			sampler2D _DisplacementTex;
			float _Magnitude;	
            float4 _MainTex_ST;
			float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);

				o.normal = v.normal;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 disp = tex2D(_DisplacementTex, i.uv).xy;
				disp = ((disp * 2) - 1) * _Magnitude;
				float4 color = tex2D(_MainTex, i.uv + disp);
				return color;
            }
            ENDCG
        }
    }
}
