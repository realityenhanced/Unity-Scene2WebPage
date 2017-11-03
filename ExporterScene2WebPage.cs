#if UNITY_EDITOR
#define BUILD_SCENE2WEBPAGE

using UnityEngine;
using UnityEditor;

public class ExporterScene2WebPage : EditorWindow {

    public static void ShowExplorer(string itemPath)
    {
        itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
        System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
    }

    [MenuItem("Tools/Convert Scene to Web page")]
    static void Init()
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        var exportPath = Application.temporaryCachePath + "/" + "UnityScene2Webpage.gltf";
        var exporter = new SceneToGlTFWiz();
        exporter.Export(exportPath, null, /*buildzip*/ false, true, true, true);

        ShowExplorer(exportPath);
#else // and error dialog if not standalone
		EditorUtility.DisplayDialog("Error", "Your build target must be set to standalone", "Okay");
#endif
    }
}
#endif