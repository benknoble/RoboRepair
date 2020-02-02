using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneOnActivate : MonoBehaviour
{

    public string next_scene;

    void OnEnable()
    {
        print($"loading scene {next_scene}");
        SceneManager.LoadScene(next_scene);
    }
}
