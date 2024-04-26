using UnityNoise;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;
#if UNITY_2020_2_OR_NEWER
using UnityEditor.AssetImporters;
#else
using UnityEditor.Experimental.AssetImporters;
#endif

namespace UnityNoiseEditor
{
	[ScriptedImporter(0, "noise")]
	public class NoiseTextureImporter : ScriptedImporter
	{
		public enum CompressionLevel
		{
			None = -1,
			HighQuality = 100,
			NormalQuality = 50,
			LowQuality = 0
		}

		public enum NoiseType
		{
			Perlin = 0,
			Simplex = 1,
			Cellular = 2,
			Voronoi = 3
		}

		public const int HISTOGRAM_BANDS = 64;

		public Vector2Int resolution = new Vector2Int(128, 128);

		public NoiseType noiseType = NoiseType.Perlin;
		[Range(1, 8)]
		public int octaves = 1;
		[Min(1.1f)]
		public float lacunarity = 2;
		[Range(0, 1)]
		public float persistence = 0.5f;
		public Vector2 scale = new Vector2(8, 8);
		[Range(0.01f, 1)]
		public float depth = 1f;
		public VoronoiSettings voronoiSettings = VoronoiSettings.Default;

		public int seed = 0;

		public Gradient gradient = new Gradient() { colorKeys = new GradientColorKey[] { new GradientColorKey(Color.black, 0), new GradientColorKey(Color.white, 1) } };
		public bool showOutOfRangeValues = false;

		[Space(10)]
		public bool linear = false;
		public bool generateMipMaps = true;
		public TextureWrapMode wrapMode = TextureWrapMode.Clamp;
		public FilterMode filterMode = FilterMode.Bilinear;
		public CompressionLevel compression = CompressionLevel.NormalQuality;

		[HideInInspector]
		public float[] histogram;

		public override void OnImportAsset(AssetImportContext ctx)
		{
			resolution.x = Mathf.Clamp(resolution.x, 1, 4096);
			resolution.y = Mathf.Clamp(resolution.y, 1, 4096);

			Random.InitState(seed);
			var offset = new Vector2(Random.value * short.MaxValue, Random.value * short.MaxValue);
			Vector2 actualScale = scale;
			actualScale.x *= resolution.x / (float)resolution.y;
			var settings = new FractalSettings(octaves, lacunarity, persistence, actualScale, offset, depth, voronoiSettings, false);
			int width = resolution.x;
			float fwidth = resolution.x;
			float fheight = resolution.y;
			Color[] colors = new Color[resolution.x * resolution.y];

			int[] histogramBands = new int[HISTOGRAM_BANDS];
			Parallel.For(0, resolution.y, y =>
			{
				for(int x = 0; x < resolution.x; x++)
				{
					var n = GetNoise(new Vector2(x / fwidth, y / fheight), settings);
					colors[y * width + x] = gradient.Evaluate(n);
					if(showOutOfRangeValues)
					{
						if(n < 0) colors[y * width + x] = Color.red;
						else if(n > 1) colors[y * width + x] = Color.blue;
					}
					int band = Mathf.FloorToInt(Mathf.Clamp((int)(n * HISTOGRAM_BANDS), 0, HISTOGRAM_BANDS - 1));
					histogramBands[band]++;
				}
			});
			histogram = new float[HISTOGRAM_BANDS];
			for(int i = 0; i < HISTOGRAM_BANDS; i++)
			{
				histogram[i] = histogramBands[i] / (float)(resolution.x * resolution.y);
			}

			var texture = new Texture2D(resolution.x, resolution.y, TextureFormat.RGBA32, generateMipMaps, linear);
			texture.alphaIsTransparency = true;
			texture.wrapMode = wrapMode;
			texture.filterMode = filterMode;
			texture.SetPixels(colors);
			texture.Apply();
			if(compression != CompressionLevel.None)
			{
				var q = compression == CompressionLevel.HighQuality ? TextureCompressionQuality.Best
					: compression == CompressionLevel.NormalQuality ? TextureCompressionQuality.Normal
					: TextureCompressionQuality.Fast;
				EditorUtility.CompressTexture(texture, TextureFormat.DXT5, q);
			}
			ctx.AddObjectToAsset("texture", texture);
		}

		private float GetNoise(Vector2 pos, FractalSettings settings)
		{
			switch(noiseType)
			{
				case NoiseType.Perlin:
					return PerlinNoise.Instance.GetNoise2D(pos, settings);
				case NoiseType.Simplex:
					return SimplexNoise.Instance.GetNoise2D(pos, settings);
				case NoiseType.Cellular:
					return CellularNoise.Instance.GetNoise2D(pos, settings);
				case NoiseType.Voronoi:
					return VoronoiNoise.Instance.GetNoise2D(pos, settings);
				default: return 0;
			}
		}

		[MenuItem("Assets/Create/Texture2D/Perlin Noise Texture")]
		public static void CreateCheckerTextureAsset() => ProjectWindowUtil.CreateAssetWithContent("New Perlin Texture.noise", "");
	}
}
