using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class BuildScript
{
    public static void BuildAndroid()
    {
        // ������ ���� (���� ���� Ȯ��)
        string[] scenes = { "Assets/Scenes/SampleScene.unity" };
        foreach (var s in scenes)
        {
            if (!File.Exists(s))
                Debug.LogError($"�� ������ �������� ����: {s}");
        }

        // ������Ʈ ��Ʈ ���� ���
        string projectPath = Application.dataPath.Replace("/Assets", "");

        // ��� ��� (���� ���)
        string outputPath = Path.Combine(projectPath, "build/Android/Android.apk");

        // ��� ���� ����
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
            Debug.Log($"���� ���� ����: {outputDir}");
        }

        // ���� �ɼ�
        BuildOptions options = BuildOptions.None; // �ʿ��ϸ� Development �ɼ� �߰� ����

        Debug.Log($"���� ����: {outputPath}");

        // ���� ���� ����
        var report = BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.Android, options);

        // ���� ��� üũ
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            Debug.Log($"APK ���� ����: {outputPath}");
        else
            Debug.LogError($"APK ���� ����! ����: {report.summary.result}");
    }
}
