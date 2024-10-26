#include "Common.cginc"

float2 GetRandomDir(float2 i)
{
	float rand = hash(i) * 6.282;
	return float2(sin(rand), cos(rand));
}

float3 GetRandomDir(float3 i)
{
	float rand1 = hash(i) * 6.282;
	float rand2 = hash(i + 1) * 6.282;
	return normalize(float3(sin(rand1), cos(rand1), sin(rand2)));
}

float4 GetRandomDir(float4 i)
{
	float rand1 = hash(i) * 6.282;
	float rand2 = hash(i + 1) * 6.282;
	return normalize(float4(sin(rand1), cos(rand1), sin(rand2), cos(rand2)));
}

float DotGrid(float2 pos, int2 c)
{
	float2 dir = GetRandomDir(c);
	return dot(pos - c, dir);
}

float DotGrid(float3 pos, int3 c)
{
	float3 dir = GetRandomDir(c);
	return dot(pos - c, dir);
}

float DotGrid(float4 pos, int4 c)
{
	float4 dir = GetRandomDir(c);
	return dot(pos - c, dir);
}

float PowLerp(float a, float b, float t)
{
	return pow(t, 2.0) * (3.0 - 2.0 * t) * (b - a) + a;
}

float GetPerlinNoise1D(float x)
{
	int x1 = floor(x);
	int x2 = x1 + 1;
	float wx = x - x1;
	float g0 = DotGrid(float2(x, x), x1);
	float g1 = DotGrid(float2(x, x), x2);
	float n = PowLerp(g0, g1, wx) * 0.78 + 0.45;
	n = normalToUniform(n, 0.18);
	return n;
}

float GetPerlinNoise2D(float2 pos)
{
	int2 low = floor(pos);
	int2 high = low + 1;
	float2 w = frac(pos);
	float g00 = DotGrid(pos, int2(low.x, low.y));
	float g10 = DotGrid(pos, int2(high.x, low.y));
	float g01 = DotGrid(pos, int2(low.x, high.y));
	float g11 = DotGrid(pos, int2(high.x, high.y));
	//Interpolate on x axis
	float ix0 = PowLerp(g00, g10, w.x);
	float ix1 = PowLerp(g01, g11, w.x);
	//Interpolate on y axis
	float n = PowLerp(ix0, ix1, w.y) * 0.74 + 0.49;
	n = normalToUniform(n, 0.17);
	return n;
}

float GetPerlinNoise3D(float3 pos)
{
	int3 low = floor(pos);
	int3 high = low + 1;
	float3 w = frac(pos);
	float g000 = DotGrid(pos, int3(low.x, low.y, low.z));
	float g100 = DotGrid(pos, int3(high.x, low.y, low.z));
	float g010 = DotGrid(pos, int3(low.x, high.y, low.z));
	float g110 = DotGrid(pos, int3(high.x, high.y, low.z));
	float g001 = DotGrid(pos, int3(low.x, low.y, high.z));
	float g101 = DotGrid(pos, int3(high.x, low.y, high.z));
	float g011 = DotGrid(pos, int3(low.x, high.y, high.z));
	float g111 = DotGrid(pos, int3(high.x, high.y, high.z));
	//Interpolate on x axis
	float ix00 = PowLerp(g000, g100, w.x);
	float ix10 = PowLerp(g010, g110, w.x);
	float ix01 = PowLerp(g001, g101, w.x);
	float ix11 = PowLerp(g011, g111, w.x);
	//Interpolate on y axis
	float ixy0 = PowLerp(ix00, ix10, w.y);
	float ixy1 = PowLerp(ix01, ix11, w.y);
	//Interpolate on z axis
	float n = PowLerp(ixy0, ixy1, w.z) * 0.8 + 0.48;
	n = normalToUniform(n, 0.16);
	return n;
}

float GetPerlinNoise4D(float4 pos)
{
	int4 low = floor(pos);
	int4 high = low + 1;
	float4 w = frac(pos);
	float g0000 = DotGrid(pos, int4(low.x, low.y, low.z, low.w));
	float g1000 = DotGrid(pos, int4(high.x, low.y, low.z, low.w));
	float g0100 = DotGrid(pos, int4(low.x, high.y, low.z, low.w));
	float g1100 = DotGrid(pos, int4(high.x, high.y, low.z, low.w));
	float g0010 = DotGrid(pos, int4(low.x, low.y, high.z, low.w));
	float g1010 = DotGrid(pos, int4(high.x, low.y, high.z, low.w));
	float g0110 = DotGrid(pos, int4(low.x, high.y, high.z, low.w));
	float g1110 = DotGrid(pos, int4(high.x, high.y, high.z, low.w));
	float g0001 = DotGrid(pos, int4(low.x, low.y, low.z, high.w));
	float g1001 = DotGrid(pos, int4(high.x, low.y, low.z, high.w));
	float g0101 = DotGrid(pos, int4(low.x, high.y, low.z, high.w));
	float g1101 = DotGrid(pos, int4(high.x, high.y, low.z, high.w));
	float g0011 = DotGrid(pos, int4(low.x, low.y, high.z, high.w));
	float g1011 = DotGrid(pos, int4(high.x, low.y, high.z, high.w));
	float g0111 = DotGrid(pos, int4(low.x, high.y, high.z, high.w));
	float g1111 = DotGrid(pos, int4(high.x, high.y, high.z, high.w));
	//Interpolate on x axis
	float ix000 = PowLerp(g0000, g1000, w.x);
	float ix100 = PowLerp(g0100, g1100, w.x);
	float ix010 = PowLerp(g0010, g1010, w.x);
	float ix110 = PowLerp(g0110, g1110, w.x);
	float ix001 = PowLerp(g0001, g1001, w.x);
	float ix101 = PowLerp(g0101, g1101, w.x);
	float ix011 = PowLerp(g0011, g1011, w.x);
	float ix111 = PowLerp(g0111, g1111, w.x);
	//Interpolate on y axis
	float iy00 = PowLerp(ix000, ix100, w.y);
	float iy10 = PowLerp(ix010, ix110, w.y);
	float iy01 = PowLerp(ix001, ix101, w.y);
	float iy11 = PowLerp(ix011, ix111, w.y);
	//Interpolate on z axis
	float iz0 = PowLerp(iy00, iy10, w.z);
	float iz1 = PowLerp(iy01, iy11, w.z);
	//Interpolate on w axis
	float n = PowLerp(iz0, iz1, w.w) * 0.9 + 0.5;
	n = normalToUniform(n, 0.15);
	return n;
}

float ComputePerlinNoise1D(float pos, FractalSettings settings)
{
	float v = 0.0;
	float intensity = 1.0;
	FOR_FRACTAL
	{
		addNoise(v, GetPerlinNoise1D(pos), intensity);
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return v;
}

float ComputePerlinNoise2D(float2 pos, FractalSettings settings)
{
	float v = 0.0;
	float intensity = 1.0;
	FOR_FRACTAL
	{
		addNoise(v, GetPerlinNoise2D(pos), intensity);
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return v;
}

float ComputePerlinNoise3D(float3 pos, FractalSettings settings)
{
	float v = 0.0;
	float intensity = 1.0;
	FOR_FRACTAL
	{
		addNoise(v, GetPerlinNoise3D(pos), intensity);
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return v;
}

float ComputePerlinNoise4D(float4 pos, FractalSettings settings)
{
	float v = 0.0;
	float intensity = 1.0;
	FOR_FRACTAL
	{
		addNoise(v, GetPerlinNoise4D(pos), intensity);
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return v;
}