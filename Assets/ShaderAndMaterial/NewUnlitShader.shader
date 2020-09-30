Shader "TA/MyUnlitShader"
{
    Properties
    {
        _Color("I am Color", Color) = (1,1,1,1)
    }
    SubShader
    {	Tags
		{
			"PreviewType" = "Plane" //预览的时候用面板预览
		}
        Pass
        { 
            CGPROGRAM
            #pragma vertex vert 
            #pragma fragment frag 

			half4 _Color;

			struct appdata{
				float4 vertex:POSITION;
				float2 uv:TEXCOORD0;
			};

			struct v2f{
				float4 pos:SV_POSITION;
				float2 uv:TEXCOORD0;
			};

			v2f vert(appdata v){
			  v2f o;
			  o.pos = UnityObjectToClipPos(v.vertex);
			  o.uv = v.uv;
			  return o;
			}

			fixed checker(float2 uv){
				float2 repeatUV=uv*10;
				float2 c = floor(repeatUV)/2;
				float checker = frac( c.x+c.y )*2;
				return checker;
			
			}
            float4 frag(v2f i):SV_TARGET
            {	
				fixed col = checker(i.uv);
				return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "EditorName"
}
