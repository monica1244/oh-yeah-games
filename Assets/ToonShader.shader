Shader "Unlit/ToonShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", COLOR) = (1, 1, 1, 1)
        [HDR]
        _AmbientColor("Ambient Color", COLOR) = (0.1, 0.1, 0.1, 1)
        _SpecularColor("Specular Color", COLOR) = (0.9, 0.9, 0.9, 1)
        _Shine("Shine", FLOAT) = 400
    }
    SubShader
    {
        Tags { 
			"LightMode" = "ForwardBase"
			"PassFlags" = "OnlyDirectional"
        }
        LOD 100

        Pass
        {
            Tags { 	"LightMode" = "ForwardBase"
	"PassFlags" = "OnlyDirectional" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;

            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float3 worldViewDir : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
                SHADOW_COORDS(3)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _AmbientColor;
            float4 _SpecularColor;
            float4 _Shine;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldViewDir = WorldSpaceViewDir(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                TRANSFER_SHADOW(o);
                return o;
            }

            float cellShade(float3 lightDir, float3 normal) {
                //float3 lightAngle = max(0.0, dot(normalize(lightDir), normalize(normal)));
                float3 lightAngle = dot(normalize(lightDir), normal);
                return smoothstep(0, 0.01, lightAngle);
            }

            fixed4 frag (v2f i) : SV_Target
            {                
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float normal = normalize(i.worldNormal);

                // calculate light angle
                float cellshade = cellShade(_WorldSpaceLightPos0.xyz, normal);

                // calculate specular reflection
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 reflecDir = reflect(-viewDir, normal);
                float specReflection = max(0, dot(_WorldSpaceLightPos0.xyz, reflecDir));
                specReflection = pow(specReflection, _Shine);
                specReflection = _SpecularColor * smoothstep(0.005, 0.1, specReflection);

                return col * ((cellshade * _LightColor0 * SHADOW_ATTENUATION(i)) + _AmbientColor + specReflection) * _Color;
            }
            ENDCG
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
