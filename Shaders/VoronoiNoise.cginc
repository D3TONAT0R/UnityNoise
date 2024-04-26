#include "Common.cginc"

half2 GetCell(float d, half2 pos)
{
	half x = hash(pos.x * 21.73 + pos.y * 32.19) * 0.37 * d;
	half y = hash(pos.x * 17.37 + pos.y * 9.73) * 0.37 * d;
	return pos + half2(x, y);
}

half3 GetCell(float d, half3 pos)
{
	half x = hash(pos.x * 21.73 + pos.y * 32.19 + pos.z * 47.93) * 0.37 * d;
	half y = hash(pos.x * 17.37 + pos.y * 9.73 + pos.z * 7.21) * 0.37 * d;
	half z = hash(pos.x * 9.41 + pos.y * 91.27 + pos.z * 13.79) * 0.37 * d;
	return pos + half3(x, y, z);
}

half4 GetCell(float d, half4 pos)
{
    half x = hash(pos.x * 21.73 + pos.y * 32.19 + pos.z * 47.93) * 0.37 * d;
    half y = hash(pos.x * 17.37 + pos.y * 9.73 + pos.z * 7.21) * 0.37 * d;
    half z = hash(pos.x * 9.41 + pos.y * 91.27 + pos.z * 13.79) * 0.37 * d;
	half w = hash(pos.x * 13.79 + pos.y * 9.41 + pos.z * 91.27) * 0.37 * d;
    return pos + half4(x, y, z, w);
}

#ifndef VORONOI_DISTANCE_FUNC
#define VORONOI_DISTANCE_FUNC distance
#endif

float manhattanDistance(float2 a, float2 b)
{
    return (abs(b.x - a.x) + abs(b.y - a.y)) * 0.5;
}

float manhattanDistance(float3 a, float3 b)
{
    return (abs(b.x - a.x) + abs(b.y - a.y) + abs(b.z - a.z)) * 0.5;
}

float manhattanDistance(float4 a, float4 b)
{
    return (abs(b.x - a.x) + abs(b.y - a.y) + abs(b.z - a.z) + abs(b.w - a.w)) * 0.5;
}

float chebyshevDistance(float2 a, float2 b)
{
    return max(abs(b.x - a.x), abs(b.y - a.y));
}

float chebyshevDistance(float3 a, float3 b)
{
    return max(max(abs(b.x - a.x), abs(b.y - a.y)), abs(b.z - a.z));
}

float chebyshevDistance(float4 a, float4 b)
{
    return max(max(max(abs(b.x - a.x), abs(b.y - a.y)), abs(b.z - a.z)), abs(b.w - a.w));
}

half nearest(half min, inout half2 nearestCell, half2 pos, half2 cell)
{
    half dist = VORONOI_DISTANCE_FUNC(pos, cell);
	if (dist < min)
	{
		min = dist;
		nearestCell = cell;
	}
	return min;
}

half nearest(half min, inout half3 nearestCell, half3 pos, half3 cell)
{
    half dist = VORONOI_DISTANCE_FUNC(pos, cell);
    if (dist < min)
    {
        min = dist;
        nearestCell = cell;
    }
    return min;
}

half nearest(half min, inout half4 nearestCell, half4 pos, half4 cell)
{
    half dist = VORONOI_DISTANCE_FUNC(pos, cell);
    if (dist < min)
    {
        min = dist;
        nearestCell = cell;
    }
    return min;
}

half2 GetVoronoiNoise1D(float pos, float d)
{
	int x1 = floor(pos);
	int x2 = x1 + 1;
	int x0 = x1 - 1;
	half c0 = GetCell(d, half2(x0, 0));
	half c1 = GetCell(d, half2(x1, 0));
	half c2 = GetCell(d, half2(x2, 0));
	half min = 999.99;
	half2 nearestCell = half2(0.0, 0.0);
	min = nearest(min, nearestCell, pos, c0);
	min = nearest(min, nearestCell, pos, c1);
	min = nearest(min, nearestCell, pos, c2);
	return half2(min * 1.8 - 1.0, hash(nearestCell));
}

