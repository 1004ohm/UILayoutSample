using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class BuildScript
{
    public static void BuildAndroid()
    {
        // ������ ����
        string[] scenes = {
            "Assets/Scenes/SampleScene.unity"
        };

        // ������Ʈ ��Ʈ ���� ���
        string projectPath = Application.dataPath.Replace("/Assets", "");

        // ��� ��� (���� ���)
        string outputPath = Path.Combine(projectPath, "build/Android/Android.apk");

        // ������ ������ ������ ����
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // ���� ���� ����
        BuildPipeline.BuildPlayer(
            scenes,
            outputPath,
            BuildTarget.Android,
            BuildOptions.None
        );
    }
}
