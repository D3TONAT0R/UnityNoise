using UnityEngine;

namespace UnityNoise
{
	public class CellularNoise : NoiseGeneratorBase<CellularNoise>
	{
		protected override float CalcNoise(int dimensions, Vector4 pos, FractalSettings settings, Vector4 repeat)
		{
			if(dimensions == 1)
			{
				int x1 = Mathf.FloorToInt(pos.x);
				int x2 = x1 + 1;
				float x = Mathf.SmoothStep(0, 1, pos.x - x1);
				float c0 = GetCellValue(x1, 0, 0, 0);
				float c1 = GetCellValue(x2, 0, 0, 0);
				return Mathf.Lerp(c0, c1, x);
			}
			else if(dimensions == 2)
			{
				int x1 = Mathf.FloorToInt(pos.x);
				int x2 = x1 + 1;
				int y1 = Mathf.FloorToInt(pos.y);
				int y2 = y1 + 1;
				float x = Mathf.SmoothStep(0, 1, pos.x - x1);
				float y = Mathf.SmoothStep(0, 1, pos.y - y1);
				float c00 = GetCellValue(x1, y1, 0, 0);
				float c10 = GetCellValue(x2, y1, 0, 0);
				float c01 = GetCellValue(x1, y2, 0, 0);
				float c11 = GetCellValue(x2, y2, 0, 0);
				float c0 = Mathf.Lerp(c00, c10, x);
				float c1 = Mathf.Lerp(c01, c11, x);
				return Mathf.Lerp(c0, c1, y);
			}
			else if(dimensions == 3)
			{
				int x1 = Mathf.FloorToInt(pos.x);
				int x2 = x1 + 1;
				int y1 = Mathf.FloorToInt(pos.y);
				int y2 = y1 + 1;
				int z1 = Mathf.FloorToInt(pos.z);
				int z2 = z1 + 1;
				float x = Mathf.SmoothStep(0, 1, pos.x - x1);
				float y = Mathf.SmoothStep(0, 1, pos.y - y1);
				float z = Mathf.SmoothStep(0, 1, pos.z - z1);
				float c000 = GetCellValue(x1, y1, z1, 0);
				float c100 = GetCellValue(x2, y1, z1, 0);
				float c010 = GetCellValue(x1, y2, z1, 0);
				float c110 = GetCellValue(x2, y2, z1, 0);
				float c001 = GetCellValue(x1, y1, z2, 0);
				float c101 = GetCellValue(x2, y1, z2, 0);
				float c011 = GetCellValue(x1, y2, z2, 0);
				float c111 = GetCellValue(x2, y2, z2, 0);
				float c00 = Mathf.Lerp(c000, c100, x);
				float c10 = Mathf.Lerp(c010, c110, x);
				float c01 = Mathf.Lerp(c001, c101, x);
				float c11 = Mathf.Lerp(c011, c111, x);
				float c0 = Mathf.Lerp(c00, c10, y);
				float c1 = Mathf.Lerp(c01, c11, y);
				return Mathf.Lerp(c0, c1, z);
			}
			else if(dimensions == 4)
			{
				int x1 = Mathf.FloorToInt(pos.x);
				int x2 = x1 + 1;
				int y1 = Mathf.FloorToInt(pos.y);
				int y2 = y1 + 1;
				int z1 = Mathf.FloorToInt(pos.z);
				int z2 = z1 + 1;
				int w1 = Mathf.FloorToInt(pos.w);
				int w2 = w1 + 1;
				float x = Mathf.SmoothStep(0, 1, pos.x - x1);
				float y = Mathf.SmoothStep(0, 1, pos.y - y1);
				float z = Mathf.SmoothStep(0, 1, pos.z - z1);
				float w = Mathf.SmoothStep(0, 1, pos.w - w1);
				float c0000 = GetCellValue(x1, y1, z1, w1);
				float c1000 = GetCellValue(x2, y1, z1, w1);
				float c0100 = GetCellValue(x1, y2, z1, w1);
				float c1100 = GetCellValue(x2, y2, z1, w1);
				float c0010 = GetCellValue(x1, y1, z2, w1);
				float c1010 = GetCellValue(x2, y1, z2, w1);
				float c0110 = GetCellValue(x1, y2, z2, w1);
				float c1110 = GetCellValue(x2, y2, z2, w1);
				float c0001 = GetCellValue(x1, y1, z1, w2);
				float c1001 = GetCellValue(x2, y1, z1, w2);
				float c0101 = GetCellValue(x1, y2, z1, w2);
				float c1101 = GetCellValue(x2, y2, z1, w2);
				float c0011 = GetCellValue(x1, y1, z2, w2);
				float c1011 = GetCellValue(x2, y1, z2, w2);
				float c0111 = GetCellValue(x1, y2, z2, w2);
				float c1111 = GetCellValue(x2, y2, z2, w2);
				float c000 = Mathf.Lerp(c0000, c1000, x);
				float c100 = Mathf.Lerp(c0100, c1100, x);
				float c010 = Mathf.Lerp(c0010, c1010, x);
				float c110 = Mathf.Lerp(c0110, c1110, x);
				float c001 = Mathf.Lerp(c0001, c1001, x);
				float c101 = Mathf.Lerp(c0101, c1101, x);
				float c011 = Mathf.Lerp(c0011, c1011, x);
				float c111 = Mathf.Lerp(c0111, c1111, x);
				float c00 = Mathf.Lerp(c000, c100, y);
				float c10 = Mathf.Lerp(c010, c110, y);
				float c01 = Mathf.Lerp(c001, c101, y);
				float c11 = Mathf.Lerp(c011, c111, y);
				float c0 = Mathf.Lerp(c00, c10, z);
				float c1 = Mathf.Lerp(c01, c11, z);
				return Mathf.Lerp(c0, c1, w);
			}
			else
			{
				return 0;
			}
		}

		private static float GetCellValue(int x, int y, int z, int w)
		{
			return Mathf.Sin(Hash(x, y, z, w));
		}
	}
}
