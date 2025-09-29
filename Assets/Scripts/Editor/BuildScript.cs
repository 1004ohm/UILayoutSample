using System.Collections;
using System.Collections.Generic;
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

        // ��� ���
        string outputPath = "build/Android/Android.apk";

        // ���� ���� ����
        BuildPipeline.BuildPlayer(
            scenes,
            outputPath,
            BuildTarget.Android,
            BuildOptions.None
        );
    }
}
