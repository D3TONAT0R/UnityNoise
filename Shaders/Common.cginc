#ifndef UNITY_NOISE_COMMON_INCLUDED
#define UNITY_NOISE_COMMON_INCLUDED

#define FOR_FRACTAL for(int i = 0; i < min(settings.octaves, 8); i++)

struct FractalSettings
{
    int octaves;
    float lacunarity;
    float persistence;
    float smoothing;
};

//TODO: find different magic numbers to avoid patterns
half hash(float4 pos)
{
    return frac(sin(dot(pos, float4(12.9898, 378.233, 45.164, 94.673))) * 43758.5453) * 2.0 - 1.0;
}
			
half hash(float pos)
{
    return hash(float4(pos, 0.0, 0.0, 0.0));
}

half hash(float2 pos)
{
    return hash(float4(pos, 0.0, 0.0));
}

half hash(float3 pos)
{
    return hash(float4(pos, 0.0));
}

int mod(int x, int m)
{
    int a = x % m;
    return a < 0 ? a + m : a;
}

#endif