using UnityEngine;
using UnityEngine.UIElements;

namespace UnityNoise
{
	public class VoronoiNoise : NoiseGeneratorBase<VoronoiNoise>
	{

		protected override float CalcNoise(int dimensions, Vector4 pos, FractalSettings settings, Vector4 repeat)
		{
			float d = settings.voronoiSettings.voronoiDistortion * 0.8f;
			DistanceType dt = settings.voronoiSettings.distanceType;
			float min = float.MaxValue;
			Vector2 nearestCell = Vector2.zero;
			if(dimensions == 1)
			{
				int x1 = (int)pos.x;
				int x2 = x1 + 1;
				int x0 = x1 - 1;
				float c0 = GetCell(d, x0, 0, repeat, settings.offset).x;
				float c1 = GetCell(d, x1, 0, repeat, settings.offset).x;
				float c2 = GetCell(d, x2, 0, repeat, settings.offset).x;
				float nearestCell1D = 0;
				min = Min1D(min, ref nearestCell1D, pos.x, c0, repeat.x);
				min = Min1D(min, ref nearestCell1D, pos.x, c1, repeat.x);
				min = Min1D(min, ref nearestCell1D, pos.x, c2, repeat.x);
				nearestCell = new Vector2(nearestCell1D, 0);
			}
			else if(dimensions == 2)
			{
				var pos2 = new Vector2(pos.x, pos.y);
				int x1 = (int)pos.x;
				int y1 = (int)pos.y;
				int x2 = x1 + 1;
				int y2 = y1 + 1;
				int x0 = x1 - 1;
				int y0 = y1 - 1;
				Vector2 c00 = GetCell(d, x0, y0, repeat, settings.offset);
				Vector2 c10 = GetCell(d, x1, y0, repeat, settings.offset);
				Vector2 c20 = GetCell(d, x2, y0, repeat, settings.offset);
				Vector2 c01 = GetCell(d, x0, y1, repeat, settings.offset);
				Vector2 c11 = GetCell(d, x1, y1, repeat, settings.offset);
				Vector2 c21 = GetCell(d, x2, y1, repeat, settings.offset);
				Vector2 c02 = GetCell(d, x0, y2, repeat, settings.offset);
				Vector2 c12 = GetCell(d, x1, y2, repeat, settings.offset);
				Vector2 c22 = GetCell(d, x2, y2, repeat, settings.offset);
				min = Min2D(min, ref nearestCell, pos2, c00, dt, repeat);
				min = Min2D(min, ref nearestCell, pos2, c10, dt, repeat);
				min = Min2D(min, ref nearestCell, pos2, c20, dt, repeat);
				min = Min2D(min, ref nearestCell, pos2, c01, dt, repeat);
				min = Min2D(min, ref nearestCell, pos2, c11, dt, repeat);
				min = Min2D(min, ref nearestCell, pos2, c21, dt, repeat);
				min = Min2D(min, ref nearestCell, pos2, c02, dt, repeat);
				min = Min2D(min, ref nearestCell, pos2, c12, dt, repeat);
				min = Min2D(min, ref nearestCell, pos2, c22, dt, repeat);
			}
			else if(dimensions == 3)
			{
				var pos3 = new Vector3(pos.x, pos.y, pos.z);
				int x1 = (int)pos.x;
				int y1 = (int)pos.y;
				int z1 = (int)pos.z;
				int x2 = x1 + 1;
				int y2 = y1 + 1;
				int z2 = z1 + 1;
				int x0 = x1 - 1;
				int y0 = y1 - 1;
				int z0 = z1 - 1;
				Vector3 c000 = GetCell(d, x0, y0, z0, repeat, settings.offset);
				Vector3 c100 = GetCell(d, x1, y0, z0, repeat, settings.offset);
				Vector3 c200 = GetCell(d, x2, y0, z0, repeat, settings.offset);
				Vector3 c010 = GetCell(d, x0, y1, z0, repeat, settings.offset);
				Vector3 c110 = GetCell(d, x1, y1, z0, repeat, settings.offset);
				Vector3 c210 = GetCell(d, x2, y1, z0, repeat, settings.offset);
				Vector3 c020 = GetCell(d, x0, y2, z0, repeat, settings.offset);
				Vector3 c120 = GetCell(d, x1, y2, z0, repeat, settings.offset);
				Vector3 c220 = GetCell(d, x2, y2, z0, repeat, settings.offset);
				Vector3 c001 = GetCell(d, x0, y0, z1, repeat, settings.offset);
				Vector3 c101 = GetCell(d, x1, y0, z1, repeat, settings.offset);
				Vector3 c201 = GetCell(d, x2, y0, z1, repeat, settings.offset);
				Vector3 c011 = GetCell(d, x0, y1, z1, repeat, settings.offset);
				Vector3 c111 = GetCell(d, x1, y1, z1, repeat, settings.offset);
				Vector3 c211 = GetCell(d, x2, y1, z1, repeat, settings.offset);
				Vector3 c021 = GetCell(d, x0, y2, z1, repeat, settings.offset);
				Vector3 c121 = GetCell(d, x1, y2, z1, repeat, settings.offset);
				Vector3 c221 = GetCell(d, x2, y2, z1, repeat, settings.offset);
				Vector3 c002 = GetCell(d, x0, y0, z2, repeat, settings.offset);
				Vector3 c102 = GetCell(d, x1, y0, z2, repeat, settings.offset);
				Vector3 c202 = GetCell(d, x2, y0, z2, repeat, settings.offset);
				Vector3 c012 = GetCell(d, x0, y1, z2, repeat, settings.offset);
				Vector3 c112 = GetCell(d, x1, y1, z2, repeat, settings.offset);
				Vector3 c212 = GetCell(d, x2, y1, z2, repeat, settings.offset);
				Vector3 c022 = GetCell(d, x0, y2, z2, repeat, settings.offset);
				Vector3 c122 = GetCell(d, x1, y2, z2, repeat, settings.offset);
				Vector3 c222 = GetCell(d, x2, y2, z2, repeat, settings.offset);
				Vector3 nearestCell3D = Vector3.zero;
				min = Vector3.Distance(pos3, c000);
				min = Min3D(min, ref nearestCell3D, pos3, c100, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c200, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c010, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c110, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c210, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c020, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c120, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c220, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c001, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c101, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c201, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c011, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c111, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c211, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c021, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c121, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c221, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c002, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c102, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c202, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c012, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c112, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c212, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c022, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c122, dt, repeat);
				min = Min3D(min, ref nearestCell3D, pos3, c222, dt, repeat);
				nearestCell = nearestCell3D;
			}
			else
			{
				min = 0;
			}
			if(settings.voronoiSettings.voronoiType == VoronoiType.Distance)
			{
				return (dt == DistanceType.Euclidean ? Mathf.Sqrt(min) : min) * 2.25f - 1f;
			}
			else
			{
				return Hash((int)(nearestCell.x * 127.31f), (int)(nearestCell.y * 127.31f), 0, 0);
			}
		}

