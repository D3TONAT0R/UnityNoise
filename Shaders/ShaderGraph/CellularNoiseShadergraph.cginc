#include "../CellularNoise.cginc"

#define SETUP_SETTINGS				\
FractalSettings settings;			\
settings.octaves = floor(octaves);	\
settings.lacunarity = lacunarity;	\
settings.persistence = persistence;

void ComputeCellularNoise1D_float(float pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputeCellularNoise1D(pos, settings);
}

void ComputeCellularNoise1D_half(half pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputeCellularNoise1D(pos, settings);
}

void ComputeCellularNoise2D_float(float2 pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputeCellularNoise2D(pos, settings);
}

void ComputeCellularNoise2D_half(half2 pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputeCellularNoise2D(pos, settings);
}

void ComputeCellularNoise3D_float(float3 pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputeCellularNoise3D(pos, settings);
}

void ComputeCellularNoise3D_half(half3 pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputeCellularNoise3D(pos, settings);
}

void ComputeCellularNoise4D_float(float4 pos, float octaves, float lacunarity, float persistence, out float result)
{
	SETUP_SETTINGS
	result = ComputeCellularNoise4D(pos, settings);
}

void ComputeCellularNoise4D_half(half4 pos, half octaves, half lacunarity, half persistence, out half result)
{
	SETUP_SETTINGS
	result = ComputeCellularNoise4D(pos, settings);
}