using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class BuildScript
{
    public static void BuildAndroid()
    {
        // 1. 빌드할 씬 설정
        string[] scenes = { "Assets/Scenes/SampleScene.unity" };

        // 씬 파일 존재 확인
        for (int i = 0; i < scenes.Length; i++)
        {
            if (!File.Exists(scenes[i]))
            {
                Debug.LogWarning($"씬 파일이 존재하지 않음: {scenes[i]}");
                // 없으면 기본 씬 자동 생성
                var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                EditorSceneManager.SaveScene(newScene, scenes[i]);
                Debug.Log($"씬 파일 생성: {scenes[i]}");
            }
        }

        // 2. 프로젝트 루트 절대 경로
        string projectPath = Path.GetFullPath(Application.dataPath + "/..");

        // 3. 출력 경로 (절대 경로)
        string outputDir = Path.Combine(projectPath, "build", "Android");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
            Debug.Log($"빌드 폴더 생성: {outputDir}");
        }
        string outputPath = Path.Combine(outputDir, "Android.apk");

        // 4. 빌드 옵션
        BuildOptions options = BuildOptions.None; // 필요하면 Development 추가 가능

        Debug.Log($"빌드 시작: {outputPath}");

        // 5. 실제 빌드 실행
        BuildReport report = BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.Android, options);

        // 6. 빌드 결과 체크
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"APK 빌드 성공: {outputPath}");
        }
        else
        {
            Debug.LogError($"APK 빌드 실패! 상태: {report.summary.result}");
            throw new System.Exception("Unity Build Failed!");
        }

        // 7. 추가: 빌드 후 라이브러리 초기화 문제 방지
        AssetDatabase.Refresh();
    }
}