		private Vector2 GetCell(float d, int x, int y, Vector4 repeat, Vector4 offset)
		{
			if(repeat != Vector4.zero)
			{
				x = WrapCell(x, repeat.x, offset.x);
				y = WrapCell(y, repeat.y, offset.y);
			}
			var hashX = Hash(x * 4213 + y * 2719) * 0.5f * d;
			var hashY = Hash(x * 23 + y * 127) * 0.5f * d;
			return new Vector2(x + hashX, y + hashY);
		}

		private Vector3 GetCell(float d, int x, int y, int z, Vector4 repeat, Vector4 offset)
		{
			if(repeat != Vector4.zero)
			{
				x = WrapCell(x, repeat.x, offset.x);
				y = WrapCell(y, repeat.y, offset.y);
				z = WrapCell(z, repeat.z, offset.z);
			}
			var hashX = Hash(x * 213 + y * 519 + z * 17) * 0.5f * d;
			var hashY = Hash(x * 23 + y * 127 + z * 337) * 0.5f * d;
			var hashZ = Hash(x * 223 + y * 31 + z * 213) * 0.5f * d;
			return new Vector3(x + hashX, y + hashY, z + hashZ);
		}

		private float Distance(float a, float b, float repeat)
		{
			if(repeat != 0)
			{
				float d = Mathf.Abs(b - a);
				if(d > repeat * 0.5f) d = repeat - d;
				return d;
			}
			else
			{
				return Mathf.Abs(b - a);
			}
		}

