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
			base.OnInspectorGUI();
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
