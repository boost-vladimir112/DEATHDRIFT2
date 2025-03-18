using UnityEngine;
using UnityEditor;
using System.IO;

public class OggToMp3Replacer : EditorWindow
{
    private string targetFolder = "Assets/Audio"; // Укажи свою папку

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

            // Удаляем уже созданный mp3.meta, если существует
            if (File.Exists(mp3MetaPath))
            {
                File.Delete(mp3MetaPath);
                Debug.Log($"Deleted auto-created meta: {mp3MetaPath}");
            }

            // Переименовываем ogg.meta -> mp3.meta
            if (File.Exists(oggMetaPath))
            {
                File.Move(oggMetaPath, mp3MetaPath);
                Debug.Log($"Meta replaced: {oggMetaPath} -> {mp3MetaPath}");
            }
            else
            {
                Debug.LogWarning($"No meta found for: {oggPath}");
            }

            // Удаляем сам .ogg файл
            File.Delete(oggPath);
            Debug.Log($"Deleted OGG: {oggPath}");
        }

        AssetDatabase.Refresh();
        Debug.Log("Replacement complete!");
    }
}
