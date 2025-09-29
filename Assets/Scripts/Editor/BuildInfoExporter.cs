using UnityEditor;
using System.IO;

/// <summary>
/// 작성일자   : 2025-09-26
/// 작성자     : 함민우 (ham001117@naver.com)                                            
/// 클래스용도 : 빌드 버전 추출기            
/// </summary>
public class BuildInfoExporter
{
    /// <summary>
    /// 파일에 빌드 버전 텍스트로 작성해 내보내기
    /// </summary>
    [MenuItem("Build/Export Version")]
    public static void ExportVersion()
    {
        var version = PlayerSettings.bundleVersion;
        File.WriteAllText("unity_version.txt", version);
        UnityEngine.Debug.Log($"Exported Unity Version: {version}");
    }
}
