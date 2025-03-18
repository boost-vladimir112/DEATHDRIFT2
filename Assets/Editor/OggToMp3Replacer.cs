using UnityEngine;
using UnityEditor;
using System.IO;

public class OggToMp3Replacer : EditorWindow
{
    private string targetFolder = "Assets/UniversalVehicleController/ART/Sounds"; // ”кажи свою папку здесь

    [MenuItem("Tools/Replace OGG with MP3")]
    public static void ShowWindow()
    {
        GetWindow(typeof(OggToMp3Replacer), false, "OGG to MP3 Replacer");
    }

    void OnGUI()
    {
        GUILayout.Label("Replace OGG files with MP3", EditorStyles.boldLabel);
        targetFolder = EditorGUILayout.TextField("Target Folder", targetFolder);

        if (GUILayout.Button("Start Replacement"))
        {
            ReplaceFiles();
        }
    }

    void ReplaceFiles()
    {
        string[] oggFiles = Directory.GetFiles(targetFolder, "*.ogg", SearchOption.AllDirectories);

        foreach (var oggPath in oggFiles)
        {
            string mp3Path = Path.ChangeExtension(oggPath, ".mp3");

            if (!File.Exists(mp3Path))
            {
                Debug.LogWarning($"MP3 version not found for: {oggPath}");
                continue;
            }

            string oggMetaPath = oggPath + ".meta";
            string mp3MetaPath = mp3Path + ".meta";

            if (File.Exists(oggMetaPath))
            {
                File.Move(oggMetaPath, mp3MetaPath);
            }

            // Delete the original OGG file
            File.Delete(oggPath);
            Debug.Log($"Replaced: {oggPath} -> {mp3Path}");
        }

        AssetDatabase.Refresh();
        Debug.Log("Replacement complete!");
    }
}
