using UnityEngine;

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
				Vector2 c0 = GetCell(d, x0, 0);
				Vector2 c1 = GetCell(d, x1, 0);
				Vector2 c2 = GetCell(d, x2, 0);
				min = Min(min, ref nearestCell, new Vector2(pos.x, 0), c0, dt);
				min = Min(min, ref nearestCell, new Vector2(pos.x, 0), c1, dt);
				min = Min(min, ref nearestCell, new Vector2(pos.x, 0), c2, dt);
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
				Vector2 c00 = GetCell(d, x0, y0);
				Vector2 c10 = GetCell(d, x1, y0);
				Vector2 c20 = GetCell(d, x2, y0);
				Vector2 c01 = GetCell(d, x0, y1);
				Vector2 c11 = GetCell(d, x1, y1);
				Vector2 c21 = GetCell(d, x2, y1);
				Vector2 c02 = GetCell(d, x0, y2);
				Vector2 c12 = GetCell(d, x1, y2);
				Vector2 c22 = GetCell(d, x2, y2);
				min = Min(min, ref nearestCell, pos2, c00, dt);
				min = Min(min, ref nearestCell, pos2, c10, dt);
				min = Min(min, ref nearestCell, pos2, c20, dt);
				min = Min(min, ref nearestCell, pos2, c01, dt);
				min = Min(min, ref nearestCell, pos2, c11, dt);
				min = Min(min, ref nearestCell, pos2, c21, dt);
				min = Min(min, ref nearestCell, pos2, c02, dt);
				min = Min(min, ref nearestCell, pos2, c12, dt);
				min = Min(min, ref nearestCell, pos2, c22, dt);
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
				Vector3 c000 = GetCell(d, x0, y0, z0);
				Vector3 c100 = GetCell(d, x1, y0, z0);
				Vector3 c200 = GetCell(d, x2, y0, z0);
				Vector3 c010 = GetCell(d, x0, y1, z0);
				Vector3 c110 = GetCell(d, x1, y1, z0);
				Vector3 c210 = GetCell(d, x2, y1, z0);
				Vector3 c020 = GetCell(d, x0, y2, z0);
				Vector3 c120 = GetCell(d, x1, y2, z0);
				Vector3 c220 = GetCell(d, x2, y2, z0);
				Vector3 c001 = GetCell(d, x0, y0, z1);
				Vector3 c101 = GetCell(d, x1, y0, z1);
				Vector3 c201 = GetCell(d, x2, y0, z1);
				Vector3 c011 = GetCell(d, x0, y1, z1);
				Vector3 c111 = GetCell(d, x1, y1, z1);
				Vector3 c211 = GetCell(d, x2, y1, z1);
				Vector3 c021 = GetCell(d, x0, y2, z1);
				Vector3 c121 = GetCell(d, x1, y2, z1);
				Vector3 c221 = GetCell(d, x2, y2, z1);
				Vector3 c002 = GetCell(d, x0, y0, z2);
				Vector3 c102 = GetCell(d, x1, y0, z2);
				Vector3 c202 = GetCell(d, x2, y0, z2);
				Vector3 c012 = GetCell(d, x0, y1, z2);
				Vector3 c112 = GetCell(d, x1, y1, z2);
				Vector3 c212 = GetCell(d, x2, y1, z2);
				Vector3 c022 = GetCell(d, x0, y2, z2);
				Vector3 c122 = GetCell(d, x1, y2, z2);
				Vector3 c222 = GetCell(d, x2, y2, z2);
				min = Vector3.Distance(pos3, c000);
				min = Mathf.Min(min, Distance(pos3, c100, dt));
				min = Mathf.Min(min, Distance(pos3, c200, dt));
				min = Mathf.Min(min, Distance(pos3, c010, dt));
				min = Mathf.Min(min, Distance(pos3, c110, dt));
				min = Mathf.Min(min, Distance(pos3, c210, dt));
				min = Mathf.Min(min, Distance(pos3, c020, dt));
				min = Mathf.Min(min, Distance(pos3, c120, dt));
				min = Mathf.Min(min, Distance(pos3, c220, dt));
				min = Mathf.Min(min, Distance(pos3, c001, dt));
				min = Mathf.Min(min, Distance(pos3, c101, dt));
				min = Mathf.Min(min, Distance(pos3, c201, dt));
				min = Mathf.Min(min, Distance(pos3, c011, dt));
				min = Mathf.Min(min, Distance(pos3, c111, dt));
				min = Mathf.Min(min, Distance(pos3, c211, dt));
				min = Mathf.Min(min, Distance(pos3, c021, dt));
				min = Mathf.Min(min, Distance(pos3, c121, dt));
				min = Mathf.Min(min, Distance(pos3, c221, dt));
				min = Mathf.Min(min, Distance(pos3, c002, dt));
				min = Mathf.Min(min, Distance(pos3, c102, dt));
				min = Mathf.Min(min, Distance(pos3, c202, dt));
				min = Mathf.Min(min, Distance(pos3, c012, dt));
				min = Mathf.Min(min, Distance(pos3, c112, dt));
				min = Mathf.Min(min, Distance(pos3, c212, dt));
				min = Mathf.Min(min, Distance(pos3, c022, dt));
				min = Mathf.Min(min, Distance(pos3, c122, dt));
				min = Mathf.Min(min, Distance(pos3, c222, dt));
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

		private Vector2 GetCell(float d, int x, int y)
		{
			var hashX = Hash(x * 4213 + y * 2719) * 0.5f * d;
			var hashY = Hash(x * 23 + y * 127) * 0.5f * d;
			return new Vector2(x + hashX, y + hashY);
		}

		private Vector3 GetCell(float d, int x, int y, int z)
		{
			var hashX = Hash(x * 213 + y * 519 + z * 17) * 0.5f * d;
			var hashY = Hash(x * 23 + y * 127 + z * 337) * 0.5f * d;
			var hashZ = Hash(x * 223 + y * 31 + z * 213) * 0.5f * d;
			return new Vector3(x + hashX, y + hashY, z + hashZ);
		}

		private float Distance(Vector2 a, Vector2 b, DistanceType distanceType)
		{
			if(distanceType == DistanceType.Euclidean)
			{
				return (b - a).sqrMagnitude;
			}
			else if(distanceType == DistanceType.Manhattan)
			{
				return Mathf.Abs(b.x - a.x) + Mathf.Abs(b.y - a.y);
			}
			else if(distanceType == DistanceType.Chebyshev)
			{
				return Mathf.Max(Mathf.Abs(b.x - a.x), Mathf.Abs(b.y - a.y));
			}
			else
			{
				return 0;
			}
		}

		private float Distance(Vector3 a, Vector3 b, DistanceType distanceType)
		{
			if(distanceType == DistanceType.Euclidean)
			{
				return (b - a).sqrMagnitude;
			}
			else if(distanceType == DistanceType.Manhattan)
			{
				return Mathf.Abs(b.x - a.x) + Mathf.Abs(b.y - a.y) + Mathf.Abs(b.z - a.z);
			}
			else if(distanceType == DistanceType.Chebyshev)
			{
				return Mathf.Max(Mathf.Abs(b.x - a.x), Mathf.Max(Mathf.Abs(b.y - a.y), Mathf.Abs(b.z - a.z)));
			}
			else
			{
				return 0;
			}
		}

		private float Min(float min, ref Vector2 nearestCell, Vector2 pos, Vector2 cell, DistanceType dt)
		{
			float d = Distance(cell, pos, dt);
			if(d < min)
			{
				min = d;
				nearestCell = cell;
			}
			return min;
		}

		private float Min(float min, ref Vector3 nearestCell, Vector3 pos, Vector3 cell, DistanceType dt)
		{
			float d = Distance(cell, pos, dt);
			if(d < min)
			{
				min = d;
				nearestCell = cell;
			}
			return min;
		}
	}
}