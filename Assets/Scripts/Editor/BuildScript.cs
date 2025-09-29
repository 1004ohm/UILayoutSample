using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class BuildScript
{
    public static void BuildAndroid()
    {
        // 1. ������ �� ����
        string[] scenes = { "Assets/Scenes/SampleScene.unity" };

        // �� ���� ���� Ȯ��
        for (int i = 0; i < scenes.Length; i++)
        {
            if (!File.Exists(scenes[i]))
            {
                Debug.LogWarning($"�� ������ �������� ����: {scenes[i]}");
                // ������ �⺻ �� �ڵ� ����
                var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                EditorSceneManager.SaveScene(newScene, scenes[i]);
                Debug.Log($"�� ���� ����: {scenes[i]}");
            }
        }

        // 2. ������Ʈ ��Ʈ ���� ���
        string projectPath = Path.GetFullPath(Application.dataPath + "/..");

        // 3. ��� ��� (���� ���)
        string outputDir = Path.Combine(projectPath, "build", "Android");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
            Debug.Log($"���� ���� ����: {outputDir}");
        }
        string outputPath = Path.Combine(outputDir, "Android.apk");

        // 4. ���� �ɼ�
        BuildOptions options = BuildOptions.None; // �ʿ��ϸ� Development �߰� ����

        Debug.Log($"���� ����: {outputPath}");

        // 5. ���� ���� ����
        BuildReport report = BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.Android, options);

        // 6. ���� ��� üũ
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"APK ���� ����: {outputPath}");
        }
        else
        {
            Debug.LogError($"APK ���� ����! ����: {report.summary.result}");
            throw new System.Exception("Unity Build Failed!");
        }

        // 7. �߰�: ���� �� ���̺귯�� �ʱ�ȭ ���� ����
        AssetDatabase.Refresh();
    }
}
