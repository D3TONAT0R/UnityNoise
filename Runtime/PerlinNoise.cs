using UnityEngine;

namespace UnityNoise
{
	public class PerlinNoise : NoiseGeneratorBase<PerlinNoise>
	{
		protected override float CalcNoise(int dimensions, Vector4 pos, NoiseParameters parameters, Vector4 wrap)
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

			if(wrap != Vector4.zero)
			{
				x1 = WrapCell(x1, wrap.x, parameters.offset.x);
				x2 = WrapCell(x2, wrap.x, parameters.offset.x);
				y1 = WrapCell(y1, wrap.y, parameters.offset.y);
				y2 = WrapCell(y2, wrap.y, parameters.offset.y);
				z1 = WrapCell(z1, wrap.z, parameters.offset.z);
				z2 = WrapCell(z2, wrap.z, parameters.offset.z);
				w1 = WrapCell(w1, wrap.w, parameters.offset.w);
				w2 = WrapCell(w2, wrap.w, parameters.offset.w);
				pos = Wrap(pos, wrap, parameters.offset);
			}

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
				return PowLerp(ix1, ix2, wy) * 1.6f;
			}
			else if(dimensions == 3)
			{
				//Interpolate on the x axis
				float ix11 = PowLerp(DotGrid(dimensions, pos, x1, y1, z1), DotGrid(dimensions, pos, x2, y1, z1), wx);
				float ix21 = PowLerp(DotGrid(dimensions, pos, x1, y2, z1), DotGrid(dimensions, pos, x2, y2, z1), wx);
				float ix12 = PowLerp(DotGrid(dimensions, pos, x1, y1, z2), DotGrid(dimensions, pos, x2, y1, z2), wx);
				float ix22 = PowLerp(DotGrid(dimensions, pos, x1, y2, z2), DotGrid(dimensions, pos, x2, y2, z2), wx);
				//Interpolate on the y axis
				float iy1 = PowLerp(ix11, ix21, wy);
				float iy2 = PowLerp(ix12, ix22, wy);
				//Interpolate on the z axis
				return PowLerp(iy1, iy2, wz) * 1.6f;
			}
			else if(dimensions == 4)
			{
				//Interpolate on the x axis
				float ix111 = PowLerp(DotGrid(dimensions, pos, x1, y1, z1, w1), DotGrid(dimensions, pos, x2, y1, z1, w1), wx);
				float ix211 = PowLerp(DotGrid(dimensions, pos, x1, y2, z1, w1), DotGrid(dimensions, pos, x2, y2, z1, w1), wx);
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
				return PowLerp(iz1, iz2, ww) * 1.6f;
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
			local.x = Mathf.Repeat(local.x + 1, 2) - 1;
			local.y = Mathf.Repeat(local.y + 1, 2) - 1;
			local.z = Mathf.Repeat(local.z + 1, 2) - 1;
			local.w = Mathf.Repeat(local.w + 1, 2) - 1;
			return Vector4.Dot(local, vec);
		}

		private static float PowLerp(float a, float b, float t)
		{
			return Mathf.Pow(t, 2f) * (3f - 2f * t) * (b - a) + a;
		}

		private static Vector4 GetRandomDirectionVector(int dimensions, Vector4 i)
		{
			if(dimensions == 1)
			{
				return new Vector4(HashF(i.x), 0, 0, 0);
			}
			else if(dimensions == 2)
			{
				float h = HashF(i.x + 1738.3973f * i.y);
				return new Vector2(Mathf.Sin(h), Mathf.Cos(h));
			}
			else if(dimensions == 3)
			{
				float h1 = HashF(i);
				float h2 = HashF(i + new Vector4(1f, 0, 0, 0));
				float h3 = HashF(i + new Vector4(2f, 0, 0, 0));
				return new Vector3(Mathf.Sin(h1 * 1000f), Mathf.Sin(h2 * 1000f), Mathf.Sin(h3 * 1000f)).normalized;
			}
			else if(dimensions == 4)
			{
				float h1 = HashF(i);
				float h2 = HashF(i + new Vector4(1f, 0, 0, 0));
				float h3 = HashF(i + new Vector4(2f, 0, 0, 0));
				float h4 = HashF(i + new Vector4(3f, 0, 0, 0));
				return new Vector4(Mathf.Sin(h1 * 1000f), Mathf.Sin(h2 * 1000f), Mathf.Sin(h3 * 1000f), Mathf.Sin(h4 * 1000f)).normalized;
			}
			return Vector4.zero;
				/*
				float random = HashF(i); //(float)(2920f * Mathf.Sin(i.x * 21942f + i.y * 171324f + 8912f) * Mathf.Cos(i.x * 23157f * i.y * 217832f + 9758f));
				var vec2 = new Vector2(
					(float)Mathf.Sin(random),
					dimensions >= 2 ? (float)Mathf.Cos(random) : 0);
				if(dimensions <= 2)
				{
					return vec2;
				}
				else if(dimensions == 3)
				{
					float u = random;
					float v = HashF(random + 1f);
					float theta = 2f * Mathf.PI * u;
					float phi = Mathf.Acos(2 * v - 1);

					float x = (float)(Mathf.Sin(phi) * Mathf.Cos(theta));
					float y = (float)(Mathf.Sin(phi) * Mathf.Sin(theta));
					float z = (float)Mathf.Cos(phi);
					return new Vector3(x, y, z).normalized;
				}
				else
				{
					float u1 = random;
					float u2 = HashF(random + 1f);
					float u3 = HashF(random + 2f);
					float u4 = HashF(random + 3f);

					float theta1 = 2 * Mathf.PI * random;
					float theta2 = 2 * Mathf.PI * u2;
					float theta3 = 2 * Mathf.PI * u3;
					float theta4 = 2 * Mathf.PI * u4;

					float r1 = Mathf.Sqrt(1 - u1);
					float r2 = Mathf.Sqrt(u1);

					float x = (float)(r1 * Mathf.Cos(theta1));
					float y = (float)(r1 * Mathf.Sin(theta2));
					float z = (float)(r2 * Mathf.Cos(theta3));
					float w = (float)(r2 * Mathf.Sin(theta4));
					return new Vector4(x, y, z, w).normalized;
				}
				*/
			}

		private static float RandomGaussianDistribution(float seed)
		{
			float u1 = 1.0f - Random.Range(0.0f, 1.0f);
			float u2 = 1.0f - Random.Range(0.0f, 1.0f);
			float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
			return seed + randStdNormal;
		}
	}
}
