public static class BuildScript
{
    public static void BuildAndroid()
    {
        // 빌드할 씬 설정
        string[] scenes = { "Assets/Scenes/SampleScene.unity" };

        // 바탕화면 경로
        string projectPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // 날짜와 버전으로 파일명 생성
        string date = System.DateTime.Now.ToString("yyyyMMdd");
        string version = UnityEditor.PlayerSettings.bundleVersion;
        string fileName = $"{date}_{version}";

        // 출력 디렉토리 (폴더만)
        string outputDir = System.IO.Path.Combine(projectPath, "build", "Android");
        if (!System.IO.Directory.Exists(outputDir))
        {
            System.IO.Directory.CreateDirectory(outputDir);
        }

        // APK 파일 전체 경로 (파일명 포함)
        string outputPath = System.IO.Path.Combine(outputDir, fileName);

        // 빌드 옵션
        UnityEditor.BuildOptions options = UnityEditor.BuildOptions.None;

        // 실제 빌드 실행
        UnityEditor.Build.Reporting.BuildReport report = UnityEditor.BuildPipeline.BuildPlayer(scenes, outputPath, UnityEditor.BuildTarget.Android, options);

        // 빌드 결과 체크
        string resultFile = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "unity_build_result.txt");
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            System.IO.File.WriteAllText(resultFile, "SUCCESS");
        }
        else
        {
            System.IO.File.WriteAllText(resultFile, $"FAIL: {report.summary.result}");
        }

        // 추가: 빌드 후 라이브러리 초기화 문제 방지
        UnityEditor.AssetDatabase.Refresh();
    }

    public static void InitLibrary()
    {
        UnityEditor.AssetDatabase.Refresh();
    }
}