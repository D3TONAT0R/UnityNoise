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

#define SQRT_2 1.4142135623730951
#define A1 0.254829592
#define A2 -0.284496736
#define A3 1.421413741
#define A4 -1.453152027
#define A5 1.061405429
#define P 0.3275911

// Approximation of the error function (erf)
float erfApprox(float x)
{
    // Save the sign of x
    int sign = (x >= 0) ? 1 : -1;
    x = abs(x);

    // Apply the approximation formula
    float t = 1.0 / (1.0 + P * x);
    float y = 1.0 - (((((A5 * t + A4) * t) + A3) * t + A2) * t + A1) * t * exp(-x * x);

    return sign * y;
}

// CDF function for normal distribution using erfApprox
float normalCDF(float x, float mean, float stddev = 1.0)
{
    float z = (x - mean) / stddev;
    return 0.5 * (1 + erfApprox(z / SQRT_2));
}

float normalToUniform(float x, float stdDev)
{
    return normalCDF(x, 0.5, stdDev);
}

void addNoise(inout float v, float b, float intensity)
{
    v = lerp(v, b, intensity);
}

void addNoise(inout float2 v, float2 b, float intensity)
{
    v = lerp(v, b, intensity);
}

#endif