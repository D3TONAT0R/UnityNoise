#include "Common.cginc"

half GetCellularNoise1D(float cell, float s)
{
	half t = lerp(frac(cell), smoothstep(0.0, 1.0, frac(cell)), s);
	half c0 = floor(cell);
	half v0 = hash(c0);
	half v1 = hash(c0 + 1.0);
	return lerp(v0, v1, t);
}

half GetCellularNoise2D(float2 cell, float s)
{
	half2 t = lerp(frac(cell), smoothstep(float2(0.0, 0.0), float2(1.0, 1.0), frac(cell)), s);
	half2 c00 = floor(cell);
	half2 v00 = hash(c00);
	half2 v10 = hash(c00 + float2(1.0, 0.0));
	half2 v01 = hash(c00 + float2(0.0, 1.0));
	half2 v11 = hash(c00 + float2(1.0, 1.0));
	half v0 = lerp(v00, v10, t.x);
	half v1 = lerp(v01, v11, t.x);
	return lerp(v0, v1, t.y);
}

half GetCellularNoise3D(float3 cell, float s)
{
	half3 t = lerp(frac(cell), smoothstep(float3(0.0, 0.0, 0.0), float3(1.0, 1.0, 1.0), frac(cell)), s);
	half3 c000 = floor(cell);
	half3 v000 = hash(c000);
	half3 v100 = hash(c000 + float3(1.0, 0.0, 0.0));
	half3 v010 = hash(c000 + float3(0.0, 1.0, 0.0));
	half3 v110 = hash(c000 + float3(1.0, 1.0, 0.0));
	half3 v001 = hash(c000 + float3(0.0, 0.0, 1.0));
	half3 v101 = hash(c000 + float3(1.0, 0.0, 1.0));
	half3 v011 = hash(c000 + float3(0.0, 1.0, 1.0));
	half3 v111 = hash(c000 + float3(1.0, 1.0, 1.0));
	half v00 = lerp(v000, v100, t.x);
	half v10 = lerp(v010, v110, t.x);
	half v01 = lerp(v001, v101, t.x);
	half v11 = lerp(v011, v111, t.x);
	half v0 = lerp(v00, v10, t.y);
	half v1 = lerp(v01, v11, t.y);
	return lerp(v0, v1, t.z);
}

half GetCellularNoise4D(float4 cell, float s)
{
	half4 t = lerp(frac(cell), smoothstep(float4(0.0, 0.0, 0.0, 0.0), float4(1.0, 1.0, 1.0, 1.0), frac(cell)), s);
	half4 c0000 = floor(cell);
	half4 v0000 = hash(c0000);
	half4 v1000 = hash(c0000 + float4(1.0, 0.0, 0.0, 0.0));
	half4 v0100 = hash(c0000 + float4(0.0, 1.0, 0.0, 0.0));
	half4 v1100 = hash(c0000 + float4(1.0, 1.0, 0.0, 0.0));
	half4 v0010 = hash(c0000 + float4(0.0, 0.0, 1.0, 0.0));
	half4 v1010 = hash(c0000 + float4(1.0, 0.0, 1.0, 0.0));
	half4 v0110 = hash(c0000 + float4(0.0, 1.0, 1.0, 0.0));
	half4 v1110 = hash(c0000 + float4(1.0, 1.0, 1.0, 0.0));
	half4 v0001 = hash(c0000 + float4(0.0, 0.0, 0.0, 1.0));
	half4 v1001 = hash(c0000 + float4(1.0, 0.0, 0.0, 1.0));
	half4 v0101 = hash(c0000 + float4(0.0, 1.0, 0.0, 1.0));
	half4 v1101 = hash(c0000 + float4(1.0, 1.0, 0.0, 1.0));
	half4 v0011 = hash(c0000 + float4(0.0, 0.0, 1.0, 1.0));
	half4 v1011 = hash(c0000 + float4(1.0, 0.0, 1.0, 1.0));
	half4 v0111 = hash(c0000 + float4(0.0, 1.0, 1.0, 1.0));
	half4 v1111 = hash(c0000 + float4(1.0, 1.0, 1.0, 1.0));
	half v000 = lerp(v0000, v1000, t.x);
	half v100 = lerp(v0100, v1100, t.x);
	half v010 = lerp(v0010, v1010, t.x);
	half v110 = lerp(v0110, v1110, t.x);
	half v001 = lerp(v0001, v1001, t.x);
	half v101 = lerp(v0101, v1101, t.x);
	half v011 = lerp(v0011, v1011, t.x);
	half v111 = lerp(v0111, v1111, t.x);
	half v00 = lerp(v000, v100, t.y);
	half v10 = lerp(v010, v110, t.y);
	half v01 = lerp(v001, v101, t.y);
	half v11 = lerp(v011, v111, t.y);
	half v0 = lerp(v00, v10, t.z);
	half v1 = lerp(v01, v11, t.z);
	return lerp(v0, v1, t.w);
}

float ComputeCellularNoise1D(float pos, FractalSettings settings)
{
	float v = 0.0;
	float intensity = 1.0;
	FOR_FRACTAL
	{
        addNoise(v, GetCellularNoise1D(pos, settings.smoothing), intensity);
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return saturate(v * 0.5 + 0.5);
}

float ComputeCellularNoise2D(float2 pos, FractalSettings settings)
{
	float v = 0.0;
	float intensity = 1.0;
	FOR_FRACTAL
	{
        addNoise(v, GetCellularNoise2D(pos, settings.smoothing), intensity);
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return saturate(v * 0.5 + 0.5);
}

float ComputeCellularNoise3D(float3 pos, FractalSettings settings)
{
	float v = 0.0;
	float intensity = 1.0;
	FOR_FRACTAL
	{
		addNoise(v, GetCellularNoise3D(pos, settings.smoothing), intensity);
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return saturate(v * 0.5 + 0.5);
}

float ComputeCellularNoise4D(float4 pos, FractalSettings settings)
{
	float v = 0.0;
	float intensity = 1.0;
	FOR_FRACTAL
	{
		addNoise(v, GetCellularNoise4D(pos, settings.smoothing), intensity);
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return saturate(v * 0.5 + 0.5);
}