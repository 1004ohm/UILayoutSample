public static class BuildScript
{
    public static void BuildAndroid()
    {
        // ������ �� ����
        string[] scenes = { "Assets/Scenes/SampleScene.unity" };

        // ����ȭ�� ���
        string projectPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // ��¥�� �������� ���ϸ� ����
        string date = System.DateTime.Now.ToString("yyyyMMdd");
        string version = UnityEditor.PlayerSettings.bundleVersion;
        string fileName = $"{date}_{version}";

        // ��� ���丮 (������)
        string outputDir = System.IO.Path.Combine(projectPath, "build", "Android");
        if (!System.IO.Directory.Exists(outputDir))
        {
            System.IO.Directory.CreateDirectory(outputDir);
        }

        // APK ���� ��ü ��� (���ϸ� ����)
        string outputPath = System.IO.Path.Combine(outputDir, fileName);

        // ���� �ɼ�
        UnityEditor.BuildOptions options = UnityEditor.BuildOptions.None;

        // ���� ���� ����
        UnityEditor.Build.Reporting.BuildReport report = UnityEditor.BuildPipeline.BuildPlayer(scenes, outputPath, UnityEditor.BuildTarget.Android, options);

        // ���� ��� üũ
        string resultFile = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "unity_build_result.txt");
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            System.IO.File.WriteAllText(resultFile, "SUCCESS");
        }
        else
        {
            System.IO.File.WriteAllText(resultFile, $"FAIL: {report.summary.result}");
        }

        // �߰�: ���� �� ���̺귯�� �ʱ�ȭ ���� ����
        UnityEditor.AssetDatabase.Refresh();
    }

    public static void InitLibrary()
    {
        UnityEditor.AssetDatabase.Refresh();
    }
}