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
	public struct VoronoiSettings
	{
		public static readonly VoronoiSettings Default = new VoronoiSettings(1, DistanceType.Euclidean, VoronoiType.Distance);

		[Range(0, 1)]
		public float voronoiDistortion;
		public DistanceType distanceType;
		public VoronoiType voronoiType;

		public VoronoiSettings(float voronoiDistortion, DistanceType distanceType, VoronoiType voronoiType)
		{
			this.voronoiDistortion = voronoiDistortion;
			this.distanceType = distanceType;
			this.voronoiType = voronoiType;
		}
	}

	[System.Serializable]
	public struct FractalSettings
	{
		public static readonly FractalSettings Simple = new FractalSettings(1, 2, 0.5f, 1 / 16f);

		[Range(1, 8)]
		public int octaves;
		[Min(1.001f)]
		public float lacunarity;
		[Range(0, 1)]
		public float persistence;
		public Vector4 scale;
		public Vector4 offset;
		[Min(0.001f)]
		public float depth;
		public VoronoiSettings voronoiSettings;

		public bool clamp;

		public FractalSettings(int octaves, float lacunarity, float persistence, Vector4 scale, Vector4 offset = default, float depth = 1f, VoronoiSettings? voronoiSettings = null, bool clamp = false)
		{
			this.octaves = octaves;
			this.lacunarity = lacunarity;
			this.persistence = persistence;
			this.scale = scale;
			this.offset = offset;
			this.depth = depth;
			this.voronoiSettings = voronoiSettings ?? VoronoiSettings.Default;
			this.clamp = clamp;
		}

		public FractalSettings(int octaves, float lacunarity, float persistence, float scale, Vector4 offset = default, float depth = 1f, VoronoiSettings? voronoiSettings = null, bool clamp = false)
		{
			this.octaves = octaves;
			this.lacunarity = lacunarity;
			this.persistence = persistence;
			this.scale = new Vector4(scale, scale, scale, scale);
			this.offset = offset;
			this.depth = depth;
			this.voronoiSettings = voronoiSettings ?? VoronoiSettings.Default;
			this.clamp = clamp;
		}
	}
}