half2 GetVoronoiNoise2D(float2 pos, float s)
{
	int x1 = floor(pos.x);
	int x2 = x1 + 1;
	int x0 = x1 - 1;
	int y1 = floor(pos.y);
	int y2 = y1 + 1;
	int y0 = y1 - 1;
	half2 c00 = GetCell(s, half2(x0, y0));
	half2 c10 = GetCell(s, half2(x1, y0));
	half2 c20 = GetCell(s, half2(x2, y0));
	half2 c01 = GetCell(s, half2(x0, y1));
	half2 c11 = GetCell(s, half2(x1, y1));
	half2 c21 = GetCell(s, half2(x2, y1));
	half2 c02 = GetCell(s, half2(x0, y2));
	half2 c12 = GetCell(s, half2(x1, y2));
	half2 c22 = GetCell(s, half2(x2, y2));
	half min = 99.99;
	half2 nearestCell = half2(0.0, 0.0);
	min = nearest(min, nearestCell, pos, c00);
	min = nearest(min, nearestCell, pos, c10);
	min = nearest(min, nearestCell, pos, c20);
	min = nearest(min, nearestCell, pos, c01);
	min = nearest(min, nearestCell, pos, c11);
	min = nearest(min, nearestCell, pos, c21);
	min = nearest(min, nearestCell, pos, c02);
	min = nearest(min, nearestCell, pos, c12);
	min = nearest(min, nearestCell, pos, c22);
	return half2(min * 1.8 - 1.0, hash(nearestCell));
}

half2 GetVoronoiNoise3D(float3 pos, float s)
{
	int x1 = floor(pos.x);
	int x2 = x1 + 1;
	int x0 = x1 - 1;
	int y1 = floor(pos.y);
	int y2 = y1 + 1;
	int y0 = y1 - 1;
	int z1 = floor(pos.z);
	int z2 = z1 + 1;
	int z0 = z1 - 1;
	half3 c000 = GetCell(s, half3(x0, y0, z0));
	half3 c100 = GetCell(s, half3(x1, y0, z0));
	half3 c200 = GetCell(s, half3(x2, y0, z0));
	half3 c010 = GetCell(s, half3(x0, y1, z0));
	half3 c110 = GetCell(s, half3(x1, y1, z0));
	half3 c210 = GetCell(s, half3(x2, y1, z0));
	half3 c020 = GetCell(s, half3(x0, y2, z0));
	half3 c120 = GetCell(s, half3(x1, y2, z0));
	half3 c220 = GetCell(s, half3(x2, y2, z0));
	half3 c001 = GetCell(s, half3(x0, y0, z1));
	half3 c101 = GetCell(s, half3(x1, y0, z1));
	half3 c201 = GetCell(s, half3(x2, y0, z1));
	half3 c011 = GetCell(s, half3(x0, y1, z1));
	half3 c111 = GetCell(s, half3(x1, y1, z1));
	half3 c211 = GetCell(s, half3(x2, y1, z1));
	half3 c021 = GetCell(s, half3(x0, y2, z1));
	half3 c121 = GetCell(s, half3(x1, y2, z1));
	half3 c221 = GetCell(s, half3(x2, y2, z1));
	half3 c002 = GetCell(s, half3(x0, y0, z2));
	half3 c102 = GetCell(s, half3(x1, y0, z2));
	half3 c202 = GetCell(s, half3(x2, y0, z2));
	half3 c012 = GetCell(s, half3(x0, y1, z2));
	half3 c112 = GetCell(s, half3(x1, y1, z2));
	half3 c212 = GetCell(s, half3(x2, y1, z2));
	half3 c022 = GetCell(s, half3(x0, y2, z2));
	half3 c122 = GetCell(s, half3(x1, y2, z2));
	half3 c222 = GetCell(s, half3(x2, y2, z2));
	half min = 99.99;
	half3 nearestCell = half3(0.0, 0.0, 0.0);
	min = nearest(min, nearestCell, pos, c000);
	min = nearest(min, nearestCell, pos, c100);
	min = nearest(min, nearestCell, pos, c200);
	min = nearest(min, nearestCell, pos, c010);
	min = nearest(min, nearestCell, pos, c110);
	min = nearest(min, nearestCell, pos, c210);
	min = nearest(min, nearestCell, pos, c020);
	min = nearest(min, nearestCell, pos, c120);
	min = nearest(min, nearestCell, pos, c220);
	min = nearest(min, nearestCell, pos, c001);
	min = nearest(min, nearestCell, pos, c101);
	min = nearest(min, nearestCell, pos, c201);
	min = nearest(min, nearestCell, pos, c011);
	min = nearest(min, nearestCell, pos, c111);
	min = nearest(min, nearestCell, pos, c211);
	min = nearest(min, nearestCell, pos, c021);
	min = nearest(min, nearestCell, pos, c121);
	min = nearest(min, nearestCell, pos, c221);
	min = nearest(min, nearestCell, pos, c002);
	min = nearest(min, nearestCell, pos, c102);
	min = nearest(min, nearestCell, pos, c202);
	min = nearest(min, nearestCell, pos, c012);
	min = nearest(min, nearestCell, pos, c112);
	min = nearest(min, nearestCell, pos, c212);
	min = nearest(min, nearestCell, pos, c022);
	min = nearest(min, nearestCell, pos, c122);
	min = nearest(min, nearestCell, pos, c222);
	return half2(min * 1.8 - 1.0, hash(nearestCell));
}

