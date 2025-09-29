using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BuildScript
{
    public static void BuildAndroid()
    {
        // ºôµåÇÒ ¾Àµé
        string[] scenes = {
            "Assets/Scenes/SampleScene.unity"
        };

        // Ãâ·Â °æ·Î
        string outputPath = "build/Android/Android.apk";

        // ½ÇÁ¦ ºôµå ½ÇÇà
        BuildPipeline.BuildPlayer(
            scenes,
            outputPath,
            BuildTarget.Android,
            BuildOptions.None
        );
    }
}
