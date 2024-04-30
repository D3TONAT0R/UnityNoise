using UnityEngine;

namespace UnityNoise
{
	public enum DistanceType
	{
		Euclidean,
		Manhattan,
		Chebyshev
	}

	public enum VoronoiType
	{
		Distance,
		Cell
	}

	[System.Serializable]
	public struct VoronoiParameters
	{
		public static readonly VoronoiParameters Default = new VoronoiParameters(1, DistanceType.Euclidean, VoronoiType.Distance);

		[Range(0, 1)]
		public float voronoiDistortion;
		public DistanceType distanceType;
		public VoronoiType voronoiType;

		public VoronoiParameters(float voronoiDistortion, DistanceType distanceType, VoronoiType voronoiType)
		{
			this.voronoiDistortion = voronoiDistortion;
			this.distanceType = distanceType;
			this.voronoiType = voronoiType;
		}
	}

	[System.Serializable]
	public struct FractalParameters
	{
		public static readonly FractalParameters Simple = new FractalParameters(1);

		public static readonly FractalParameters Fractal4Octaves = new FractalParameters(4);

		public static readonly FractalParameters Fractal8Octaves = new FractalParameters(8);

		[Range(1, 8)]
		public int octaves;
		[Min(1.001f)]
		public float lacunarity;
		[Range(0, 1)]
		public float persistence;

		public bool IsValid => octaves > 0 && lacunarity > 0 && persistence > 0;

		public FractalParameters(int octaves, float lacunarity = 2f, float persistence = 0.5f)
		{
			this.octaves = octaves;
			this.lacunarity = lacunarity;
			this.persistence = persistence;
		}
	}

	[System.Serializable]
	public struct NoiseParameters
	{
		public static readonly NoiseParameters Simple = new NoiseParameters(1f);

		[Min(0.001f)]
		public Vector4 scale;
		public Vector4 offset;
		[Min(0.001f)]
		public float depth;
		public Vector4 repeat;

		public FractalParameters fractalParameters;
		public VoronoiParameters voronoiParameters;
		public CellularNoise.FilterType cellFilter;

		public bool clampOutput;

		public NoiseParameters(Vector4 scale, Vector4 offset = default, float depth = 1f, Vector4 repeat = default,
			FractalParameters? fractalParameters = null, VoronoiParameters? voronoiParameters = null, 
			CellularNoise.FilterType cellFilter = CellularNoise.FilterType.Smooth, bool clampOutput = false)
		{
			this.scale = scale;
			this.offset = offset;
			this.depth = depth;
			this.repeat = repeat;
			this.fractalParameters = fractalParameters ?? FractalParameters.Simple;
			this.voronoiParameters = voronoiParameters ?? VoronoiParameters.Default;
			this.cellFilter = cellFilter;
			this.clampOutput = clampOutput;
		}

		public NoiseParameters(float scale, Vector4 offset = default, float depth = 1f, Vector4 repeat = default,
			FractalParameters? fractalParameters = null, VoronoiParameters? voronoiParameters = null, 
			CellularNoise.FilterType cellFilter = CellularNoise.FilterType.Smooth, bool clampOutput = false)
		: this(new Vector4(scale, scale, scale, scale), offset, depth, repeat, fractalParameters, voronoiParameters, cellFilter, clampOutput)
		{

		}

		public NoiseParameters(Vector2 scale, Vector4 offset = default, float depth = 1f, Vector4 repeat = default,
			FractalParameters? fractalParameters = null, VoronoiParameters? voronoiParameters = null,
			CellularNoise.FilterType cellFilter = CellularNoise.FilterType.Smooth, bool clampOutput = false)
		: this(new Vector4(scale.x, scale.y, 1, 1), offset, depth, repeat, fractalParameters, voronoiParameters, cellFilter, clampOutput)
		{

		}

		public NoiseParameters(Vector3 scale, Vector4 offset = default, float depth = 1f, Vector4 repeat = default,
			FractalParameters? fractalParameters = null, VoronoiParameters? voronoiParameters = null,
			CellularNoise.FilterType cellFilter = CellularNoise.FilterType.Smooth, bool clampOutput = false)
		: this(new Vector4(scale.x, scale.y, scale.z, 1), offset, depth, repeat, fractalParameters, voronoiParameters, cellFilter, clampOutput)
		{

		}
	}
}