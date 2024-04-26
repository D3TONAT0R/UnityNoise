#include "../PerlinNoise.cginc"

#define SETUP_SETTINGS				\
FractalSettings settings;			\
settings.octaves = floor(octaves);	\
settings.lacunarity = lacunarity;	\
settings.persistence = persistence;

void ComputePerlinNoise1D_float(float pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputePerlinNoise1D(pos, settings);
}

void ComputePerlinNoise1D_half(half pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputePerlinNoise1D(pos, settings);
}

void ComputePerlinNoise2D_float(float2 pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputePerlinNoise2D(pos, settings);
}

void ComputePerlinNoise2D_half(half2 pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputePerlinNoise2D(pos, settings);
}

void ComputePerlinNoise3D_float(float3 pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputePerlinNoise3D(pos, settings);
}

void ComputePerlinNoise3D_half(half3 pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputePerlinNoise3D(pos, settings);
}

void ComputePerlinNoise4D_float(float4 pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputePerlinNoise4D(pos, settings);
}

void ComputePerlinNoise4D_half(half4 pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputePerlinNoise4D(pos, settings);
}