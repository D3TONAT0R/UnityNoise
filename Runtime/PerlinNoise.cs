using UnityEngine;

namespace UnityNoise
{
	public class PerlinNoise : NoiseGeneratorBase<PerlinNoise>
	{
		protected override float CalcNoise(int dimensions, Vector4 pos, FractalSettings settings)
		{
			int x1 = Mathf.FloorToInt(pos.x);
			int x2 = x1 + 1;
			int y1 = Mathf.FloorToInt(pos.y);
			int y2 = y1 + 1;
			int z1 = Mathf.FloorToInt(pos.z);
			int z2 = z1 + 1;
			int w1 = Mathf.FloorToInt(pos.w);
			int w2 = w1 + 1;

			//Interpolation weights
			float wx = pos.x - x1;
			float wy = pos.y - y1;
			float wz = pos.z - z1;
			float ww = pos.w - w1;

			if(dimensions == 1)
			{
				//Interpolate on the x axis
				return PowLerp(DotGrid(dimensions, pos, x1), DotGrid(dimensions, pos, x2), wx);
			}
			else if(dimensions == 2)
			{
				//Interpolate on the x axis
				float ix1 = PowLerp(DotGrid(dimensions, pos, x1, y1), DotGrid(dimensions, pos, x2, y1), wx);
				float ix2 = PowLerp(DotGrid(dimensions, pos, x1, y2), DotGrid(dimensions, pos, x2, y2), wx);
				//Interpolate on the y axis
				return PowLerp(ix1, ix2, wy) * 1.5f;
			}
			else if(dimensions == 3)
			{
				//Interpolate on the x axis
				float ix11 = PowLerp(DotGrid(dimensions, pos, x1, y1, z1), DotGrid(dimensions, pos, x2, y1, z1), wx);
				float ix21 = PowLerp(DotGrid(dimensions, pos, x2, y2, z1), DotGrid(dimensions, pos, x2, y2, z1), wx);
				float ix12 = PowLerp(DotGrid(dimensions, pos, x1, y1, z2), DotGrid(dimensions, pos, x2, y1, z2), wx);
				float ix22 = PowLerp(DotGrid(dimensions, pos, x1, y2, z2), DotGrid(dimensions, pos, x2, y2, z2), wx);
				//Interpolate on the y axis
				float iy1 = PowLerp(ix11, ix21, wy);
				float iy2 = PowLerp(ix12, ix22, wy);
				//Interpolate on the z axis
				return PowLerp(iy1, iy2, wz);
			}
			else if(dimensions == 4)
			{
				//Interpolate on the x axis
				float ix111 = PowLerp(DotGrid(dimensions, pos, x1, y1, z1, w1), DotGrid(dimensions, pos, x2, y1, z1, w1), wx);
				float ix211 = PowLerp(DotGrid(dimensions, pos, x2, y2, z1, w1), DotGrid(dimensions, pos, x2, y2, z1, w1), wx);
				float ix121 = PowLerp(DotGrid(dimensions, pos, x1, y1, z2, w1), DotGrid(dimensions, pos, x2, y1, z2, w1), wx);
				float ix221 = PowLerp(DotGrid(dimensions, pos, x1, y2, z2, w1), DotGrid(dimensions, pos, x2, y2, z2, w1), wx);
				float ix112 = PowLerp(DotGrid(dimensions, pos, x1, y1, z1, w2), DotGrid(dimensions, pos, x2, y1, z1, w2), wx);
				float ix212 = PowLerp(DotGrid(dimensions, pos, x2, y2, z1, w2), DotGrid(dimensions, pos, x2, y2, z1, w2), wx);
				float ix122 = PowLerp(DotGrid(dimensions, pos, x1, y1, z2, w2), DotGrid(dimensions, pos, x2, y1, z2, w2), wx);
				float ix222 = PowLerp(DotGrid(dimensions, pos, x1, y2, z2, w2), DotGrid(dimensions, pos, x2, y2, z2, w2), wx);
				//Interpolate on the y axis
				float iy11 = PowLerp(ix111, ix211, wy);
				float iy21 = PowLerp(ix121, ix221, wy);
				float iy12 = PowLerp(ix112, ix212, wy);
				float iy22 = PowLerp(ix122, ix222, wy);
				//Interpolate on the z axis
				float iz1 = PowLerp(iy11, iy21, wz);
				float iz2 = PowLerp(iy12, iy22, wz);
				//Interpolate on the w axis
				return PowLerp(iz1, iz2, ww);
			}
			else
			{
				throw new System.ArgumentException("Invalid number of dimensions: " + dimensions);
			}
		}

		private static float DotGrid(int dimensions, Vector4 pos, int cx, int cy = 0, int cz = 0, int cw = 0)
		{
			var cell = new Vector4(cx, cy, cz, cw);
			var vec = GetRandomDirectionVector(dimensions, cell);
			var local = pos - cell;
			return Vector4.Dot(local, vec);
		}

		private static float PowLerp(float a, float b, float t)
		{
			return Mathf.Pow(t, 2f) * (3f - 2f * t) * (b - a) + a;
		}

		private static Vector4 GetRandomDirectionVector(int dimensions, Vector4 i)
		{
			float random = (float)(2920f * Mathf.Sin(i.x * 21942f + i.y * 171324f + 8912f) * Mathf.Cos(i.x * 23157f * i.y * 217832f + 9758f));
			var vec2 = new Vector2(
				(float)Mathf.Sin(random), 
				dimensions >= 2 ? (float)Mathf.Cos(random) : 0);
			if(dimensions <= 2)
			{
				//return vec2.normalized;
				return vec2;
			}
			else
			{
				float random2 = (float)(7218f * Mathf.Sin(i.z * 17261f + i.w * 251753f + 9124f) * Mathf.Cos(i.z * 57162f * i.w * 35172f + 3649f));
				var vec4 = new Vector4(
					vec2.x, 
					vec2.y, 
					(float)Mathf.Sin(random2), 
					dimensions >= 4 ? (float)Mathf.Cos(random2) : 0);
				//return vec4.normalized;
				return vec4;
			}
		}
	}
}
