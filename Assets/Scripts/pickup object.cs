using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public GameObject uiObject;      // Drag your UI panel here
    public GameObject objectB;       // Drag Object B here

    private void OnMouseDown()
    {
        // Hide this object
        gameObject.SetActive(false);

        // Show UI
        uiObject.SetActive(true);

        // Enable Object B
        objectB.SetActive(true);
    }
}