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

        var filesPath = "Assets/Editor/Scene2WebPage/Resources/";
        bool haveFilesBeenCopied = false;
        try
        {
            FileUtil.CopyFileOrDirectory(filesPath + "default.html", Application.temporaryCachePath + "/default.html");

            // The JS extension has special significance in Unity. Storing the JS file as TXT and renaming it while copying it over to the target folder.
            FileUtil.CopyFileOrDirectory(filesPath + "GLTFLoaderjs.txt", Application.temporaryCachePath + "/GLTFLoader.js");

            haveFilesBeenCopied = true;
        }
        catch
        {
            haveFilesBeenCopied = false;
            Debug.LogError("Please make sure the Scene2WebPage is under the Assets/Editor folder!");
        }

        if (haveFilesBeenCopied)
        {
            Application.OpenURL("file:///" + Application.temporaryCachePath + "/default.html");

            // Wait for some time for the Web page to load before opening the folder.
            System.Threading.Thread.Sleep(1000);
        }
        ShowExplorer(exportPath);
#else // and error dialog if not standalone
		EditorUtility.DisplayDialog("Error", "Your build target must be set to standalone", "Okay");
#endif
    }
}
#endif