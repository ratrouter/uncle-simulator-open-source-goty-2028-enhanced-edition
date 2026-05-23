using UnityEngine;

public class ClickObjectB : MonoBehaviour
{
    public GameObject uiObject;   // Your UI panel
    public GameObject objectC;    // The completely different object

    private void OnMouseDown()
    {
        if (uiObject != null)
            uiObject.SetActive(false);

        if (objectC != null)
            objectC.SetActive(true);

        gameObject.SetActive(false); // Hide Object B
    }
}