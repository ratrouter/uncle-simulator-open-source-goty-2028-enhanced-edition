using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTouch : MonoBehaviour
{
    public string sceneName = "gasser"; // Name of the scene to load

    void OnTriggerEnter(Collider other)
    {
        // Load the scene when another object touches this trigger
        SceneManager.LoadScene(sceneName);
    }
}