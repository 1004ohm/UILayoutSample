using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class BuildScript
{
    public static void BuildAndroid()
    {
        // 빌드할 씬들
        string[] scenes = {
            "Assets/Scenes/SampleScene.unity"
        };

        // 프로젝트 루트 절대 경로
        string projectPath = Application.dataPath.Replace("/Assets", "");

        // 출력 경로 (절대 경로)
        string outputPath = Path.Combine(projectPath, "build/Android/Android.apk");

        // 빌드할 폴더가 없으면 생성
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // 실제 빌드 실행
        BuildPipeline.BuildPlayer(
            scenes,
            outputPath,
            BuildTarget.Android,
            BuildOptions.None
        );
    }
}
