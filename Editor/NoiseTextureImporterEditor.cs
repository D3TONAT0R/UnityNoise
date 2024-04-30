using System.Linq;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace UnityNoiseEditor
{
	[CustomEditor(typeof(NoiseTextureImporter))]
	public class NoiseTextureImporterEditor : AssetImporterEditor
	{
		public override void OnInspectorGUI()
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.resolution)));
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.noiseType)));
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.use3DNoise)));

			var noiseType = (NoiseTextureImporter.NoiseType)serializedObject.FindProperty(nameof(NoiseTextureImporter.noiseType)).intValue;
			if(noiseType == NoiseTextureImporter.NoiseType.Voronoi)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.voronoiSettings)));
			}
			else if(noiseType == NoiseTextureImporter.NoiseType.Cellular)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.cellFilter)));
			}

			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.scale)));
			using(new EditorGUILayout.HorizontalScope())
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.seed)));
				if(GUILayout.Button("Random", GUILayout.Width(60)))
				{
					serializedObject.FindProperty(nameof(NoiseTextureImporter.seed)).intValue = Random.Range(short.MinValue, short.MaxValue);
				}
			}
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.depth)));
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.tiled)));

			GUILayout.Space(10);
			GUILayout.Label("Fractal Settings", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.octaves)));
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.lacunarity)));
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.persistence)));

			GUILayout.Space(10);
			GUILayout.Label("Mapping Settings", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.useRemappingCurve)));
			if(serializedObject.FindProperty(nameof(NoiseTextureImporter.useRemappingCurve)).boolValue)
			{
				var curveProp = serializedObject.FindProperty(nameof(NoiseTextureImporter.remappingCurve));
				curveProp.animationCurveValue = EditorGUILayout.CurveField(curveProp.displayName, curveProp.animationCurveValue, 
					Color.green, new Rect(0, 0, 1, 1));
			}
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.gradient)));
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.showOutOfRangeValues)));

			GUILayout.Space(10);
			GUILayout.Label("Texture Settings", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.linear)));
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.generateMipMaps)));
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.wrapMode)));
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.filterMode)));
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(NoiseTextureImporter.compression)));

			ApplyRevertGUI();

			GUILayout.Space(10);
			GUILayout.Label("Histogram", EditorStyles.boldLabel);
			var rect = EditorGUILayout.GetControlRect(false, 64);
			EditorGUI.DrawRect(rect, Color.black);
			var importer = target as NoiseTextureImporter;
			if(importer.histogram != null)
			{
				float max = importer.histogram.Max();
				for(int i = 0; i < importer.histogram.Length; i++)
				{
					Rect r = rect;
					r.width /= importer.histogram.Length;
					r.x += r.width * i;
					float h = importer.histogram[i] / max;
					r.xMin = Mathf.RoundToInt(r.xMin);
					r.xMax = Mathf.RoundToInt(r.xMax);
					r.yMin += Mathf.RoundToInt(64 - h * 64);
					r.yMax = rect.yMax;
					EditorGUI.DrawRect(r, Color.white);
				}

			}
		}
	}
}
