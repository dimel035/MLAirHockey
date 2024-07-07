using UnityEngine;

public class CursorControl : MonoBehaviour
{
    void Start()
    {
        // Lock the cursor to the game window
        Cursor.lockState = CursorLockMode.Confined;
        // Make the cursor visible
        Cursor.visible = true;
    }

    void Update()
    {
        // Example: Toggle cursor visibility with the space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.visible = !Cursor.visible;
        }

        // Example: Lock the cursor to the center with the L key
        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Example: Unlock the cursor with the U key
        if (Input.GetKeyDown(KeyCode.U))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
