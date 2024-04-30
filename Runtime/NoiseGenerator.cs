using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace UnityNoise
{
	public abstract class NoiseGeneratorBase
	{
		public float GetNoise1D(float pos, NoiseParameters? settings = null)
		{
			return GetNoise(1, new Vector4(pos, 0, 0, 0), settings ?? NoiseParameters.Simple);
		}

		public float GetNoise2D(Vector2 pos, NoiseParameters? settings = null)
		{
			return GetNoise(2, pos, settings ?? NoiseParameters.Simple);
		}

		public float GetNoise3D(Vector3 pos, NoiseParameters? settings = null)
		{
			return GetNoise(3, pos, settings ?? NoiseParameters.Simple);
		}

		public float GetNoise4D(Vector4 pos, NoiseParameters? settings = null)
		{
			return GetNoise(4, pos, settings ?? NoiseParameters.Simple);
		}

		protected virtual float GetNoise(int dimensions, Vector4 pos, NoiseParameters parameters)
		{
			float noise = 0;
			float intensity = 1f;

			var scale = parameters.scale;
			var wrap = parameters.repeat;
			var fractalParams = parameters.fractalParameters;
			for(int i = 0; i < fractalParams.octaves; i++)
			{
				var offsetPos = Vector4.Scale(pos, scale) + parameters.offset;
				float value = CalcNoise(dimensions, offsetPos, parameters, wrap);
				if(i == 0)
				{
					noise = value;
				}
				else
				{
					noise += value * intensity;
				}
				scale *= fractalParams.lacunarity;
				wrap *= fractalParams.lacunarity;
				intensity *= fractalParams.persistence;
			}
			noise *= parameters.depth;
			noise = noise * 0.5f + 0.5f;
			if(parameters.clampOutput) noise = Mathf.Clamp01(noise);
			return noise;
		}

		protected abstract float CalcNoise(int dimensions, Vector4 pos, NoiseParameters settings, Vector4 wrap);

		protected static int WrapCell(int pos, float repeat, float offset)
		{
			if(repeat > 0)
			{
				return Mathf.FloorToInt(Mathf.Repeat(pos - offset, repeat) + offset);
			}
			else
			{
				return pos;
			}
		}

		protected static Vector4 Wrap(Vector4 pos, Vector4 repeat, Vector4 offset)
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

		protected static float HashF(Vector4 pos)
		{
			//return random value between 0 and 1
			return Mathf.Repeat(Mathf.Sin(Vector4.Dot(pos, new Vector4(12.9898f, 378.233f, 45.164f, 94.673f))) * 43758.5453f, 1f) * 2.0f - 1.0f;
		}

		protected static float HashF(float pos)
		{
			return HashF(new Vector4(pos, 0, 0, 0));
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