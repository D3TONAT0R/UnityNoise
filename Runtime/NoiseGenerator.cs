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
			var repeat = settings.repeat;
			for(int i = 0; i < settings.octaves; i++)
			{
				var offsetPos = Vector4.Scale(pos, scale) + settings.offset;
				float value = CalcNoise(dimensions, offsetPos, settings, repeat);
				if(i == 0)
				{
					noise = value;
				}
				else
				{
					noise += value * intensity;
				}
				scale *= settings.lacunarity;
				repeat *= settings.lacunarity;
				intensity *= settings.persistence;
			}
			noise *= settings.depth;
			noise = noise * 0.5f + 0.5f;
			if(settings.clamp) noise = Mathf.Clamp01(noise);
			return noise;
		}

		protected abstract float CalcNoise(int dimensions, Vector4 pos, FractalSettings settings, Vector4 repeat);

		protected int WrapCell(int pos, float repeat, float offset)
		{
			if(repeat > 0)
			{
				return (int)(Mathf.Repeat(pos - offset, repeat) + offset);
			}
			else
			{
				return pos;
			}
		}

		protected Vector4 Wrap(Vector4 pos, Vector4 repeat, Vector4 offset)
		{
			pos -= offset;
			if(repeat.x > 0) pos.x = Mathf.Repeat(pos.x, repeat.x);
			if(repeat.y > 0) pos.y = Mathf.Repeat(pos.y, repeat.y);
			if(repeat.z > 0) pos.z = Mathf.Repeat(pos.z, repeat.z);
			if(repeat.w > 0) pos.w = Mathf.Repeat(pos.w, repeat.w);
			return pos + offset;
		}

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