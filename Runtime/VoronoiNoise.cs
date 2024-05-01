using UnityEngine;
using UnityEngine.UIElements;

namespace UnityNoise
{
	public class VoronoiNoise : NoiseGeneratorBase<VoronoiNoise>
	{

		protected override float CalcNoise(int dimensions, Vector4 pos, NoiseParameters parameters, Vector4 wrap)
		{
			float d = parameters.voronoiParameters.voronoiDistortion * 0.8f;
			DistanceType dt = parameters.voronoiParameters.distanceType;
			float min = float.MaxValue;
			Vector2 nearestCell = Vector2.zero;
			if(dimensions == 1)
			{
				int x1 = Mathf.FloorToInt(pos.x);
				int x2 = x1 + 1;
				int x0 = x1 - 1;
				float c0 = GetCell(d, x0, 0, wrap, parameters.offset).x;
				float c1 = GetCell(d, x1, 0, wrap, parameters.offset).x;
				float c2 = GetCell(d, x2, 0, wrap, parameters.offset).x;
				float nearestCell1D = 0;
				min = Min1D(min, ref nearestCell1D, pos.x, c0, wrap.x);
				min = Min1D(min, ref nearestCell1D, pos.x, c1, wrap.x);
				min = Min1D(min, ref nearestCell1D, pos.x, c2, wrap.x);
				nearestCell = new Vector2(nearestCell1D, 0);
			}
			else if(dimensions == 2)
			{
				var pos2 = new Vector2(pos.x, pos.y);
				int x1 = Mathf.FloorToInt(pos.x);
				int y1 = Mathf.FloorToInt(pos.y);
				int x2 = x1 + 1;
				int y2 = y1 + 1;
				int x0 = x1 - 1;
				int y0 = y1 - 1;
				Vector2 c00 = GetCell(d, x0, y0, wrap, parameters.offset);
				Vector2 c10 = GetCell(d, x1, y0, wrap, parameters.offset);
				Vector2 c20 = GetCell(d, x2, y0, wrap, parameters.offset);
				Vector2 c01 = GetCell(d, x0, y1, wrap, parameters.offset);
				Vector2 c11 = GetCell(d, x1, y1, wrap, parameters.offset);
				Vector2 c21 = GetCell(d, x2, y1, wrap, parameters.offset);
				Vector2 c02 = GetCell(d, x0, y2, wrap, parameters.offset);
				Vector2 c12 = GetCell(d, x1, y2, wrap, parameters.offset);
				Vector2 c22 = GetCell(d, x2, y2, wrap, parameters.offset);
				min = Min2D(min, ref nearestCell, pos2, c00, dt, wrap);
				min = Min2D(min, ref nearestCell, pos2, c10, dt, wrap);
				min = Min2D(min, ref nearestCell, pos2, c20, dt, wrap);
				min = Min2D(min, ref nearestCell, pos2, c01, dt, wrap);
				min = Min2D(min, ref nearestCell, pos2, c11, dt, wrap);
				min = Min2D(min, ref nearestCell, pos2, c21, dt, wrap);
				min = Min2D(min, ref nearestCell, pos2, c02, dt, wrap);
				min = Min2D(min, ref nearestCell, pos2, c12, dt, wrap);
				min = Min2D(min, ref nearestCell, pos2, c22, dt, wrap);
			}
			else if(dimensions == 3)
			{
				var pos3 = new Vector3(pos.x, pos.y, pos.z);
				int x1 = Mathf.FloorToInt(pos.x);
				int y1 = Mathf.FloorToInt(pos.y);
				int z1 = Mathf.FloorToInt(pos.z);
				int x2 = x1 + 1;
				int y2 = y1 + 1;
				int z2 = z1 + 1;
				int x0 = x1 - 1;
				int y0 = y1 - 1;
				int z0 = z1 - 1;
				Vector3 c000 = GetCell(d, x0, y0, z0, wrap, parameters.offset);
				Vector3 c100 = GetCell(d, x1, y0, z0, wrap, parameters.offset);
				Vector3 c200 = GetCell(d, x2, y0, z0, wrap, parameters.offset);
				Vector3 c010 = GetCell(d, x0, y1, z0, wrap, parameters.offset);
				Vector3 c110 = GetCell(d, x1, y1, z0, wrap, parameters.offset);
				Vector3 c210 = GetCell(d, x2, y1, z0, wrap, parameters.offset);
				Vector3 c020 = GetCell(d, x0, y2, z0, wrap, parameters.offset);
				Vector3 c120 = GetCell(d, x1, y2, z0, wrap, parameters.offset);
				Vector3 c220 = GetCell(d, x2, y2, z0, wrap, parameters.offset);
				Vector3 c001 = GetCell(d, x0, y0, z1, wrap, parameters.offset);
				Vector3 c101 = GetCell(d, x1, y0, z1, wrap, parameters.offset);
				Vector3 c201 = GetCell(d, x2, y0, z1, wrap, parameters.offset);
				Vector3 c011 = GetCell(d, x0, y1, z1, wrap, parameters.offset);
				Vector3 c111 = GetCell(d, x1, y1, z1, wrap, parameters.offset);
				Vector3 c211 = GetCell(d, x2, y1, z1, wrap, parameters.offset);
				Vector3 c021 = GetCell(d, x0, y2, z1, wrap, parameters.offset);
				Vector3 c121 = GetCell(d, x1, y2, z1, wrap, parameters.offset);
				Vector3 c221 = GetCell(d, x2, y2, z1, wrap, parameters.offset);
				Vector3 c002 = GetCell(d, x0, y0, z2, wrap, parameters.offset);
				Vector3 c102 = GetCell(d, x1, y0, z2, wrap, parameters.offset);
				Vector3 c202 = GetCell(d, x2, y0, z2, wrap, parameters.offset);
				Vector3 c012 = GetCell(d, x0, y1, z2, wrap, parameters.offset);
				Vector3 c112 = GetCell(d, x1, y1, z2, wrap, parameters.offset);
				Vector3 c212 = GetCell(d, x2, y1, z2, wrap, parameters.offset);
				Vector3 c022 = GetCell(d, x0, y2, z2, wrap, parameters.offset);
				Vector3 c122 = GetCell(d, x1, y2, z2, wrap, parameters.offset);
				Vector3 c222 = GetCell(d, x2, y2, z2, wrap, parameters.offset);
				Vector3 nearestCell3D = Vector3.zero;
				min = Vector3.Distance(pos3, c000);
				min = Min3D(min, ref nearestCell3D, pos3, c100, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c200, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c010, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c110, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c210, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c020, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c120, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c220, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c001, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c101, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c201, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c011, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c111, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c211, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c021, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c121, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c221, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c002, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c102, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c202, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c012, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c112, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c212, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c022, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c122, dt, wrap);
				min = Min3D(min, ref nearestCell3D, pos3, c222, dt, wrap);
				nearestCell = nearestCell3D;
			}
			else if(dimensions == 4)
			{
				int x1 = Mathf.FloorToInt(pos.x);
				int y1 = Mathf.FloorToInt(pos.y);
				int z1 = Mathf.FloorToInt(pos.z);
				int w1 = Mathf.FloorToInt(pos.w);
				int x2 = x1 + 1;
				int y2 = y1 + 1;
				int z2 = z1 + 1;
				int w2 = w1 + 1;
				int x0 = x1 - 1;
				int y0 = y1 - 1;
				int z0 = z1 - 1;
				int w0 = w1 - 1;
				Vector4 c0000 = GetCell(d, x0, y0, z0, w0, wrap, parameters.offset);
				Vector4 c1000 = GetCell(d, x1, y0, z0, w0, wrap, parameters.offset);
				Vector4 c2000 = GetCell(d, x2, y0, z0, w0, wrap, parameters.offset);
				Vector4 c0100 = GetCell(d, x0, y1, z0, w0, wrap, parameters.offset);
				Vector4 c1100 = GetCell(d, x1, y1, z0, w0, wrap, parameters.offset);
				Vector4 c2100 = GetCell(d, x2, y1, z0, w0, wrap, parameters.offset);
				Vector4 c0200 = GetCell(d, x0, y2, z0, w0, wrap, parameters.offset);
				Vector4 c1200 = GetCell(d, x1, y2, z0, w0, wrap, parameters.offset);
				Vector4 c2200 = GetCell(d, x2, y2, z0, w0, wrap, parameters.offset);
				Vector4 c0010 = GetCell(d, x0, y0, z1, w0, wrap, parameters.offset);
				Vector4 c1010 = GetCell(d, x1, y0, z1, w0, wrap, parameters.offset);
				Vector4 c2010 = GetCell(d, x2, y0, z1, w0, wrap, parameters.offset);
				Vector4 c0110 = GetCell(d, x0, y1, z1, w0, wrap, parameters.offset);
				Vector4 c1110 = GetCell(d, x1, y1, z1, w0, wrap, parameters.offset);
				Vector4 c2110 = GetCell(d, x2, y1, z1, w0, wrap, parameters.offset);
				Vector4 c0210 = GetCell(d, x0, y2, z1, w0, wrap, parameters.offset);
				Vector4 c1210 = GetCell(d, x1, y2, z1, w0, wrap, parameters.offset);
				Vector4 c2210 = GetCell(d, x2, y2, z1, w0, wrap, parameters.offset);
				Vector4 c0020 = GetCell(d, x0, y0, z2, w0, wrap, parameters.offset);
				Vector4 c1020 = GetCell(d, x1, y0, z2, w0, wrap, parameters.offset);
				Vector4 c2020 = GetCell(d, x2, y0, z2, w0, wrap, parameters.offset);
				Vector4 c0120 = GetCell(d, x0, y1, z2, w0, wrap, parameters.offset);
				Vector4 c1120 = GetCell(d, x1, y1, z2, w0, wrap, parameters.offset);
				Vector4 c2120 = GetCell(d, x2, y1, z2, w0, wrap, parameters.offset);
				Vector4 c0220 = GetCell(d, x0, y2, z2, w0, wrap, parameters.offset);
				Vector4 c1220 = GetCell(d, x1, y2, z2, w0, wrap, parameters.offset);
				Vector4 c2220 = GetCell(d, x2, y2, z2, w0, wrap, parameters.offset);
				Vector4 c0001 = GetCell(d, x0, y0, z0, w1, wrap, parameters.offset);
				Vector4 c1001 = GetCell(d, x1, y0, z0, w1, wrap, parameters.offset);
				Vector4 c2001 = GetCell(d, x2, y0, z0, w1, wrap, parameters.offset);
				Vector4 c0101 = GetCell(d, x0, y1, z0, w1, wrap, parameters.offset);
				Vector4 c1101 = GetCell(d, x1, y1, z0, w1, wrap, parameters.offset);
				Vector4 c2101 = GetCell(d, x2, y1, z0, w1, wrap, parameters.offset);
				Vector4 c0201 = GetCell(d, x0, y2, z0, w1, wrap, parameters.offset);
				Vector4 c1201 = GetCell(d, x1, y2, z0, w1, wrap, parameters.offset);
				Vector4 c2201 = GetCell(d, x2, y2, z0, w1, wrap, parameters.offset);
				Vector4 c0011 = GetCell(d, x0, y0, z1, w1, wrap, parameters.offset);
				Vector4 c1011 = GetCell(d, x1, y0, z1, w1, wrap, parameters.offset);
				Vector4 c2011 = GetCell(d, x2, y0, z1, w1, wrap, parameters.offset);
				Vector4 c0111 = GetCell(d, x0, y1, z1, w1, wrap, parameters.offset);
				Vector4 c1111 = GetCell(d, x1, y1, z1, w1, wrap, parameters.offset);
				Vector4 c2111 = GetCell(d, x2, y1, z1, w1, wrap, parameters.offset);
				Vector4 c0211 = GetCell(d, x0, y2, z1, w1, wrap, parameters.offset);
				Vector4 c1211 = GetCell(d, x1, y2, z1, w1, wrap, parameters.offset);
				Vector4 c2211 = GetCell(d, x2, y2, z1, w1, wrap, parameters.offset);
				Vector4 c0021 = GetCell(d, x0, y0, z2, w1, wrap, parameters.offset);
				Vector4 c1021 = GetCell(d, x1, y0, z2, w1, wrap, parameters.offset);
				Vector4 c2021 = GetCell(d, x2, y0, z2, w1, wrap, parameters.offset);
				Vector4 c0121 = GetCell(d, x0, y1, z2, w1, wrap, parameters.offset);
				Vector4 c1121 = GetCell(d, x1, y1, z2, w1, wrap, parameters.offset);
				Vector4 c2121 = GetCell(d, x2, y1, z2, w1, wrap, parameters.offset);
				Vector4 c0221 = GetCell(d, x0, y2, z2, w1, wrap, parameters.offset);
				Vector4 c1221 = GetCell(d, x1, y2, z2, w1, wrap, parameters.offset);
				Vector4 c2221 = GetCell(d, x2, y2, z2, w1, wrap, parameters.offset);
				Vector4 c0002 = GetCell(d, x0, y0, z0, w2, wrap, parameters.offset);
				Vector4 c1002 = GetCell(d, x1, y0, z0, w2, wrap, parameters.offset);
				Vector4 c2002 = GetCell(d, x2, y0, z0, w2, wrap, parameters.offset);
				Vector4 c0102 = GetCell(d, x0, y1, z0, w2, wrap, parameters.offset);
				Vector4 c1102 = GetCell(d, x1, y1, z0, w2, wrap, parameters.offset);
				Vector4 c2102 = GetCell(d, x2, y1, z0, w2, wrap, parameters.offset);
				Vector4 c0202 = GetCell(d, x0, y2, z0, w2, wrap, parameters.offset);
				Vector4 c1202 = GetCell(d, x1, y2, z0, w2, wrap, parameters.offset);
				Vector4 c2202 = GetCell(d, x2, y2, z0, w2, wrap, parameters.offset);
				Vector4 c0012 = GetCell(d, x0, y0, z1, w2, wrap, parameters.offset);
				Vector4 c1012 = GetCell(d, x1, y0, z1, w2, wrap, parameters.offset);
				Vector4 c2012 = GetCell(d, x2, y0, z1, w2, wrap, parameters.offset);
				Vector4 c0112 = GetCell(d, x0, y1, z1, w2, wrap, parameters.offset);
				Vector4 c1112 = GetCell(d, x1, y1, z1, w2, wrap, parameters.offset);
				Vector4 c2112 = GetCell(d, x2, y1, z1, w2, wrap, parameters.offset);
				Vector4 c0212 = GetCell(d, x0, y2, z1, w2, wrap, parameters.offset);
				Vector4 c1212 = GetCell(d, x1, y2, z1, w2, wrap, parameters.offset);
				Vector4 c2212 = GetCell(d, x2, y2, z1, w2, wrap, parameters.offset);
				Vector4 c0022 = GetCell(d, x0, y0, z2, w2, wrap, parameters.offset);
				Vector4 c1022 = GetCell(d, x1, y0, z2, w2, wrap, parameters.offset);
				Vector4 c2022 = GetCell(d, x2, y0, z2, w2, wrap, parameters.offset);
				Vector4 c0122 = GetCell(d, x0, y1, z2, w2, wrap, parameters.offset);
				Vector4 c1122 = GetCell(d, x1, y1, z2, w2, wrap, parameters.offset);
				Vector4 c2122 = GetCell(d, x2, y1, z2, w2, wrap, parameters.offset);
				Vector4 c0222 = GetCell(d, x0, y2, z2, w2, wrap, parameters.offset);
				Vector4 c1222 = GetCell(d, x1, y2, z2, w2, wrap, parameters.offset);
				Vector4 c2222 = GetCell(d, x2, y2, z2, w2, wrap, parameters.offset);
				Vector4 nearestCell4D = Vector4.zero;
				min = Vector4.Distance(pos, c0000);
				min = Min4D(min, ref nearestCell4D, pos, c1000, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2000, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0100, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1100, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2100, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0200, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1200, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2200, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0010, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1010, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2010, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0110, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1110, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2110, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0210, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1210, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2210, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0020, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1020, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2020, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0120, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1120, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2120, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0220, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1220, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2220, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0001, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1001, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2001, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0101, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1101, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2101, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0201, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1201, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2201, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0011, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1011, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2011, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0111, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1111, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2111, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0211, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1211, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2211, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0021, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1021, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2021, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0121, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1121, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2121, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0221, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1221, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2221, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0002, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1002, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2002, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0102, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1102, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2102, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0202, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1202, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2202, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0012, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1012, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2012, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0112, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1112, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2112, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0212, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1212, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2212, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0022, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1022, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2022, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0122, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1122, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2122, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c0222, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c1222, dt, wrap);
				min = Min4D(min, ref nearestCell4D, pos, c2222, dt, wrap);
				nearestCell = nearestCell4D;
			}
			else
			{
				min = 0;
			}
			if(parameters.voronoiParameters.voronoiType == VoronoiType.Distance)
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

		private Vector4 GetCell(float d, int x, int y, int z, int w, Vector4 repeat, Vector4 offset)
		{
			if(repeat != Vector4.zero)
			{
				x = WrapCell(x, repeat.x, offset.x);
				y = WrapCell(y, repeat.y, offset.y);
				z = WrapCell(z, repeat.z, offset.z);
				w = WrapCell(w, repeat.w, offset.w);
			}
			var hashX = Hash(x * 213 + y * 519 + z * 17 + w * 13) * 0.5f * d;
			var hashY = Hash(x * 23 + y * 127 + z * 337 + w * 7) * 0.5f * d;
			var hashZ = Hash(x * 223 + y * 31 + z * 213 + w * 3) * 0.5f * d;
			var hashW = Hash(x * 13 + y * 17 + z * 19 + w * 23) * 0.5f * d;
			return new Vector4(x + hashX, y + hashY, z + hashZ, w + hashW);
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
				else if(distanceType == DistanceType.Manhattan) return Mathf.Abs(b.x - a.x) + Mathf.Abs(b.y - a.y) + Mathf.Abs(b.z - a.z);
				else if(distanceType == DistanceType.Chebyshev) return Mathf.Max(Mathf.Abs(b.x - a.x), Mathf.Max(Mathf.Abs(b.y - a.y), Mathf.Abs(b.z - a.z)));
			}
			return 0;
		}

