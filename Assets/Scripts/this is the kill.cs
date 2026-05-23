using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class CubeTouchReset : MonoBehaviour
{
    public string sceneName = "Gasser3"; // Scene to reset to

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object we touched is the player
        if (collision.gameObject.name == "FPCHARACTER")
        {
            // Optional: print a debug message
            Debug.Log("Player touched! Resetting scene...");

            // Reload the scene
            SceneManager.LoadScene(sceneName);
        }
    }
}