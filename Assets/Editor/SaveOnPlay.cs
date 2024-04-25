using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoadAttribute]
public class SaveOnPlay
{
    static SaveOnPlay()
    {
        EditorApplication.playModeStateChanged += SaveCurrentScene;
    }

    private static void SaveCurrentScene(PlayModeStateChange state)
    {
        if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
        {
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }
    }
}