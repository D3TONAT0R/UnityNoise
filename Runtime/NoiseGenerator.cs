using UnityEngine;

namespace UnityNoise
{
	public abstract class NoiseGeneratorBase
	{
		public float GetNoise1D(float pos, FractalSettings? settings = null)
		{
			return GetNoise(1, new Vector4(pos, 0, 0, 0), settings ?? FractalSettings.Simple);
		}

		public float GetNoise2D(Vector2 pos, FractalSettings? settings = null)
		{
			return GetNoise(2, pos, settings ?? FractalSettings.Simple);
		}

		public float GetNoise3D(Vector3 pos, FractalSettings? settings = null)
		{
			return GetNoise(3, pos, settings ?? FractalSettings.Simple);
		}

		public float GetNoise4D(Vector4 pos, FractalSettings? settings = null)
		{
			return GetNoise(4, pos, settings ?? FractalSettings.Simple);
		}

		protected virtual float GetNoise(int dimensions, Vector4 pos, FractalSettings settings)
		{
			float noise = 0;
			float intensity = 1f;

			var scale = settings.scale;
			for(int i = 0; i < settings.octaves; i++)
			{
				var offsetPos = settings.offset + Vector4.Scale(pos, scale);
				float value = CalcNoise(dimensions, offsetPos, settings);
				if(i == 0)
				{
					noise = value;
				}
				else
				{
					noise += value * intensity;
				}
				scale *= settings.lacunarity;
				intensity *= settings.persistence;
			}
			noise *= settings.depth;
			noise = noise * 0.5f + 0.5f;
			if(settings.clamp) noise = Mathf.Clamp01(noise);
			return noise;
		}

		protected abstract float CalcNoise(int dimensions, Vector4 pos, FractalSettings settings);

		protected static float Hash(int x, int y, int z, int w)
		{
			unchecked
			{
				int p = x * 16 + y * 327 + z * 431 + w * 123;
				p = (p << 13) ^ p;
				return (1.0f - ((p * (p * p * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0f);
			}
		}

		protected static float Hash(int x)
		{
			unchecked
			{
				int p = x * 167;
				p = (p << 13) ^ p;
				return (1.0f - ((p * (p * p * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0f);
			}
		}
	}

	public abstract class NoiseGeneratorBase<T> : NoiseGeneratorBase where T : NoiseGeneratorBase<T>, new()
	{

		public static T Instance
		{
			get
			{
				instance ??= new T();
				return instance;
			}
		}
		private static T instance;
	}
}