half2 GetVoronoiNoise4D(float4 pos, float s)
{
	int x1 = floor(pos.x);
    int x2 = x1 + 1;
    int x0 = x1 - 1;
    int y1 = floor(pos.y);
    int y2 = y1 + 1;
    int y0 = y1 - 1;
    int z1 = floor(pos.z);
    int z2 = z1 + 1;
    int z0 = z1 - 1;
    int w1 = floor(pos.w);
    int w2 = w1 + 1;
    int w0 = w1 - 1;
    half4 c0000 = GetCell(s, half4(x0, y0, z0, w0));
    half4 c1000 = GetCell(s, half4(x1, y0, z0, w0));
    half4 c2000 = GetCell(s, half4(x2, y0, z0, w0));
    half4 c0100 = GetCell(s, half4(x0, y1, z0, w0));
    half4 c1100 = GetCell(s, half4(x1, y1, z0, w0));
    half4 c2100 = GetCell(s, half4(x2, y1, z0, w0));
    half4 c0200 = GetCell(s, half4(x0, y2, z0, w0));
    half4 c1200 = GetCell(s, half4(x1, y2, z0, w0));
    half4 c2200 = GetCell(s, half4(x2, y2, z0, w0));
    half4 c0010 = GetCell(s, half4(x0, y0, z1, w0));
    half4 c1010 = GetCell(s, half4(x1, y0, z1, w0));
    half4 c2010 = GetCell(s, half4(x2, y0, z1, w0));
    half4 c0110 = GetCell(s, half4(x0, y1, z1, w0));
    half4 c1110 = GetCell(s, half4(x1, y1, z1, w0));
    half4 c2110 = GetCell(s, half4(x2, y1, z1, w0));
    half4 c0210 = GetCell(s, half4(x0, y2, z1, w0));
	half4 c1210 = GetCell(s, half4(x1, y2, z1, w0));
	half4 c2210 = GetCell(s, half4(x2, y2, z1, w0));
	half4 c0020 = GetCell(s, half4(x0, y0, z2, w0));
	half4 c1020 = GetCell(s, half4(x1, y0, z2, w0));
	half4 c2020 = GetCell(s, half4(x2, y0, z2, w0));
	half4 c0120 = GetCell(s, half4(x0, y1, z2, w0));
	half4 c1120 = GetCell(s, half4(x1, y1, z2, w0));
	half4 c2120 = GetCell(s, half4(x2, y1, z2, w0));
	half4 c0220 = GetCell(s, half4(x0, y2, z2, w0));
	half4 c1220 = GetCell(s, half4(x1, y2, z2, w0));
	half4 c2220 = GetCell(s, half4(x2, y2, z2, w0));
	half4 c0001 = GetCell(s, half4(x0, y0, z0, w1));
	half4 c1001 = GetCell(s, half4(x1, y0, z0, w1));
	half4 c2001 = GetCell(s, half4(x2, y0, z0, w1));
	half4 c0101 = GetCell(s, half4(x0, y1, z0, w1));
	half4 c1101 = GetCell(s, half4(x1, y1, z0, w1));
	half4 c2101 = GetCell(s, half4(x2, y1, z0, w1));
	half4 c0201 = GetCell(s, half4(x0, y2, z0, w1));
	half4 c1201 = GetCell(s, half4(x1, y2, z0, w1));
	half4 c2201 = GetCell(s, half4(x2, y2, z0, w1));
	half4 c0011 = GetCell(s, half4(x0, y0, z1, w1));
	half4 c1011 = GetCell(s, half4(x1, y0, z1, w1));
	half4 c2011 = GetCell(s, half4(x2, y0, z1, w1));
	half4 c0111 = GetCell(s, half4(x0, y1, z1, w1));
	half4 c1111 = GetCell(s, half4(x1, y1, z1, w1));
	half4 c2111 = GetCell(s, half4(x2, y1, z1, w1));
	half4 c0211 = GetCell(s, half4(x0, y2, z1, w1));
	half4 c1211 = GetCell(s, half4(x1, y2, z1, w1));
	half4 c2211 = GetCell(s, half4(x2, y2, z1, w1));
	half4 c0021 = GetCell(s, half4(x0, y0, z2, w1));
	half4 c1021 = GetCell(s, half4(x1, y0, z2, w1));
	half4 c2021 = GetCell(s, half4(x2, y0, z2, w1));
	half4 c0121 = GetCell(s, half4(x0, y1, z2, w1));
	half4 c1121 = GetCell(s, half4(x1, y1, z2, w1));
	half4 c2121 = GetCell(s, half4(x2, y1, z2, w1));
	half4 c0221 = GetCell(s, half4(x0, y2, z2, w1));
	half4 c1221 = GetCell(s, half4(x1, y2, z2, w1));
	half4 c2221 = GetCell(s, half4(x2, y2, z2, w1));
	half4 c0002 = GetCell(s, half4(x0, y0, z0, w2));
	half4 c1002 = GetCell(s, half4(x1, y0, z0, w2));
	half4 c2002 = GetCell(s, half4(x2, y0, z0, w2));
	half4 c0102 = GetCell(s, half4(x0, y1, z0, w2));
	half4 c1102 = GetCell(s, half4(x1, y1, z0, w2));
	half4 c2102 = GetCell(s, half4(x2, y1, z0, w2));
	half4 c0202 = GetCell(s, half4(x0, y2, z0, w2));
	half4 c1202 = GetCell(s, half4(x1, y2, z0, w2));
	half4 c2202 = GetCell(s, half4(x2, y2, z0, w2));
	half4 c0012 = GetCell(s, half4(x0, y0, z1, w2));
	half4 c1012 = GetCell(s, half4(x1, y0, z1, w2));
	half4 c2012 = GetCell(s, half4(x2, y0, z1, w2));
	half4 c0112 = GetCell(s, half4(x0, y1, z1, w2));
	half4 c1112 = GetCell(s, half4(x1, y1, z1, w2));
	half4 c2112 = GetCell(s, half4(x2, y1, z1, w2));
	half4 c0212 = GetCell(s, half4(x0, y2, z1, w2));
	half4 c1212 = GetCell(s, half4(x1, y2, z1, w2));
	half4 c2212 = GetCell(s, half4(x2, y2, z1, w2));
	half4 c0022 = GetCell(s, half4(x0, y0, z2, w2));
	half4 c1022 = GetCell(s, half4(x1, y0, z2, w2));
	half4 c2022 = GetCell(s, half4(x2, y0, z2, w2));
	half4 c0122 = GetCell(s, half4(x0, y1, z2, w2));
	half4 c1122 = GetCell(s, half4(x1, y1, z2, w2));
	half4 c2122 = GetCell(s, half4(x2, y1, z2, w2));
	half4 c0222 = GetCell(s, half4(x0, y2, z2, w2));
	half4 c1222 = GetCell(s, half4(x1, y2, z2, w2));
	half4 c2222 = GetCell(s, half4(x2, y2, z2, w2));
	half min = 99.99;
	half4 nearestCell = half4(0.0, 0.0, 0.0, 0.0);
	min = nearest(min, nearestCell, pos, c0000);
	min = nearest(min, nearestCell, pos, c1000);
	min = nearest(min, nearestCell, pos, c2000);
	min = nearest(min, nearestCell, pos, c0100);
	min = nearest(min, nearestCell, pos, c1100);
	min = nearest(min, nearestCell, pos, c2100);
	min = nearest(min, nearestCell, pos, c0200);
	min = nearest(min, nearestCell, pos, c1200);
	min = nearest(min, nearestCell, pos, c2200);
	min = nearest(min, nearestCell, pos, c0010);
	min = nearest(min, nearestCell, pos, c1010);
	min = nearest(min, nearestCell, pos, c2010);
	min = nearest(min, nearestCell, pos, c0110);
	min = nearest(min, nearestCell, pos, c1110);
	min = nearest(min, nearestCell, pos, c2110);
	min = nearest(min, nearestCell, pos, c0210);
	min = nearest(min, nearestCell, pos, c1210);
	min = nearest(min, nearestCell, pos, c2210);
	min = nearest(min, nearestCell, pos, c0020);
	min = nearest(min, nearestCell, pos, c1020);
	min = nearest(min, nearestCell, pos, c2020);
	min = nearest(min, nearestCell, pos, c0120);
	min = nearest(min, nearestCell, pos, c1120);
	min = nearest(min, nearestCell, pos, c2120);
	min = nearest(min, nearestCell, pos, c0220);
	min = nearest(min, nearestCell, pos, c1220);
	min = nearest(min, nearestCell, pos, c2220);
    min = nearest(min, nearestCell, pos, c0001);
    min = nearest(min, nearestCell, pos, c1001);
    min = nearest(min, nearestCell, pos, c2001);
    min = nearest(min, nearestCell, pos, c0101);
    min = nearest(min, nearestCell, pos, c1101);
    min = nearest(min, nearestCell, pos, c2101);
    min = nearest(min, nearestCell, pos, c0201);
    min = nearest(min, nearestCell, pos, c1201);
    min = nearest(min, nearestCell, pos, c2201);
    min = nearest(min, nearestCell, pos, c0011);
    min = nearest(min, nearestCell, pos, c1011);
    min = nearest(min, nearestCell, pos, c2011);
    min = nearest(min, nearestCell, pos, c0111);
    min = nearest(min, nearestCell, pos, c1111);
    min = nearest(min, nearestCell, pos, c2111);
    min = nearest(min, nearestCell, pos, c0211);
    min = nearest(min, nearestCell, pos, c1211);
    min = nearest(min, nearestCell, pos, c2211);
    min = nearest(min, nearestCell, pos, c0021);
    min = nearest(min, nearestCell, pos, c1021);
    min = nearest(min, nearestCell, pos, c2021);
    min = nearest(min, nearestCell, pos, c0121);
    min = nearest(min, nearestCell, pos, c1121);
    min = nearest(min, nearestCell, pos, c2121);
    min = nearest(min, nearestCell, pos, c0221);
    min = nearest(min, nearestCell, pos, c1221);
    min = nearest(min, nearestCell, pos, c2221);
    min = nearest(min, nearestCell, pos, c0002);
    min = nearest(min, nearestCell, pos, c1002);
    min = nearest(min, nearestCell, pos, c2002);
    min = nearest(min, nearestCell, pos, c0102);
    min = nearest(min, nearestCell, pos, c1102);
    min = nearest(min, nearestCell, pos, c2102);
    min = nearest(min, nearestCell, pos, c0202);
    min = nearest(min, nearestCell, pos, c1202);
    min = nearest(min, nearestCell, pos, c2202);
    min = nearest(min, nearestCell, pos, c0012);
    min = nearest(min, nearestCell, pos, c1012);
    min = nearest(min, nearestCell, pos, c2012);
    min = nearest(min, nearestCell, pos, c0112);
    min = nearest(min, nearestCell, pos, c1112);
    min = nearest(min, nearestCell, pos, c2112);
    min = nearest(min, nearestCell, pos, c0212);
    min = nearest(min, nearestCell, pos, c1212);
    min = nearest(min, nearestCell, pos, c2212);
    min = nearest(min, nearestCell, pos, c0022);
    min = nearest(min, nearestCell, pos, c1022);
    min = nearest(min, nearestCell, pos, c2022);
    min = nearest(min, nearestCell, pos, c0122);
    min = nearest(min, nearestCell, pos, c1122);
    min = nearest(min, nearestCell, pos, c2122);
    min = nearest(min, nearestCell, pos, c0222);
    min = nearest(min, nearestCell, pos, c1222);
    min = nearest(min, nearestCell, pos, c2222);
	return half2(min * 1.8 - 1.0, hash(nearestCell));
}

