#include "../SimplexNoise.cginc"

#define SETUP_SETTINGS				\
FractalSettings settings;			\
settings.octaves = floor(octaves);	\
settings.lacunarity = lacunarity;	\
settings.persistence = persistence;

void ComputeSimplexNoise1D_float(float pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputeSimplexNoise1D(pos, settings);
}

void ComputeSimplexNoise1D_half(half pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputeSimplexNoise1D(pos, settings);
}

void ComputeSimplexNoise2D_float(float2 pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputeSimplexNoise2D(pos, settings);
}

void ComputeSimplexNoise2D_half(half2 pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputeSimplexNoise2D(pos, settings);
}

void ComputeSimplexNoise3D_float(float3 pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputeSimplexNoise3D(pos, settings);
}

void ComputeSimplexNoise3D_half(half3 pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputeSimplexNoise3D(pos, settings);
}

//Not yet implemented
/*
void ComputeSimplexNoise4D_float(float4 pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputeSimplexNoise4D(pos, settings);
}

void ComputeSimplexNoise4D_half(half4 pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputeSimplexNoise4D(pos, settings);
}
*/