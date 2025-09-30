public static class BuildScript
{
    public static void BuildAndroid()
    {
        // 1. 빌드할 씬 설정
        string[] scenes = { "Assets/Scenes/SampleScene.unity" };

        // 씬 파일 존재 확인
        for (int i = 0; i < scenes.Length; i++)
        {
            if (!System.IO.File.Exists(scenes[i]))
            {
                // 없으면 기본 씬 자동 생성
                var newScene = UnityEditor.SceneManagement.EditorSceneManager.NewScene(UnityEditor.SceneManagement.NewSceneSetup.DefaultGameObjects, UnityEditor.SceneManagement.NewSceneMode.Single);
                UnityEditor.SceneManagement.EditorSceneManager.SaveScene(newScene, scenes[i]);
            }
        }

        // 2. 프로젝트 루트 절대 경로
        string projectPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // 날짜와 버전으로 파일명 생성
        string date = System.DateTime.Now.ToString("yyyyMMdd");
        string version = UnityEditor.PlayerSettings.bundleVersion;
        string fileName = $"{date}_{version}.apk";
        string outputDir = System.IO.Path.Combine(projectPath, "build", "Android", fileName);

        // 3. 출력 경로 (절대 경로)
        //string outputDir = System.IO.Path.Combine(projectPath, "build", "Android");
        if (!System.IO.Directory.Exists(outputDir))
        {
            System.IO.Directory.CreateDirectory(outputDir);
        }
        string outputPath = System.IO.Path.Combine(outputDir, "Android.apk");

        // 4. 빌드 옵션
        UnityEditor.BuildOptions options = UnityEditor.BuildOptions.None; // 필요하면 Development 추가 가능

        // 5. 실제 빌드 실행
        UnityEditor.Build.Reporting.BuildReport report = UnityEditor.BuildPipeline.BuildPlayer(scenes, outputPath, UnityEditor.BuildTarget.Android, options);

        // 6. 빌드 결과 체크
        string resultFile = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "unity_build_result.txt");
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            System.IO.File.WriteAllText(resultFile, "SUCCESS");
        }
        else
        {
            System.IO.File.WriteAllText(resultFile, $"FAIL: {report.summary.result}");
        }

        // 7. 추가: 빌드 후 라이브러리 초기화 문제 방지
        UnityEditor.AssetDatabase.Refresh();
    }

    public static void InitLibrary()
    {
        UnityEditor.AssetDatabase.Refresh();
    }
}