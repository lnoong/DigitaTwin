// Upgrade NOTE: upgraded instancing buffer 'FactoryConveyor' to new syntax.

// Made with Amplify Shader Editor v1.9.1
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Factory/Conveyor"
{
	Properties
	{
		_Speed("Speed", Range( -2 , 2)) = 1
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Metallic("Metallic", 2D) = "black" {}
		_Smoothness("Smoothness", 2D) = "gray" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform sampler2D _Albedo;
		uniform sampler2D _Metallic;
		uniform sampler2D _Smoothness;

		UNITY_INSTANCING_BUFFER_START(FactoryConveyor)
			UNITY_DEFINE_INSTANCED_PROP(float, _Speed)
#define _Speed_arr FactoryConveyor
		UNITY_INSTANCING_BUFFER_END(FactoryConveyor)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float _Speed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Speed_arr, _Speed);
			float temp_output_38_0 =  ( _Speed_Instance - 0.0 > 0.0 ? 1.0 : _Speed_Instance - 0.0 <= 0.0 && _Speed_Instance + 0.0 >= 0.0 ? 1.0 : -1.0 ) ;
			float4 appendResult43 = (float4(0.0 , temp_output_38_0 , 0.0 , 0.0));
			float2 break33 = i.uv_texcoord;
			float4 appendResult34 = (float4(break33.x , ( break33.y * temp_output_38_0 ) , 0.0 , 0.0));
			float2 panner2 = ( _Time.y * ( _Speed_Instance * appendResult43 ).xy + appendResult34.xy);
			o.Normal = UnpackNormal( tex2D( _Normal, panner2 ) );
			o.Albedo = tex2D( _Albedo, panner2 ).rgb;
			o.Metallic = tex2D( _Metallic, panner2 ).r;
			o.Smoothness = tex2D( _Smoothness, panner2 ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19100
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1435.314,284.1031;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleTimeNode;8;-1593.274,100.6337;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;2;-1370.512,52.75456;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;9;-982.3573,332.8484;Inherit;True;Property;_Metallic;Metallic;3;0;Create;True;0;0;0;False;0;False;-1;None;87a16d42f472ec9458b25964ba130549;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-987.8161,108.3206;Inherit;True;Property;_Normal;Normal;2;0;Create;True;0;0;0;False;0;False;-1;None;2591dc394c7fd844b8248f331eb922d6;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-985.6453,-132.6507;Inherit;True;Property;_Albedo;Albedo;1;0;Create;True;0;0;0;False;0;False;-1;None;95571bc7685bbbd4c9b3421bc1c41271;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-978.9114,536.1547;Inherit;True;Property;_Smoothness;Smoothness;4;0;Create;True;0;0;0;False;0;False;-1;None;34ea401025b97b44cba66b7bfdbf1b13;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;34;-1361.156,-155.598;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.BreakToComponentsNode;33;-1579.156,-160.598;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-1513.465,-42.72397;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-2188.263,-160.424;Inherit;False;Constant;_Float1;Float 0;5;0;Create;True;0;0;0;False;0;False;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-2192.566,-227.924;Inherit;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-2196.601,-311.7614;Inherit;False;Constant;_Float2;Float 0;5;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;43;-1651.897,323.9386;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TFHCIf;38;-1923.165,-254.9241;Inherit;False;6;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-2303.215,162.7517;Float;False;InstancedProperty;_Speed;Speed;0;0;Create;True;0;0;0;False;0;False;1;0.5;-2;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;35;-1924.276,353.1133;Inherit;False;Constant;_Vector0;Vector 0;6;0;Create;True;0;0;0;False;0;False;0,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-1908.188,-1.767304;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;63;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Factory/Conveyor;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;3;0
WireConnection;7;1;43;0
WireConnection;2;0;34;0
WireConnection;2;2;7;0
WireConnection;2;1;8;0
WireConnection;9;1;2;0
WireConnection;5;1;2;0
WireConnection;4;1;2;0
WireConnection;10;1;2;0
WireConnection;34;0;33;0
WireConnection;34;1;36;0
WireConnection;33;0;1;0
WireConnection;36;0;33;1
WireConnection;36;1;38;0
WireConnection;43;0;42;0
WireConnection;43;1;38;0
WireConnection;38;0;3;0
WireConnection;38;1;42;0
WireConnection;38;2;39;0
WireConnection;38;3;39;0
WireConnection;38;4;40;0
WireConnection;63;0;4;0
WireConnection;63;1;5;0
WireConnection;63;3;9;0
WireConnection;63;4;10;0
ASEEND*/
//CHKSM=782DEFE1409FDA99C00DE154C274287828376128