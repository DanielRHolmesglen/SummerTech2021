// Shader created with Shader Forge v1.42 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Enhanced by Antoine Guillon / Arkham Development - http://www.arkham-development.com/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.42;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:0,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3541,x:32719,y:32712,varname:node_3541,prsc:2|emission-1245-RGB,olwid-1653-OUT,olcol-2153-OUT;n:type:ShaderForge.SFN_Color,id:8459,x:31982,y:32486,ptovrint:False,ptlb:Colour_01,ptin:_Colour_01,varname:node_8459,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.03346387,c2:0.06794743,c3:0.1509434,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7671,x:31982,y:32809,ptovrint:False,ptlb:node_7671,ptin:_node_7671,varname:node_7671,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8122be27b3ce90b46ba74b481d58b20c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:6648,x:31982,y:32648,ptovrint:False,ptlb:Colour_02,ptin:_Colour_02,varname:node_6648,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4179868,c2:0.5972911,c3:0.8773585,c4:1;n:type:ShaderForge.SFN_Lerp,id:2153,x:32546,y:32893,varname:node_2153,prsc:2|A-8459-RGB,B-6648-RGB,T-678-OUT;n:type:ShaderForge.SFN_Posterize,id:2437,x:32174,y:32839,varname:node_2437,prsc:2|IN-7671-RGB,STPS-2677-OUT;n:type:ShaderForge.SFN_Vector1,id:2677,x:31982,y:32978,varname:node_2677,prsc:2,v1:4;n:type:ShaderForge.SFN_Vector1,id:7486,x:31641,y:33351,varname:node_7486,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Time,id:9368,x:31641,y:33231,varname:node_9368,prsc:2;n:type:ShaderForge.SFN_Multiply,id:339,x:31831,y:33247,varname:node_339,prsc:2|A-9368-T,B-7486-OUT;n:type:ShaderForge.SFN_TexCoord,id:6768,x:31831,y:33105,varname:node_6768,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:435,x:32009,y:33159,varname:node_435,prsc:2,spu:1,spv:1|UVIN-6768-UVOUT,DIST-339-OUT;n:type:ShaderForge.SFN_Panner,id:1420,x:32009,y:33297,varname:node_1420,prsc:2,spu:1,spv:1|UVIN-6768-UVOUT,DIST-339-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:2123,x:32009,y:33484,ptovrint:False,ptlb:Cloud,ptin:_Cloud,varname:node_2123,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8122be27b3ce90b46ba74b481d58b20c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2923,x:32191,y:33319,varname:node_2923,prsc:2,tex:8122be27b3ce90b46ba74b481d58b20c,ntxv:0,isnm:False|UVIN-1420-UVOUT,TEX-2123-TEX;n:type:ShaderForge.SFN_Tex2d,id:835,x:32191,y:33127,varname:node_835,prsc:2,tex:8122be27b3ce90b46ba74b481d58b20c,ntxv:0,isnm:False|UVIN-435-UVOUT,TEX-2123-TEX;n:type:ShaderForge.SFN_Multiply,id:678,x:32415,y:33214,varname:node_678,prsc:2|A-835-RGB,B-2923-RGB;n:type:ShaderForge.SFN_Color,id:1165,x:32393,y:32524,ptovrint:False,ptlb:Base_Colour,ptin:_Base_Colour,varname:node_1165,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_LightVector,id:2069,x:31000,y:32578,varname:node_2069,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:1303,x:31000,y:32730,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:6924,x:31320,y:32696,varname:node_6924,prsc:2,dt:0|A-2069-OUT,B-1303-OUT;n:type:ShaderForge.SFN_Step,id:9806,x:31607,y:32682,varname:node_9806,prsc:2|A-9569-OUT,B-6924-OUT;n:type:ShaderForge.SFN_Vector1,id:9569,x:31567,y:32830,varname:node_9569,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:1245,x:31741,y:32927,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_1245,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e217bae0ed7fa9e42aa7c72a12fe89a8,ntxv:0,isnm:False|UVIN-9058-OUT;n:type:ShaderForge.SFN_Append,id:9058,x:31457,y:32869,varname:node_9058,prsc:2|A-6924-OUT,B-4652-OUT;n:type:ShaderForge.SFN_Vector1,id:4652,x:31280,y:32967,varname:node_4652,prsc:2,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:1653,x:32546,y:33053,ptovrint:False,ptlb:OutlineWidth,ptin:_OutlineWidth,varname:node_1653,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;proporder:8459-7671-6648-2123-1165-1245-1653;pass:END;sub:END;*/

Shader "Unlit/Outline" {
    Properties {
        _Colour_01 ("Colour_01", Color) = (0.03346387,0.06794743,0.1509434,1)
        _node_7671 ("node_7671", 2D) = "white" {}
        _Colour_02 ("Colour_02", Color) = (0.4179868,0.5972911,0.8773585,1)
        _Cloud ("Cloud", 2D) = "white" {}
        _Base_Colour ("Base_Colour", Color) = (0.5,0.5,0.5,1)
        _Ramp ("Ramp", 2D) = "white" {}
        _OutlineWidth ("OutlineWidth", Float ) = 0.05
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles vulkan 
            #pragma target 3.0
            uniform float4 _Colour_01;
            uniform float4 _Colour_02;
            uniform sampler2D _Cloud; uniform float4 _Cloud_ST;
            uniform float _OutlineWidth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_OutlineWidth,1) );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_9368 = _Time;
                float node_339 = (node_9368.g*0.1);
                float2 node_435 = (i.uv0+node_339*float2(1,1));
                float4 node_835 = tex2D(_Cloud,TRANSFORM_TEX(node_435, _Cloud));
                float2 node_1420 = (i.uv0+node_339*float2(1,1));
                float4 node_2923 = tex2D(_Cloud,TRANSFORM_TEX(node_1420, _Cloud));
                return fixed4(lerp(_Colour_01.rgb,_Colour_02.rgb,(node_835.rgb*node_2923.rgb)),0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #ifndef UNITY_PASS_FORWARDBASE
            #define UNITY_PASS_FORWARDBASE
            #endif //UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles vulkan 
            #pragma target 3.0
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                UNITY_LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                UNITY_TRANSFER_LIGHTING(o, float2(0,0));
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float node_6924 = dot(lightDirection,i.normalDir);
                float2 node_9058 = float2(node_6924,0.0);
                float4 _Ramp_var = tex2D(_Ramp,TRANSFORM_TEX(node_9058, _Ramp));
                float3 emissive = _Ramp_var.rgb;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
