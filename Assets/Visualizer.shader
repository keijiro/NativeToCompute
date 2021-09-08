Shader "Visualizer"
{
    SubShader
    {
        Pass
        {
            Cull Off ZTest Always

            CGPROGRAM

            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "UnityCG.cginc"

            StructuredBuffer<float> _Data;

            void Vertex(uint vid : SV_VertexID,
                        out float4 position : SV_Position,
                        out float2 uv : TEXCOORD0)
            {
                float x = vid & 1;
                float y = (vid == 2) || (vid > 3);
                position = float4(x * 2 - 1, y * 2 - 1, 0, 1);
                uv = x;
            }

            float4 Fragment(float4 position : SV_Position,
                            float2 uv : TEXCOORD0) : SV_Target
            {
                return _Data[frac(uv.x) * 256];
            }

            ENDCG
        }
    }
}