		private float Distance(Vector2 a, Vector2 b, DistanceType distanceType, Vector4 repeat)
		{
			if(repeat != Vector4.zero)
			{
				Vector2 d = new Vector2(Mathf.Abs(b.x - a.x), Mathf.Abs(b.y - a.y));
				if(d.x > repeat.x * 0.5f) d.x = repeat.x - d.x;
				if(d.y > repeat.y * 0.5f) d.y = repeat.y - d.y;
				if(distanceType == DistanceType.Euclidean) return d.sqrMagnitude;
				else if(distanceType == DistanceType.Manhattan) return d.x + d.y;
				else if(distanceType == DistanceType.Chebyshev) return Mathf.Max(d.x, d.y);
			}
			else
			{
				if(distanceType == DistanceType.Euclidean) return (b - a).sqrMagnitude;
				else if(distanceType == DistanceType.Manhattan) return Mathf.Abs(b.x - a.x) + Mathf.Abs(b.y - a.y);
				else if(distanceType == DistanceType.Chebyshev) return Mathf.Max(Mathf.Abs(b.x - a.x), Mathf.Max(Mathf.Abs(b.y - a.y)));
			}
			return 0;
		}

		private float Distance(Vector3 a, Vector3 b, DistanceType distanceType, Vector4 repeat)
		{
			if(repeat != Vector4.zero)
			{
				Vector3 d = new Vector3(Mathf.Abs(b.x - a.x), Mathf.Abs(b.y - a.y), Mathf.Abs(b.z - a.z));
				if(d.x > repeat.x * 0.5f) d.x = repeat.x - d.x;
				if(d.y > repeat.y * 0.5f) d.y = repeat.y - d.y;
				if(d.z > repeat.z * 0.5f) d.z = repeat.z - d.z;
				if(distanceType == DistanceType.Euclidean) return d.sqrMagnitude;
				else if(distanceType == DistanceType.Manhattan) return d.x + d.y + d.z;
				else if(distanceType == DistanceType.Chebyshev) return Mathf.Max(d.x, Mathf.Max(d.y, d.z));
			}
			else
			{
				if(distanceType == DistanceType.Euclidean) return (b - a).sqrMagnitude;
				else if(distanceType == DistanceType.Manhattan) return Mathf.Abs(b.x - a.x) + Mathf.Abs(b.y - a.y);
				else if(distanceType == DistanceType.Chebyshev) return Mathf.Max(Mathf.Abs(b.x - a.x), Mathf.Max(Mathf.Abs(b.y - a.y)));
			}
			return 0;
		}

		private float Min1D(float min, ref float nearestCell, float pos, float cell, float repeat)
		{
			float d = Distance(cell, pos, repeat);
			if(d < min)
			{
				min = d;
				nearestCell = cell;
			}
			return min;
		}

		private float Min2D(float min, ref Vector2 nearestCell, Vector2 pos, Vector2 cell, DistanceType dt, Vector4 repeat)
		{
			float d = Distance(cell, pos, dt, repeat);
			if(d < min)
			{
				min = d;
				nearestCell = cell;
			}
			return min;
		}

		private float Min3D(float min, ref Vector3 nearestCell, Vector3 pos, Vector3 cell, DistanceType dt, Vector4 repeat)
		{
			float d = Distance(cell, pos, dt, repeat);
			if(d < min)
			{
				min = d;
				nearestCell = cell;
			}
			return min;
		}
	}
}