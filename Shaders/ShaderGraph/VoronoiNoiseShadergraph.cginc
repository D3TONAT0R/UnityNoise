#include "../VoronoiNoise.cginc"

#define SETUP_SETTINGS				\
FractalSettings settings;			\
settings.octaves = floor(octaves);	\
settings.lacunarity = lacunarity;	\
settings.persistence = persistence;

void ComputeVoronoiNoise1D_float(float pos, float octaves, float lacunarity, float persistence, out float2 result)
{
	SETUP_SETTINGS
	result = ComputeVoronoiNoise1D(pos, settings);
}

void ComputeVoronoiNoise1D_half(half pos, half octaves, half lacunarity, half persistence, out half2 result)
{
	SETUP_SETTINGS
	result = ComputeVoronoiNoise1D(pos, settings);
}

void ComputeVoronoiNoise2D_float(float2 pos, float octaves, float lacunarity, float persistence, out float2 result)
{
	SETUP_SETTINGS
    result = ComputeVoronoiNoise2D(pos, settings);
}

void ComputeVoronoiNoise2D_half(half2 pos, half octaves, half lacunarity, half persistence, out half2 result)
{
	SETUP_SETTINGS
    result = ComputeVoronoiNoise2D(pos, settings);
}

void ComputeVoronoiNoise3D_float(float3 pos, float octaves, float lacunarity, float persistence, out float2 result)
{
	SETUP_SETTINGS
    result = ComputeVoronoiNoise3D(pos, settings);
}

void ComputeVoronoiNoise3D_half(half3 pos, half octaves, half lacunarity, half persistence, out half2 result)
{
	SETUP_SETTINGS
    result = ComputeVoronoiNoise3D(pos, settings);
}

void ComputeVoronoiNoise4D_float(float4 pos, float octaves, float lacunarity, float persistence, out float2 result)
{
	SETUP_SETTINGS
    result = ComputeVoronoiNoise4D(pos, settings);
}

void ComputeVoronoiNoise4D_half(half4 pos, half octaves, half lacunarity, half persistence, out half2 result)
{
	SETUP_SETTINGS
    result = ComputeVoronoiNoise4D(pos, settings);
}