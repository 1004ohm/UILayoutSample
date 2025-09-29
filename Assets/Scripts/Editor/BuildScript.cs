using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class BuildScript
{
    public static void BuildAndroid()
    {
        // 빌드할 씬들 (존재 여부 확인)
        string[] scenes = { "Assets/Scenes/SampleScene.unity" };
        foreach (var s in scenes)
        {
            if (!File.Exists(s))
                Debug.LogError($"씬 파일이 존재하지 않음: {s}");
        }

        // 프로젝트 루트 절대 경로
        string projectPath = Application.dataPath.Replace("/Assets", "");

        // 출력 경로 (절대 경로)
        string outputPath = Path.Combine(projectPath, "build/Android/Android.apk");

        // 출력 폴더 생성
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
            Debug.Log($"빌드 폴더 생성: {outputDir}");
        }

        // 빌드 옵션
        BuildOptions options = BuildOptions.None; // 필요하면 Development 옵션 추가 가능

        Debug.Log($"빌드 시작: {outputPath}");

        // 실제 빌드 실행
        var report = BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.Android, options);

        // 빌드 결과 체크
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            Debug.Log($"APK 빌드 성공: {outputPath}");
        else
            Debug.LogError($"APK 빌드 실패! 상태: {report.summary.result}");
    }
}
