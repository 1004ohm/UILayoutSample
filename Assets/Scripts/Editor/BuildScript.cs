public static class BuildScript
{
    public static void BuildAndroid()
    {
        // 1. ������ �� ����
        string[] scenes = { "Assets/Scenes/SampleScene.unity" };

        // �� ���� ���� Ȯ��
        for (int i = 0; i < scenes.Length; i++)
        {
            if (!System.IO.File.Exists(scenes[i]))
            {
                // ������ �⺻ �� �ڵ� ����
                var newScene = UnityEditor.SceneManagement.EditorSceneManager.NewScene(UnityEditor.SceneManagement.NewSceneSetup.DefaultGameObjects, UnityEditor.SceneManagement.NewSceneMode.Single);
                UnityEditor.SceneManagement.EditorSceneManager.SaveScene(newScene, scenes[i]);
            }
        }

        // 2. ������Ʈ ��Ʈ ���� ���
        string projectPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // ��¥�� �������� ���ϸ� ����
        string date = System.DateTime.Now.ToString("yyyyMMdd");
        string version = UnityEditor.PlayerSettings.bundleVersion;
        string fileName = $"{date}_{version}.apk";
        string outputDir = System.IO.Path.Combine(projectPath, "build", "Android", fileName);

        // 3. ��� ��� (���� ���)
        //string outputDir = System.IO.Path.Combine(projectPath, "build", "Android");
        if (!System.IO.Directory.Exists(outputDir))
        {
            System.IO.Directory.CreateDirectory(outputDir);
        }
        string outputPath = System.IO.Path.Combine(outputDir, "Android.apk");

        // 4. ���� �ɼ�
        UnityEditor.BuildOptions options = UnityEditor.BuildOptions.None; // �ʿ��ϸ� Development �߰� ����

        // 5. ���� ���� ����
        UnityEditor.Build.Reporting.BuildReport report = UnityEditor.BuildPipeline.BuildPlayer(scenes, outputPath, UnityEditor.BuildTarget.Android, options);

        // 6. ���� ��� üũ
        string resultFile = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "unity_build_result.txt");
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            System.IO.File.WriteAllText(resultFile, "SUCCESS");
        }
        else
        {
            System.IO.File.WriteAllText(resultFile, $"FAIL: {report.summary.result}");
        }

        // 7. �߰�: ���� �� ���̺귯�� �ʱ�ȭ ���� ����
        UnityEditor.AssetDatabase.Refresh();
    }

    public static void InitLibrary()
    {
        UnityEditor.AssetDatabase.Refresh();
    }
}