#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MassRendererSystem.Data
{
    [CustomEditor(typeof(RenderDataCreator))]
    public class RenderDataCreatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RenderDataCreator creator = (RenderDataCreator)target;

            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();

            GUILayout.Space(20);

            SerializedProperty prototypesProp = serializedObject.FindProperty("_prototypes");
            SerializedProperty settingsProp = serializedObject.FindProperty("_bakerSettings");

            bool isValid = prototypesProp.arraySize > 0 && settingsProp.objectReferenceValue != null;

            if (!isValid)
            {
                EditorGUILayout.HelpBox("Assign Prototypes and Baker Settings to enable baking.", MessageType.Warning);
            }

            using (new EditorGUI.DisabledGroupScope(!isValid))
            {
                if (GUILayout.Button("Bake RenderData", GUILayout.Height(30)))
                {
                    OnBakeButtonClicked(creator);
                }
            }
        }

        private void OnBakeButtonClicked(RenderDataCreator creator)
        {
            string absolutePath = EditorUtility.OpenFolderPanel("Select Save Folder", "Assets", "");

            if (string.IsNullOrEmpty(absolutePath)) return; 

            if (!absolutePath.StartsWith(Application.dataPath))
            {
                EditorUtility.DisplayDialog("Invalid Path", "Please select a folder inside the project's Assets directory.", "OK");
                return;
            }

            string relativePath = "Assets" + absolutePath.Substring(Application.dataPath.Length);

            try
            {
                var result = creator.GenerateAndSave(relativePath);

                if (result != null)
                {
                    Debug.Log($"<color=green>RenderData Successfully Baked!</color> Saved to: {relativePath}");

                    EditorGUIUtility.PingObject(result);
                    Selection.activeObject = result;
                }
            }
            catch (System.Exception ex)
            {
                EditorUtility.DisplayDialog("Bake Error", $"Failed to bake data:\n{ex.Message}", "Close");
                Debug.LogException(ex);
            }
        }
    }
}
#endif