		private float Distance(Vector4 a, Vector4 b, DistanceType distanceType, Vector4 repeat)
		{
			if(repeat != Vector4.zero)
			{
				Vector4 d = new Vector4(Mathf.Abs(b.x - a.x), Mathf.Abs(b.y - a.y), Mathf.Abs(b.z - a.z), Mathf.Abs(b.w - a.w));
				if(d.x > repeat.x * 0.5f) d.x = repeat.x - d.x;
				if(d.y > repeat.y * 0.5f) d.y = repeat.y - d.y;
				if(d.z > repeat.z * 0.5f) d.z = repeat.z - d.z;
				if(d.w > repeat.w * 0.5f) d.w = repeat.w - d.w;
				if(distanceType == DistanceType.Euclidean) return d.sqrMagnitude;
				else if(distanceType == DistanceType.Manhattan) return d.x + d.y + d.z + d.w;
				else if(distanceType == DistanceType.Chebyshev) return Mathf.Max(d.x, Mathf.Max(d.y, Mathf.Max(d.z, d.w)));
			}
			else
			{
				if(distanceType == DistanceType.Euclidean) return (b - a).sqrMagnitude;
				else if(distanceType == DistanceType.Manhattan) return Mathf.Abs(b.x - a.x) + Mathf.Abs(b.y - a.y) + Mathf.Abs(b.z - a.z) + Mathf.Abs(b.w - a.w);
				else if(distanceType == DistanceType.Chebyshev) return Mathf.Max(Mathf.Abs(b.x - a.x), Mathf.Max(Mathf.Abs(b.y - a.y), Mathf.Max(Mathf.Abs(b.z - a.z), Mathf.Abs(b.w - a.w))));
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

		private float Min4D(float min, ref Vector4 nearestCell, Vector4 pos, Vector4 cell, DistanceType dt, Vector4 repeat)
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