float2 ComputeVoronoiNoise1D(float pos, FractalSettings settings)
{
	float2 v = 0.0;
	float intensity = 1.0;
	for (int i = 0; i < settings.octaves; i++)
	{
		v += GetVoronoiNoise1D(pos, 1) * intensity;
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return float2(saturate(v * 0.5 + 0.5));
}

float2 ComputeVoronoiNoise2D(float2 pos, FractalSettings settings)
{
	float2 v = 0.0;
	float intensity = 1.0;
	for (int i = 0; i < settings.octaves; i++)
	{
		v += GetVoronoiNoise2D(pos, 1) * intensity;
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return float2(saturate(v * 0.5 + 0.5));
}

float2 ComputeVoronoiNoise3D(float3 pos, FractalSettings settings)
{
	float2 v = 0.0;
	float intensity = 1.0;
	for (int i = 0; i < settings.octaves; i++)
	{
		v += GetVoronoiNoise3D(pos, 1) * intensity;
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return float2(saturate(v * 0.5 + 0.5));
}

float2 ComputeVoronoiNoise4D(float4 pos, FractalSettings settings)
{
	float2 v = 0.0;
	float intensity = 1.0;
	for (int i = 0; i < settings.octaves; i++)
	{
		v += GetVoronoiNoise4D(pos, 1) * intensity;
		pos *= settings.lacunarity;
		intensity *= settings.persistence;
	}
	return float2(saturate(v * 0.5 + 0.5));
}